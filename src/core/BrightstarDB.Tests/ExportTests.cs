using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace BrightstarDB.Tests
{
    //[TestFixture("type=rest;endpoint=http://localhost:8090/brightstar", "c:\\brightstar")]
    [TestFixture("type=embedded;storesDirectory=brightstar", "brightstar")]
    public class ExportTests : ParameterizedClientTestBase
    {
        private string _storeName;

        public ExportTests(string connectionString, string serviceDirectoryPath):base(connectionString, serviceDirectoryPath){}

        [TestFixtureSetUp]
        public override void SetUp()
        {
            base.SetUp();
            _storeName = CreateTestStore();
        }

        
        private string CreateTestStore()
        {
            var storeName = "ExportTests_" + DateTime.Now.Ticks;
            var client = GetClient();
            client.CreateStore(storeName);
            var job1 = client.ExecuteTransaction(storeName, null, null,
                                      @"<http://example.org/s> <http://example.org/p> <http://example.org/o> .");
            Assert.That(job1.JobCompletedOk);
            var job2 = client.ExecuteTransaction(storeName, null, null,
                                                 @"<http://example.org/s1> <http://example.org/p1> <http://example.org/o1> .",
                                                 "http://example.org/g1");
            Assert.That(job2.JobCompletedOk);
            return storeName;
        }

        [Test]
        public void TestExportJob()
        {
            var storeName = "TestExportJob_" + DateTime.Now.Ticks;
            var client = GetClient();
            client.CreateStore(storeName);
            var jobInfo = client.ExecuteTransaction(storeName, null, null,
                                                    "<http://example.org/s> <http://example.org/p> <http://example.org/o>",
                                                    waitForCompletion: true);
            Assert.That(jobInfo.JobCompletedOk);

            jobInfo = client.StartExport(storeName, storeName + ".nq");
            jobInfo = WaitForJob(jobInfo, client, storeName);
            Assert.That(jobInfo.JobCompletedOk);

            AssertExportedFileExists(storeName + ".nq");
        }

        [Test]
        public void TestExportGraphFormat(
            [Values("rdf", "nt", "ttl", "n3")] string exportFormat
            )
        {
            var exportFileName = _storeName + "." + exportFormat;
            var client = GetClient();
            var jobInfo = client.StartExport(_storeName, exportFileName, format: RdfFormat.GetResultsFormat(exportFormat));
            WaitForJob(jobInfo, client, _storeName);
            Assert.That(jobInfo.JobCompletedOk);
            AssertExportedFileExists(exportFileName);
        }

        private void AssertExportedFileExists(string exportedFileName)
        {
            Assert.That(File.Exists(Path.Combine(ServiceDirectoryPath, "import", exportedFileName)),
                "Could not find expected export file '{0}' in directory '{1}'",
                exportedFileName,
                Path.GetFullPath(Path.Combine(ServiceDirectoryPath, "import")));
        }
    }
}

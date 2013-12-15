#if !PORTABLE
using System;
using System.Threading;
using BrightstarDB.Client;
using BrightstarDB.Server.Modules;
using BrightstarDB.Server.Modules.Permissions;
using NUnit.Framework;
using Nancy.Hosting.Self;

namespace BrightstarDB.Tests
{
    public class ClientTestBase
    {
        private static NancyHost _serviceHost;
        private static bool _closed;
        private static readonly object HostLock = new object();

        protected static void StartService()
        {
            StartServer();
        }

        protected static void CloseService()
        {
            lock (HostLock)
            {
                _serviceHost.Stop();
                _closed = true;
            }
        }

        private static void StartServer()
        {
            lock (HostLock)
            {
#if SDK_TESTS
    // We assume that the test framework starts up the service for us.
#else
                if (_serviceHost == null || _closed)
                {
                    _serviceHost = new NancyHost(new BrightstarBootstrapper(
                                                     BrightstarService.GetClient(),
                                                     new FallbackStorePermissionsProvider(StorePermissions.All, StorePermissions.All),
                                                     new FallbackSystemPermissionsProvider(SystemPermissions.All, SystemPermissions.All)),
                                                     new HostConfiguration { AllowChunkedEncoding = false },
                                                 new Uri("http://localhost:8090/brightstar/"));
                    _serviceHost.Start();
                }
#endif
            }
        }

        public static IJobInfo WaitForJob(IJobInfo job, IBrightstarService client, string storeName)
        {
            var cycleCount = 0;
            while (!job.JobCompletedOk && !job.JobCompletedWithErrors && cycleCount < 100)
            {
                Thread.Sleep(500);
                cycleCount++;
                job = client.GetJobInfo(storeName, job.JobId);
            }
            if (!job.JobCompletedOk && !job.JobCompletedWithErrors)
            {
                Assert.Fail("Job did not complete in time.");
            }
            return job;
        }

    }
}
#endif
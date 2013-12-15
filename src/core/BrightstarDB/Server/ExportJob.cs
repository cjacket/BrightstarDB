using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
#if PORTABLE
using BrightstarDB.Portable.Adaptation;
using BrightstarDB.Portable.Compatibility;
using BrightstarDB.Storage;
#endif
using BrightstarDB.Rdf;
using VDS.RDF;

namespace BrightstarDB.Server
{
    internal class ExportJob
    {
        private readonly Guid _jobId;
        private readonly StoreWorker _storeWorker;
        private readonly string _outputFileName;
        private readonly string _graphUri;
        private readonly RdfFormat _format;
        private Action<Guid, Exception> _errorCallback;
        private Action<Guid> _successCallback;

        public ExportJob(Guid jobId, StoreWorker storeWorker, string outputFileName, string graphUri, RdfFormat format)
        {
            // Only graphUri can be null
            if (storeWorker == null) throw new ArgumentNullException("storeWorker");
            if (outputFileName == null) throw new ArgumentNullException("outputFileName");
            if (format == null) throw new ArgumentNullException("format");

            _jobId = jobId;
            _storeWorker = storeWorker;
            _outputFileName = outputFileName;
            _graphUri = graphUri;
            _format = format;
        }

        public void Run(Action<Guid, Exception> errorCallback, Action<Guid> successCallback)
        {
            _errorCallback = errorCallback;
            _successCallback = successCallback;
            ThreadPool.QueueUserWorkItem(RunExport, this);
        }

        private static void RunExport(object jobData)
        {
            var exportJob = jobData as ExportJob;
            if (exportJob == null) return;
            try
            {
                var storeDirectory = exportJob._storeWorker.WriteStore.DirectoryPath;
                var exportDirectory = Path.Combine(Path.GetDirectoryName(storeDirectory), "import");
#if PORTABLE
                var persistenceManager = PlatformAdapter.Resolve<IPersistenceManager>();
                if (!persistenceManager.DirectoryExists(exportDirectory)) persistenceManager.CreateDirectory(exportDirectory);
                var filePath = Path.Combine(exportDirectory, exportJob._outputFileName);
#else
                if (!Directory.Exists(exportDirectory)) Directory.CreateDirectory(exportDirectory);
                var filePath = Path.Combine(exportDirectory, exportJob._outputFileName);
#endif
                Logging.LogDebug("Export file path calculated as '{0}'", filePath);
                // Determine which graphs to write out
                string[] graphs;
                if (!String.IsNullOrEmpty(exportJob._graphUri))
                {
                    graphs = new string[] {exportJob._graphUri};
                }
                else
                {
                    graphs = exportJob._format.SupportsDatasets ? null : new string[] {Constants.DefaultGraphUri};
                }

                // Write the dataset or graph format requested
                if (exportJob._format.SupportsDatasets)
                {
                    WriteDataset(exportJob._storeWorker, graphs, exportJob._format, filePath);
                }
                else
                {
                    WriteGraph(exportJob._storeWorker, graphs, exportJob._format, filePath);
                }
                exportJob._successCallback(exportJob._jobId);
            }
            catch (Exception ex)
            {
                Logging.LogError(BrightstarEventId.ExportDataError, "Error Exporting Data {0} {1}", ex.Message, ex.StackTrace);
                exportJob._errorCallback(exportJob._jobId, ex);
            }

        }

        private static void WriteDataset(StoreWorker storeWorker, string[] graphs, RdfFormat format, string fileName)
        {
            if (format.DefaultExtension.Equals("nq"))
            {
#if PORTABLE
                using (var outputStream = persistenceManager.GetOutputStream(fileName, FileMode.Create))
#else
                using (var outputStream = File.Open(fileName, FileMode.Create, FileAccess.Write))
#endif
                {
                    // For NQuads use our own faster serializer that doesn't group quads by graph
                    using (var streamWriter = new StreamWriter(outputStream, format.Encoding))
                    {
                        var nqWriter =
                            new BrightstarTripleSinkAdapter(new NQuadsWriter(streamWriter,
                                                                             (graphs != null && graphs.Any())
                                                                                 ? graphs.First()
                                                                                 : Constants.DefaultGraphUri));
                        foreach (var t in storeWorker.ReadStore.Match(null, null, null, graphs: graphs))
                        {
                            nqWriter.Triple(t);
                        }
                        streamWriter.Flush();
                    }
                }
            }
            else
            {
                // Create an adapter to ITripleStore
                var tripleStore = new BrightstarTripleStoreAdapter(storeWorker.ReadStore);
                var writer = MimeTypesHelper.GetStoreWriter(format.MediaTypes[0]);
                writer.Save(tripleStore, fileName);
            }
        }

        private static void WriteGraph(StoreWorker storeWorker, string[] graphs, RdfFormat format, string fileName)
        {
            if (format.DefaultExtension.Equals("nt"))
            {
                // For NTriples use our own faster serializer
#if PORTABLE
                using (var outputStream = persistenceManager.GetOutputStream(fileName, FileMode.Create))
#else
                using (var outputStream = File.Open(fileName, FileMode.Create, FileAccess.Write))
#endif
                {
                    using (var streamWriter = new StreamWriter(outputStream, format.Encoding))
                    {
                        var ntWriter = new BrightstarTripleSinkAdapter(new NTriplesWriter(streamWriter));
                        foreach (var t in storeWorker.ReadStore.Match(null, null, null, graphs: graphs))
                        {
                            ntWriter.Triple(t);
                        }
                        streamWriter.Flush();
                    }
                }
            }
            else
            {
                var graph = new BrightstarGraphAdapter(storeWorker.ReadStore, graphs.First());
                var writer = MimeTypesHelper.GetWriter(format.MediaTypes[0]);
                writer.Save(graph, fileName);
            }
        }
    }
}

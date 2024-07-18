using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Dropbox.Api;
using MHLab.Patch.Core.Client.IO;
using MHLab.Patch.Core.IO;
using MHLab.Patch.Core.Utilities.Asserts;

namespace MHLab.Patch.Core.Client.Advanced.IO.Dropbox
{
    public class DropboxDownloader : FileDownloader
    {
        private static readonly int TasksAmount = Math.Max(1, Math.Min(8, Environment.ProcessorCount));
        private Task[] _tasks;

        public DropboxDownloader(IFileSystem fileSystem) : base(fileSystem)
        {
            _tasks = new Task[TasksAmount];
            DownloadMetrics = new SmartDownloadMetrics(_tasks);
            DownloadSpeedMeter = new SmartDownloadSpeedMeter(DownloadMetrics);
        }

        public override void Download(List<DownloadEntry> entries, Action<DownloadEntry> onEntryStarted, Action<long> onChunkDownloaded, Action<DownloadEntry> onEntryCompleted)
        {
            if (Credentials != null)
            {
                Assert.Check(Credentials is NetworkCredential);
            }
            var credentials = (NetworkCredential)Credentials;

            entries.Sort((entry1, entry2) =>
            {
                return entry1.Definition.Size.CompareTo(entry2.Definition.Size);
            });

            var queue = new Queue<DownloadEntry>(entries);

            for (var i = 0; i < TasksAmount; i++)
            {
                var task = Task.Run(() =>
                {
                    using (var client = new DropboxClient(credentials.Password))
                    {
                        var entryStarted = onEntryStarted;
                        var entryCompleted = onEntryCompleted;

                        while (true)
                        {
                            DownloadEntry entry;

                            lock (queue)
                            {
                                if (queue.Count == 0) break;
                                entry = queue.Dequeue();
                            }

                            if (IsCanceled) return;

                            entryStarted?.Invoke(entry);

                            int retriesCount = 0;

                            while (retriesCount < MaxDownloadRetries)
                            {
                                try
                                {
                                    var url = entry.PartialRemoteUrl;
                                    if (!url.StartsWith("/"))
                                        url = "/" + url;

                                    using (var response = client.Files.DownloadAsync(url).Result)
                                    {
                                        FileSystem.DeleteFile((FilePath)entry.DestinationFile);
                                        var result = response.GetContentAsByteArrayAsync().Result;
                                        FileSystem.WriteAllBytesToFile((FilePath)entry.DestinationFile, result);
                                        onChunkDownloaded?.Invoke(result.LongLength);
                                    }

                                    while (IsPaused)
                                    {
                                        Thread.Sleep(150);
                                    }

                                    retriesCount = MaxDownloadRetries;
                                }
                                catch
                                {
                                    retriesCount++;

                                    if (retriesCount >= MaxDownloadRetries)
                                    {
                                        throw new WebException($"All retries have been tried for {entry.RemoteUrl}.");
                                    }

                                    Thread.Sleep(DelayForRetryMilliseconds + (DelayForRetryMilliseconds * retriesCount));
                                }
                            }

                            entryCompleted?.Invoke(entry);
                        }
                    }
                });

                _tasks[i] = task;
            }

            Task.WaitAll(_tasks);
            DownloadSpeedMeter.Reset();
        }

        public override string DownloadString(DownloadEntry entry)
        {
            if (Credentials != null)
            {
                Assert.Check(Credentials is NetworkCredential);
            }
            var credentials = (NetworkCredential)Credentials;
            
            using (var client = new DropboxClient(credentials.Password))
            {
                var url = entry.PartialRemoteUrl;
                if (!url.StartsWith("/"))
                    url = "/" + url;
                using (var response = client.Files.DownloadAsync(url).Result)
                {
                    return response.GetContentAsStringAsync().Result;
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using MHLab.Patch.Core.Client.IO;
using MHLab.Patch.Core.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MHLab.Patch.Core.Client.Advanced.IO.GDrive
{
    public class GDriveDownloader : FileDownloader
    {
        public string FolderId = "12VvTPs421f4GhLkMTQOfDX4WsTWW0c3D";
        public string ApiKey = "AIzaSyBtWENcNQ7ury6Tk11q4sLNO8l7B8dcz4g";

        private static readonly int TasksAmount = Math.Max(1, Math.Min(8, Environment.ProcessorCount));
        private Task[] _tasks;

        private string FolderContentUri => $"https://www.googleapis.com/drive/v3/files?q='{FolderId}'+in+parents&key={ApiKey}";

        public GDriveDownloader(IFileSystem fileSystem) : base(fileSystem)
        {
            _tasks = new Task[TasksAmount];
            DownloadMetrics = new SmartDownloadMetrics(_tasks);
            DownloadSpeedMeter = new SmartDownloadSpeedMeter(DownloadMetrics);
        }

        public override void Download(List<DownloadEntry> entries, Action<DownloadEntry> onEntryStarted, Action<long> onChunkDownloaded, Action<DownloadEntry> onEntryCompleted)
        {
            entries.Sort((entry1, entry2) =>
            {
                return entry1.Definition.Size.CompareTo(entry2.Definition.Size);
            });

            var fileMap = GetFileIds();
            
            var queue = new Queue<DownloadEntry>(entries);

            for (var i = 0; i < TasksAmount; i++)
            {
                var task = Task.Run(() =>
                {
                    using (var client = new WebClient())
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
                                    client.DownloadFile(entry.RemoteUrl, entry.DestinationFile);
                                    
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

        private Dictionary<string, string> GetFileIds()
        {
            var nextPageToken = string.Empty;
            var uri = FolderContentUri;
            var fileMap = new Dictionary<string, string>();
            
            using (var client = new HttpClient())
            {
                do
                {
                    var pagedUri = uri;
                    if (!string.IsNullOrWhiteSpace(nextPageToken))
                    {
                        pagedUri += $"&pageToken={nextPageToken}";
                    }

                    var serializedContent = client.GetStringAsync(pagedUri).Result;

                    var deserializedContent = (JObject) JsonConvert.DeserializeObject(serializedContent);

                    nextPageToken = (string) deserializedContent["nextPageToken"];

                    foreach (var fileEntry in (JArray)deserializedContent["files"])
                    {
                        var fileId = (string) fileEntry["id"];
                        var fileName = (string) fileEntry["name"];

                        fileMap.Add(fileName, fileId);
                    }
                } while (!string.IsNullOrWhiteSpace(nextPageToken));
            }

            return fileMap;
        }
    }
}

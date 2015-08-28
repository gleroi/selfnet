using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using PCLStorage;

namespace Selfnet.App
{
    public class ItemsStore : IDisposable
    {
        private ICollection<Item> Items { get; set; }
        private string Filepath { get; }

        public ItemsStore(string path)
        {
            this.Filepath = path;
            var task = this.ReadItems();
            this.Items = task.Result;
        }

        public ICollection<Item> All()
        {
            return this.Items;
        }

        public void Add(IEnumerable<Item> items)
        {
            foreach (var item in items)
            {
                this.Items.Add(item);
            }
        }

        public void Commit()
        {
            this.WriteItems().Wait();
        }

        private async Task<ICollection<Item>>  ReadItems()
        {
            IFile file = await FileSystem.Current.GetFileFromPathAsync(this.Filepath);
            try
            {
                using (var stream = await file.OpenAsync(FileAccess.Read))
                {
                    var serializer = new XmlSerializer(typeof (List<Item>));
                    var items = serializer.Deserialize(stream) as List<Item>;
                    return items;
                }
            }
            catch (Exception ex)
            {
                return new List<Item>();
            }
        }

        private async Task WriteItems()
        {
            IFile file = await FileSystem.Current.GetFileFromPathAsync(this.Filepath);
            using (var stream = await file.OpenAsync(FileAccess.ReadAndWrite))
            {
                var serializer = new XmlSerializer(typeof (List<Item>));
                serializer.Serialize(stream, this.Items.ToList());
            }
        }

        private bool disposed = false;
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                // managed resources
            }

            // unmanagedd resources

            this.disposed = true;
        }
    }
}

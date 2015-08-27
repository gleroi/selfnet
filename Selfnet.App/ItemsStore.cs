using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using PCLStorage;

namespace Selfnet.App
{
    public class ItemsStore : IDisposable
    {
        private ICollection<Item> Items { get; } = new List<Item>();
        private string Filepath { get; }

        public ItemsStore(string path)
        {
            this.Filepath = path;
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
            this.WriteItems();
        }

        private async void WriteItems()
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

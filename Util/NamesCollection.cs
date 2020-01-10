using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;

namespace TotallyFairRace.Util
{
    public class NamesCollection : IEnumerable<string>
    {
        private static NamesCollection _singleton = null;

        private NamesCollection()
        {
            Names = new List<string>();
        }

        public async Task ReadNamesAsync()
        {
            string names;
            var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri(@"ms-appx:///Assets/PlayerNames/names.txt"));
            using (var inputStream = await file.OpenReadAsync())
            using (var classicStream = inputStream.AsStreamForRead())
            using (var streamReader = new StreamReader(classicStream))
            {
                names = await streamReader.ReadToEndAsync().ConfigureAwait(true);
            }
            Names.AddRange(names.Split(", ", StringSplitOptions.RemoveEmptyEntries));
        }

        public static NamesCollection GetInstance() => _singleton ?? (_singleton = new NamesCollection());
        public IEnumerator<string> GetEnumerator() => Names.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public string this[int i] => Names[i];
        public List<string> Names { get; }
    }
}
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Game.Scripts.App.SaveLoad.Storage
{
    public class LocalFileStorage : IDataStorage
    {
        private const string DATABASE_FILE_NAME = "Database.txt"; 
        
        private readonly string _filePath;
        private readonly string _fileName;

        private string FullPath => Path.Combine(_filePath, _fileName);
        private string DatabaseFullPath => Path.Combine(_filePath, DATABASE_FILE_NAME);

        private HashSet<int> _gameVersions;

        public LocalFileStorage(
            string filePath,
            string fileName)
        {
            _filePath = filePath;
            _fileName = fileName;

            if (!File.Exists(DatabaseFullPath))
            {
                _gameVersions = new();
                return;
            }
            
            var bytes = File.ReadAllBytes(DatabaseFullPath);
            var content = Encoding.UTF8.GetString(bytes);
            
            if (string.IsNullOrEmpty(content))
            {
                _gameVersions = new ();
                return;
            }

            _gameVersions = JsonConvert.DeserializeObject<HashSet<int>>(content)
                            ?? new();
        }

        public bool Write(in string data, out int version)
        {
            if (_gameVersions.Count > 0)
            {
                var maxVersion = _gameVersions.Max();
                version = maxVersion + 1;
            }
            else
            {
                version = 1;
            }

            var fileName = GetFileName(version);
            var bytes = Encoding.UTF8.GetBytes(data);
            
            File.WriteAllBytes(fileName, bytes);
            _gameVersions.Add(version);

            var databaseJson = JsonConvert.SerializeObject(_gameVersions);
            var databaseBytes = Encoding.UTF8.GetBytes(databaseJson);
            File.WriteAllBytes(DatabaseFullPath, databaseBytes);
            
            return true;
        }

        public bool Read(in int version, out string data)
        {
            data = default;
            
            var fileName = GetFileName(version);
            if (!File.Exists(fileName))
            {
                return false;
            }

            var bytes = File.ReadAllBytes(fileName);
            data = Encoding.UTF8.GetString(bytes);
            return true;
        }

        public void Clear()
        {
            var fileNames = Directory.GetFiles(_filePath);
            var emptyContent = string.Empty;
            var emptyBytes = Encoding.UTF8.GetBytes(emptyContent);
            foreach (var fileName in fileNames)
            {
                File.WriteAllBytes(fileName, emptyBytes);
            }
        }

        private string GetFileName(in int version) => 
            string.Format(FullPath, version);
    }
}
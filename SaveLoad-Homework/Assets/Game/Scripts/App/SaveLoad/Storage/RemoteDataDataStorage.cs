using Cysharp.Threading.Tasks;
using UnityEngine.Networking;

namespace Game.App
{
    public class RemoteDataDataStorage : IRemoteDataStorage
    {
        private readonly string _url;

        public RemoteDataDataStorage(string url)
        {
            _url = url;
        }

        public async UniTask<bool> Write(int version, string data)
        {
            var fullUrl = $"{_url}/save?version={version}";
            var request = UnityWebRequest.Put(fullUrl, data);
            await request.SendWebRequest();
            return request.result == UnityWebRequest.Result.Success;
        }

        public async UniTask<(bool, string)> Read(int version)
        {
            var fullUrl = $"{_url}/load?version={version}";
            var request = UnityWebRequest.Get(fullUrl);
            await request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                var json = request.downloadHandler.text;
                if (!string.IsNullOrEmpty(json))
                {
                    return (true, json);
                }
            }

            return (false, string.Empty);
        }
    }
}
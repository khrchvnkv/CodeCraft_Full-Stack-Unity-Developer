using Cysharp.Threading.Tasks;

namespace Game.App
{
    public class EntityRepository : IRepository
    {
        private readonly ILocalDataStorage _localDataStorage;
        private readonly IRemoteDataStorage _remoteDataStorage;

        public EntityRepository(
            ILocalDataStorage localDataStorage, 
            IRemoteDataStorage remoteDataStorage)
        {
            _localDataStorage = localDataStorage;
            _remoteDataStorage = remoteDataStorage;
        }

        async UniTask<(bool, string)> IRepository.GetData(int version)
        {
            var result = _localDataStorage.Read(version, out var data);
            if (!result)
            {
                (result, data) = await _remoteDataStorage.Read(version);
            }

            return (result, data);
        }

        async UniTask<(bool, int)> IRepository.SetData(string data)
        {
            var localResult = _localDataStorage.Write(data, out var version);
            if (localResult)
            {
                var remoteResult = await _remoteDataStorage.Write(version, data);
                return (remoteResult, version);
            }
            
            return (false, -1);
        }
    }
}
using UnityEngine;

namespace Game.App
{
    public class PlayerPrefsDataStorage : ILocalDataStorage
    {
        private const string SavedVersion = "version";
        private const string DataKey = "progress";

        public bool Write(in string data, out int version)
        {
            version = PlayerPrefs.GetInt(SavedVersion, 1) + 1;
            PlayerPrefs.SetInt(SavedVersion, version);
            PlayerPrefs.SetString(DataKey, data);

            return true;
        }

        public bool Read(in int version, out string data)
        {
            data = string.Empty;
            if (PlayerPrefs.HasKey(SavedVersion) && PlayerPrefs.GetInt(SavedVersion) == version)
            {
                if (PlayerPrefs.HasKey(DataKey))
                {
                    data = PlayerPrefs.GetString(DataKey);
                    return true;
                }
            }

            return false;
        }

        public void Clear() => PlayerPrefs.DeleteAll();
    }
}
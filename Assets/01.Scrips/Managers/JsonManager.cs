namespace Managers.Jsonmanager
{
    //System
    using System.IO;

    //UnityEngine
    using UnityEngine;

    public class JsonManager : MonoBehaviour
    {
        public static JsonManager instance;

        [Header("DataManagerInfo")]
        [SerializeField] private string filename = "GameData.json";

        public Data gameData = new Data();
        private Data resetGameData = new Data();

        private string path = default;

        private void Awake()
        {
            #region Singleton
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(instance.gameObject);
            }
            DontDestroyOnLoad(this.gameObject);
            #endregion

            path = Path.Combine(Application.dataPath, filename);
        }

        // ������ ����
        public void Save()
        {
            string data = JsonUtility.ToJson(gameData, true);
            File.WriteAllText(path, data);
        }

        // ������ �ҷ�����
        public void Load()
        {
            if (File.Exists(path))
            {
                string data = File.ReadAllText(path);
                gameData = JsonUtility.FromJson<Data>(data);
            }
            else
            {
                Save();
            }
        }

        // ������ ����
        public void ResetData()
        {
            gameData = resetGameData;
            Save();
        }
    }

    [System.Serializable]
    public class Data
    {
        public int money                = 0;
        public int bestSocre            = 0;

        //UpgradeTypes
        public int healthLevel          = 0;
        public int attackDamageLevel    = 0;
        public int moveSpeedLevel       = 0;
        public int defenseLevel         = 0;
        public int attackSpeedLevel     = 0;
    }
}




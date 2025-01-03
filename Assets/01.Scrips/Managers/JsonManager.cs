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

            path = Path.Combine(Application.persistentDataPath, filename);
        }

        // 데이터 저장
        public void Save()
        {
            string data = JsonUtility.ToJson(gameData, true);
            File.WriteAllText(path, data);
        }

        // 데이터 불러오기
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

        // 데이터 리셋
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
        public int AttackDamageLevel    = 0;
        public int MoveSpeedLevel       = 0;
        public int DefenseLevel         = 0;
        public int AttackSpeedLevel     = 0;
    }
}




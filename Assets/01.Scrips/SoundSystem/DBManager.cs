using System.IO;
using System.Text;
using UnityEngine;
namespace DataManage
{

    public static class DBManager
    {
        private readonly static string jsonPath = Application.dataPath + "/JsonFolder/";

        private static void CreateFolder()
        {
            if (!Directory.Exists(jsonPath))
            {
                Debug.Log("폴더 없음");
                Directory.CreateDirectory(jsonPath);
                Debug.Log(jsonPath + " 위치에 폴더 생성");

            }
        }

        private static string CreateJsonfileName(string fileName)
        {
            return Path.Combine(jsonPath, fileName + ".json");
        }

        public static void ToJson<T>(T type, string name, bool s)
        {
            CreateFolder();
            string jsonData = JsonUtility.ToJson(type, s);
            string path = CreateJsonfileName(name);
            File.WriteAllText(path, jsonData);
        }

        public static T FromJson<T>(string name)
        {
            CreateFolder();
            string path = CreateJsonfileName(name);
            if (!File.Exists(path))
            {
                Debug.Log("존재 하지 않습니다.");
                T t = default(T);
                ToJson<T>(t, name, true);
                Debug.Log("생성하였습니다.");
                return t;
            }
            string data = File.ReadAllText(path);
            return JsonUtility.FromJson<T>(data);

        }

    }
}
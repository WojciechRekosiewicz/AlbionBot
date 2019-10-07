using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbionBot
{
    class DataStorage
    {
        private static Dictionary<string, string> pairs = new Dictionary<string, string>();

        public static void AddPairToStorage(string key, string value)
        {
            pairs.Add(key, value);
            SaveData();
        }

        public static int GetPairsCount()
        {
            return pairs.Count();
        }

        static DataStorage()
        {
            if(!ValitadeStorageFile("DataStorage.json")) return;
            string json = File.ReadAllText("DataStorage.json");
            //string json = ValitadeStorageFile("DataStorage.json");
            var pairs = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            
        }

        public static void SaveData()
        {
            string json = JsonConvert.SerializeObject(pairs, Formatting.Indented);
            File.WriteAllText("DataStorage.json", json);
        }

        private static bool ValitadeStorageFile(string file)
        {
            if(!File.Exists(file))
            {
                File.WriteAllText(file, "");
                SaveData();
                return false;
            }
            return true;
        }
    }

   
}

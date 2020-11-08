using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Uwp_Sqlite.Data
{
    public static class SettingsContext
    {
         
        private static Settings _settings { get; set; }

        static SettingsContext()
        {
            var path = Directory.GetCurrentDirectory() + @"\settings.json";
            var json = File.ReadAllText(path);

            _settings = JsonConvert.DeserializeObject<Settings>(json);

        }

        public static IEnumerable<string> GetStatus()
        {            
            return _settings.status;           
        }

        public static int GetMaxItemsCount()
        {
            return _settings.maxItemsCount;
        }
        }

    public class Settings
    {
        public string[] status { get; set; }
        public int maxItemsCount { get; set; }
    }

}
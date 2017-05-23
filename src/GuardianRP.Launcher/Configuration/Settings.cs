using GuardianRP.Launcher.FileSystem;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuardianRP.Launcher.Configuration {

    internal class Settings : JsonFile {

        internal static readonly Settings Instance = new Settings();

        [JsonProperty]
        public string ApiEndpoint { private set; get; } = "guardianrp.cz";

        private Settings(string filename = "config.json") : base(filename) {
            if(Exists) Load(); else Save();
        }

    }

}

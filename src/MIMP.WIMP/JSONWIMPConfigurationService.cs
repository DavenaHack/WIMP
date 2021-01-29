using Newtonsoft.Json;
using System;
using System.IO;

namespace MIMP.WIMP
{
    public class JSONWIMPConfigurationService : IWIMPConfigurationService
    {

        public string Path { get; }


        public JSONWIMPConfigurationService(string path)
        {
            Path = path ?? throw new ArgumentNullException(nameof(path));
        }


        public WIMPConfiguration Load()
        {
            if (!File.Exists(Path))
                return null;
            return JsonConvert.DeserializeObject<WIMPConfiguration>(File.ReadAllText(Path));
        }

        public void Save(WIMPConfiguration configuration)
        {
            File.WriteAllText(Path, JsonConvert.SerializeObject(configuration));
        }

    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace JurBanksFeatures
{
	public class ModConfiguration
	{
        public ModConfiguration(bool valueOfParameters)
        {
            enableFollowFeature = valueOfParameters;
            enableHideoutFeature = valueOfParameters;
        }

        public ModConfiguration()
        {
        }

        const string configPath = @"JurBankFeaturesConfig.txt";

        public bool enableFollowFeature { get; set; }
        public bool enableHideoutFeature { get; set; }

        public static ModConfiguration Load()
        {
            if (File.Exists(configPath))
            {
                var serializer = new XmlSerializer(typeof(ModConfiguration));
                using (var reader = new StreamReader(configPath))
                    return (ModConfiguration)serializer.Deserialize(reader);
            }
            return new ModConfiguration(true);
        }

        public void Save()
        {
            var serializer = new XmlSerializer(typeof(ModConfiguration));
            using (var writer = new StreamWriter(configPath))
                serializer.Serialize(writer, this);
        }
    }
}

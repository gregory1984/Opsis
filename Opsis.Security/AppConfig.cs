using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Reflection;

namespace Opsis.Security
{
    public static class AppConfig
    {
        /// <summary>
        /// Encrypts selected section inside an app.config file of the specified assembly.
        /// </summary>
        /// <param name="sectionName">Section to encrypt</param>
        /// <param name="exePath">Assembly name on which an app.config name is based.</param>
        public static void EncryptSectionIfPlain(string sectionName, string exePath)
        {
            var config = ConfigurationManager.OpenExeConfiguration(exePath);
            var section = config.GetSection(sectionName);

            if (!section.SectionInformation.IsProtected)
            {
                section.SectionInformation.ProtectSection("DataProtectionConfigurationProvider");
                section.SectionInformation.ForceSave = true;
                config.Save(ConfigurationSaveMode.Modified);
            }
        }
    }
}

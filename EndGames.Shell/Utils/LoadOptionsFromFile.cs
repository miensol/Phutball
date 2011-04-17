using System.IO;
using System.Windows;
using System.Xml.Serialization;
using EndGames.Phutball;
using EndGames.Utils;

namespace EndGames.Shell.Utils
{
    public class SaveOptionsToFileOnExit : IStartupTask
    {
        private readonly App _application;
        private readonly PhutballOptions _phutballOptions;

        public SaveOptionsToFileOnExit(App application, PhutballOptions phutballOptions)
        {
            _application = application;
            _phutballOptions = phutballOptions;
        }

        public void Execute()
        {
            _application.Exit += SavePhutballOptionsToFile;
        }

        private void SavePhutballOptionsToFile(object sender, ExitEventArgs e)
        {
            try
            {
                if(File.Exists(LoadOptionsFromFile.OptionsXmlFile))
                {
                    File.Delete(LoadOptionsFromFile.OptionsXmlFile);
                }
                using(var optionsFile = File.OpenWrite(LoadOptionsFromFile.OptionsXmlFile))
                {
                    var serializer = new XmlSerializer(typeof (PhutballOptions));
                    serializer.Serialize(optionsFile, _phutballOptions);
                }
            }
// ReSharper disable EmptyGeneralCatchClause
            catch
// ReSharper restore EmptyGeneralCatchClause
            {}
        }
    }

    public class LoadOptionsFromFile : IStartupTask
    {
        private readonly PhutballOptions _defaultOptions;
        public static readonly string OptionsXmlFile = "options.xml";

        public LoadOptionsFromFile(PhutballOptions defaultOptions)
        {
            _defaultOptions = defaultOptions;
        }

        public void Execute()
        {
            var serializer = new XmlSerializer(typeof (PhutballOptions));
            try
            {
                using (var optionsFile = File.OpenRead(OptionsXmlFile))
                {
                    var options = (PhutballOptions)serializer.Deserialize(optionsFile);
                    _defaultOptions.Update(options);   
                }
            }
// ReSharper disable EmptyGeneralCatchClause
            catch
// ReSharper restore EmptyGeneralCatchClause
            {}
            
        }
    }
}
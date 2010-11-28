using log4net;

namespace EndGames.Utils
{
    public class Log
    {
        private static readonly ILog _log = LogManager.GetLogger("Phutball");

        public static ILog Current
        {
            get { return _log; }
        }
    }
}
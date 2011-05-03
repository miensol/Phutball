using System;
using System.Threading;

namespace Phutball
{
    public class LongRunningProcess
    {
        private static CancellationTokenSource _cancelationTokenSurce;

        public static CancellationTokenSource StartNew()
        {
            if(_cancelationTokenSurce != null)
            {
                throw new InvalidOperationException("must clear long running process first");
            }
            _cancelationTokenSurce = new CancellationTokenSource();
            return _cancelationTokenSurce;
        }

        public static CancellationToken Current
        {
            get
            {
                if(_cancelationTokenSurce == null)
                {
                    return CancellationToken.None;
                }
                return _cancelationTokenSurce.Token;
            }
        }

        public static void Clear()
        {
            _cancelationTokenSurce = null;
        }
    }
}
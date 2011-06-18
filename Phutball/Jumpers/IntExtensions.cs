using System;

namespace Phutball.Jumpers
{
    public static class IntExtensions
    {
        public static int GetSign(this int value)
        {
            if(value == 0)
            {
                return 0;
            }
            return value/Math.Abs(value);
        }
    }
}
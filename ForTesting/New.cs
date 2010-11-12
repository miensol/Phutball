using System;

namespace ForTesting
{
    public class New
    {
        private static Random _random = new Random();

        public static string String()
        {
            return Guid.NewGuid().ToString();
        }

        public static int Int()
        {
            return _random.Next();
        }
    }
}
using EndGames.Utils;

namespace EndGames.Phutball
{
    public class PhutballGameStateEnum : EnumOf<PhutballGameStateEnum>
    {
        private readonly string _description;
        public static readonly PhutballGameStateEnum NotStarted = new PhutballGameStateEnum("Not started");
        public static readonly PhutballGameStateEnum Started = new PhutballGameStateEnum("Started");
        public static readonly PhutballGameStateEnum CurrentPlayerWon = new PhutballGameStateEnum("Current player won");

        private PhutballGameStateEnum(string description)
        {
            _description = description;
        }
    }
}
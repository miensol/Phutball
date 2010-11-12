using System.Collections.Generic;

namespace EndGames.Phutball.Jumpers
{
    public interface IJump 
    {
        IEnumerable<Field> GetJumpedFields();
        Field EndField { get; }
    }
}
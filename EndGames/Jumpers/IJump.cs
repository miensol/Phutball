using System.Collections.Generic;

namespace Phutball.Jumpers
{
    public interface IJump 
    {
        IEnumerable<Field> GetJumpedFields();
        Field EndField { get; }
    }
}
using System.Collections.Generic;

namespace EndGames.Phutball.Events
{
    public class PhutballGameFieldsChanged
    {
        public IEnumerable<Field> ChangedFields { get; set; }
    }
}
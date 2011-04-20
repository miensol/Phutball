using System.Collections.Generic;

namespace Phutball.Events
{
    public class PhutballGameFieldsChanged
    {
        public IEnumerable<Field> ChangedFields { get; set; }
    }
}
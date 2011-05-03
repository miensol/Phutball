using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Phutball.Search;

namespace Phutball.Shell.Presenters
{
    public class MoveStrategiesCollection : IEnumerable<MoveStrategyButtonModel>
    {
        private readonly IMoveFinders _moveFinders;

        public MoveStrategiesCollection(IMoveFinders moveFinders)
        {
            _moveFinders = moveFinders;
        }

        public IEnumerator<MoveStrategyButtonModel> GetEnumerator()
        {
            return typeof (IMoveFinders).GetMethods()
                .Where(mi => typeof (IMoveFindingStartegy).IsAssignableFrom(mi.ReturnType))
                .Select(mi => new MoveStrategyButtonModel
                                  {
                                      ChooseStrategy = (factory) => (IMoveFindingStartegy) mi.Invoke(_moveFinders, null),
                                      StrategyName =  Regex.Replace(mi.Name,"([A-Z])"," $1",RegexOptions.Compiled).Trim()
                                  })
                .OrderBy(model=> model.StrategyName)
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
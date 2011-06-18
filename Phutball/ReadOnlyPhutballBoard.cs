using System.Collections.Generic;
using System.Linq;
using Phutball.Jumpers;

namespace Phutball
{
    public class ReadOnlyPhutballBoard 
    {
        protected IPhutballOptions _options;
        protected readonly IFieldsGraph _fieldsGraph;

        public ReadOnlyPhutballBoard(IFieldsGraph fieldsGraph, IPhutballOptions options)
        {
            _fieldsGraph = fieldsGraph;
            _options = options;
        }

        public IEnumerable<Field> GetCurrentFields()
        {
            return _fieldsGraph.GetFields();
        }

        public IStoneJumper GetStoneJumper(Field fromfield, Field toField)
        {
            return new StoneJumper(_fieldsGraph, fromfield, toField);
        }        

        public Field GetField(int fieldId)
        {
            return _fieldsGraph.GetFieldCloned(fieldId);
        }
        
        public bool IsEndingConfiguration()
        {
            var selectedField = _fieldsGraph.GetFields()
                .FirstOrDefault(field => field.IsWinningField(_options.RowCount) && field.HasStone && field.Stone.CanSelect);
            return selectedField != null;
        }

        public bool CanPlaceBlackStone(Field field)
        {
            return _fieldsGraph.CanPlaceBlackStone(field);
        }


    }
}
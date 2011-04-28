using System;
using System.Collections.Generic;
using System.Linq;

namespace Phutball
{
    public class FieldsGraph : IFieldsGraph
    {
        private IPhutballOptions _options;
        private Dictionary<int, Field> _fieldMap = new Dictionary<int, Field>();
        private Field _whiteField;
        private HashSet<Field> _blackFields = new HashSet<Field>();
        private Lazy<TargetBorderEnum> _targetBorders;

        public FieldsGraph(IPhutballOptions options)
        {
            _options = options;
            Initialize();
        }

        private FieldsGraph()
        {
        }

        public Field GetFieldCloned(int fieldId)
        {
            return (Field) _fieldMap[fieldId].Clone();
        }

        public int ColumnCount
        {
            get { return _options.ColumnCount; }
        }

        public int RowCount
        {
            get { return _options.RowCount; }
        }

        public Tuple<int, int> GetCoordinates(Field field)
        {
            int row = field.Id/ColumnCount;
            int column = field.Id%ColumnCount;
            return Tuple.Create(row, column);
        }

        public bool IsValidPlaceForWhiteField(Tuple<int, int> cords)
        {
            return IsValidRow(cords.Item1) && IsValidColumnForWhiteField(cords.Item1, cords.Item2);
        }        

        public Field GetField(Tuple<int, int> coordinates)
        {
            return GetFieldCloned(GetFieldIndex(coordinates.Item1, coordinates.Item2));
        }

        public Field GetWhiteField()
        {
            return GetFieldCloned(_whiteField.Id);
        }

        public Tuple<int, int> GetWhiteFieldCoords()
        {
            return GetCoordinates(_whiteField);
        }

        public void UpdateFields(params Field[] fieldsToUpdate)
        {
            fieldsToUpdate.Each(UpdateField);
        }

        private void UpdateField(Field field)
        {
            var fieldToUpdate = _fieldMap[field.Id];
            fieldToUpdate.Stone = field.Stone;
            fieldToUpdate.Selected = field.Selected;
            _blackFields.Remove(fieldToUpdate);
            if(fieldToUpdate.HasWhiteStone)
            {
                _whiteField = fieldToUpdate;    
            }else
            {
                if(fieldToUpdate.HasBlackStone)
                {
                    _blackFields.Add(fieldToUpdate);    
                }                
            }
        }

        public IEnumerable<Field> GetFields()
        {
            return _fieldMap.Values.Select(field=> (Field)field.Clone());
        }

        public IEnumerable<Field> GetBlackFields()
        {
            return _blackFields.ToList();
        }

        private bool IsValidColumnForWhiteField(int rowIndex, int columnIndex)
        {
            if(rowIndex == 0 || rowIndex == RowCount - 1)
            {
                return true;
            }
            return IsValidColumnForBlackField(columnIndex);
        }

        private bool IsValidColumnForBlackField(int columnIndex)
        {
            return columnIndex > 0 && columnIndex < ColumnCount - 1;
        }

        private bool IsValidRow(int rowIndex)
        {
            return rowIndex >= 0 && rowIndex < RowCount;
        }

        public void Initialize()
        {
            _blackFields = new HashSet<Field>();
            
            for (int rowIndex = 0; rowIndex < RowCount; ++rowIndex)
            {
                AddColumnsInRow(rowIndex);
            }

            _targetBorders = new Lazy<TargetBorderEnum>(()=> new TargetBorderEnum(this));
        }

        public bool CanPlaceBlackStone(Tuple<int,int> coords)
        {
            return IsValidRowForBlackField(coords.Item1) && IsValidColumnForBlackField(coords.Item2)
                   && GetField(coords).IsEmpty;
        }

        public TargetBorderEnum Borders()
        {
            return _targetBorders.Value;
        }

        private bool IsValidRowForBlackField(int rowIndex)
        {
            return rowIndex > 0 && rowIndex < RowCount - 1;
        }

        public bool CanPlaceBlackStone(Field field)
        {
            return field.IsEmpty && FieldIsInMiddleRows(field) && FieldIsInMiddleColumns(field);
        }

        private bool FieldIsInMiddleColumns(Field field)
        {
            return field.IsInMiddleColumns(ColumnCount);
        }

        private bool FieldIsInMiddleRows(Field field)
        {
            return field.IsInMiddleRows(RowCount);
        }

        private void AddColumnsInRow(int row)
        {
            for (int col = 0; col < ColumnCount; ++col)
            {
                var newField = GetNewField(row, col);
                _fieldMap[newField.Id] = newField;
            }
        }

        private Field GetNewField(int row, int col)
        {
            int index = GetFieldIndex(row, col);
            var field = new Field(index, row, col);
            if (IsCentralField(row, col))
            {
                field.PlaceWhiteStone();
                _whiteField = field;
            }
            return field;
        }

        private int GetFieldIndex(int row, int col)
        {
            return row*ColumnCount + col;
        }

        private bool IsCentralField(int row, int col)
        {
            return row == RowCount/2 && col == ColumnCount/2;
        }

        public object Clone()
        {
            return new FieldsGraph
                       {
                           _options = _options,
                           _fieldMap = _fieldMap.ToDictionary(kv => kv.Key, kv => (Field) kv.Value.Clone()),
                           _whiteField = (Field) _whiteField.Clone(),
                           _targetBorders = _targetBorders,
                           _blackFields = _blackFields.ToHashSet()
                       };
        }

        public override string ToString()
        {
            return "White: {0}, Black: [{1}]".ToFormat(_whiteField.ToString(), _fieldMap.Values.Where(v=> v.HasStone && v.HasWhiteStone == false)
                .Aggregate("", (agg,cur)=> agg + "(" + cur.ToString() +"), "));
        }
    }
}
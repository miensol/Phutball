﻿using System;
using System.Collections.Generic;
using EndGames.Phutball.Jumpers;
using System.Linq;

namespace EndGames.Phutball
{
    public class FieldsGraph : IFieldsGraph
    {
        private readonly IPhutballOptions _options;
        private Dictionary<int, Field> _fieldMap = new Dictionary<int, Field>();
        private Field _whiteField;

        public FieldsGraph(IPhutballOptions options)
        {
            _options = options;
            BuildFields();
        }
        
        public Field GetField(int fieldId)
        {
            return _fieldMap[fieldId];
        }

        int IFieldsGraph.ColumnCount
        {
            get { return _options.ColumnCount; }
        }

        int IFieldsGraph.RowCount
        {
            get { return _options.RowCount; }
        }

        public Tuple<int, int> GetCoordinates(Field field)
        {
            int row = field.Id/ColumnCount();
            int column = field.Id%ColumnCount();
            return new Tuple<int, int>(row, column);
        }

        public bool IsValidCoordinate(Tuple<int, int> cords)
        {
            return IsValidRow(cords.Item1) && IsValidColumn(cords.Item2);
        }

        public Field GetField(Tuple<int, int> coordinates)
        {
            return GetField(GetFieldIndex(coordinates.Item1, coordinates.Item2));
        }

        public Field GetWhiteField()
        {
            return _whiteField;
        }

        public void UpdateFields(params Field[] fieldsToUpdate)
        {
            fieldsToUpdate.Each(UpdateField);
        }

        private void UpdateField(Field field)
        {
            _fieldMap[field.Id] = field;
            if(field.HasWhiteStone)
            {
                _whiteField = field;
            }
        }        


        public IEnumerable<Field> GetFields()
        {
            return _fieldMap.Values;
        }

        private bool IsValidColumn(int columnIndex)
        {
            return columnIndex >= 0 && columnIndex < ColumnCount();
        }

        private bool IsValidRow(int rowIndex)
        {
            return rowIndex >= 0 && rowIndex < RowCount();
        }

        private void BuildFields()
        {
            for (int rowIndex = 0; rowIndex < RowCount(); ++rowIndex)
            {
                AddColumnsInRow(rowIndex);
            }
        }

        private void AddColumnsInRow(int row)
        {
            for (int col = 0; col < ColumnCount(); ++col)
            {
                var newField = GetNewField(row, col);
                _fieldMap[newField.Id] = newField;
            }
        }

        private int ColumnCount()
        {
            return _options.ColumnCount;
        }

        private int RowCount()
        {
            return _options.RowCount;
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
            return row*ColumnCount() + col;
        }

        private bool IsCentralField(int row, int col)
        {
            return row == RowCount()/2 && col == ColumnCount()/2;
        }

        public object Clone()
        {
            var fieldsGraph = new FieldsGraph(_options);
            fieldsGraph.UpdateFields(_fieldMap.Values.ToArray());
            return fieldsGraph;
        }
    }
}
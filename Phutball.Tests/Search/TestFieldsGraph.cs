using System;
using System.Collections.Generic;

namespace Phutball.Tests.Search
{
    public class TestFieldsGraph : IGraphBuilder
    {
        private readonly int _rowCount;
        private readonly int _columnCount;
        private Func<FieldsGraph> _graphCreator;

        public TestFieldsGraph(int rowCount, int columnCount)
        {
            _rowCount = rowCount;
            _columnCount = columnCount;
            _graphCreator = () => CreateDefaultFieldsGraph(rowCount, columnCount);
        }

        public TestFieldsGraph(FieldType[][] board)
        {
            _graphCreator = () => CreateFieldsFromMatrix(board);
        }

        private FieldsGraph CreateFieldsFromMatrix(FieldType[][] graphs)
        {
            int columnCount = graphs[0].Length;
            int rowCOunt = graphs.Length;
            var fields = new List<Field>();
            for(int row = 0; row < rowCOunt; ++row)
            {
                for(int column = 0; column < columnCount; ++column)
                {
                    var index = row*columnCount + column;
                    fields.Add(GetField(index, row, column, graphs[row][column]));
                }
            }
            var fieldsGraph = new FieldsGraph(new PhutballOptions
                                                  {
                                                      RowCount = rowCOunt,
                                                      ColumnCount = columnCount
                                                  });
            fieldsGraph.UpdateFields(fields.ToArray());
            return fieldsGraph;
        }

        public Field GetField(int index, int row, int column, FieldType type)
        {
            var field = new Field(index, row, column);
            if (type.Equals(FieldType.Black))
            {
                field.PlaceBlackStone();
            }
            if (type.Equals(FieldType.White))
            {
                field.PlaceWhiteStone();
            }
            return field;
        }

        private FieldsGraph CreateDefaultFieldsGraph(int rowCount, int columnCount)
        {
            return new FieldsGraph(new PhutballOptions {RowCount = rowCount, ColumnCount = columnCount});
        }

        public IFieldsGraph Build()
        {
            return _graphCreator();
        }
    }
}
using System;

namespace Phutball.Tests.Search
{
    public class RandomGraphBuilder : IGraphBuilder
    {
        private int _rowCount;
        private int _columnCount;
        private readonly double blackProbability;

        public RandomGraphBuilder(int rowCount, int columnCount, double blackProbability)
        {
            this._rowCount = rowCount;
            this._columnCount = columnCount;
            this.blackProbability = blackProbability;
        }

        public IFieldsGraph Build()
        {
            var blackCount = Math.Floor(blackProbability*_rowCount*_columnCount);
            _rowCount = _rowCount + 4;
            _columnCount = _columnCount + 2;


            var input = new FieldType[_rowCount][];

            for (int row = 0; row < _rowCount; ++row)
            {
                input[row] = new FieldType[_columnCount];
                for (int column = 0; column < _columnCount; ++column)
                {
                    input[row][column] = FieldType.Empty;
//                    if (row < 2 || column < 1 || row >= _rowCount - 2 || column >= _columnCount - 1)
//                    {
//                        continue;
//                    }
//                    var placeBlack = _random.NextDouble() < blackProbability;
//                    if (placeBlack)
//                    {
//                        input[row][column] = FieldType.Black;
//                    }
                }
            }

            while (blackCount > 0)
            {
                var rowIndex = RandomSource.Next(2, _rowCount - 2);
                var colIndex = RandomSource.Next(1, _columnCount - 1);
                if (input[rowIndex][colIndex] == FieldType.Empty)
                {
                    input[rowIndex][colIndex] = FieldType.Black;
                    blackCount--;
                }
            }

            bool whiteWasPlaced = false;
            for (int row = _rowCount/2, row2 = _rowCount/2 + 1;
                 row >= 2 && row2 < _rowCount - 2 && !whiteWasPlaced;
                 --row, ++row2)
            {
                for (int col = (_columnCount)/2, col2 = (_columnCount/2) + 1;
                     col >= 1 && col2 <= _columnCount - 1;
                     ++col2,
                     --col)
                {
                    if (input[row][col] == FieldType.Empty)
                    {
                        input[row][col] = FieldType.White;
                        whiteWasPlaced = true;
                        break;
                    }
                    if (input[row2][col] == FieldType.Empty)
                    {
                        input[row2][col] = FieldType.White;
                        whiteWasPlaced = true;
                        break;
                    }
                    if (input[row][col2] == FieldType.Empty)
                    {
                        input[row][col2] = FieldType.White;
                        whiteWasPlaced = true;
                        break;
                    }
                    if (input[row2][col2] == FieldType.Empty)
                    {
                        input[row2][col2] = FieldType.White;
                        whiteWasPlaced = true;
                        break;
                    }
                }
            }
            if(whiteWasPlaced == false)
            {
                throw new InvalidOperationException();
            }

            return new TestFieldsGraph(input).Build();
        }
    }
}
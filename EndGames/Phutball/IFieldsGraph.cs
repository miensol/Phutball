using System;
using System.Collections.Generic;

namespace EndGames.Phutball
{
    public interface IFieldsGraph : ICloneable, IFieldsUpdater
    {
        IEnumerable<Field> GetFields();
        Field GetFieldCloned(int fieldId);
        int ColumnCount { get; }
        int RowCount { get; }
        Tuple<int, int> GetCoordinates(Field field);
        bool IsValidPlaceForWhiteField(Tuple<int, int> cords);
        Field GetField(Tuple<int, int> coordinates);
        void Initialize();
        bool CanPlaceBlackStone(Field field);
        bool CanPlaceBlackStone(Tuple<int,int> coords);
        TargetBorderEnum Borders();
        IEnumerable<Field> GetBlackFields();
    }
}
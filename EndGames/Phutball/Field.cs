using System;

namespace EndGames.Phutball
{
    public class Field  : ICloneable
    {
        private readonly int _id;

        public Field(int id, int rowIndex, int columnIndex)
        {
            RowIndex = rowIndex;
            ColumnIndex = columnIndex;
            _id = id;
        }

        public int Id
        {
            get { return _id; }
        }

        public int RowIndex { get; private set; }

        public int ColumnIndex { get; private set; }

        public IStone Stone { get; set; }

        public bool HasStone
        {
            get { return Stone != null; }
        }

        public bool CanSelect
        {
            get { return HasStone && Stone.CanSelect; }
        }

        public bool Selected { get; private set; }

        public bool HasWhiteStone   
        {
            get { return HasStone && Stone.CanSelect; }
        }

        public void PlaceBlackStone()
        {
            Stone = new BlackStone();
        }

        public void Select()
        {
            Selected = true;
        }

        public void DeSelect()
        {
            Selected = false;
        }

        public void RemoveStone()
        {
            Stone = null;
        }

        public void PlaceWhiteStone()
        {
            Stone = new WhiteStone();
        }

        public bool IsInMiddleRows(int rowCount)
        {
            return RowIndex > 0 && RowIndex < rowCount - 1;
        }

        public bool IsWinningField(int rowCount)
        {
            return RowIndex <= 1 || RowIndex >= rowCount - 2;
        }

        public object Clone()
        {
            return new Field(_id, RowIndex, ColumnIndex)
                       {
                           Selected = Selected,
                           Stone = Stone
                       };
        }

        public bool IsInMiddleColumns(int columnCount)
        {
            return ColumnIndex > 0 && ColumnIndex < columnCount - 1;
        }

        public override string ToString()
        {
            return "{0},{1}".ToFormat(RowIndex, ColumnIndex);
        }
    }
}
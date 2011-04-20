using System;

namespace Phutball.Jumpers
{
    public static class FieldExtensions
    {
        public static Tuple<int,int> GetDirectionTo(this Field from, Field to)
        {
            var xdelta = to.ColumnIndex - from.ColumnIndex;
            var ydelta = to.RowIndex - from.RowIndex;
            return new Tuple<int, int>(ydelta.GetSign(), xdelta.GetSign());
        }
    }
}
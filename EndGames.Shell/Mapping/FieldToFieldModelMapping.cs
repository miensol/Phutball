using System;
using System.Windows;
using AutoMapper;
using EndGames.Phutball;
using EndGames.Shell.Models;
using EndGames.Utils;

namespace EndGames.Shell.Mapping
{
    public class FieldToFieldModelMapping : IStartupTask
    {
        private readonly IPhutballOptions _options;

        public FieldToFieldModelMapping(IPhutballOptions options)
        {
            _options = options;
        }

        public void Execute()
        {
            Mapper.CreateMap<Field, FieldModel>()
                .ForMember(dst => dst.Lines, opt => opt.MapFrom(GetLinesByField));
        }

        private LinesModel GetLinesByField(Field src)
        {
            return new LinesModel
                       {
                           Up = BoolToVisibility(NotTwoFirstRows(src) && NotLastRow(src) && InMiddleColumns(src)),
                           Down = BoolToVisibility(NotFirstRow(src) && NotLastTwoRows(src) && InMiddleColumns(src)),
                           Left = BoolToVisibility(InMiddleRows(src) && NotTwoFristColumns(src) && NotLastColumn(src)),
                           Right = BoolToVisibility(InMiddleRows(src) && NotTwoLastColumns(src) && NotFirstColumn(src))
                            };
        }

        private bool InMiddleColumns(Field src)
        {
            return NotFirstColumn(src) && NotLastColumn(src);
        }

        private bool NotFirstColumn(Field src)
        {
            return src.ColumnIndex > 0;
        }

        private bool NotLastColumn(Field src)
        {
            return src.ColumnIndex < _options.ColumnCount - 1;
        }

        private bool NotLastTwoRows(Field src)
        {
            return src.RowIndex < _options.RowCount - 2;
        }

        private bool NotFirstRow(Field src)
        {
            return src.RowIndex > 0;
        }

        private bool NotTwoLastColumns(Field src)
        {
            return src.ColumnIndex < _options.ColumnCount - 2;
        }

        private bool InMiddleRows(Field src)
        {
            return src.IsInMiddleRows(_options.RowCount);
        }

        private bool NotTwoFristColumns(Field src)
        {
            return src.ColumnIndex > 1;
        }

        private bool NotLastRow(Field src)
        {
            return src.RowIndex < _options.RowCount - 1;
        }

        private bool NotTwoFirstRows(Field src)
        {
            return src.RowIndex > 1;
        }

        private Visibility BoolToVisibility(bool isVisible)
        {
            return Mapper.Map<bool, Visibility>(isVisible);
        }
    }
}
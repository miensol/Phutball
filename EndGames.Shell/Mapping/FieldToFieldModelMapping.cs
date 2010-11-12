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
                           Up =
                               BoolToVisibility(src.RowIndex > 1 && NotLastRow(src)),
                           Down = BoolToVisibility(NotFirstRow(src) && src.RowIndex < _options.RowCount - 2),
                           Left = BoolToVisibility(InMiddleRows(src) && NotFirstColumn(src)),
                                Right = BoolToVisibility(InMiddleRows(src) && NotLastColumn(src))
                            };
        }

        private bool NotLastColumn(Field src)
        {
            return src.ColumnIndex < _options.ColumnCount - 1;
        }

        private bool InMiddleRows(Field src)
        {
            return src.IsInMiddleRows(_options.RowCount);
        }

        private bool NotFirstColumn(Field src)
        {
            return src.ColumnIndex>0;
        }

        private bool NotLastRow(Field src)
        {
            return src.RowIndex < _options.RowCount - 1;
        }

        private bool NotFirstRow(Field src)
        {
            return src.RowIndex > 0;
        }

        private Visibility BoolToVisibility(bool isVisible)
        {
            return Mapper.Map<bool, Visibility>(isVisible);
        }
    }
}
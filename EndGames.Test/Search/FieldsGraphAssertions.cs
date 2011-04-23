using System.Diagnostics;
using ForTesting;

namespace Phutball.Tests.Search
{
    public static class FieldsGraphAssertions
    {
        [DebuggerStepThrough]
        public static IFieldsGraph ShouldHaveWhiteFieldAt(this IFieldsGraph graph, int row, int column)
        {
            var whiteField = graph.GetWhiteField();
            whiteField.RowIndex.ShouldEqual(row, "Row should be {0} but was {1}");
            whiteField.ColumnIndex.ShouldEqual(column, "Column should be {0} but was {1}");
            return graph;
        }
    }
}
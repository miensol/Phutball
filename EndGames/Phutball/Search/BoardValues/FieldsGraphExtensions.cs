namespace EndGames.Phutball.Search.BoardValues
{
    public static class FieldsGraphExtensions
    {
        public static TargetBorderEnum Borders(this IFieldsGraph fieldsGraph)
        {
            return new TargetBorderEnum(fieldsGraph);
        }
    }
}
using System.Collections.Generic;

namespace EndGames.Games
{
    //public class CheckersFieldsFactory
    //{
    //    private IPlayer _firstPlayer;

    //    private IPlayer _secondPlayer;

    //    private readonly int _boardSize;
    //    private int _fieldCount;

    //    public CheckersFieldsFactory(int boardSize, IPlayer firstPlayer,IPlayer secondPlayer)
    //    {
    //        _firstPlayer = firstPlayer;
    //        _secondPlayer = secondPlayer;
    //        _boardSize = boardSize;
    //        _fieldCount = _boardSize*_boardSize;
    //    }

    //    public IEnumerable<IField> CreateInitialFields()
    //    {
    //        var fields = new List<IField>();
    //        for (int fieldIndex = 0; fieldIndex < _fieldCount; ++fieldIndex)
    //        {
    //            fields.Add(CreateNewField(fieldIndex));
    //        }
    //        return fields;
    //    }
    //    private IField CreateNewField(int index)
    //    {
    //        return BlackOrWhite(index) ? (IField)new WhiteField(index) : CreateBlackCheckerField(index);
    //    }

    //    private bool BlackOrWhite(int index)
    //    {
    //        var row = index / _boardSize;
    //        return row.IsEven() ^ index.IsEven();
    //    }

    //    private BlackField CreateBlackCheckerField(int index)
    //    {
    //        var blackField = new BlackField(index);
    //        blackField.Pawn = CreatePawnOnIndex(index);
    //        return blackField;
    //    }

    //    private IPawn CreatePawnOnIndex(int index)
    //    {
    //        if (IsPawnOnIndex(index))
    //        {
    //            return GetWhiteOrBlackPawn(index);
    //        }
    //        return null;
    //    }

    //    private IPawn GetWhiteOrBlackPawn(int index)
    //    {
    //        if (IsFirstPlayerField(index))
    //        {
    //            return new WhitePawn(_firstPlayer);
    //        }
    //        return new BlackPawn(_secondPlayer);
    //    }

    //    private bool IsPawnOnIndex(int index)
    //    {
    //        int row = index / _boardSize;
    //        int middleRow = _boardSize / 2;
    //        return row > middleRow || row < middleRow - 1;
    //    }

    //    private bool IsFirstPlayerField(int index)
    //    {
    //        return index < _fieldCount / 2;
    //    }
    //}
}
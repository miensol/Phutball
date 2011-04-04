using System.Collections.Generic;
using EndGames.Phutball.Jumpers;

namespace EndGames.Phutball
{
    public interface IPhutballBoard : IFieldsUpdater
    {
        IEnumerable<Field> GetCurrentFields();
        Field GetField(int fieldId);
        void Initialize();
        bool CanPlaceBlackStone(Field field);
        IStoneJumper GetStoneJumper(Field fromfield, Field toField);
    }

}
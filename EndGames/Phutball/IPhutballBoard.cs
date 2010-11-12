using System.Collections.Generic;
using EndGames.Phutball.Jumpers;

namespace EndGames.Phutball
{
    public interface IFieldsUpdater
    {
        void UpdateFields(params Field[] field);
    }

    public interface IPhutballBoard : IFieldsUpdater
    {
        IEnumerable<Field> GetCurrentFields();
        Field GetField(int fieldId);
        void Initialize();
        bool IsEndingConfiguration();
        bool CanPlaceBlackStone(Field field);
        IStoneJumper GetStoneJumper(Field fromfield, Field toField);
    }

}
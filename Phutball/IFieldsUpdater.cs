namespace Phutball
{
    public interface IFieldsUpdater
    {
        void UpdateFields(params Field[] field);
        Field GetWhiteField();
    }
}
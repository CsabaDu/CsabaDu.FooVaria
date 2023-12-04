namespace CsabaDu.FooVaria.Common.Behaviors
{
    public interface IQuantifiable
    {
        decimal GetDefaultQuantity();

        void ValidateQuantity(ValueType? quantity, string paramName); // TypeCode
    }

    public interface IQuantifiable<out T> : IQuantifiable where T : struct
    {
        T GetQuantity();
    }
}

namespace CsabaDu.FooVaria.Common.Behaviors
{
    public interface IQuantifiable
    {
        decimal DefaultQuantity { get; }

        void ValidateQuantity(ValueType? quantity, string paramName);
    }

    public interface IQuantifiable<T> : IQuantifiable where T : struct
    {
        T GetQuantity();
    }
}

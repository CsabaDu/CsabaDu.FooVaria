namespace CsabaDu.FooVaria.Common.Behaviors
{
    public interface IQuantifiable
    {
        decimal DefaultQuantity { get; }

        void ValidateQuantity(ValueType? quantity);
    }

    public interface IQuantifiable<T> : IQuantifiable where T : struct
    {
        T GetQuantity();
    }
}

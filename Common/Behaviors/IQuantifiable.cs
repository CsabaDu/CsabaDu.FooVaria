namespace CsabaDu.FooVaria.Common.Behaviors
{
    public interface IQuantifiable
    {
        decimal GetDefaultQuantity();
    }

    public interface IQuantifiable<T> : IQuantifiable where T : struct
    {
        T GetQuantity();

        void ValidateQuantity(T quantity);
    }
}

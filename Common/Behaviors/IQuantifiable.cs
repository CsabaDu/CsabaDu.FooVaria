namespace CsabaDu.FooVaria.Common.Behaviors
{
    public interface IQuantifiable
    {
        decimal DefaultQuantity { get; }

        //void ValidateDefaultQuantity(decimal defaultQuantity);
    }

    public interface IQuantifiable<T> : IQuantifiable where T : struct
    {
        T GetQuantity();

        void ValidateQuantity(T quantity);
    }
}

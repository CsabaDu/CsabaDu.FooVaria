namespace CsabaDu.FooVaria.Common.Behaviors
{
    public interface IQuantifiable/* : ICommonBase*/
    {
        //decimal DefaultQuantity { get; }

        decimal GetDefaultQuantity();

        void ValidateQuantity(ValueType? quantity, string paramName);
    }

    public interface IQuantifiable<T> : IQuantifiable where T : struct
    {
        T GetQuantity();
    }
}

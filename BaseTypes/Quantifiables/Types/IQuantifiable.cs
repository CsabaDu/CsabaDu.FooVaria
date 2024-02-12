namespace CsabaDu.FooVaria.BaseTypes.Quantifiables.Types
{
    public interface IQuantifiable : IMeasurable, IDefaultQuantity
    {
        void ValidateQuantity(ValueType? quantity, string paramName);
        void ValidateQuantity(IQuantifiable? quantifiable, string paramName);

    }

    public interface IQuantifiable<TSelf> : IQuantifiable, IExchange<TSelf, Enum>, IFit<TSelf>
        where TSelf : class, IQuantifiable
    {
        TSelf GetQuantifiable(MeasureUnitCode measureUnitCode, decimal defaultQuantity);

        void ValidateQuantifiable(IQuantifiable? quantifiable, string paramName);
    }
}

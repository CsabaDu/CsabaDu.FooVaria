namespace CsabaDu.FooVaria.BaseTypes.Quantifiables.Types
{
    public interface IQuantifiable : IMeasurable, IDefaultQuantity/*, IProportional<IQuantifiable>, IExchangeable<Enum>*/
    {
        void ValidateQuantity(ValueType? quantity, string paramName);
        void ValidateQuantity(IQuantifiable? quantifiable, string paramName);

    }

    public interface IQuantifiable<TSelf> : IQuantifiable, IExchange<TSelf, Enum>, IFit<TSelf>
        where TSelf : class, IQuantifiable
    {
        TSelf GetQuantifiable(MeasureUnitCode measureUnitCode, decimal defaultQuantity);

        //void ValidateQuantity(TSelf other, string paramName);
        void ValidateQuantifiable(IQuantifiable? quantifiable, string paramName);
    }
}

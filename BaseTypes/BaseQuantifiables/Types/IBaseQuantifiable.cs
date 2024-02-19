namespace CsabaDu.FooVaria.BaseTypes.BaseQuantifiables.Types
{
    public interface IBaseQuantifiable : IMeasurable, IDefaultQuantity
    {
        void ValidateQuantity(ValueType? quantity, string paramName);
        void ValidateQuantity(IBaseQuantifiable? baseQuantifiable, string paramName);
    }

    //public interface IQuantifiable<TSelf> : IBaseQuantifiable, IExchange<TSelf, Enum>, IFit<TSelf>
    //    where TSelf : class, IBaseQuantifiable
    //{
    //    //MeasureUnitCode MeasureUnitCode { get; init; }

    //    TSelf GetQuantifiable(MeasureUnitCode measureUnitCode, decimal defaultQuantity);

    //    void ValidateQuantifiable(IBaseQuantifiable? baseQuantifiable, string paramName);
    //}
}

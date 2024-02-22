namespace CsabaDu.FooVaria.BaseTypes.BaseMeasures.Types
{
    public interface IBaseMeasure : IQuantifiable, IExchangeRate, IRateComponentCode, IDecimalQuantity, IQuantityTypeCode, ILimitMode, IEqualityComparer<IBaseMeasure>
    {
        object Quantity { get; init; }

        IBaseMeasure GetBaseMeasure(ValueType quantity);
        IBaseMeasure GetBaseMeasure(IBaseMeasurement baseMeasurement, ValueType quantity);
        IBaseMeasurement GetBaseMeasurement();
        IBaseMeasurementFactory GetBaseMeasurementFactory();
    }

    public interface IBaseMeasure<TSelf> : IBaseMeasure, ILimitable<TSelf>
        where TSelf : class, IBaseMeasure
    {
        TSelf GetBaseMeasure(Enum measureUnit, ValueType quantity);
        TSelf GetBaseMeasure(string name, ValueType quantity);
        TSelf? GetBaseMeasure(Enum measureUnit, decimal exchangeRate, ValueType quantity, string customName);
        TSelf? GetBaseMeasure(string customName, MeasureUnitCode measureUnitCode, decimal exchangeRate, ValueType quantity);
        TSelf GetBaseMeasure(IBaseMeasure baseMeasure);

        #region Default implementations
        public bool TryGetBaseMeasure(Enum measureUnit, decimal exchangeRate, ValueType quantity, string customName, [NotNullWhen(true)] out TSelf? baseMeasure)
        {
            baseMeasure = GetBaseMeasure(measureUnit, exchangeRate, quantity, customName);

            return baseMeasure != null;
        }

        public bool TryGetBaseMeasure(string customName, MeasureUnitCode measureUnitCode, decimal exchangeRate, ValueType quantity, [NotNullWhen(true)] out TSelf? baseMeasure)
        {
            baseMeasure = GetBaseMeasure(customName, measureUnitCode, exchangeRate, quantity);

            return baseMeasure != null;
        }
        #endregion
    }

    public interface IBaseMeasure<TSelf, TNum> : IBaseMeasure, IQuantity<TNum>, ICommonBase<TSelf>
        where TSelf : class, IBaseMeasure/*, IDefaultBaseMeasure*/
        where TNum : struct
    {
        TSelf GetBaseMeasure(TNum quantity);
    }
}

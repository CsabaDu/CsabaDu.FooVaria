namespace CsabaDu.FooVaria.BaseTypes.BaseMeasures.Types
{
    public interface IBaseMeasure : IQuantifiable, IExchangeRate, IRateComponentCode, IQuantityTypeCode, ILimitMode, IEqualityComparer<IBaseMeasure>
    {
        IBaseMeasure GetBaseMeasure(ValueType quantity);
        IBaseMeasure GetBaseMeasure(IBaseMeasurement baseMeasurement, ValueType quantity);
        IBaseMeasurement GetBaseMeasurement();
        IBaseMeasurementFactory GetBaseMeasurementFactory();
    }

    public interface IBaseMeasure<TSelf> : IBaseMeasure
        where TSelf : class, IBaseMeasure
    {
        TSelf GetBaseMeasure(Enum measureUnit, ValueType quantity);
        TSelf GetBaseMeasure(string name, ValueType quantity);
        TSelf? GetBaseMeasure(Enum measureUnit, decimal exchangeRate, ValueType quantity, string customName);
        TSelf? GetBaseMeasure(string customName, MeasureUnitCode measureUnitCode, decimal exchangeRate, ValueType quantity);
        TSelf GetBaseMeasure(IBaseMeasure baseMeasure);

        //#region Default implementations
        //public bool TryGetBaseMeasure(Enum measureUnit, decimal exchangeRate, ValueType quantity, string customName, [NotNullWhen(true)] out TComparable? baseMeasure)
        //{
        //    baseMeasure = GetBaseMeasure(measureUnit, exchangeRate, quantity, customName);

        //    return baseMeasure is not null;
        //}

        //public bool TryGetBaseMeasure(string customName, MeasureUnitCode measureUnitCode, decimal exchangeRate, ValueType quantity, [NotNullWhen(true)] out TComparable? baseMeasure)
        //{
        //    baseMeasure = GetBaseMeasure(customName, measureUnitCode, exchangeRate, quantity);

        //    return baseMeasure is not null;
        //}
        //#endregion
    }

    public interface IBaseMeasure<TSelf, TNum> : IBaseMeasure, IQuantity<TNum>, ICommonBase<TSelf>
        where TSelf : class, IBaseMeasure
        where TNum : struct
    {
        TNum Quantity { get; init; }

        TSelf GetBaseMeasure(TNum quantity);
    }
}

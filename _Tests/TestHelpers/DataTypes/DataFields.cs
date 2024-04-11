namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes;

public class DataFields
{
    #region Readonly fields
    public readonly RandomParams RandomParams = new();
    public readonly RootObject RootObject = new();
    #endregion

    //public IBaseMeasure baseMeasure;
    public Enum context;
    public decimal defaultQuantity;
    public decimal decimalQuantity;
    public bool isTrue;
    public ILimiter limiter;
    public LimitMode? limitMode;
    public Enum measureUnit;
    public MeasureUnitCode measureUnitCode;
    public Type measureUnitType;
    public object obj;
    public string paramName;
    public ValueType quantity;
    public TypeCode quantityTypeCode;
    public RateComponentCode rateComponentCode;
    public RoundingMode roundingMode;
    public TypeCode typeCode;
}

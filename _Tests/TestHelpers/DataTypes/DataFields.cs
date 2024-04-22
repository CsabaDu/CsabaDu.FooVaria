namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes;

public class DataFields
{
    #region Readonly fields
    public readonly RandomParams RandomParams = new();
    public readonly RootObject RootObject = new();
    #endregion

    public Enum context;
    public decimal defaultQuantity;
    public decimal decimalQuantity;
    public double doubleQuantity;
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
    public ISpreadMeasure spreadMeasure;
    public TypeCode typeCode;

    protected string testCase;

    public void SetDefaultQuantity()
    {
        defaultQuantity = Convert.ToDecimal(quantity);
        defaultQuantity *= GetExchangeRate(measureUnit, nameof(measureUnit));
        defaultQuantity = defaultQuantity.Round(RoundingMode.DoublePrecision);
    }

    public void SetDoubleQuantity(double quantity)
    {
        quantityTypeCode = TypeCode.Double;
        this.quantity
            = doubleQuantity
            = quantity;

        SetDefaultQuantity();
    }

    public void SetMeasureUnit(Enum measureUnit)
    {
        this.measureUnit = measureUnit;
        measureUnitCode = GetMeasureUnitCode();
        measureUnitType = measureUnit.GetType();

    }

    public void SetRandomMeasureUnit(MeasureUnitCode measureUnitCode)
    {
        this.measureUnitCode = measureUnitCode;
        measureUnit = RandomParams.GetRandomMeasureUnit(measureUnitCode);
        measureUnitType = measureUnit.GetType();
    }

    public void SetRandomQuantity(TypeCode quantityTypeCode)
    {
        this.quantityTypeCode = quantityTypeCode;
        quantity = RandomParams.GetRandomValueType(quantityTypeCode);

        SetDefaultQuantity();
    }

    public void SetBaseMeasureFields(MeasureUnitCode measureUnitCode, double quantity)
    {
        SetRandomMeasureUnit(measureUnitCode);
        SetDoubleQuantity(quantity);
    }

    public MeasureUnitCode GetMeasureUnitCode()
    {
        return Measurable.GetMeasureUnitCode(measureUnit);
    }
}

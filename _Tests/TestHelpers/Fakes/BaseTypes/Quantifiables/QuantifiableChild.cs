namespace CsabaDu.FooVaria.Tests.TestHelpers.Fakes.BaseTypes.Quantifiables;

public class QuantifiableChild(IRootObject rootObject, string paramName) : Quantifiable(rootObject, paramName)
{
    #region Members

    // Shape(IRootObject rootObject, string paramName)
    // int IComparable<IQuantifiable>.CompareTo(IQuantifiable other)
    // bool IEquatable<IQuantifiable>.Equals(IQuantifiable other)
    // IQuantifiable IExchange<IQuantifiable, Enum>.ExchangeTo(Enum context)
    // bool? ILimitable.FitsIn(ILimiter limiter)
    // bool? IFit<IQuantifiable>.FitsIn(IQuantifiable other, LimitMode? limitMode)
    // Enum IMeasureUnit.GetBaseMeasureUnitValue()
    // ValueType IQuantity.GetBaseQuantityValue()
    // decimal IDecimalQuantity.GetDecimalQuantity()
    // Enum IDefaultMeasureUnit.GetDefaultMeasureUnit()
    // IEnumerable<string> IDefaultMeasureUnit.GetDefaultMeasureUnitNames()
    // decimal IDefaultQuantity.GetDefaultQuantityValue()
    // IFactory ICommonBase.GetFactoryValue()
    // RateComponentCode IMeasureUnitCode.GetMeasureUnitCode()
    // Type IMeasureUnit.GetMeasureUnitType()
    // IQuantifiable IQuantifiable.GetQuantifiable(RateComponentCode measureUnitCode, decimal defaultQuantity)
    // object IRound<IQuantifiable>.GetQuantity(RoundingMode roundingMode)
    // object IQuantity.GetQuantity(TypeCode quantityTypeCode)
    // TypeCode IQuantityType.GetQuantityTypeCode()
    // bool IMeasureUnitCode.HasMeasureUnitCode(RateComponentCode measureUnitCode)
    // bool IExchangeable<Enum>.IsExchangeableTo(Enum context)
    // decimal IProportional<IQuantifiable>.ProportionalTo(IQuantifiable other)
    // IQuantifiable IRound<IQuantifiable>.Round(RoundingMode roundingMode)
    // bool ITryExchange<IQuantifiable, Enum>.TryExchangeTo(Enum context, out IQuantifiable exchanged)
    // void IDefaultMeasureUnit.ValidateMeasureUnit(Enum measureUnit, string paramName)
    // void IMeasurable.ValidateMeasureUnitCode(IMeasurable measurable, string paramName)
    // void IMeasureUnitCode.ValidateMeasureUnitCode(RateComponentCode measureUnitCode, string paramName)
    // void IBaseQuantifiable.ValidateQuantity(ValueType defaultQuantity, string paramName)
    #endregion

    #region Test helpers
    public QuantifiableReturnValues ReturnValues { private get; set; }

    protected static QuantifiableReturnValues GetReturn(Enum measureUnit, decimal defaultQuantity, IQuantifiableFactory factory)
    {
        return new()
        {
            GetDefaultQuantityValue = defaultQuantity,
            GetBaseMeasureUnitValue = measureUnit,
            GetFactoryValue = factory,
        };
    }

    public static QuantifiableChild GetQuantifiableChild(decimal defaultQuantity, Enum measureUnit = null, IQuantifiableFactory factory = null)
    {
        return new(Fields.RootObject, Fields.paramName)
        {
            ReturnValues = GetReturn(measureUnit, defaultQuantity, factory),
        };
    }

    public static QuantifiableChild GetQuantifiableChild(DataFields fields)
    {
        return GetQuantifiableChild(fields.defaultQuantity, fields.measureUnit);
    }
    #endregion

    public override sealed Enum GetBaseMeasureUnit() => ReturnValues.GetBaseMeasureUnitValue;

    public override sealed ValueType GetBaseQuantity()
    {
        ValueType quantity = ReturnValues.GetDefaultQuantityValue;
        TypeCode typeCode = GetQuantityTypeCode();

        return (ValueType)quantity.ToQuantity(typeCode);
    }

    public override sealed decimal GetDefaultQuantity() => ReturnValues.GetDefaultQuantityValue;

    public override sealed IFactory GetFactory() => ReturnValues.GetFactoryValue;

    public override sealed IQuantifiable Round(RoundingMode roundingMode)
    {
        decimal defaultQuantity = GetDefaultQuantity().Round(roundingMode);
        MeasureUnitCode measureUnitCode = GetMeasureUnitCode();

        return GetQuantifiableChild(defaultQuantity, measureUnitCode.GetDefaultMeasureUnit());
    }

    public override sealed bool TryExchangeTo(Enum context, [NotNullWhen(true)] out IQuantifiable exchanged)
    {
        return FakeMethods.TryExchange(this, qetQuantifiableChild, context, out exchanged);

        #region Local methods
        QuantifiableChild qetQuantifiableChild()
        {
            Enum measureUnit = GetMeasureUnitElements(context, nameof(context)).MeasureUnit;

            return GetQuantifiableChild(ReturnValues.GetDefaultQuantityValue, measureUnit);
        }
        #endregion
    }
}

﻿namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.QuantifiablesTests.Fakes;

internal class QuantifiableChild(IRootObject rootObject, string paramName) : Quantifiable(rootObject, paramName)
{
    #region Members

    // Quantifiable(IRootObject rootObject, string paramName)
    // int IComparable<IQuantifiable>.CompareTo(IQuantifiable other)
    // bool IEquatable<IQuantifiable>.Equals(IQuantifiable other)
    // IQuantifiable IExchange<IQuantifiable, Enum>.ExchangeTo(Enum context)
    // bool? ILimitable.FitsIn(ILimiter limiter)
    // bool? IFit<IQuantifiable>.FitsIn(IQuantifiable other, LimitMode? limitMode)
    // Enum IMeasureUnit.GetBaseMeasureUnit()
    // ValueType IQuantity.GetBaseQuantity()
    // decimal IDecimalQuantity.GetDecimalQuantity()
    // Enum IDefaultMeasureUnit.GetDefaultMeasureUnit()
    // IEnumerable<string> IDefaultMeasureUnit.GetDefaultMeasureUnitNames()
    // decimal IDefaultQuantity.GetDefaultQuantity()
    // IFactory ICommonBase.GetFactory()
    // MeasureUnitCode IMeasureUnitCode.GetMeasureUnitCode()
    // Type IMeasureUnit.GetMeasureUnitType()
    // IQuantifiable IQuantifiable.GetQuantifiable(MeasureUnitCode measureUnitCode, decimal defaultQuantity)
    // object IRound<IQuantifiable>.GetQuantity(RoundingMode roundingMode)
    // object IQuantity.GetQuantity(TypeCode quantityTypeCode)
    // TypeCode IQuantityType.GetQuantityTypeCode()
    // bool IMeasureUnitCode.HasMeasureUnitCode(MeasureUnitCode measureUnitCode)
    // bool IExchangeable<Enum>.IsExchangeableTo(Enum context)
    // decimal IProportional<IQuantifiable>.ProportionalTo(IQuantifiable other)
    // IQuantifiable IRound<IQuantifiable>.Round(RoundingMode roundingMode)
    // bool ITryExchange<IQuantifiable, Enum>.TryExchangeTo(Enum context, out IQuantifiable exchanged)
    // void IDefaultMeasureUnit.ValidateMeasureUnit(Enum measureUnit, string paramName)
    // void IMeasurable.ValidateMeasureUnitCode(IMeasurable measurable, string paramName)
    // void IMeasureUnitCode.ValidateMeasureUnitCode(MeasureUnitCode measureUnitCode, string paramName)
    // void IBaseQuantifiable.ValidateQuantity(ValueType quantity, string paramName)
    #endregion

    #region Test helpers
    public QuantifiableReturn Return { private get; set; }

    internal static QuantifiableChild GetQuantifiableChild(decimal defaultQuantity, Enum measureUnit = null, IQuantifiableFactory factory = null)
    {
        DataFields fields = new();

        return new(fields.RootObject, fields.paramName)
        {
            Return = new()
            {
                GetDefaultQuantity = defaultQuantity,
                GetBaseMeasureUnit = measureUnit,
                GetFactory = factory,
            }
        };
    }
    #endregion

    public override sealed Enum GetBaseMeasureUnit() => Return.GetBaseMeasureUnit;

    public override sealed ValueType GetBaseQuantity()
    {
        ValueType quantity = Return.GetDefaultQuantity;
        TypeCode typeCode = GetQuantityTypeCode();

        return (ValueType)quantity.ToQuantity(typeCode);
    }

    public override sealed decimal GetDefaultQuantity() => Return.GetDefaultQuantity;

    public override sealed IFactory GetFactory() => Return.GetFactory;

    public override sealed IQuantifiable Round(RoundingMode roundingMode)
    {
        decimal quantity = GetDefaultQuantity().Round(roundingMode);

        return GetQuantifiableChild(quantity, GetMeasureUnitCode());
    }

    public override sealed bool TryExchangeTo(Enum context, [NotNullWhen(true)] out IQuantifiable exchanged)
    {
        exchanged = null;

        if (!IsExchangeableTo(context)) return false;

        Enum measureUnit = GetMeasureUnitElements(context, nameof(context)).MeasureUnit;
        exchanged = GetQuantifiableChild(Return.GetDefaultQuantity, measureUnit);

        return true;
    }
}

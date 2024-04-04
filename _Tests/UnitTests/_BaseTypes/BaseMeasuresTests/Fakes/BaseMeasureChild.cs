﻿namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.BaseMeasuresTests.Fakes;

internal class BaseMeasureChild(IRootObject rootObject, string paramName) : BaseMeasure(rootObject, paramName)
{
    #region Members

    // int IComparable<IQuantifiable>.CompareTo(IQuantifiable? other)
    // bool IEquatable<IQuantifiable>.Equals(IQuantifiable? other)
    // bool IEqualityComparer<IBaseMeasure>.Equals(IBaseMeasure? x, IBaseMeasure? y)
    // bool? ILimitable.FitsIn(ILimiter? limiter)
    // bool? IFit<IQuantifiable>.FitsIn(IQuantifiable? other, LimitMode? limitMode)
    // IBaseMeasure IBaseMeasure.GetBaseMeasure(ValueType quantity)
    // IBaseMeasure IBaseMeasure.GetBaseMeasure(IBaseMeasurement baseMeasurement, ValueType quantity)
    // IBaseMeasurement IBaseMeasure.GetBaseMeasurement()
    // IBaseMeasurementFactory IBaseMeasure.GetBaseMeasurementFactory()
    // Enum IMeasureUnit.GetBaseMeasureUnit()
    // ValueType IQuantity.GetBaseQuantity()
    // decimal IDecimalQuantity.GetDecimalQuantity()
    // Enum IDefaultMeasureUnit.GetDefaultMeasureUnit()
    // IEnumerable<string> IDefaultMeasureUnit.GetDefaultMeasureUnitNames()
    // decimal IDefaultQuantity.GetDefaultQuantity()
    // decimal IExchangeRate.GetExchangeRate()
    // IFactory ICommonBase.GetFactory()
    // int IEqualityComparer<IBaseMeasure>.GetHashCode(IBaseMeasure obj)
    // LimitMode? ILimitMode.GetLimitMode()
    // MeasureUnitCode IMeasureUnitCode.GetMeasureUnitCode()
    // Type IMeasureUnit.GetMeasureUnitType()
    // IQuantifiable IQuantifiable.GetQuantifiable(MeasureUnitCode measureUnitCode, decimal defaultQuantity)
    // object IRound<IQuantifiable>.GetQuantity(RoundingMode roundingMode)
    // object IQuantity.GetQuantity(TypeCode quantityTypeCode)
    // TypeCode IQuantityType.GetQuantityTypeCode()
    // TypeCode? IQuantityTypeCode.GetQuantityTypeCode(object quantity)
    // RateComponentCode IRateComponentCode.GetRateComponentCode()
    // bool IMeasureUnitCode.HasMeasureUnitCode(MeasureUnitCode measureUnitCode)
    // bool IExchangeable<Enum>.IsExchangeableTo(Enum? context)
    // decimal IProportional<IQuantifiable>.ProportionalTo(IQuantifiable? other)
    // IQuantifiable IRound<IQuantifiable>.Round(RoundingMode roundingMode)
    // bool ITryExchange<IQuantifiable, Enum>.TryExchangeTo(Enum context, out IQuantifiable? exchanged)
    // void IExchangeRate.ValidateExchangeRate(decimal exchangeRate, string paramName)
    // void IDefaultMeasureUnit.ValidateMeasureUnit(Enum? measureUnit, string paramName)
    // void IMeasurable.ValidateMeasureUnitCode(IMeasurable? measurable, string paramName)
    // void IMeasureUnitCode.ValidateMeasureUnitCode(MeasureUnitCode measureUnitCode, string paramName)
    // void IBaseQuantifiable.ValidateQuantity(ValueType? quantity, string paramName)

    #endregion

    #region Test helpers
    public BaseMeasureReturn Return { private get; set; }

    internal static BaseMeasureChild GetBaseMeasureChild(Enum measureUnit, ValueType quantity, IBaseMeasureFactory factory = null)
    {
        IBaseMeasurement baseMeasurement = BaseMeasurementFactory.CreateBaseMeasurement(measureUnit);
        DataFields fields = new();

        return new(fields.RootObject, fields.paramName)
        {
            Return = new()
            {
                GetBaseMeasurement = baseMeasurement,
                GetBaseQuantity = quantity,
                GetFactory = factory,
            }
        };
    }
    #endregion

    public override IBaseMeasurement GetBaseMeasurement() => Return.GetBaseMeasurement;

    public override IBaseMeasurementFactory GetBaseMeasurementFactory() => BaseMeasurementFactory;

    public override ValueType GetBaseQuantity() => Return.GetBaseQuantity;

    public override IFactory GetFactory() => Return.GetFactory;

    public override bool TryExchangeTo(Enum context, [NotNullWhen(true)] out IQuantifiable exchanged)
    {
        exchanged = null;

        if (!IsExchangeableTo(context)) return false;

        Enum measureUnit = GetMeasureUnitElements(context, nameof(context)).MeasureUnit;
        exchanged = GetBaseMeasureChild(measureUnit, GetBaseQuantity());

        return true;
    }
}

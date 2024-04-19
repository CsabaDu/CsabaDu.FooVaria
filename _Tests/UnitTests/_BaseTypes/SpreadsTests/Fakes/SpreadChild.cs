﻿namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.SpreadsTests.Fakes;

internal class SpreadChild(IRootObject rootObject, string paramName) : Spread(rootObject, paramName)
{
    #region Members

    // ISpread ISpread.GetSpread(ISpreadMeasure spreadMeasure)
    // IQuantifiable IQuantifiable.GetQuantifiable(MeasureUnitCode measureUnitCode, decimal defaultQuantity)
    // void IBaseQuantifiable.ValidateQuantity(ValueType? quantity, string paramName)
    // void IMeasurable.ValidateMeasureUnitCode(IMeasurable? measurable, string paramName)
    // IFactory ICommonBase.GetFactory()
    // Enum IDefaultMeasureUnit.GetDefaultMeasureUnit()
    // IEnumerable<string> IDefaultMeasureUnit.GetDefaultMeasureUnitNames()
    // void IDefaultMeasureUnit.ValidateMeasureUnit(Enum? measureUnit, string paramName)
    // bool IMeasureUnitCode.HasMeasureUnitCode(MeasureUnitCode measureUnitCode)
    // MeasureUnitCode IMeasureUnitCode.GetMeasureUnitCode()
    // void IMeasureUnitCode.ValidateMeasureUnitCode(MeasureUnitCode measureUnitCode, string paramName)
    // Enum IMeasureUnit.GetBaseMeasureUnit()
    // TypeCode IQuantityType.GetQuantityTypeCode()
    // decimal IDefaultQuantity.GetDefaultQuantity()
    // Type IMeasureUnit.GetMeasureUnitType()
    // bool? ILimitable.FitsIn(ILimiter? limiter)
    // bool ITryExchange<IQuantifiable, Enum>.TryExchangeTo(Enum context, out IQuantifiable? exchanged)
    // decimal IProportional<IQuantifiable>.ProportionalTo(IQuantifiable? other)
    // bool IExchangeable<Enum>.IsExchangeableTo(Enum? context)
    // bool? IFit<IQuantifiable>.FitsIn(IQuantifiable? other, LimitMode? limitMode)
    // int IComparable<IQuantifiable>.CompareTo(IQuantifiable? other)
    // bool IEquatable<IQuantifiable>.Equals(IQuantifiable? other)
    // IQuantifiable IRound<IQuantifiable>.Round(RoundingMode roundingMode)
    // object IRound<IQuantifiable>.GetQuantity(RoundingMode roundingMode)
    // decimal IDecimalQuantity.GetDecimalQuantity()
    // ISpreadMeasure ISpreadMeasure.GetSpreadMeasure()
    // ISpreadMeasure? ISpreadGetSpreadMeasure(IQuantifiable? quantifiable)
    // MeasureUnitCode ISpreadMeasure.GetSpreadMeasureUnitCode()
    // void ISpreadMeasure.ValidateSpreadMeasure(ISpreadMeasure? spreadMeasure, string paramName)
    // double IQuantity<double>.GetQuantity()
    // ValueType IQuantity.GetBaseQuantity()
    // object IQuantity.GetQuantity(TypeCode quantityTypeCode)

    #endregion

    #region Test helpers
    private static DataFields Fields = new();
    public SpreadReturn Return { private get; set; } = new();
    protected ISpreadMeasure SpreadMeasure { get; set; }

    internal static SpreadChild GetSpreadChild(Enum measureUnit, ValueType quantity, ISpreadFactory factory = null, RateComponentCode? rateComponentCode = null)
    {
        return new(Fields.RootObject, Fields.paramName)
        {
            Return = new()
            {
                GetFactory = factory,
            },
            SpreadMeasure = GetSpreadMeasureBaseMeasureObject(measureUnit, quantity, rateComponentCode),
        };
    }

    internal static SpreadChild GetSpreadChild(ISpreadMeasure spreadMeasure, ISpreadFactory factory = null)
    {
        return new(Fields.RootObject, Fields.paramName)
        {
            Return = new()
            {
                GetFactory = factory,
            },
            SpreadMeasure = spreadMeasure,
        };
    }
    #endregion

    public override IFactory GetFactory() => Return.GetFactory;

    public override ISpreadMeasure GetSpreadMeasure() => SpreadMeasure;

    public override bool TryExchangeTo(Enum context, [NotNullWhen(true)] out IQuantifiable? exchanged)
    {
        return FakeMethods.TryExchange(this, getSpreadChild, context, out exchanged);

        #region Local methods
        IQuantifiable getSpreadChild()
        {
            Enum measureUnit = GetMeasureUnitElements(context, nameof(context)).MeasureUnit;

            return GetSpreadChild(measureUnit, GetBaseQuantity());
        }
        #endregion
    }
}

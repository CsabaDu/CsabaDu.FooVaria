
namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.QuantifiablesTests.Fakes
{
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
        // bool IExchange<IQuantifiable, Enum>.TryExchangeTo(Enum context, out IQuantifiable exchanged)
        // void IDefaultMeasureUnit.ValidateMeasureUnit(Enum measureUnit, string paramName)
        // void IMeasurable.ValidateMeasureUnitCode(IMeasurable measurable, string paramName)
        // void IMeasureUnitCode.ValidateMeasureUnitCode(MeasureUnitCode measureUnitCode, string paramName)
        // void IBaseQuantifiable.ValidateQuantity(ValueType quantity, string paramName)

        #endregion

        public override IQuantifiable ExchangeTo(Enum context) // abstract
        {
            throw new NotImplementedException();
        }

        public override Enum GetBaseMeasureUnit()
        {
            throw new NotImplementedException();
        }

        public override ValueType GetBaseQuantity() // abstract
        {
            throw new NotImplementedException();
        }

        public override decimal GetDefaultQuantity()
        {
            throw new NotImplementedException();
        }

        public override IFactory GetFactory()
        {
            throw new NotImplementedException();
        }

        public override IQuantifiable Round(RoundingMode roundingMode) // abstract
        {
            throw new NotImplementedException();
        }
    }
}

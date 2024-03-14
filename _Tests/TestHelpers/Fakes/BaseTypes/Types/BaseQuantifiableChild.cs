namespace CsabaDu.FooVaria.Tests.TestHelpers.Fakes.BaseTypes.Types
{
    public sealed class BaseQuantifiableChild(IRootObject rootObject, string paramName) : BaseQuantifiable(rootObject, paramName)
    {
        #region Members

        // void IBaseQuantifiable.ValidateQuantity(ValueType quantity, string paramName)
        // void IBaseQuantifiable.ValidateQuantity(IBaseQuantifiable baseQuantifiable, string paramName)
        // void IMeasurable.ValidateMeasureUnitCode(IMeasurable measurable, string paramName)
        // Enum IDefaultMeasureUnit.GetDefaultMeasureUnit()
        // IEnumerable<string> IDefaultMeasureUnit.GetDefaultMeasureUnitNames()
        // void IDefaultMeasureUnit.ValidateMeasureUnit(Enum measureUnit, string paramName)
        // bool IMeasureUnitCode.HasMeasureUnitCode(MeasureUnitCode measureUnitCode)
        // MeasureUnitCode IMeasureUnitCode.GetMeasureUnitCode()
        // void IMeasureUnitCode.ValidateMeasureUnitCode(MeasureUnitCode measureUnitCode, string paramName)
        // TypeCode IQuantityType.GetQuantityTypeCode()
        // Type IMeasureUnit.GetMeasureUnitType()
        // IFactory ICommonBase.GetFactory()
        // Enum IMeasureUnit.GetBaseMeasureUnit()

        #endregion

        internal BaseQuantifiableReturns Return { private get; set; }

        public override bool? FitsIn(ILimiter limiter) => limiter is IBaseQuantifiable ?
            Return.FitsIn
            : null;

        public override Enum GetBaseMeasureUnit() => Return.GetBaseMeasureUnit;

        public override IFactory GetFactory() => Return.GetFactory;

        public override decimal GetDefaultQuantity() => Return.GetDefaultQuantity;
    }
}

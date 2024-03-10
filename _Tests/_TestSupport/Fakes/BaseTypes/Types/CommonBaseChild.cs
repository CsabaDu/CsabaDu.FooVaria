namespace CsabaDu.FooVaria.Tests.TestSupport.Fakes.BaseTypes.Types
{
    internal sealed class CommonBaseChild(IRootObject rootObject, string paramName) : CommonBase(rootObject, paramName)
    {
        // CommonBaseChild CommonBaseChild(IRootObject rootObject, string paramName)
        // IFactory ICommonBase.GetFactory()

        internal CommonBaseReturns Returns { private get; set; }

        public override IFactory GetFactory() => Returns.GetFactory;
    }

    internal sealed class BaseQuantifiableChild(IRootObject rootObject, string paramName) : BaseQuantifiable(rootObject, paramName)
    {
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

        internal BaseQuantifiableReturns Returns { private get; set; }

        public override bool? FitsIn(ILimiter limiter) => limiter is IBaseQuantifiable ?
            Returns.FitsIn
            : null;

        public override Enum GetBaseMeasureUnit() => Returns.GetBaseMeasureUnit;

        public override IFactory GetFactory() => Returns.GetFactory;

        public override decimal GetDefaultQuantity() => Returns.GetDefaultQuantity;
    }
}

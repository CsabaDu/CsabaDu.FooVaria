//namespace CsabaDu.FooVaria.Tests.TestHelpers.Fakes.BaseTypes.Types
//{
//    public sealed class BaseQuantifiableChild(IRootObject rootObject, string paramName) : BaseQuantifiable(rootObject, paramName)
//    {
//        #region Members

//        // bool Measurable.Equals(object? obj)
//        // bool? ILimitable.FitsIn(ILimiter limiter)
//        // Enum IMeasureUnit.GetBaseMeasureUnit()
//        // Enum IDefaultMeasureUnit.GetDefaultMeasureUnit()
//        // IEnumerable<string> IDefaultMeasureUnit.GetDefaultMeasureUnitNames()
//        // decimal IDefaultQuantity.GetDefaultQuantity()
//        // IFactory ICommonBase.GetFactory()
//        // int Measurable.GetHashCode()
//        // MeasureUnitCode IMeasureUnitCode.GetMeasureUnitCode()
//        // Type IMeasureUnit.GetMeasureUnitType()
//        // TypeCode IQuantityType.GetQuantityTypeCode()
//        // bool IMeasureUnitCode.HasMeasureUnitCode(MeasureUnitCode measureUnitCode)
//        // void IDefaultMeasureUnit.ValidateMeasureUnit(Enum measureUnit, string paramName)
//        // void IMeasurable.ValidateMeasureUnitCode(IMeasurable measurable, string paramName)
//        // void IMeasureUnitCode.ValidateMeasureUnitCode(MeasureUnitCode measureUnitCode, string paramName)
//        // void IBaseQuantifiable.ValidateQuantity(ValueType quantity, string paramName)
//        // void IBaseQuantifiable.ValidateQuantity(IBaseQuantifiable baseQuantifiable, string paramName)

//        #endregion

//        public BaseQuantifiableReturns Return { private get; set; }

//        public override bool? FitsIn(ILimiter limiter) => limiter is IBaseQuantifiable ?
//            Return.FitsIn
//            : null;

//        public override Enum GetBaseMeasureUnit() => Return.GetBaseMeasureUnit;

//        public override IFactory GetFactory() => Return.GetFactory;

//        public override decimal GetDefaultQuantity() => Return.GetDefaultQuantity;

//    }
//}

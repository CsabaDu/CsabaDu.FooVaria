namespace CsabaDu.FooVaria.Tests.TestSupport.Fakes.Common.Types
{
    internal sealed class CommonBaseChild : CommonBase
    {
        public CommonBaseChild(IFactory factory) : base(factory)
        {
        }

        public CommonBaseChild(ICommonBase other) : base(other)
        {
        }

        public CommonBaseChild(IFactory factory, ICommonBase commonBase) : base(factory, commonBase)
        {
        }
    }

    internal sealed class BaseRateChild : BaseRate
    {
        public BaseRateChild(IBaseRate other) : base(other)
        {
        }

        public BaseRateChild(IBaseRateFactory factory, MeasureUnitTypeCode measureUnitTypeCode) : base(factory, measureUnitTypeCode)
        {
        }

        public BaseRateChild(IBaseRateFactory factory, Enum measureUnit) : base(factory, measureUnit)
        {
        }

        public BaseRateChild(IBaseRateFactory factory, IBaseMeasurable baseMeasurable) : base(factory, baseMeasurable)
        {
        }

        public override decimal GetDefaultQuantity()
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<MeasureUnitTypeCode> GetMeasureUnitTypeCodes()
        {
            throw new NotImplementedException();
        }

        public override MeasureUnitTypeCode GetNumeratorMeasureUnitTypeCode()
        {
            throw new NotImplementedException();
        }

        public override bool IsValidMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode)
        {
            throw new NotImplementedException();
        }


        public override void Validate(IFooVariaObject fooVariaObject)
        {
            base.Validate(fooVariaObject);
        }

        public override void ValidateMeasureUnit(Enum measureUnit)
        {
            base.ValidateMeasureUnit(measureUnit);
        }

        public override void ValidateMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode)
        {
            base.ValidateMeasureUnitTypeCode(measureUnitTypeCode);
        }
    }
}

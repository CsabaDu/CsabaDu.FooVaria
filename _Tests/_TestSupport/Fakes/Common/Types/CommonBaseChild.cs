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

        public BaseRateChild(IFactory factory, MeasureUnitTypeCode measureUnitTypeCode) : base(factory, measureUnitTypeCode)
        {
        }

        public BaseRateChild(IFactory factory, Enum measureUnit) : base(factory, measureUnit)
        {
        }

        public BaseRateChild(IFactory factory, IBaseMeasurable baseMeasurable) : base(factory, baseMeasurable)
        {
        }

        public override decimal GetDefaultQuantity()
        {
            throw new NotImplementedException();
        }

        public override Enum GetMeasureUnit()
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
    }
}

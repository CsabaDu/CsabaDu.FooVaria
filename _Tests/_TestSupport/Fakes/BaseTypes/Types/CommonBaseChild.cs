using CsabaDu.FooVaria.BaseTypes.BaseQuantifiables.Behaviors;
using CsabaDu.FooVaria.BaseTypes.BaseQuantifiables.Types;
using CsabaDu.FooVaria.Tests.TestSupport.Fakes.BaseTypes.Returns;

namespace CsabaDu.FooVaria.Tests.TestSupport.Fakes.BaseTypes.Types
{
    internal sealed class CommonBaseChild(IRootObject rootObject, string paramName) : CommonBase(rootObject, paramName)
    {
        internal CommonBaseReturns Returns { private get; set; }

        public override IFactory GetFactory() => Returns.GetFactory;
    }

    internal sealed class BaseQuantifiableChild(IRootObject rootObject, string paramName) : BaseQuantifiable(rootObject, paramName)
    {
        internal BaseQuantifiableReturns Returns { private get; set; }

        public override bool? FitsIn(ILimiter limiter) => limiter is IBaseQuantifiable ?
            Returns.FitsIn
            : null;

        public override Enum GetBaseMeasureUnit() => Returns.GetBaseMeasureUnit;

        public override IFactory GetFactory() => Returns.GetFactory;

        public override decimal GetDefaultQuantity() => Returns.GetDefaultQuantity;
    }
}

﻿using CsabaDu.FooVaria.Common.Behaviors;

namespace CsabaDu.FooVaria.Tests.TestSupport.Fakes.Common.Types;

internal sealed class BaseSpreadChild : BaseSpread
{
    public BaseSpreadChild(IBaseSpread other) : base(other)
    {
    }

    public BaseSpreadChild(IBaseSpreadFactory factory, IBaseMeasurable baseMeasurable) : base(factory, baseMeasurable)
    {
    }

    public override IBaseSpread ExchangeTo(Enum context)
    {
        throw new NotImplementedException();
    }

    public override decimal GetDefaultQuantity()
    {
        throw new NotImplementedException();
    }

    public override ISpreadMeasure GetSpreadMeasure()
    {
        throw new NotImplementedException();
    }

    public override bool IsValidMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode)
    {
        throw new NotImplementedException();
    }
}

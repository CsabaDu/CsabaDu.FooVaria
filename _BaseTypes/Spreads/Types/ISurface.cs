﻿using CsabaDu.FooVaria.BaseTypes.BaseQuantifiables.Behaviors;

namespace CsabaDu.FooVaria.BaseTypes.Spreads.Types;

public interface ISurface : ISpread, IExchange<ISurface, AreaUnit>
{
    ISurface GetSurface();
}

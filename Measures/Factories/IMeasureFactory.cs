﻿using CsabaDu.FooVaria.BaseTypes.Common.Factories;

namespace CsabaDu.FooVaria.Measures.Factories;

public interface IMeasureFactory : IBaseMeasureFactory<IMeasure>, IDeepCopyFactory<IMeasure>
{
    IMeasurementFactory MeasurementFactory { get; init; }
}

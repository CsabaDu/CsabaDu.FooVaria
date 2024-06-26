﻿using CsabaDu.FooVaria.BaseTypes.Common.Factories;

namespace CsabaDu.FooVaria.Measures.Factories;

public interface IMeasureFactory : IBaseMeasureFactory<IMeasure>, IFactory<IMeasure>
{
    IMeasurementFactory MeasurementFactory { get; init; }
}

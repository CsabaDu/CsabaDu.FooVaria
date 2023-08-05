﻿namespace CsabaDu.FooVaria.Measurables.Factories;

public interface IMeasurableFactory
{
    IMeasurable Create(IMeasurable measurable);
    IMeasurable CreateDefault(IMeasurable measurable);
}

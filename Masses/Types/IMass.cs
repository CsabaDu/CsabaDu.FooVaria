﻿namespace CsabaDu.FooVaria.Masses.Types;

public interface IMass : IBaseQuantifiable, IDensity, IVolumeWeightRatio, IFit<IMass>, IExchange<IMass, WeightUnit>, IExchange<IMass, VolumeUnit>, ISpreadMeasure/*, ILimitable*/, IMeasureUnitCodes
{
    IWeight Weight { get; init; }
    IBody GetBody();
    IMeasure? this[MeasureUnitCode measureUnitCode] { get; }

    IVolume GetVolume();
    IMass GetMass(IWeight weight, IBody body);
    IBodyFactory GetBodyFactory();

    void ValidateMassComponent(IBaseQuantifiable? massComponent, string paramName);
}

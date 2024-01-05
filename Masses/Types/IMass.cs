namespace CsabaDu.FooVaria.Masses.Types;

public interface IMass : IQuantifiable, IBody, IDensity, IVolumeWeightRatio
{
    IWeight Weight { get; init; }
    IMeasure? this[MeasureUnitTypeCode measureUnitTypeCode] { get; }

    IVolume GetVolume();
    IMass GetMass(IWeight weight, IBody body);
    IBodyFactory GetBodyFactory();

    void ValidateMassComponent(IQuantifiable? massComponent, string paramName);
}

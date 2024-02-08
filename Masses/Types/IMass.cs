namespace CsabaDu.FooVaria.Masses.Types;

public interface IMass : IQuantifiable, IDensity, IVolumeWeightRatio, IFit<IMass>, IExchange<IMass, Enum>, ISpreadMeasure
{
    IWeight Weight { get; init; }
    IBody Body { get; init; }
    IMeasure? this[MeasureUnitCode measureUnitCode] { get; }

    IVolume GetVolume();
    IMass GetMass(IWeight weight, IBody body);
    IBodyFactory GetBodyFactory();

    void ValidateMassComponent(IQuantifiable? massComponent, string paramName);
}

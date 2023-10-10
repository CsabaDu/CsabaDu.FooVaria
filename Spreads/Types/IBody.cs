namespace CsabaDu.FooVaria.Spreads.Types;

public interface IBody : ISpread<IVolume, VolumeUnit>
{
    IBody GetBody(IExtent radius, IExtent height);
    IBody GetBody(IExtent length, IExtent width, IExtent height);
}

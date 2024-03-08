namespace CsabaDu.FooVaria.Masses.Types;

public interface IDryMass : IMass, ICommonBase<IDryMass>
{
    IDryBody DryBody { get; init; }
    IDryMassFactory Factory { get; init; }

    IDryMass GetDryMass(IWeight weight, IDryBody dryBody);
    IDryMass GetDryMass(IWeight weight, IPlaneShape baseFace, IExtent height);
    IDryMass GetDryMass(IWeight weight, params IExtent[] shapeExtents);
}

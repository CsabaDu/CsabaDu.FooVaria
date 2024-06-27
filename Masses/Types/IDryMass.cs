using CsabaDu.FooVaria.BaseTypes.Common.Behaviors;

namespace CsabaDu.FooVaria.Masses.Types;

public interface IDryMass : IMass, IDeepCopy<IDryMass>, IExchange<IDryMass, ExtentUnit>, IConcreteFactory<IDryMassFactory>
{
    IDryBody DryBody { get; init; }
    //IDryMassFactory Factory { get; init; }

    IDryMass GetDryMass(IWeight weight, IDryBody dryBody);
    IDryMass GetDryMass(IWeight weight, IPlaneShape baseFace, IExtent height);
    IDryMass GetDryMass(IWeight weight, params IExtent[] shapeExtents);
    IDryMass GetDryMass(IDryBody dryBody, IProportion density);
}

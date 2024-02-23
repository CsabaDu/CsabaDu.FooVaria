namespace CsabaDu.FooVaria.Masses.Factories.Implementations;

public sealed class DryMassFactory(IProportionFactory proportionFactory, IDryBodyFactory bodyFactory) : MassFactory(proportionFactory, bodyFactory), IDryMassFactory
{
    #region Public methods
    public IDryMass Create(IWeight weight, IDryBody dryBody)
    {
        return new DryMass(this, weight, dryBody);
    }

    public IDryMass Create(IWeight weight, IPlaneShape baseFace, IExtent height)
    {
        return new DryMass(this, weight, baseFace, height);
    }

    public IDryMass Create(IWeight weight, params IExtent[] shapeExtents)
    {
        return new DryMass(this, weight, shapeExtents);
    }

    public IDryMass CreateNew(IDryMass other)
    {
        return new DryMass(other);
    }

    #region Override methods
    public override IDryMass Create(IWeight weight, IBody body)
    {
        if (body is IDryBody dryBody) return Create(weight, dryBody);

        throw ArgumentTypeOutOfRangeException(nameof(body), body);
    }

    public override IDryBodyFactory GetBodyFactory()
    {
        return (IDryBodyFactory)BodyFactory;
    }

    public override IMeasureFactory GetMeasureFactory()
    {
        return GetBodyFactory().GetMeasureFactory();
    }
    #endregion
    #endregion
}

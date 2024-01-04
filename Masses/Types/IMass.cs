namespace CsabaDu.FooVaria.Masses.Types
{
    public interface IMass : IQuantifiable, IBody
    {
        IWeight Weight { get; init; }
        //IVolume GetVolume();

        IWeight GetVolumeWeight();
        IWeight GetVolumeWeight(decimal ratio);
        MeasureUnitTypeCode GetMeasureUnitTypeCode(decimal ratio);
        double GetQuantity(decimal ratio);
        decimal GetDefaultQuantity(decimal ratio);
        IProportion<WeightUnit, VolumeUnit> GetDensity();
        IMass GetMass(IWeight weight, IBody body);

        void ValidateMassComponent(IQuantifiable massComponent, string paramName);
    }

    public interface IBulkMass : IMass, ICommonBase<IBulkMass>
    {
        IBulkBody BulkBody { get; init; }

        IBulkMass GetBulkMass(IWeight weight, IVolume volume);
    }

    public interface IDryMass : IMass, ICommonBase<IDryMass>
    {
        IDryBody DryBody { get; init; }

        IDryMass GetDryMass(IWeight weight, IDryBody dryBody);
        IDryMass GetDryMass(IWeight weight, IPlaneShape baseFace, IExtent height);
        IDryMass GetDryMass(IWeight weight, params IExtent[] shapeExtents);
    }
}

namespace CsabaDu.FooVaria.Masses.Types.Implementations
{
    internal abstract class Mass : Quantifiable, IMass
    {
        private protected Mass(IMass other) : base(other)
        {
            Weight = other.Weight;
        }

        private protected Mass(IQuantifiableFactory factory, IWeight weight, IBody body) : base(factory, weight)
        {
            ValidateMassComponent(weight, nameof(weight));
            ValidateMassComponent(body, nameof(body));

            Weight = weight; 
        }

        public IWeight Weight { get; init; }

        public decimal GetDefaultQuantity(decimal ratio)
        {
            return GetVolumeWeight(ratio).DefaultQuantity;
        }

        public IProportion<WeightUnit, VolumeUnit> GetDensity()
        {
            throw new NotImplementedException();
        }

        public IMass GetMass(IWeight weight, IBody body)
        {
            throw new NotImplementedException();
        }

        public MeasureUnitTypeCode GetMeasureUnitTypeCode()
        {
            bool isVolumeWeightGreater = GetDensity().DefaultQuantity < 1;

            return GetMeasureUnitTypeCode(isVolumeWeightGreater);
        }

        public MeasureUnitTypeCode GetMeasureUnitTypeCode(decimal ratio)
        {
            bool isVolumeWeightGreater = GetVolumeWeight(ratio).CompareTo(Weight) < 0;

            return GetMeasureUnitTypeCode(isVolumeWeightGreater);
        }

        public double GetQuantity(decimal ratio)
        {
            return GetVolumeWeight(ratio).GetQuantity();
        }

        public double GetQuantity()
        {
            return GetVolumeWeight().GetQuantity();
        }

        public ISpreadMeasure GetSpreadMeasure()
        {
            return GetBody().GetSpreadMeasure();
        }

        public IWeight GetVolumeWeight()
        {
            IWeight volumeWeight = GetVolumeWeight(this);

            return GetGreaterWeight(volumeWeight);
        }

        public IWeight GetVolumeWeight(decimal ratio)
        {
            ValidateQuantity(ratio, nameof(ratio));

            IWeight volumeWeight = GetVolumeWeight(this);
            volumeWeight = (IWeight)volumeWeight.Multiply(ratio);

            return GetGreaterWeight(volumeWeight);
        }

        public void ValidateMassComponent(IQuantifiable massComponent, string paramName)
        {
            decimal defaultQuantity = NullChecked(massComponent, paramName).GetDefaultQuantity();

            ValidateQuantity(defaultQuantity, paramName);
        }

        public override IEnumerable<MeasureUnitTypeCode> GetMeasureUnitTypeCodes()
        {
            yield return MeasureUnitTypeCode.WeightUnit;
            yield return MeasureUnitTypeCode.VolumeUnit;
        }

        public override sealed Enum GetMeasureUnit()
        {
            return Weight.GetMeasureUnit();
        }

        public override sealed decimal GetDefaultQuantity()
        {
            return GetVolumeWeight().DefaultQuantity;
        }

        public abstract int CompareTo(IBaseSpread? other);
        public abstract bool Equals(IBaseSpread? other);
        public abstract IBaseSpread? ExchangeTo(Enum context);
        public abstract bool? FitsIn(IBaseSpread? comparable, LimitMode? limitMode);
        public abstract IBaseSpread GetBaseSpread(ISpreadMeasure spreadMeasure);
        public abstract IBody GetBody();
        public abstract bool IsExchangeableTo(Enum? context);
        public abstract decimal ProportionalTo(IBaseSpread comparable);
        public abstract void ValidateSpreadMeasure(ISpreadMeasure? spreadMeasure, string paramName);

        private IWeight GetGreaterWeight(IWeight volumeWeight)
        {
            return volumeWeight.CompareTo(Weight) < 0 ?
                (IWeight)volumeWeight.ExchangeTo(GetMeasureUnit())!
                : Weight;
        }

        private static MeasureUnitTypeCode GetMeasureUnitTypeCode(bool isVolumeWeightGreater)
        {
            return isVolumeWeightGreater ?
                MeasureUnitTypeCode.VolumeUnit
                : MeasureUnitTypeCode.WeightUnit;
        }

        private static IWeight GetVolumeWeight(IMass mass)
        {
            IVolume volume = (IVolume)mass.GetSpreadMeasure();

            return volume.ConvertMeasure();
        }
    }
}

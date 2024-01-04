using CsabaDu.FooVaria.Common.Statics;
using CsabaDu.FooVaria.RateComponents.Types;

namespace CsabaDu.FooVaria.Masses.Types.Implementations
{
    internal abstract class Mass : Quantifiable, IMass
    {
        #region Constructors
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

        protected Mass(IQuantifiableFactory factory, IWeight weight, params IExtent[] shapeExtents) : base(factory, MeasureUnitTypeCode.WeightUnit, shapeExtents)
        {
            ValidateMassComponent(weight, nameof(weight));

            Weight = weight;
        }
        #endregion

        #region Properties
        public IWeight Weight { get; init; }
        public IMeasure? this[MeasureUnitTypeCode measureUnitTypeCode] => measureUnitTypeCode switch
        {
            MeasureUnitTypeCode.VolumeUnit => GetVolume(),
            MeasureUnitTypeCode.WeightUnit => Weight,

            _ => null,
        };
        #endregion

        #region Public methods
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

        public double GetQuantity()
        {
            return GetVolumeWeight().GetQuantity();
        }

        public double GetQuantity(decimal ratio)
        {
            return GetVolumeWeight(ratio).GetQuantity();
        }

        public ISpreadMeasure GetSpreadMeasure()
        {
            return GetBody().GetSpreadMeasure();
        }

        public IVolume GetVolume()
        {
            return (IVolume)GetSpreadMeasure();
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
            if (NullChecked(massComponent, paramName) is not IWeight or IVolume or IBody)
            {
                throw ArgumentTypeOutOfRangeException(paramName, massComponent);
            }

            decimal defaultQuantity = massComponent.GetDefaultQuantity();

            ValidateQuantity(defaultQuantity, paramName);
        }

        #region Override methods
        public override IEnumerable<MeasureUnitTypeCode> GetMeasureUnitTypeCodes()
        {
            yield return MeasureUnitTypeCode.WeightUnit;
            yield return MeasureUnitTypeCode.VolumeUnit;
        }

        #region Sealed methods
        public override sealed Enum GetMeasureUnit()
        {
            return Weight.GetMeasureUnit();
        }

        public override sealed decimal GetDefaultQuantity()
        {
            return GetVolumeWeight().DefaultQuantity;
        }
        #endregion
        #endregion

        #region Abstract methods
        public abstract int CompareTo(IBaseSpread? other);
        public abstract bool Equals(IBaseSpread? other);
        public abstract IBaseSpread? ExchangeTo(Enum context);
        public abstract bool? FitsIn(IBaseSpread? comparable, LimitMode? limitMode);
        public abstract IBaseSpread GetBaseSpread(ISpreadMeasure spreadMeasure);
        public abstract IBody GetBody();
        public bool IsExchangeableTo(Enum? context)
        {
            if (context is MeasureUnitTypeCode measureUnitTypeCode) return haseMeasureUnitTypeCode(measureUnitTypeCode);

            return IsValidMeasureUnit(context) && haseMeasureUnitTypeCode(MeasureUnitTypes.GetMeasureUnitTypeCode(context!));

            #region Local methods
            bool haseMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode)
            {
                return GetMeasureUnitTypeCodes().Contains(measureUnitTypeCode);
            }
            #endregion
        }

        public abstract decimal ProportionalTo(IBaseSpread comparable);
        public abstract void ValidateSpreadMeasure(ISpreadMeasure? spreadMeasure, string paramName);
        #endregion
        #endregion

        #region Private methods
        private IWeight GetGreaterWeight(IWeight volumeWeight)
        {
            return volumeWeight.CompareTo(Weight) < 0 ?
                (IWeight)volumeWeight.ExchangeTo(GetMeasureUnit())!
                : Weight;
        }

        #region Static methods
        private static MeasureUnitTypeCode GetMeasureUnitTypeCode(bool isVolumeWeightGreater)
        {
            return isVolumeWeightGreater ?
                MeasureUnitTypeCode.VolumeUnit
                : MeasureUnitTypeCode.WeightUnit;
        }

        private static IWeight GetVolumeWeight(IMass mass)
        {
            IVolume volume = mass.GetVolume();

            return volume.ConvertMeasure();
        }
        #endregion
        #endregion
    }
}

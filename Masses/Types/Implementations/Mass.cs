namespace CsabaDu.FooVaria.Masses.Types.Implementations
{
    internal abstract class Mass : BaseSpread, IMass
    {
        #region Constructors
        private protected Mass(IMass other) : base(other)
        {
            Weight = other.Weight;
        }

        private protected Mass(IMassFactory factory, IWeight weight, IBody body) : base(factory, weight)
        {
            validateParams();

            Weight = weight;

            #region Local methods
            void validateParams()
            {
                ValidateMassComponent(weight, nameof(weight));
                ValidateMassComponent(body, nameof(body));
            }
            #endregion
        }

        private protected Mass(IMassFactory factory, IWeight weight, IPlaneShape baseFace, IExtent height) : base(factory, MeasureUnitCode.WeightUnit, baseFace, height)
        {
            validateParams();

            Weight = weight;

            #region Local methods
            void validateParams()
            {
                ValidateMassComponent(weight, nameof(weight));
                _ = NullChecked(baseFace, nameof(baseFace));
                ValidateMassComponent(height, nameof(height));
            }
            #endregion
        }

        private protected Mass(IMassFactory factory, IWeight weight, params IExtent[] shapeExtents) : base(factory, MeasureUnitCode.WeightUnit, shapeExtents)
        {
            validateParams();

            Weight = weight;

            #region Local methods
            void validateParams()
            {
                ValidateMassComponent(weight, nameof(weight));
                foreach (IExtent item in NullChecked(shapeExtents, nameof(shapeExtents)))
                {
                    ValidateMassComponent(item, nameof(shapeExtents));
                }
            }
            #endregion
        }
        #endregion

        #region Properties
        public IWeight Weight { get; init; }
        public IMeasure? this[MeasureUnitCode measureUnitCode] => measureUnitCode switch
        {
            MeasureUnitCode.VolumeUnit => GetVolume(),
            MeasureUnitCode.WeightUnit => Weight,

            _ => null,
        };
        #endregion

        #region Public methods
        public IWeight GetChargeableWeight(decimal ratio, WeightUnit weightUnit, RoundingMode roundingMode)
        {
            ValidateMeasureUnit(weightUnit, nameof(weightUnit));

            return (IWeight)GetVolumeWeight(ratio)
                .ExchangeTo(weightUnit)!
                .Round(roundingMode);
        }

        public decimal GetDefaultQuantity(decimal ratio)
        {
            return GetVolumeWeight(ratio).GetDefaultQuantity();
        }

        public IProportion<WeightUnit, VolumeUnit> GetDensity()
        {
            return GetFactory().CreateDensity(this);
        }

        public MeasureUnitCode GetMeasureUnitCode(decimal ratio)
        {
            bool isVolumeWeightGreater = GetVolumeWeight(ratio).CompareTo(Weight) < 0;

            return GetMeasureUnitCode(isVolumeWeightGreater);
        }

        public double GetQuantity(decimal ratio)
        {
            return GetVolumeWeight(ratio).GetQuantity();
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

        public void ValidateMassComponent(IQuantifiable? massComponent, string paramName)
        {
            if (NullChecked(massComponent, paramName) is not IExtent or IWeight or IVolume or IBody)
            {
                throw ArgumentTypeOutOfRangeException(paramName, massComponent!);
            }

            decimal defaultQuantity = massComponent!.GetDefaultQuantity();

            ValidateQuantity(defaultQuantity, paramName);
        }

        #region Override methods
        public override bool Equals(IBaseSpread? other)
        {
            return base.Equals(other)
                && other is IMass mass
                && Weight.Equals(mass.Weight);
        }

        public override IBaseSpread? ExchangeTo(Enum measureUnit)
        {
            if (measureUnit is not WeightUnit weightUnit) return base.ExchangeTo(measureUnit);

            IBaseMeasure? exchanged = Weight.ExchangeTo(weightUnit);

            if (exchanged is not IWeight weight) return null;

            return GetMass(weight, GetBody());
        }

        public override bool? FitsIn(IBaseSpread? baseSpread, LimitMode? limitMode)
        {
            bool? bodyFitsIn = GetBody().FitsIn(baseSpread, limitMode);

            if (baseSpread is not IMass mass) return bodyFitsIn;

            bool? weightFitsIn = Weight.FitsIn(mass.Weight, limitMode);

            return BothFitIn(bodyFitsIn, weightFitsIn);
        }

        public override IMassFactory GetFactory()
        {
            return (IMassFactory)Factory;
        }

        public override IEnumerable<MeasureUnitCode> GetMeasureUnitCodes()
        {
            return MeasureUnitCodes.Where(x => this[x] != null);
        }

        #region Sealed methods
        public override sealed int CompareTo(IBaseSpread? baseSpread)
        {
            int bodyComparison = base.CompareTo(baseSpread);

            if (baseSpread is not IMass mass) return bodyComparison;

            return GetVolumeWeight().CompareTo(mass.GetVolumeWeight());
        }

        public override sealed IBaseSpread GetBaseSpread(ISpreadMeasure spreadMeasure)
        {
            return GetFactory().CreateBaseSpread(spreadMeasure);
        }

        public override sealed decimal GetDefaultQuantity()
        {
            return GetVolumeWeight().GetDefaultQuantity();
        }

        public override sealed int GetHashCode()
        {
            return HashCode.Combine(Weight, GetBody());
        }

        public override sealed Enum GetMeasureUnit()
        {
            return Weight.GetMeasureUnit();
        }

        public override sealed MeasureUnitCode GetMeasureUnitCode()
        {
            bool isVolumeWeightGreater = GetDensity().GetDefaultQuantity() < 1;

            return GetMeasureUnitCode(isVolumeWeightGreater);
        }

        public override sealed double GetQuantity()
        {
            return GetVolumeWeight().GetQuantity();
        }

        public override sealed ISpreadMeasure GetSpreadMeasure()
        {
            return GetBody().GetSpreadMeasure();
        }

        public override sealed bool IsExchangeableTo(Enum? context)
        {
            if (context is MeasureUnitCode measureUnitCode) return hasMeasureUnitCode(measureUnitCode);

            return BaseMeasurement.IsValidMeasureUnit(context) && hasMeasureUnitCode(GetMeasureUnitCode(context!));

            #region Local methods
            bool hasMeasureUnitCode(MeasureUnitCode measureUnitCode)
            {
                return GetMeasureUnitCodes().Contains(measureUnitCode);
            }
            #endregion
        }

        public override sealed decimal ProportionalTo(IBaseSpread baseSpread)
        {
            if (baseSpread is not IMass mass) return base.ProportionalTo(baseSpread);

            return GetVolumeWeight().ProportionalTo(mass.GetVolumeWeight());
        }

        public override sealed void ValidateQuantity(ValueType? quantity, string paramName)
        {
            object? obj = NullChecked(quantity, nameof(quantity)).ToQuantity(TypeCode.Decimal);

            if (obj is not decimal validQuantity) throw ArgumentTypeOutOfRangeException(paramName, quantity!);

            if (validQuantity > 0) return;

            throw QuantityArgumentOutOfRangeException(paramName, quantity!);
        }
        #endregion
        #endregion

        #region Virtual methods
        public virtual IBodyFactory GetBodyFactory()
        {
            return GetFactory().BodyFactory;
        }
        #endregion

        #region Abstract methods
        public abstract IBody GetBody();
        public abstract IMass GetMass(IWeight weight, IBody body);
        #endregion
        #endregion

        #region Protected methods
        #region Static methods
        protected static bool? BothFitIn(bool? firstFitsIn, bool? secondFitsIn)
        {
            if (firstFitsIn == null || secondFitsIn == null) return null;

            if (firstFitsIn != secondFitsIn) return false;

            return firstFitsIn;
        }
        #endregion
        #endregion

        #region Private methods
        private IWeight GetGreaterWeight(IWeight volumeWeight)
        {
            return volumeWeight.CompareTo(Weight) < 0 ?
                (IWeight)volumeWeight.ExchangeTo(GetMeasureUnit())!
                : Weight;
        }

        private MeasureUnitCode GetMeasureUnitCode(bool isVolumeWeightGreater)
        {
            return isVolumeWeightGreater ?
                GetBody().MeasureUnitCode
                : Weight.MeasureUnitCode;
        }

        #region Static methods
        private static IWeight GetVolumeWeight(IMass mass)
        {
            IVolume volume = mass.GetVolume();

            return volume.ConvertMeasure();
        }
        #endregion
        #endregion
    }
}
namespace CsabaDu.FooVaria.Masses.Types.Implementations
{
    internal abstract class Mass : Quantifiable, IMass
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
        public int CompareTo(IBaseSpread? baseSpread)
        {
            int bodyComparison = GetBody().CompareTo(baseSpread);

            if (baseSpread is not IMass mass) return bodyComparison;

            return GetVolumeWeight().CompareTo(mass.GetVolumeWeight());
        }

        public bool Equals(IBaseSpread? other)
        {
            return other is IMass mass
                && Weight.Equals(mass.Weight)
                && GetBody().Equals(mass.GetBody());
        }

        public IBaseSpread GetBaseSpread(ISpreadMeasure spreadMeasure)
        {
            return GetFactory().CreateBaseSpread(spreadMeasure);
        }

        public decimal GetDefaultQuantity(decimal ratio)
        {
            return GetVolumeWeight(ratio).GetDefaultQuantity();
        }

        public IProportion<WeightUnit, VolumeUnit> GetDensity()
        {
            return GetFactory().CreateDensity(this);
        }

        public MeasureUnitCode GetMeasureUnitCode()
        {
            bool isVolumeWeightGreater = GetDensity().GetDefaultQuantity() < 1;

            return GetMeasureUnitCode(isVolumeWeightGreater);
        }

        public MeasureUnitCode GetMeasureUnitCode(decimal ratio)
        {
            bool isVolumeWeightGreater = GetVolumeWeight(ratio).CompareTo(Weight) < 0;

            return GetMeasureUnitCode(isVolumeWeightGreater);
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

        public bool IsExchangeableTo(Enum? context)
        {
            if (context is MeasureUnitCode measureUnitCode) return hasMeasureUnitCode(measureUnitCode);

            return IsValidMeasureUnit(context) && hasMeasureUnitCode(MeasureUnitTypes.GetMeasureUnitCode(context!));

            #region Local methods
            bool hasMeasureUnitCode(MeasureUnitCode measureUnitCode)
            {
                return GetMeasureUnitCodes().Contains(measureUnitCode);
            }
            #endregion
        }

        public decimal ProportionalTo(IBaseSpread baseSpread)
        {
            decimal bodyProportion = GetBody().ProportionalTo(baseSpread);

            if (baseSpread is not IMass mass) return bodyProportion;

            return GetVolumeWeight().ProportionalTo(mass.GetVolumeWeight());
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

        public void ValidateSpreadMeasure(ISpreadMeasure? spreadMeasure, string paramName)
        {
            ValidateMassComponent((IMeasure?)spreadMeasure, paramName);
        }

        #region Override methods
        public override IEnumerable<MeasureUnitCode> GetMeasureUnitCodes()
        {
            yield return MeasureUnitCode.WeightUnit;
            yield return MeasureUnitCode.VolumeUnit;
        }

        public override IMassFactory GetFactory()
        {
            return (IMassFactory)Factory;
        }

        #region Sealed methods
        public override sealed int GetHashCode()
        {
            return HashCode.Combine(Weight, GetBody());
        }

        public override sealed Enum GetMeasureUnit()
        {
            return Weight.GetMeasureUnit();
        }

        public override sealed decimal GetDefaultQuantity()
        {
            return GetVolumeWeight().GetDefaultQuantity();
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

        public virtual IBaseSpread? ExchangeTo(Enum measureUnit)
        {
            return measureUnit switch
            {
                VolumeUnit volumeUnit => exchangeMassToVolumeUnit(volumeUnit),
                WeightUnit weightUnit => exchangeMassToWeightUnit(weightUnit),

                _ => null,
            };

            #region Local methods
            IBody? exchange<T>(T body, VolumeUnit volumeUnit) where T : class, IBody
            {
                IBaseSpread? exchanged = body.ExchangeTo(volumeUnit);

                return exchanged as T;
            }

            IBody? getExchangedBody(VolumeUnit volumeUnit)
            {
                if (GetBody() is IBulkBody bulkBody)
                {
                    return exchange(bulkBody, volumeUnit);
                }

                if (GetBody() is IDryBody dryBody)
                {
                    return exchange(dryBody, volumeUnit);
                }

                return null;
            }

            IMass? exchangeMassToVolumeUnit(VolumeUnit volumeUnit)
            {
                IBody? body = getExchangedBody(volumeUnit);

                if (body == null) return null;

                return GetMass(Weight, body);
            }

            IMass? exchangeMassToWeightUnit(WeightUnit weightUnit)
            {
                IBaseMeasure? baseMeasure = Weight.ExchangeTo(weightUnit);

                if (baseMeasure is not IWeight weight) return null;

                return GetMass(weight, GetBody());
            }

            #endregion
        }

        public virtual bool? FitsIn(IBaseSpread? baseSpread, LimitMode? limitMode)
        {
            bool? bodyFitsIn = GetBody().FitsIn(baseSpread, limitMode);

            if (baseSpread is not IMass mass) return bodyFitsIn;

            bool? weightFitsIn = Weight.FitsIn(mass.Weight, limitMode);

            if (bodyFitsIn == null || weightFitsIn == null) return null;

            if (bodyFitsIn != weightFitsIn) return false;

            return bodyFitsIn;
        }
        #endregion

        #region Abstract methods
        public abstract IBody GetBody();
        public abstract IMass GetMass(IWeight weight, IBody body);

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

        public object GetQuantity(TypeCode quantityTypeCode)
        {
            throw new NotImplementedException();
        }

        public void ValidateQuantity(ValueType? quantity, TypeCode quantityTypeCode, string paramName)
        {
            throw new NotImplementedException();
        }
        #endregion
        #endregion
    }
}
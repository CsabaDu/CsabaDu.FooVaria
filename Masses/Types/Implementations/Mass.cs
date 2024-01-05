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

        private protected Mass(IMassFactory factory, IWeight weight, IPlaneShape baseFace, IExtent height) : base(factory, MeasureUnitTypeCode.WeightUnit, baseFace, height)
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

        private protected Mass(IMassFactory factory, IWeight weight, params IExtent[] shapeExtents) : base(factory, MeasureUnitTypeCode.WeightUnit, shapeExtents)
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
        public IMeasure? this[MeasureUnitTypeCode measureUnitTypeCode] => measureUnitTypeCode switch
        {
            MeasureUnitTypeCode.VolumeUnit => GetVolume(),
            MeasureUnitTypeCode.WeightUnit => Weight,

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

        public virtual IBodyFactory GetBodyFactory()
        {
            return GetFactory().BodyFactory;
        }

        public decimal GetDefaultQuantity(decimal ratio)
        {
            return GetVolumeWeight(ratio).DefaultQuantity;
        }

        public IProportion<WeightUnit, VolumeUnit> GetDensity()
        {
            return GetFactory().CreateDensity(this);
        }

        public abstract IMass GetMass(IWeight weight, IBody body);

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

        public bool IsExchangeableTo(Enum? context)
        {
            if (context is MeasureUnitTypeCode measureUnitTypeCode) return hasMeasureUnitTypeCode(measureUnitTypeCode);

            return IsValidMeasureUnit(context) && hasMeasureUnitTypeCode(MeasureUnitTypes.GetMeasureUnitTypeCode(context!));

            #region Local methods
            bool hasMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode)
            {
                return GetMeasureUnitTypeCodes().Contains(measureUnitTypeCode);
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
        public override IEnumerable<MeasureUnitTypeCode> GetMeasureUnitTypeCodes()
        {
            yield return MeasureUnitTypeCode.WeightUnit;
            yield return MeasureUnitTypeCode.VolumeUnit;
        }

        #region Sealed methods
        public override IMassFactory GetFactory()
        {
            return (IMassFactory)Factory;
        }

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
            return GetVolumeWeight().DefaultQuantity;
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
                IRateComponent? rateComponent = Weight.ExchangeTo(weightUnit);

                if (rateComponent is not IWeight weight) return null;

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
        #endregion
        #endregion

        #region Private methods
        private IWeight GetGreaterWeight(IWeight volumeWeight)
        {
            return volumeWeight.CompareTo(Weight) < 0 ?
                (IWeight)volumeWeight.ExchangeTo(GetMeasureUnit())!
                : Weight;
        }

        private MeasureUnitTypeCode GetMeasureUnitTypeCode(bool isVolumeWeightGreater)
        {
            return isVolumeWeightGreater ?
                GetBody().MeasureUnitTypeCode
                : Weight.MeasureUnitTypeCode;
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

    internal sealed class BulkMass : Mass, IBulkMass
    {
        public BulkMass(IBulkMass other) : base(other)
        {
            BulkBody = other.BulkBody;
        }

        public BulkMass(IBulkMassFactory factory, IWeight weight, IBody body) : base(factory, weight, body)
        {
            BulkBody = GetBodyFactory().Create(NullChecked(body, nameof(body)))!;
        }

        public IBulkBody BulkBody { get; init; }

        public override IBulkBody GetBody()
        {
            return BulkBody;
        }

        public override IBulkBodyFactory GetBodyFactory()
        {
            return (IBulkBodyFactory)base.GetBodyFactory();
        }

        public IBulkMass GetBulkMass(IWeight weight, IVolume volume)
        {
            return GetFactory().Create(weight, volume);
        }

        public IBulkMass GetBulkMass(IWeight weight, IBody body)
        {
            return GetFactory().Create(weight, body);
        }

        public override IBulkMassFactory GetFactory()
        {
            return (IBulkMassFactory)Factory;
        }

        public override IMass GetMass(IWeight weight, IBody body)
        {
            return GetBulkMass(weight, body);
        }

        public IBulkMass GetNew(IBulkMass other)
        {
            return GetFactory().CreateNew(other);
        }
    }

    internal sealed class DryMass : Mass, IDryMass
    {
        public DryMass(IDryMass other) : base(other)
        {
            DryBody = other.DryBody;
        }

        public DryMass(IDryMassFactory factory, IWeight weight, params IExtent[] shapeExtents) : base(factory, weight, shapeExtents)
        {
            DryBody = getDryBody();

            #region Local methods
            IDryBody getDryBody()
            {
                IBaseShape? baseShape = GetBodyFactory().CreateBaseShape(shapeExtents);

                if (baseShape is IDryBody dryBody) return dryBody;

                throw CountArgumentOutOfRangeException(shapeExtents.Length, nameof(shapeExtents));
            }
            #endregion
        }

        public DryMass(IDryMassFactory factory, IWeight weight, IPlaneShape baseFace, IExtent height) : base(factory, weight, baseFace, height)
        {
            DryBody = GetBodyFactory().Create(baseFace, height);
        }

        public IDryBody DryBody { get; init; }

        public override IDryBody GetBody()
        {
            return DryBody;
        }

        public IDryMass GetDryMass(IWeight weight, IDryBody dryBody)
        {
            return GetFactory().Create(weight, dryBody);
        }

        public IDryMass GetDryMass(IWeight weight, IPlaneShape baseFace, IExtent height)
        {
            return GetFactory().Create(weight, baseFace, height);
        }

        public IDryMass GetDryMass(IWeight weight, params IExtent[] shapeExtents)
        {
            return GetFactory().Create(weight, shapeExtents);
        }

        public IDryMass GetNew(IDryMass other)
        {
            return GetFactory().CreateNew(other);
        }
        public override IBaseSpread? ExchangeTo(Enum measureUnit)
        {
            if (measureUnit is ExtentUnit extentUnit)
            {
                IBaseSpread? baseSpread = DryBody.ExchangeTo(extentUnit);

                return baseSpread is IDryBody dryBody ?
                    GetDryMass(Weight, dryBody)
                    : null;
            }

            return base.ExchangeTo(measureUnit);
        }

        public override bool? FitsIn(IBaseSpread? baseSpread, LimitMode? limitMode)
        {
            bool? bodyFitsIn = GetBody().FitsIn(baseSpread, limitMode);

            if (baseSpread is not IMass mass) return bodyFitsIn;

            bool? weightFitsIn = Weight.FitsIn(mass.Weight, limitMode);

            if (bodyFitsIn == null || weightFitsIn == null) return null;

            if (bodyFitsIn != weightFitsIn) return false;

            return bodyFitsIn;
        }

        public override IDryBodyFactory GetBodyFactory()
        {
            return (IDryBodyFactory)base.GetBodyFactory();
        }

        public override IDryMassFactory GetFactory()
        {
            return (IDryMassFactory)Factory;
        }

        public override IEnumerable<MeasureUnitTypeCode> GetMeasureUnitTypeCodes()
        {
            return base.GetMeasureUnitTypeCodes().Append(MeasureUnitTypeCode.ExtentUnit);
        }

        public override IMass GetMass(IWeight weight, IBody body)
        {
            string paramName = nameof(body);

            if (NullChecked(body, paramName) is IDryBody dryBody) return GetDryMass(weight, dryBody);

            throw ArgumentTypeOutOfRangeException(paramName, body);
        }
    }
}

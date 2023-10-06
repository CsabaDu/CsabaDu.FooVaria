using CsabaDu.FooVaria.Common.Types.Implementations;
using CsabaDu.FooVaria.Spreads.Factories;

namespace CsabaDu.FooVaria.Spreads.Types
{
    internal abstract class Spread : BaseMeasurable, ISpread
    {
        #region Constructors
        protected Spread(ISpread other) : base(other)
        {
        }

        protected Spread(ISpreadFactory factory, IMeasure spreadMeasure) : base(factory, spreadMeasure)
        {
        }
        #endregion

        #region Public methods
        public void ValidateShapeExtents(IEnumerable<IExtent> shapeExtents)
        {
            int count = NullChecked(shapeExtents, nameof(shapeExtents)).Count();

            switch (MeasureUnitTypeCode)
            {
                case MeasureUnitTypeCode.AreaUnit:
                    validateShapeExtents(1, 2);
                    break;
                case MeasureUnitTypeCode.VolumeUnit:
                    validateShapeExtents(2, 3);
                    break;

                default:
                    throw new InvalidOperationException(null);
            }

            #region Local methods
            void validateQuantity(int minValue, int maxValue)
            {
                if (count >= minValue && count <= maxValue) return;

                throw new ArgumentOutOfRangeException(nameof(shapeExtents), count, null);
            }

            void validateShapeExtents(int minValue, int maxValue)
            {
                validateQuantity(minValue, maxValue);

                foreach (IExtent item in shapeExtents)
                {
                    decimal quantity = item.DefaultQuantity;

                    if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(shapeExtents), quantity, null);
                }

            }
            #endregion
        }

        #region Override methods
        public override IFactory GetFactory()
        {
            return (ISpreadFactory)Factory;
        }

        public override Enum GetMeasureUnit()
        {
            throw new NotImplementedException();
        }

        public override bool IsValidMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode)
        {
            return GetMeasureUnitTypeCodes().Contains(measureUnitTypeCode);
        }

        public override void ValidateMeasureUnit(Enum measureUnit)
        {
            MeasureUnitTypeCode measureUnitTypeCode = GetMeasureUnitTypeCode(measureUnit);

            if (IsValidMeasureUnitTypeCode(measureUnitTypeCode)) return;

            throw InvalidMeasureUnitEnumArgumentException(measureUnit);
        }

        #region Sealed methods
        public override sealed IEnumerable<MeasureUnitTypeCode> GetMeasureUnitTypeCodes()
        {
            yield return MeasureUnitTypeCode.AreaUnit;
            yield return MeasureUnitTypeCode.VolumeUnit;
        }

        public override sealed void ValidateMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode)
        {
            if (IsValidMeasureUnitTypeCode(measureUnitTypeCode)) return;

            throw InvalidMeasureUnitTypeCodeEnumArgumentException(measureUnitTypeCode);
        }
        #endregion
        #endregion

        #region Abstract methods
        public abstract ISpread GetSpread(ISpreadMeasure spreadMeasure);
        public abstract ISpread GetSpread(ISpread other);
        public abstract IMeasure GetSpreadMeasure();
        #endregion
        #endregion
    }

    internal abstract class Spread<T, U> : Spread, ISpread<T, U> where T : class, IMeasure, ISpreadMeasure where U : struct, Enum
    {
        #region Constructors
        protected Spread(ISpread<T, U> other) : base(other)
        {
            SpreadMeasure = other.SpreadMeasure;
        }

        protected Spread(ISpreadFactory factory, T spreadMeasure) : base(factory, spreadMeasure)
        {
            ValidateSpreadMeasure(spreadMeasure);

            SpreadMeasure = spreadMeasure;
        }
        #endregion

        #region Properties
        public T SpreadMeasure { get; init; }
        #endregion

        #region Public methods
        public int CompareTo(T? other)
        {
            return SpreadMeasure.CompareTo(other);
        }

        public bool Equals(T? other)
        {
            return SpreadMeasure.Equals(other);
        }

        public bool? FitsIn(T? other, LimitMode? limitMode)
        {
            return SpreadMeasure.FitsIn(other, limitMode);
        }

        public decimal GetDefaultQuantity()
        {
            return SpreadMeasure.DefaultQuantity;
        }

        public double GetQuantity()
        {
            return (double)SpreadMeasure.Quantity;
        }

        public T GetSpreadMeasure(params IExtent[] shapeExtents)
        {
            ValidateShapeExtents(shapeExtents);

            int count = shapeExtents.Length;

            return MeasureUnitTypeCode switch
            {
                MeasureUnitTypeCode.AreaUnit => (T)getArea(count),
                MeasureUnitTypeCode.VolumeUnit => (T)getVolume(),

                _ => throw new InvalidOperationException(null),
            };

            #region Local methods
            IMeasure getVolume()
            {
                IMeasure area = getArea(count - 1);

                decimal quantity = area.DefaultQuantity;
                quantity *= shapeExtents.Last().DefaultQuantity;

                return getSpreadMeasure<VolumeUnit>(quantity);
            }

            IMeasure getArea(int count)
            {
                return count switch
                {
                    1 => getCircleArea(),
                    2 => getRectangleArea(),

                    _ => throw new InvalidOperationException(null),
                };
            }

            IMeasure getCircleArea()
            {
                decimal quantity = shapeExtents.First().DefaultQuantity;
                quantity *= quantity;
                quantity *= Convert.ToDecimal(Math.PI);

                return getSpreadMeasure<AreaUnit>(quantity);
            }

            IMeasure getRectangleArea()
            {
                decimal quantity = shapeExtents.First().DefaultQuantity;
                quantity *= shapeExtents.ElementAt(1).DefaultQuantity;

                return getSpreadMeasure<AreaUnit>(quantity);
            }

            IMeasure getSpreadMeasure<TEnum>(decimal quantity) where TEnum : struct, Enum
            {
                return SpreadMeasure.GetMeasure(quantity, default(TEnum));
            }
            #endregion
        }

        public T GetSpreadMeasure(U measureUnit)
        {
            IBaseMeasure? exchanged = SpreadMeasure.ExchangeTo(Defined(measureUnit, nameof(measureUnit)));

            return exchanged == null ?
                throw new InvalidOperationException(null)
                : (T)SpreadMeasure.GetMeasure(exchanged);
        }

        public bool IsExchangeableTo(Enum? context)
        {
            return SpreadMeasure.IsExchangeableTo(context);
        }

        public decimal ProportionalTo(T spreadMeaasure)
        {
            return SpreadMeasure.ProportionalTo(spreadMeaasure);
        }

        public void ValidateQuantity(double quantity)
        {
            if (quantity > 0) return;

            throw QuantityArgumentOutOfRangeException(quantity);
        }

        public void ValidateSpreadMeasure(IMeasure spreadMeasure)
        {
            if (NullChecked(spreadMeasure, nameof(spreadMeasure)) is T) return;

            throw new ArgumentOutOfRangeException(nameof(spreadMeasure), spreadMeasure.GetType().Name, null);
        }

        #region Override methods
        #region Sealed methods
        public override sealed T GetSpreadMeasure()
        {
            return SpreadMeasure;
        }

        public override sealed bool IsValidMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode)
        {
            return measureUnitTypeCode == MeasureUnitTypeCode;
        }

        public override sealed void ValidateMeasureUnit(Enum measureUnit)
        {
            if (NullChecked(measureUnit, nameof(measureUnit)) is U) return;

            throw InvalidMeasureUnitEnumArgumentException(measureUnit);
        }
        #endregion
        #endregion

        #region Abstract methods
        public abstract ISpread<T, U> GetSpread(T spreadMeasure);
        public abstract ISpread<T, U> GetSpread(U measureUnit);
        public abstract ISpread<T, U> GetSpread(params IExtent[] shapeExtents);
        #endregion
        #endregion
    }
}

using CsabaDu.FooVaria.Proportions.Types;
using static CsabaDu.FooVaria.Common.Statics.MeasureUnitTypes;
using static CsabaDu.FooVaria.Measurables.Statics.MeasureUnits;

namespace CsabaDu.FooVaria.Proportions.Factories.Implementations
{
    public abstract class ProportionFactory : IBaseRateFactory, IProportionFactory
    {
        #region Public methods
        #region Abstract methods
        public abstract IProportion Create(IBaseRate baseRate);
        public abstract IProportion Create(IBaseMeasure numerator, IMeasurement denominatorMeasurement);
        public abstract IProportion Create(MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal defaultQuantity, MeasureUnitTypeCode denominatorMeasureUnitTypeCode);
        public abstract IBaseRate Create(IQuantifiable numerator, MeasureUnitTypeCode measureUnitTypeCode);
        #endregion
        #endregion
    }

    public abstract class ProportionFactory<T, U, W> : ProportionFactory, IProportionFactory<T, U, W> where T : class, IProportion where U : struct, Enum where W : struct, Enum
    {
        #region Public methods
        public T Create(U numeratorMeasureUnit, decimal quantity, W denominatorMeasureUnit)
        {
            MeasureUnitTypeCode numeratorMeasureUnitTypeCode = GetMeasureUnitTypeCode(Defined(numeratorMeasureUnit, nameof(numeratorMeasureUnit)));
            MeasureUnitTypeCode denominatorMeasureUnitTypeCode = GetMeasureUnitTypeCode(Defined(denominatorMeasureUnit, nameof(denominatorMeasureUnit)));
            quantity = quantity * GetExchangeRate(numeratorMeasureUnit) / GetExchangeRate(denominatorMeasureUnit);

            return (T)Create(numeratorMeasureUnitTypeCode, quantity, denominatorMeasureUnitTypeCode);
        }

        public T Create(IMeasure numerator, W denominatorMeasureUnit)
        {
            return CreateProportion(numerator, denominatorMeasureUnit);
        }

        #region Override methods
        #region Sealed methods
        public override sealed T Create(IBaseMeasure numerator, IMeasurement denominatorMeasurement)
        {
            string name = nameof(denominatorMeasurement);

            if (NullChecked(denominatorMeasurement, name).GetMeasureUnit() is W denominatorMeasureUnit)
            {
                return CreateProportion(numerator, denominatorMeasureUnit);
            }

            throw new ArgumentOutOfRangeException(name, denominatorMeasurement.MeasureUnitTypeCode, null);
        }

        public override sealed T Create(IQuantifiable numerator, MeasureUnitTypeCode denominatorMeasureUnitTypeCode)
        {
            string name = nameof(numerator);

            if (NullChecked(numerator, name) is not IBaseMeasure baseMeasure)
            {
                throw ArgumentTypeOutOfRangeException(name, numerator);
            }

            W denominatorMeasureUnit = (W)Defined(denominatorMeasureUnitTypeCode, name).GetDefaultMeasureUnit();

            return CreateProportion(baseMeasure, denominatorMeasureUnit);
        }
        #endregion
        #endregion

        #region Abstract methods
        public abstract T Create(T other);
        #endregion
        #endregion

        #region Private methods
        private T CreateProportion(IBaseMeasure numerator, W denominatorMeasureUnit)
        {
            decimal quantity = NullChecked(numerator, nameof(numerator)).GetDecimalQuantity();
            Enum numeratorMeasureUnit = numerator.Measurement.GetMeasureUnit();

            ValidateMeasureUnit(numeratorMeasureUnit, typeof(U));

            return Create((U)numeratorMeasureUnit, quantity, denominatorMeasureUnit);
        }
        #endregion
    }
}

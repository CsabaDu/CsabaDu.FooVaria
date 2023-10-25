﻿using CsabaDu.FooVaria.Proportions.Factories;
using static CsabaDu.FooVaria.Measurables.Statics.MeasureUnits;

namespace CsabaDu.FooVaria.Proportions.Types.Implementations
{
    internal abstract class Proportion : BaseRate, IProportion
    {
        #region Constructors
        public Proportion(IProportionFactory factory, MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal defaultQuantity, MeasureUnitTypeCode denominatorMeasureUnitTypeCode) : base(factory, defaultQuantity, denominatorMeasureUnitTypeCode)
        {
            NumeratorMeasureUnitTypeCode = Defined(numeratorMeasureUnitTypeCode, nameof(numeratorMeasureUnitTypeCode));
        }

        protected Proportion(IProportion other) : base(other)
        {
            NumeratorMeasureUnitTypeCode = other.NumeratorMeasureUnitTypeCode;
        }

        protected Proportion(IProportionFactory factory, IBaseRate baseRate) : base(factory, baseRate)
        {
            NumeratorMeasureUnitTypeCode = baseRate.GetNumeratorMeasureUnitTypeCode();
        }
        #endregion

        #region Properties
        public MeasureUnitTypeCode NumeratorMeasureUnitTypeCode { get; init; }
        #endregion

        #region Public methods
        public IProportion GetProportion(IBaseRate baseRate)
        {
            return GetFactory().Create(baseRate);
        }

        public IProportion GetProportion(IBaseMeasure numerator, IMeasurement denominatorMeasurement)
        {
            return GetFactory().Create(numerator, denominatorMeasurement);
        }

        #region Override methods
        public override IProportionFactory GetFactory()
        {
            return (IProportionFactory)Factory;
        }

        #region Sealed methods
        public override sealed IProportion GetBaseRate(MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal defaultQuantity, MeasureUnitTypeCode denominatorMeasureUnitTypeCode)
        {
            throw new NotImplementedException();
        }

        public override sealed MeasureUnitTypeCode GetNumeratorMeasureUnitTypeCode()
        {
            return NumeratorMeasureUnitTypeCode;
        }

        public override sealed IProportion GetBaseRate(IQuantifiable numerator, IBaseMeasurable denominator)
        {
            string name = nameof(numerator);

            if (NullChecked(numerator, name) is not IBaseMeasurable baseMeasurable)
            {
                throw ArgumentTypeOutOfRangeException(name, numerator);
            }

            if (NullChecked(denominator, nameof(denominator)) is IMeasurement measurement && numerator is IBaseMeasure baseMeasure)
            {
                return GetProportion(baseMeasure, measurement);
            }

            return GetFactory().Create(baseMeasurable.MeasureUnitTypeCode, numerator.DefaultQuantity, denominator.MeasureUnitTypeCode);
        }
        #endregion
        #endregion
        #endregion
    }

    internal abstract class Proportion<T, U, W> : Proportion, IProportion<T, U, W> where T : class, IProportion where U : struct, Enum where W : struct, Enum
    {
        #region Constructors
        protected Proportion(T other) : base(other)
        {
        }

        protected Proportion(IProportionFactory<T, U, W> factory, IBaseRate baseRate) : base(factory, baseRate)
        {
        }

        protected Proportion(IProportionFactory<T, U, W> factory, MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal defaultQuantity, MeasureUnitTypeCode denominatorMeasureUnitTypeCode) : base(factory, numeratorMeasureUnitTypeCode, defaultQuantity, denominatorMeasureUnitTypeCode)
        {
        }
        #endregion

        #region Public methods
        public T GetProportion(U numeratorMeasureUnit, decimal quantity, W denominatorMeasureUnit)
        {
            return GetFactory().Create(numeratorMeasureUnit, quantity, denominatorMeasureUnit);
        }

        public T GetProportion(IMeasure numerator, W denominatorMeasureUnit)
        {
            return GetFactory().Create(numerator, denominatorMeasureUnit);
        }

        public decimal GetQuantity(U numeratorMeasureUnit, W denominatorMeasureUnit)
        {
            return DefaultQuantity
                / GetExchangeRate(numeratorMeasureUnit)
                * GetExchangeRate(denominatorMeasureUnit);
        }

        #region Override methods
        public override IProportionFactory<T, U, W> GetFactory()
        {
            return (IProportionFactory<T, U, W>)Factory;
        }
        #endregion
        #endregion
    }
}

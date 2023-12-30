﻿namespace CsabaDu.FooVaria.Proportions.Types.Implementations
{
    internal abstract class Proportion : BaseRate, IProportion
    {
        #region Constructors
        private protected Proportion(IProportionFactory factory, Enum numeratorMeasureUnit, ValueType quantity, Enum denominatorMeasureUnit) : base(factory, denominatorMeasureUnit)
        {
            NumeratorMeasureUnitTypeCode = MeasureUnitTypes.GetMeasureUnitTypeCode(numeratorMeasureUnit);
            DefaultQuantity = getValidDefaultQuantity();

            #region Local methods
            decimal getValidDefaultQuantity()
            {
                string paramName = nameof(quantity);
                object? converted = NullChecked(quantity, paramName).ToQuantity(TypeCode.Decimal);
                decimal defaultQuantity = (decimal)(converted ?? throw ArgumentTypeOutOfRangeException(paramName, quantity));

                if (defaultQuantity > 0) return defaultQuantity * GetExchangeRate(numeratorMeasureUnit) / GetExchangeRate(denominatorMeasureUnit);

                throw QuantityArgumentOutOfRangeException(paramName, quantity);
            }
            #endregion
        }
        #endregion

        #region Properties
        public MeasureUnitTypeCode NumeratorMeasureUnitTypeCode { get; init; }
        public override sealed decimal DefaultQuantity { get; init; }
        #endregion

        #region Public methods
        public IProportion GetProportion(IBaseRate baseRate)
        {
            return GetFactory().Create(baseRate);
        }

        public IProportion GetProportion(MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal defaultQuantity, MeasureUnitTypeCode denominatorMeasureUnitTypeCode)
        {
            return GetFactory().Create(numeratorMeasureUnitTypeCode, defaultQuantity, denominatorMeasureUnitTypeCode);
        }

        #region Override methods
        #region Sealed methods
        public override sealed IProportion GetBaseRate(MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal defaultQuantity, MeasureUnitTypeCode denominatorMeasureUnitTypeCode)
        {
            return GetFactory().Create(numeratorMeasureUnitTypeCode, defaultQuantity, denominatorMeasureUnitTypeCode);
        }

        public override sealed IProportionFactory GetFactory()
        {
            return (IProportionFactory)Factory;
        }

        public override sealed IEnumerable<MeasureUnitTypeCode> GetMeasureUnitTypeCodes()
        {
            return base.GetMeasureUnitTypeCodes();
        }

        public override sealed MeasureUnitTypeCode GetNumeratorMeasureUnitTypeCode()
        {
            return NumeratorMeasureUnitTypeCode;
        }

        public override sealed void ValidateQuantity(ValueType? quantity, string paramName)
        {
            object converted = NullChecked(quantity, paramName).ToQuantity(TypeCode.Decimal) ?? throw ArgumentTypeOutOfRangeException(paramName, quantity!);

            if ((decimal)converted > 0) return;

            throw QuantityArgumentOutOfRangeException(paramName, quantity);
        }

        public override sealed IMeasure Multiply(IBaseMeasure multiplier) // Validate?
        {
            if (NullChecked(multiplier, nameof(multiplier)) is not IMeasure measure)
            {
                throw ArgumentTypeOutOfRangeException(nameof(multiplier), multiplier);
            }

            ValidateMeasureUnitTypeCode(measure.MeasureUnitTypeCode, nameof(multiplier));

            decimal quantity = measure.DefaultQuantity * DefaultQuantity;
            Enum measureUnit = NumeratorMeasureUnitTypeCode.GetDefaultMeasureUnit();

            return (IMeasure)measure.GetRateComponent(measureUnit, quantity);
        }

        #endregion
        #endregion
        #endregion
    }

    internal abstract class Proportion<TDEnum> : Proportion, IProportion<TDEnum>
        where TDEnum : struct, Enum
    {
        private protected Proportion(IProportionFactory factory, Enum numeratorMeasureUnit, ValueType quantity, TDEnum denominatorMeasureUnit) : base(factory, numeratorMeasureUnit, quantity, denominatorMeasureUnit)
        {
        }

        public override sealed Enum GetMeasureUnit()
        {
            return GetNumeratorMeasureUnitTypeCode().GetDefaultMeasureUnit();
        }

        public IProportion<TDEnum> GetProportion(IBaseMeasure numerator, TDEnum denominatorMeasureUnit)
        {
            return GetFactory().Create(numerator, denominatorMeasureUnit);
        }

        public IProportion<TDEnum> GetProportion(MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal numeratorDefaultQuantity, TDEnum denominatorMeasureUnit)
        {
            return GetFactory().Create(numeratorMeasureUnitTypeCode, numeratorDefaultQuantity, denominatorMeasureUnit);
        }

        public decimal GetQuantity(TDEnum denominatorMeasureUnit)
        {
            return DefaultQuantity / GetExchangeRate(denominatorMeasureUnit);
        }

        public IBaseMeasure Multiply(TDEnum measureUnit)
        {
            decimal quantity = GetQuantity(measureUnit);

            return GetFactory().CreateBaseMeasure(measureUnit, quantity);
        }
    }

    internal sealed class Proportion<TNEnum, TDEnum> : Proportion<TDEnum>, IProportion<TNEnum, TDEnum>
        where TNEnum : struct, Enum
        where TDEnum : struct, Enum
    {
        internal Proportion(IProportionFactory factory, TNEnum numeratorMeasureUnit, ValueType quantity, TDEnum denominatorMeasureUnit) : base(factory, numeratorMeasureUnit, quantity, denominatorMeasureUnit)
        {
        }

        public override IBaseRate? ExchangeTo(IMeasurable context)
        {
            throw new NotImplementedException();
        }

        public IProportion<TNEnum, TDEnum> GetProportion(TNEnum numeratorMeasureUnit, ValueType quantity, TDEnum denominatorMeasureUnit)
        {
            return GetFactory().Create(numeratorMeasureUnit, quantity, denominatorMeasureUnit);
        }

        public decimal GetQuantity(TNEnum numeratorMeasureUnit, TDEnum denominatorMeasureUnit)
        {
            return GetQuantity(denominatorMeasureUnit) / GetExchangeRate(numeratorMeasureUnit);
        }
    }
}

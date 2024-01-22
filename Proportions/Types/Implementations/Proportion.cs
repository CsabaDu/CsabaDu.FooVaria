using CsabaDu.FooVaria.Measures.Types;

namespace CsabaDu.FooVaria.Proportions.Types.Implementations
{
    internal abstract class Proportion : BaseRate, IProportion
    {
        #region Constructors
        private protected Proportion(IProportionFactory factory, Enum numeratorMeasureUnit, ValueType quantity, Enum denominatorMeasureUnit) : base(factory, denominatorMeasureUnit)
        {
            validateMeasureUnits();

            NumeratorMeasureUnitCode = MeasureUnitTypes.GetMeasureUnitCode(numeratorMeasureUnit);
            DefaultQuantity = getValidDefaultQuantity();

            #region Local methods
            void validateMeasureUnits()
            {
                validateMeasureUnit(numeratorMeasureUnit, nameof(numeratorMeasureUnit));
                validateMeasureUnit(denominatorMeasureUnit, nameof(denominatorMeasureUnit));
            }

            void validateMeasureUnit(Enum measureUnit, string paramName)
            {
                if (IsValidMeasureUnit(measureUnit)) return;

                throw InvalidMeasureUnitEnumArgumentException(measureUnit, paramName);
            }

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
        public MeasureUnitCode NumeratorMeasureUnitCode { get; init; }
        public  decimal DefaultQuantity { get; init; }
        #endregion

        #region Public methods
        public IProportion GetProportion(IBaseRate baseRate)
        {
            return GetFactory().Create(baseRate);
        }

        public IProportion GetProportion(MeasureUnitCode numeratorMeasureUnitCode, decimal defaultQuantity, MeasureUnitCode denominatorMeasureUnitCode)
        {
            return GetFactory().Create(numeratorMeasureUnitCode, defaultQuantity, denominatorMeasureUnitCode);
        }

        public IProportion GetProportion(IBaseMeasure numerator, IBaseMeasure denominator)
        {
            return GetFactory().Create(numerator, denominator);
        }

        #region Override methods
        #region Sealed methods
        public override sealed IProportion GetBaseRate(MeasureUnitCode numeratorMeasureUnitCode, decimal defaultQuantity, MeasureUnitCode denominatorMeasureUnitCode)
        {
            return GetFactory().Create(numeratorMeasureUnitCode, defaultQuantity, denominatorMeasureUnitCode);
        }

        public override sealed IProportionFactory GetFactory()
        {
            return (IProportionFactory)Factory;
        }

        public override sealed Enum GetMeasureUnit()
        {
            return NumeratorMeasureUnitCode.GetDefaultMeasureUnit();
        }

        public override sealed IEnumerable<MeasureUnitCode> GetMeasureUnitCodes()
        {
            return base.GetMeasureUnitCodes();
        }

        public override sealed MeasureUnitCode GetNumeratorMeasureUnitCode()
        {
            return NumeratorMeasureUnitCode;
        }

        public override sealed void ValidateQuantity(ValueType? quantity, string paramName)
        {
            object converted = NullChecked(quantity, paramName).ToQuantity(TypeCode.Decimal) ?? throw ArgumentTypeOutOfRangeException(paramName, quantity!);

            if ((decimal)converted > 0) return;

            throw QuantityArgumentOutOfRangeException(paramName, quantity);
        }

        //public IMeasure Denominate(Enum multiplier) // Validate?
        //{
        //    if (NullChecked(multiplier, nameof(multiplier)) is not IMeasure measure)
        //    {
        //        throw ArgumentTypeOutOfRangeException(nameof(multiplier), multiplier);
        //    }

        //    ValidateMeasureUnitCode(measure.MeasureUnitCode, nameof(multiplier));

        //    decimal quantity = measure.GetDefaultQuantity() * DefaultQuantity;
        //    Enum measureUnit = NumeratorMeasureUnitCode.GetDefaultMeasureUnit();

        //    return (IMeasure)measure.GetBaseMeasure(measureUnit, quantity);
        //}
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

        public IMeasure Denominate(TDEnum measureUnit)
        {
            decimal quantity = GetQuantity(measureUnit);
            IMeasureFactory factory = GetFactory().MeasureFactory;

            return factory.Create(measureUnit, quantity);
        }

        public IProportion<TDEnum> GetProportion(IBaseMeasure numerator, TDEnum denominatorMeasureUnit)
        {
            return GetFactory().Create(numerator, denominatorMeasureUnit);
        }

        public IProportion<TDEnum> GetProportion(MeasureUnitCode numeratorMeasureUnitCode, decimal numeratorDefaultQuantity, TDEnum denominatorMeasureUnit)
        {
            return GetFactory().Create(numeratorMeasureUnitCode, numeratorDefaultQuantity, denominatorMeasureUnit);
        }

        public decimal GetQuantity(TDEnum denominatorMeasureUnit)
        {
            return DefaultQuantity / GetExchangeRate(denominatorMeasureUnit);
        }
    }

    internal sealed class Proportion<TNEnum, TDEnum> : Proportion<TDEnum>, IProportion<TNEnum, TDEnum>
        where TNEnum : struct, Enum
        where TDEnum : struct, Enum
    {
        internal Proportion(IProportionFactory factory, TNEnum numeratorMeasureUnit, ValueType quantity, TDEnum denominatorMeasureUnit) : base(factory, numeratorMeasureUnit, quantity, denominatorMeasureUnit)
        {
        }

        public override decimal GetDefaultQuantity()
        {
            throw new NotImplementedException();
        }

        public TNEnum GetMeasureUnit(IMeasureUnit<TNEnum>? other)
        {
            if (other == null) return default;

            return (TNEnum)other.GetMeasureUnit();
        }

        public IProportion<TNEnum, TDEnum> GetProportion(TNEnum numeratorMeasureUnit, ValueType quantity, TDEnum denominatorMeasureUnit)
        {
            return GetFactory().Create(numeratorMeasureUnit, quantity, denominatorMeasureUnit);
        }

        public decimal GetQuantity(TNEnum numeratorMeasureUnit, TDEnum denominatorMeasureUnit)
        {
            return GetQuantity(denominatorMeasureUnit) / GetExchangeRate(numeratorMeasureUnit);
        }

        public override object GetQuantity(TypeCode quantityTypeCode)
        {
            throw new NotImplementedException();
        }

        public override void ValidateQuantity(ValueType? quantity, TypeCode quantityTypeCode, string paramNamme)
        {
            throw new NotImplementedException();
        }
    }
}

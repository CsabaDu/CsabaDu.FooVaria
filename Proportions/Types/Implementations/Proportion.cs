using CsabaDu.FooVaria.Common.Types.Implementations;
using CsabaDu.FooVaria.RateComponents.Types.Implementations;

namespace CsabaDu.FooVaria.Proportions.Types.Implementations
{
    internal abstract class Proportion : BaseRate, IProportion
    {
        #region Constructors
        private protected Proportion(IProportionFactory factory, MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal defaultQuantity, MeasureUnitTypeCode denominatorMeasureUnitTypeCode) : base(factory, defaultQuantity, denominatorMeasureUnitTypeCode)
        {
            NumeratorMeasureUnitTypeCode = Defined(numeratorMeasureUnitTypeCode, nameof(numeratorMeasureUnitTypeCode));
        }

        private protected Proportion(IProportionFactory factory, IBaseRate baseRate) : base(factory, baseRate)
        {
            NumeratorMeasureUnitTypeCode = baseRate.GetNumeratorMeasureUnitTypeCode();
        }

        private protected Proportion(IProportion other) : base(other)
        {
            NumeratorMeasureUnitTypeCode = other.NumeratorMeasureUnitTypeCode;
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

        public IProportion GetProportion(IRateComponent numerator, IRateComponent denominator)
        {
            return GetFactory().Create(numerator, denominator);
        }

        #region Override methods
        #region Sealed methods
        public override sealed IProportion GetBaseRate(MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal defaultQuantity, MeasureUnitTypeCode denominatorMeasureUnitTypeCode)
        {
            return GetFactory().Create(numeratorMeasureUnitTypeCode, defaultQuantity, denominatorMeasureUnitTypeCode);
        }

        public override sealed IProportion GetBaseRate(IBaseMeasureTemp numerator, Enum denominatorMeasureUnit)
        {
            return (IProportion)GetFactory().Create(numerator, denominatorMeasureUnit);
        }

        public override IProportionFactory GetFactory()
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

            if (QuantityTypes.GetQuantityTypes().Contains(NullChecked(quantity, paramName).GetType())) return;

            throw QuantityArgumentOutOfRangeException(paramName, quantity);
        }

        public override sealed IMeasure Multiply(IBaseMeasureTemp multiplier)
        {
            MeasureUnitTypeCode measureUnitTypeCode = NullChecked(multiplier, nameof(multiplier)).MeasureUnitTypeCode;

            ValidateMeasureUnitTypeCode(measureUnitTypeCode, nameof(multiplier));

            if (multiplier is not IMeasure measure)
            {
                throw ArgumentTypeOutOfRangeException(nameof(multiplier), multiplier);
            }

            decimal quantity = measure.DefaultQuantity * DefaultQuantity;
            Enum measureUnit = MeasureUnitTypes.GetDefaultMeasureUnit(NumeratorMeasureUnitTypeCode);

            return measure.GetMeasure(quantity, measureUnit);
        }
        #endregion
        #endregion
        #endregion
    }

    internal abstract class Proportion<T, U> : Proportion, IProportion<T, U> where T : class, IProportion, IMeasureProportion where U : struct, Enum
    {
        private protected Proportion(T other) : base(other)
        {
        }

        private protected Proportion(IProportionFactory<T, U> factory, IBaseRate baseRate) : base(factory, baseRate)
        {
        }

        private protected Proportion(IProportionFactory<T, U> factory, MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal defaultQuantity, MeasureUnitTypeCode denominatorMeasureUnitTypeCode) : base(factory, numeratorMeasureUnitTypeCode, defaultQuantity, denominatorMeasureUnitTypeCode)
        {
            ValidateMeasureUnitTypeCode(denominatorMeasureUnitTypeCode, nameof(denominatorMeasureUnitTypeCode));
        }

        public T GetProportion(IMeasure numerator, U denominatorMeasureUnit)
        {
            throw new NotImplementedException();
        }

        public decimal GetQuantity(U denominatorMeasureUnit)
        {
            throw new NotImplementedException();
        }

        public override IProportionFactory<T, U> GetFactory()
        {
            return (IProportionFactory<T, U>)Factory;
        }

        public abstract T GetProportion(IRateComponent numerator, U denominatorMeasureUnit);
    }

    internal abstract class Proportion<T, W, U> : Proportion<T, U>, IProportion<T, W, U> where T : class, IProportion<T, W, U>, IMeasureProportion where U : struct, Enum where W : struct, Enum
    {
        private protected Proportion(T other) : base(other)
        {
        }

        private protected Proportion(IProportionFactory<T, W, U> factory, IBaseRate baseRate) : base(factory, baseRate)
        {
        }

        private protected Proportion(IProportionFactory<T, W, U> factory, MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal defaultQuantity, MeasureUnitTypeCode denominatorMeasureUnitTypeCode) : base(factory, numeratorMeasureUnitTypeCode, defaultQuantity, denominatorMeasureUnitTypeCode)
        {
            ValidateMeasureUnitTypeCode(numeratorMeasureUnitTypeCode, nameof(numeratorMeasureUnitTypeCode));
        }

        public T GetProportion(W numeratorMeasureUnit, ValueType quantity, U denominatorMeasureUnit)
        {
            throw new NotImplementedException();
        }

        public decimal GetQuantity(W numeratorMeasureUnit, U denominatorMeasureUnit)
        {
            throw new NotImplementedException();
        }

        public override IProportionFactory<T, W, U> GetFactory()
        {
            return (IProportionFactory<T, W, U>)Factory;
        }
    }

}



    //internal abstract class Proportion<T, U, W> : Proportion, IProportion<T, U, W> where T : class, IProportion where U : struct, Enum where W : struct, Enum
    //{
    //    #region Constructors
    //    private protected Proportion(IProportionFactory factory, IBaseRate baseRate) : base(factory, baseRate)
    //    {
    //    }

    //    private protected Proportion(IProportionFactory factory, MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal defaultQuantity, MeasureUnitTypeCode denominatorMeasureUnitTypeCode) : base(factory, numeratorMeasureUnitTypeCode, defaultQuantity, denominatorMeasureUnitTypeCode)
    //    {
    //    }
    //    #endregion

    //    #region Public methods
    //    public T GetProportion(U numeratorMeasureUnit, ValueType quantity, W denominatorMeasureUnit)
    //    {
    //        ValidateQuantity(quantity, nameof(quantity));

    //        decimal decimalQuantity = (decimal?)quantity.ToQuantity(TypeCode.Decimal) ?? throw new InvalidOperationException(null);

    //        return (T)GetFactory().Create(numeratorMeasureUnit, decimalQuantity, denominatorMeasureUnit);
    //    }

    //    public T GetProportion(IMeasure numerator, W denominatorMeasureUnit)
    //    {
    //        return (T)GetFactory().Create(numerator, denominatorMeasureUnit);
    //    }

    //    public decimal GetQuantity(U numeratorMeasureUnit, W denominatorMeasureUnit)
    //    {
    //        return DefaultQuantity
    //            / GetExchangeRate(Defined(numeratorMeasureUnit, nameof(numeratorMeasureUnit)))
    //            * GetExchangeRate(Defined(denominatorMeasureUnit, nameof(denominatorMeasureUnit)));
    //    }

    //    #region Override methods
    //    #region Sealed methods
    //    public override sealed IEnumerable<MeasureUnitTypeCode> GetMeasureUnitTypeCodes()
    //    {
    //        yield return MeasureUnitTypes.GetMeasureUnitTypeCode(typeof(U));
    //        yield return MeasureUnitTypes.GetMeasureUnitTypeCode(typeof(W));
    //    }
    //    #endregion
    //    #endregion
    //    #endregion
    //}
namespace CsabaDu.FooVaria.Proportions.Types.Implementations
{
    internal abstract class Proportion<TDEnum> : SimpleRate, IProportion<TDEnum>
        where TDEnum : struct, Enum
    {
        #region Constructors
        private protected Proportion(IProportionFactory factory, Enum numeratorMeasureUnit, ValueType quantity, TDEnum denominatorMeasureUnit) : base(factory, numeratorMeasureUnit, quantity, denominatorMeasureUnit)
        {
            
        }
        #endregion

        #region Public methods
        public IMeasure Denominate(TDEnum measureUnit)
        {
            decimal quantity = GetQuantity(measureUnit);
            IMeasureFactory factory = GetFactory().MeasureFactory;

            return factory.Create(measureUnit, quantity);
        }

        public IProportion GetProportion(IBaseMeasure numerator, IBaseMeasure denominator)
        {
            return GetFactory().Create(numerator, denominator);
        }

        public IProportion<TDEnum> GetProportion(IBaseMeasure numerator, TDEnum denominatorMeasureUnit)
        {
            return GetFactory().Create(numerator, denominatorMeasureUnit);
        }

        public decimal GetQuantity(TDEnum denominatorMeasureUnit)
        {
            return DefaultQuantity / GetExchangeRate(denominatorMeasureUnit, nameof(denominatorMeasureUnit));
        }

        #region Override methods
        #region Sealed methods
        public override sealed IProportionFactory GetFactory()
        {
            return (IProportionFactory)Factory;
        }
        #endregion
        #endregion
        #endregion
    }

    internal sealed class Proportion<TNEnum, TDEnum>(IProportionFactory factory, TNEnum numeratorMeasureUnit, ValueType quantity, TDEnum denominatorMeasureUnit) 
        : Proportion<TDEnum>(factory, numeratorMeasureUnit, quantity, denominatorMeasureUnit), IProportion<TNEnum, TDEnum>
        where TNEnum : struct, Enum
        where TDEnum : struct, Enum
    {
        #region Public methods
        public TNEnum GetMeasureUnit()
        {
            return (TNEnum)GetBaseMeasureUnit();
        }

        public IProportion<TNEnum, TDEnum> GetProportion(TNEnum numeratorMeasureUnit, ValueType quantity, TDEnum denominatorMeasureUnit)
        {
            return GetFactory().Create(numeratorMeasureUnit, quantity, denominatorMeasureUnit);
        }

        public decimal GetQuantity(TNEnum numeratorMeasureUnit, TDEnum denominatorMeasureUnit)
        {
            return GetQuantity(denominatorMeasureUnit) * GetExchangeRate(numeratorMeasureUnit, nameof(numeratorMeasureUnit));
        }

        public decimal GetQuantity(TNEnum numeratorMeasureUnit)
        {
            return DefaultQuantity * GetExchangeRate(numeratorMeasureUnit, nameof(numeratorMeasureUnit));
        }
        #endregion
    }
}

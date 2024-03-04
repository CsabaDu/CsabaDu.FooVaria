using System.ComponentModel;

namespace CsabaDu.FooVaria.Proportions.Types.Implementations
{
    //internal abstract class Proportion<TDEnum> : SimpleRate, IProportion<TDEnum>
    //    where TDEnum : struct, Enum
    //{
    //    #region Constructors
    //    private protected Proportion(IProportionFactory factory, Enum numeratorMeasureUnit, ValueType quantity, TDEnum denominatorMeasureUnit) : base(factory, numeratorMeasureUnit, quantity, denominatorMeasureUnit)
    //    {

    //    }
    //    #endregion

    //    #region Public methods
    //    public IMeasure Denominate(TDEnum measureUnit)
    //    {
    //        decimal quantity = GetQuantity(measureUnit);
    //        IMeasureFactory factory = GetFactory().MeasureFactory;

    //        return factory.Create(measureUnit, quantity);
    //    }

    //    public IProportion GetProportion(IBaseMeasure numerator, IBaseMeasure denominator)
    //    {
    //        return GetFactory().Create(numerator, denominator);
    //    }

    //    public IProportion<TDEnum> GetProportion(IBaseMeasure numerator, TDEnum denominatorMeasureUnit)
    //    {
    //        return GetFactory().Create(numerator, denominatorMeasureUnit);
    //    }

    //    public decimal GetQuantity(TDEnum denominatorMeasureUnit)
    //    {
    //        return DefaultQuantity / GetExchangeRate(denominatorMeasureUnit, nameof(denominatorMeasureUnit));
    //    }

    //    #region Override methods
    //    #region Sealed methods
    //    public override sealed IProportionFactory GetFactory()
    //    {
    //        return (IProportionFactory)Factory;
    //    }
    //    #endregion
    //    #endregion
    //    #endregion
    //}

    internal sealed class Proportion : SimpleRate, IProportion
    {
        public Proportion(IProportion other) : base(other)
        {
        }

        public Proportion(IProportionFactory factory, IBaseRate other) : base(factory, other)
        {
        }

        public Proportion(IProportionFactory factory, MeasureUnitCode numeratorCode, decimal defaultQuantity, MeasureUnitCode denominatorCode) : base(factory, numeratorCode, defaultQuantity, denominatorCode)
        {
        }

        #region Public methods

        public IProportion GetProportion(IQuantifiable numerator, IQuantifiable denominator)
        {
            return GetFactory().Create(numerator, denominator);
        }

        public IProportion GetProportion(IQuantifiable numerator, IBaseMeasurement denominator)
        {
            return GetFactory().Create(numerator, denominator);
        }

        public IProportion GetProportion(IQuantifiable numerator, Enum denominatorContext)
        {
            return GetFactory().Create(numerator, denominatorContext);
        }

        public IProportion GetProportion(Enum numeratorContext, decimal quantity, Enum denominatorContext)
        {
            return GetFactory().Create(numeratorContext, quantity, denominatorContext);
        }

        public IProportion GetProportion(IBaseRate baseRate)
        {
            return GetFactory().Create(baseRate);
        }

        public override IProportionFactory GetFactory()
        {
            return (IProportionFactory)Factory;
        }
        #endregion
    }
}

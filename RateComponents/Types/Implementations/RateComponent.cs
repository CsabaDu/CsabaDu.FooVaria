﻿namespace CsabaDu.FooVaria.RateComponents.Types.Implementations
{
    internal abstract class RateComponent : BaseMeasure, IRateComponent
    {
        #region Constructors
        private protected RateComponent(IRateComponentFactory factory/*, IMeasurement measurement*/) : base(factory)
        {
        }
        #endregion

        #region Public methods
        public IMeasurable? GetDefault(MeasureUnitCode measureUnitCode)
        {
            return GetFactory().CreateDefault(measureUnitCode);
        }

        #region Override methods
        public override IRateComponentFactory GetFactory()
        {
            return (IRateComponentFactory)Factory;
        }

        #region Sealed methods
        public override sealed IMeasurementFactory GetBaseMeasurementFactory()
        {
            return GetFactory().MeasurementFactory;
        }

        #endregion
        #endregion

        #region Virtual methods
        #endregion

        #region Abstract methods
        public object GetDefaultRateComponentQuantity()
        {
            return GetFactory().DefaultRateComponentQuantity;
        }
        #endregion
        #endregion

        #region Protected methods
        protected TNum GetDefaultRateComponentQuantity<TNum>()
            where TNum : struct
        {
            return (TNum)GetFactory().DefaultRateComponentQuantity;
        }
        #endregion
    }

    internal abstract class RateComponent<TSelf> : RateComponent, IRateComponent<TSelf>
        where TSelf : class, IRateComponent
    {
        #region Constructors
        private protected RateComponent(IRateComponentFactory factory, IMeasurement measurement) : base(factory)
        {
            Measurement = GetBaseMeasurementFactory().CreateNew(measurement);
        }
        #endregion

        #region Properties
        public IMeasurement Measurement { get; init; }
        #endregion

        #region Public methods
        #region Abstract methods
        public abstract TSelf GetDefault();
        #endregion

        #region Override methods
        #region Sealed methods
        public override sealed IMeasurement GetBaseMeasurement()
        {
            return Measurement;
        }

        public override sealed void ValidateMeasureUnit(Enum? measureUnit, string paramName)
        {
            Measurement.ValidateMeasureUnit(measureUnit!, paramName);
        }
        #endregion
        #endregion
        #endregion

        #region Protected methods
        protected decimal GetDefaultQuantity(object quantity)
        {
            return GetDefaultQuantity(quantity, GetExchangeRate());
        }


        protected TSelf GetRateComponent(ValueType quantity)
        {
            return (TSelf)GetBaseMeasure(Measurement, quantity);
        }
        #endregion
    }

    //internal abstract class RateComponent<TSelf, TNum> : RateComponent<TSelf>, IRateComponent<TSelf, TNum>
    //    where TSelf : class, IRateComponent<TSelf>
    //    where TNum : struct
    //{
    //    private protected RateComponent(IRateComponentFactory factory, IMeasurement measurement) : base(factory, measurement)
    //    {
    //    }

    //    public TNum Quantity { get; init; }

    //    public TSelf GetBaseMeasure(TNum quantity)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public TSelf GetNew(TSelf other)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public TNum GetQuantity()
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}

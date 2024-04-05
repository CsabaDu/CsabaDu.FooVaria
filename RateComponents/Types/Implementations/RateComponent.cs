using CsabaDu.FooVaria.BaseTypes.Common;
using CsabaDu.FooVaria.BaseTypes.Quantifiables.Types;
using System;

namespace CsabaDu.FooVaria.RateComponents.Types.Implementations
{
    internal abstract class RateComponent : BaseMeasure, IRateComponent
    {
        #region Constructors
        private protected RateComponent(IRateComponentFactory factory) : base(factory, nameof(factory))
        {
        }
        #endregion

        #region Public methods
        public IMeasurable? GetDefault(MeasureUnitCode measureUnitCode)
        {
            return GetRateComponentFactory().CreateDefault(measureUnitCode);
        }

        public object GetDefaultRateComponentQuantity()
        {
            return GetRateComponentFactory().DefaultRateComponentQuantity;
        }

        #region Override methods
        #region Sealed methods
        public override sealed IMeasurementFactory GetBaseMeasurementFactory()
        {
            return GetRateComponentFactory().MeasurementFactory;
        }

        public override sealed TypeCode GetQuantityTypeCode()
        {
            Type quantityType = GetBaseQuantity().GetType();

            return Type.GetTypeCode(quantityType);
        }
        #endregion
        #endregion
        #endregion

        #region Protected methods
        protected TNum GetDefaultRateComponentQuantity<TNum>()
            where TNum : struct
        {
            return (TNum)GetRateComponentFactory().DefaultRateComponentQuantity;
        }
        #endregion

        #region Private methods
        private IRateComponentFactory GetRateComponentFactory()
        {
            return (IRateComponentFactory)GetFactory();
        }
        #endregion
    }

    internal abstract class RateComponent<TSelf> : RateComponent, IRateComponent<TSelf>
        where TSelf : class, IRateComponent
    {
        #region Constructors
        private protected RateComponent(IRateComponentFactory<TSelf> factory, IMeasurement measurement) : base(factory)
        {
            Measurement = NullChecked(measurement, nameof(measurement));
        }
        #endregion

        #region Properties
        public IMeasurement Measurement { get; init; }
        #endregion

        #region Public methods
        public TSelf GetDefault()
        {
            return (TSelf)GetDefault(GetMeasureUnitCode())!;
        }

        #region Override methods
        #region Sealed methods
        public override sealed IMeasurement GetBaseMeasurement()
        {
            return Measurement;
        }

        public override sealed void ValidateMeasureUnit(Enum? measureUnit, string paramName)
        {
            Measurement.ValidateMeasureUnit(measureUnit, paramName);
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

        public override sealed bool TryExchangeTo(Enum context, [NotNullWhen(true)] out IQuantifiable? exchanged)
        {
            exchanged = null;

            if (!IsExchangeableTo(context)) return false;

            MeasureUnitElements measureUnitElements = GetMeasureUnitElements(context, nameof(context));
            exchanged = ExchangeTo(measureUnitElements.MeasureUnit);

            return exchanged is not null;
        }

        public TSelf? ExchangeTo(Enum measureUnit)
        {
            if (!IsExchangeableTo(measureUnit)) return null;

            return (TSelf)GetBaseMeasure(measureUnit);
        }
        #endregion
    }
}

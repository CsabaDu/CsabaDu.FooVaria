using System.ComponentModel;

namespace CsabaDu.FooVaria.Measurables.Types.Implementations
{
    internal abstract class Measure : BaseMeasure, IMeasure
    {
        #region Constructors
        private protected Measure(IMeasureFactory factory, ValueType quantity, Enum measureUnit) : base(factory, quantity, measureUnit)
        {
        }
        #endregion
    
        #region Public methods
        public IMeasure Add(IMeasure? other)
        {
            return GetSum(this, other, SummingMode.Add);
        }

        public IMeasure Divide(decimal divisor)
        {
            if (divisor == 0) throw new ArgumentOutOfRangeException(nameof(divisor), divisor, null);

            decimal quantity = decimal.Divide(GetDecimalQuantity(), divisor);

            return GetMeasure(quantity);
        }

        public bool? FitsIn(ILimit? limit)
        {
            return FitsIn(limit, limit?.LimitMode);
        }

        public bool? FitsIn(IBaseMeasure? baseMeasure, LimitMode? limitMode)
        {
            if (baseMeasure == null && limitMode == null) return true;

            if (baseMeasure?.IsExchangeableTo(MeasureUnitTypeCode) != true) return null;

            if (limitMode == null) return CompareTo(baseMeasure) <= 0;

            IBaseMeasure ceilingBaseMeasure = baseMeasure.Round(RoundingMode.Ceiling);
            baseMeasure = getRoundedBaseMeasure();

            if (baseMeasure == null) return null;

            int comparison = CompareTo(baseMeasure);

            return limitMode switch
            {
                LimitMode.BeEqual => comparison == 0 && ceilingBaseMeasure.Equals(baseMeasure),

                _ => comparison.FitsIn(limitMode),
            };

            #region Local methods
            IBaseMeasure? getRoundedBaseMeasure()
            {
                return limitMode switch
                {
                    LimitMode.BeNotLess or
                    LimitMode.BeGreater => ceilingBaseMeasure,

                    LimitMode.BeNotGreater or
                    LimitMode.BeLess or
                    LimitMode.BeEqual => baseMeasure!.Round(RoundingMode.Floor),

                    _ => null,
                };
            }
            #endregion
        }

        public IMeasure GetMeasure(ValueType quantity, Enum measureUnit)
        {
            return GetFactory().Create(quantity, measureUnit);
        }

        public IMeasure GetMeasure(ValueType quantity, string name)
        {
            return GetFactory().Create(quantity, name);
        }

        public IMeasure GetMeasure(ValueType quantity, Enum measureUnit, decimal exchangeRate, string customName)
        {
            return GetFactory().Create(quantity, measureUnit, exchangeRate, customName);
        }

        public IMeasure GetMeasure(ValueType quantity, string customName, decimal exchangeRate)
        {
            return GetFactory().Create(quantity, customName, MeasureUnitTypeCode, exchangeRate);
        }

        public IMeasure GetMeasure(ValueType quantity, IMeasurement measurement)
        {
            return GetFactory().Create(quantity, measurement);
        }

        public IMeasure GetMeasure(IMeasure other)
        {
            return GetFactory().Create(other);
        }

        public IMeasure GetMeasure(ValueType quantity)
        {
            if (quantity is Enum measureUnit) return GetMeasure(measureUnit);

            return GetMeasure(quantity, Measurement);
        }

        public IMeasure GetMeasure(Enum measureUnit)
        {
            IBaseMeasure excchanged = ExchangeTo(measureUnit) ?? throw InvalidMeasureUnitEnumArgumentException(measureUnit);

            return GetMeasure(excchanged);
        }

        public IMeasure Multiply(decimal multiplier)
        {
            decimal quantity = decimal.Multiply(GetDecimalQuantity(), multiplier);

            return GetMeasure(quantity);
        }

        public IMeasure Subtract(IMeasure? other)
        {
            return GetSum(this, other, SummingMode.Subtract);
        }

        #region Override methods
        #region Sealed methods
        public override sealed IMeasure GetDefault()
        {
            return GetDefault(this);
        }
        public override sealed bool Equals(IBaseMeasure? other)
        {
            return other is IMeasure
                && base.Equals(other);
        }

        public override sealed IMeasure GetBaseMeasure(ValueType quantity, Enum measureUnit)
        {
            return GetMeasure(quantity, measureUnit);
        }

        public override sealed IMeasureFactory GetFactory()
        {
            return (IMeasureFactory)Factory;
        }

        public override sealed LimitMode? GetLimitMode()
        {
            return null;
        }

        public override sealed IMeasure GetMeasurable(IMeasurable other)
        {
            return (IMeasure)GetFactory().Create(other);
        }
        public override sealed TypeCode GetQuantityTypeCode()
        {
            return base.GetQuantityTypeCode();
        }

        public override sealed void Validate(IFooVariaObject? fooVariaObject)
        {
            ValidateCommonBaseAction = () => ValidateBaseMeasure(this, fooVariaObject!);

            Validate(this, fooVariaObject);
        }

        public override sealed void ValidateQuantity(ValueType? quantity)
        {
            base.ValidateQuantity(quantity);
        }
        #endregion
        #endregion

        #region Abstract methods
        public abstract IMeasure GetMeasure(IBaseMeasure baseMeasure);
        #endregion
        #endregion

        //#region Protected methods
        //#region Static methods
        //protected static T GetMeasure<T, U>(T measure, U quantity, string name) where T : class, IMeasure where U : struct
        //{
        //    validateName();

        //    return (T)measure.GetMeasure(quantity, name);

        //    #region Local methods
        //    void validateName()
        //    {
        //        if (MeasureUnitTypes.GetDefaultNames(measure.MeasureUnitTypeCode).Contains(NullChecked(name, nameof(name)))) return;

        //        if (measure.Measurement.GetCustomNameCollection(measure.MeasureUnitTypeCode).Values.Contains(name)) return;

        //        throw new ArgumentOutOfRangeException(nameof(name), name, null);
        //    }
        //    #endregion
        //}

        //protected static T GetMeasure<T, U>(T measure, U quantity, IMeasurement measurement) where T : class, IMeasure where U : struct
        //{
        //    ValidateMeasurabe(measurement, nameof(measurement));

        //    return (T)measure.GetMeasure(quantity, measurement);
        //}

        //protected static T GetMeasure<T>(T measure, IBaseMeasure baseMeasure) where T : class, IMeasure
        //{
        //    ValidateMeasurabe(baseMeasure, nameof(baseMeasure));

        //    return (T)measure.GetMeasure(baseMeasure);
        //}

        //protected static T GetMeasure<T>(T measure, T other) where T : class, IMeasure
        //{
        //    return (T)measure.GetMeasure(other);
        //}

        //protected static T GetMeasure<T, U, W>(T measure, U quantity, W measureUnit) where T : class, IMeasure where U : struct where W : struct, Enum
        //{
        //    return (T)measure.GetMeasure(quantity, measureUnit);
        //}

        //protected static T GetMeasure<T, U>(T measure, U quantity) where T : class, IMeasure where U : struct
        //{
        //    return (T)measure.GetMeasure(quantity);
        //}

        //protected static W GetMeasureUnit<W>(IMeasure measure)
        //{
        //    return (W)measure.Measurement.MeasureUnit;
        //}
        //#endregion
        //#endregion

        //#region Private methods
        //#region Static methods
        //private static void ValidateMeasurabe<T>(T measurable, string measurableName) where T : class, IMeasurable, IRateComponentType
        //{
        //    MeasureUnitTypeCode measureUnitTypeCode = NullChecked(measurable, measurableName).MeasureUnitTypeCode;

        //    try
        //    {
        //        measurable.ValidateMeasureUnitTypeCode(measureUnitTypeCode);
        //    }
        //    catch (InvalidEnumArgumentException)
        //    {
        //        throw new ArgumentOutOfRangeException(measurableName, measureUnitTypeCode, null);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new InvalidOperationException(ex.Message, ex.InnerException);
        //    }
        //}
        //#endregion
        //#endregion
    }

    internal abstract class Measure<T, U, W> : Measure, IMeasure<T, U, W> where T : class, IMeasure, IDefaultRateComponent where U : struct where W : struct, Enum
    {
        #region Enums
        protected enum ConvertMode
        {
            Multiply,
            Divide,
        }
        #endregion

        #region Constructors
        private protected Measure(IMeasureFactory factory, ValueType quantity, Enum measureUnit) : base(factory, quantity, measureUnit)
        {
        }
        #endregion

        #region Public methods
        public T GetDefaultRateComponent()
        {
            return GetDefault((this as T)!);
        }

        public U GetDefaultRateComponentQuantity()
        {
            return GetDefaultRateComponentQuantity<U>();
        }

        public T GetMeasure(U quantity, W measureUnit)
        {
            return (T)base.GetMeasure(quantity, measureUnit);
        }

        public T GetMeasure(U quantity, string name)
        {
            validateName();

            return (T)base.GetMeasure(quantity, name);

            #region Local methods
            void validateName()
            {
                if (MeasureUnitTypes.GetDefaultNames(MeasureUnitTypeCode).Contains(NullChecked(name, nameof(name)))) return;

                if (Measurement.GetCustomNameCollection(MeasureUnitTypeCode).Values.Contains(name)) return;

                throw new ArgumentOutOfRangeException(nameof(name), name, null);
            }
            #endregion
        }

        public T GetMeasure(U quantity)
        {
            return (T)base.GetMeasure(quantity);
        }

        public T GetMeasure(U quantity, IMeasurement measurement)
        {
            return (T)base.GetMeasure(quantity, measurement);
        }

        public T GetMeasure(T other)
        {
            return (T)base.GetMeasure(other);
        }

        public W GetMeasureUnit()
        {
            return (W)Measurement.MeasureUnit;
        }

        #region Override methods
        #region Sealed methods
        public override sealed T GetMeasure(IBaseMeasure baseMeasure)
        {
            return GetMeasure(getValidQuantity(), baseMeasure.Measurement);

            #region Local methods
            U getValidQuantity()
            {
                try
                {
                    (this as IBaseMeasurable).Validate(baseMeasure);
                }
                catch (ArgumentNullException)
                {
                    throw new ArgumentNullException(nameof(baseMeasure));
                }
                catch (InvalidEnumArgumentException)
                {
                    throw new ArgumentOutOfRangeException(nameof(baseMeasure), baseMeasure.MeasureUnitTypeCode, null);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException(ex.Message, ex.InnerException);
                }

                return (U)baseMeasure.Quantity;
            }
            #endregion
        }

        public override sealed ValueType GetQuantity()
        {
            return (U)Quantity;
        }
        #endregion
        #endregion
        #endregion

        #region Protected methods
        #region Static methods
        protected static X ConvertMeasure<X, Y>(T measure, ConvertMode convertMode) where X : class, IMeasure, IConvertMeasure where Y : struct, Enum // TODO Check!!!
        {
            decimal quantity = measure.DefaultQuantity;
            decimal ratio = 1000; // Mindig?
            quantity = convertMode switch
            {
                ConvertMode.Multiply => quantity * ratio,
                ConvertMode.Divide => quantity / ratio,

                _ => throw new InvalidOperationException(null),
            };

            return (X)measure.GetMeasure(quantity, default(Y));
        }

        protected static T GetMeasure(T measure, U quantity, W measureUnit, decimal exchangeRate, string customName)
        {
            return (T)measure.GetMeasure(quantity, measureUnit, exchangeRate, customName);
        }

        protected static T GetMeasure(T measure, U quantity, string customName, decimal exchangeRate)
        {
            return (T)measure.GetMeasure(quantity, customName, exchangeRate);
        }
        #endregion
        #endregion
    }
}

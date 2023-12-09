using CsabaDu.FooVaria.RateComponents.Behaviors;
using CsabaDu.FooVaria.RateComponents.Statics;

namespace CsabaDu.FooVaria.Spreads.Types.Implementations
{
    internal abstract class Spread : BaseSpread, ISpread
    {
        #region Constructors
        private protected Spread(ISpread other) : base(other)
        {
        }

        private protected Spread(ISpreadFactory factory, IMeasure spreadMeasure) : base(factory, spreadMeasure)
        {
        }

        private protected Spread(ISpreadFactory factory, Enum measureUnit, ValueType quantity) : base(factory, measureUnit)
        {
            ValidateQuantity(quantity, nameof(quantity));
        }
        #endregion

        #region Public methods
        public void ValidateShapeExtents(string paramName, params IExtent[] shapeExtents)
        {
            SpreadMeasures.ValidateShapeExtents(MeasureUnitTypeCode, paramName, shapeExtents);
        }
        #region Override methods
        public override ISpreadFactory GetFactory()
        {
            return (ISpreadFactory)Factory;
        }

        //public override bool IsValidMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode)
        //{
        //    return GetMeasureUnitTypeCodes().Contains(measureUnitTypeCode);
        //}

        public override void ValidateMeasureUnit(Enum measureUnit, string paramName)
        {
            MeasureUnitTypeCode measureUnitTypeCode = MeasureUnitTypes.GetMeasureUnitTypeCode(measureUnit);

            if (IsValidMeasureUnitTypeCode(measureUnitTypeCode)) return;

            throw InvalidMeasureUnitEnumArgumentException(measureUnit);
        }

        #region Sealed methods
        public override sealed ISpread? ExchangeTo(Enum measureUnit)
        {
            IRateComponent? exchanged = ((IRateComponent)GetSpreadMeasure()).ExchangeTo(measureUnit);

            if (exchanged == null) return null;

            return (ISpread)GetFactory().Create((ISpreadMeasure)exchanged);
        }

        public override sealed void ValidateMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode, string paramName)
        {
            if (IsValidMeasureUnitTypeCode(measureUnitTypeCode)) return;

            throw InvalidMeasureUnitTypeCodeEnumArgumentException(measureUnitTypeCode, paramName);
        }

        public override sealed void ValidateQuantity(ValueType? quantity, string paramName)
        {
            _ = NullChecked(quantity, paramName);

            if (quantity!.ToQuantity(TypeCode.Double) is double doubleQuantity
                && doubleQuantity > 0) return;

            throw QuantityArgumentOutOfRangeException(paramName, quantity);
        }

        #endregion
        #endregion

        #region Abstract methods
        public abstract ISpread GetSpread(ISpreadMeasure spreadMeasure);
        public abstract ISpread GetSpread(params IExtent[] shapeExtents);
        public abstract ISpread GetSpread(IBaseSpread baseSpread);
        #endregion
        #endregion
    }

    internal abstract class Spread<T, U> : Spread, ISpread<T, U> where T : class, ISpread<T, U> where U : class, IMeasure<U, double>, ISpreadMeasure
    {
        private protected Spread(T other) : base(other)
        {
            SpreadMeasure = other.SpreadMeasure;
        }

        private protected Spread(ISpreadFactory<T, U> factory, U spreadMeasure) : base(factory, spreadMeasure)
        {
            SpreadMeasure = spreadMeasure;
        }

        private protected Spread(ISpreadFactory<T, U> factory, Enum measureUnit, double quantity) : base(factory, measureUnit, quantity)
        {
            SpreadMeasure = (U)factory.MeasureFactory.Create(quantity, measureUnit);
        }

        public U SpreadMeasure { get; init; }

        public T GetSpread(U spreadMeasure)
        {
            return GetFactory().Create(spreadMeasure);
        }

        public override ISpreadFactory<T, U> GetFactory()
        {
            return (ISpreadFactory<T, U>)Factory;
        }
        public override sealed U GetSpreadMeasure()
        {
            return SpreadMeasure;
        }
    }

    internal abstract class Spread<T, U, W> : Spread<T, U>, ISpread<T, U, W> where T : class, ISpread<T, U, W> where U : class, IMeasure<U, double, W>, ISpreadMeasure where W : struct, Enum
    {
        #region Constructors
        private protected Spread(T other) : base(other)
        {
            //SpreadMeasure = (TContext)other.GetSpreadMeasure();
        }

        private protected Spread(ISpreadFactory<T, U, W> factory, U spreadMeasure) : base(factory, spreadMeasure)
        {
            //SpreadMeasure = (TContext)SpreadMeasures.GetValidSpreadMeasure(spreadMeasure);
        }

        private protected Spread(ISpreadFactory<T, U, W> factory, W measureUnit, double quantity) : base(factory, measureUnit, quantity)
        {
            //SpreadMeasure = (TContext)factory.MeasureFactory.Create(quantity, measureUnit);
        }
        #endregion

        #region Public methods
        #region Override methods
        public override sealed T GetBaseSpread(ISpreadMeasure spreadMeasure)
        {
            ValidateSpreadMeasure(spreadMeasure, nameof(spreadMeasure));

            return GetFactory().Create((U)spreadMeasure);
        }

        public override ISpreadFactory<T, U, W> GetFactory()
        {
            return (ISpreadFactory<T, U, W>)Factory;
        }

        #region Sealed methods
        //public override sealed bool? FitsIn(IBaseSpread? other, LimitMode? limitMode)
        //{
        //    if (other == null) return null;

        //    TContext spreadMeasure = (TContext)SpreadMeasures.GetValidSpreadMeasure(other);

        //    return SpreadMeasure.FitsIn(spreadMeasure, limitMode);
        //}

        public override sealed T GetSpread(params IExtent[] shapeExtents)
        {
            return (T)GetFactory().Create(shapeExtents);
        }

        public override sealed T GetSpread(ISpreadMeasure spreadMeasure)
        {
            if (spreadMeasure.GetSpreadMeasure() is U validSpreadMeasure) return GetFactory().Create(validSpreadMeasure);

            throw ArgumentTypeOutOfRangeException(nameof(spreadMeasure), spreadMeasure!);
        }

        public override sealed void ValidateMeasureUnit(Enum measureUnit, string paramName)
        {
            if (NullChecked(measureUnit, paramName) is W) return;

            throw InvalidMeasureUnitEnumArgumentException(measureUnit, paramName);
        }
        #endregion
        #endregion

        #region Abstract methods
        public W GetMeasureUnit()
        {
            return SpreadMeasure.GetMeasureUnit();
        }
        public T GetSpread(W measureUnit)
        {
            U spreadMeasure = SpreadMeasure.GetMeasure(measureUnit);

            return GetFactory().Create(spreadMeasure);
        }
        #endregion
        #endregion
    }
}


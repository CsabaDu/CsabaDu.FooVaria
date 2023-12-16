using CsabaDu.FooVaria.Spreads.Behaviors;

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

        private protected Spread(ISpreadFactory factory, Enum measureUnit) : base(factory, measureUnit)
        {
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

    internal abstract class Spread<TSelf, TSMeasure> : Spread, ISpread<TSelf, TSMeasure> where TSelf : class, ISpread where TSMeasure : class, IMeasure<TSMeasure, double>, ISpreadMeasure
    {
        private protected Spread(ISpread<TSelf, TSMeasure> other) : base(other)
        {
            SpreadMeasure = other.SpreadMeasure;
        }

        private protected Spread(ISpreadFactory<TSelf, TSMeasure> factory, TSMeasure spreadMeasure) : base(factory, spreadMeasure)
        {
            SpreadMeasure = spreadMeasure;
        }

        private protected Spread(ISpreadFactory<TSelf, TSMeasure> factory, Enum measureUnit, double quantity) : base(factory, measureUnit)
        {
            SpreadMeasure = quantity > 0 ?
                (TSMeasure)factory.MeasureFactory.Create(measureUnit, quantity)
                : throw QuantityArgumentOutOfRangeException(quantity);
        }

        public TSMeasure SpreadMeasure { get; init; }

        public TSelf GetSpread(TSMeasure spreadMeasure)
        {
            return GetFactory().Create(spreadMeasure);
        }

        public override ISpreadFactory<TSelf, TSMeasure> GetFactory()
        {
            return (ISpreadFactory<TSelf, TSMeasure>)Factory;
        }
        public override sealed TSMeasure GetSpreadMeasure()
        {
            return SpreadMeasure;
        }
    }

    internal abstract class Spread<TSelf, TSMeasure, TEnum> : Spread<TSelf, TSMeasure>, ISpread<TSelf, TSMeasure, TEnum> where TSelf : class, ISpread where TSMeasure : class, IMeasure<TSMeasure, double, TEnum>, ISpreadMeasure where TEnum : struct, Enum
    {
        #region Constructors
        private protected Spread(ISpread<TSelf, TSMeasure, TEnum> other) : base(other)
        {
        }

        private protected Spread(ISpreadFactory<TSelf, TSMeasure, TEnum> factory, TSMeasure spreadMeasure) : base(factory, spreadMeasure)
        {
        }

        private protected Spread(ISpreadFactory<TSelf, TSMeasure, TEnum> factory, TEnum measureUnit, double quantity) : base(factory, measureUnit, quantity)
        {
        }
        #endregion

        #region Public methods
        #region Override methods
        public override sealed TSelf GetBaseSpread(ISpreadMeasure spreadMeasure)
        {
            ValidateSpreadMeasure(spreadMeasure, nameof(spreadMeasure));

            return GetFactory().Create((TSMeasure)spreadMeasure);
        }

        public override ISpreadFactory<TSelf, TSMeasure, TEnum> GetFactory()
        {
            return (ISpreadFactory<TSelf, TSMeasure, TEnum>)Factory;
        }

        #region Sealed methods
        //public override sealed bool? FitsIn(IBaseSpread? other, LimitMode? limitMode)
        //{
        //    if (other == null) return null;

        //    TContext spreadMeasure = (TContext)SpreadMeasures.GetValidSpreadMeasure(other);

        //    return SpreadMeasure.FitsIn(spreadMeasure, limitMode);
        //}

        public override sealed TSelf GetSpread(params IExtent[] shapeExtents)
        {
            return (TSelf)GetFactory().Create(shapeExtents);
        }

        public override sealed TSelf GetSpread(ISpreadMeasure spreadMeasure)
        {
            if (spreadMeasure.GetSpreadMeasure() is TSMeasure validSpreadMeasure) return GetFactory().Create(validSpreadMeasure);

            throw ArgumentTypeOutOfRangeException(nameof(spreadMeasure), spreadMeasure!);
        }

        public override sealed void ValidateMeasureUnit(Enum measureUnit, string paramName)
        {
            if (NullChecked(measureUnit, paramName) is TEnum) return;

            throw InvalidMeasureUnitEnumArgumentException(measureUnit, paramName);
        }
        #endregion
        #endregion

        #region Abstract methods
        public TEnum GetMeasureUnit()
        {
            return SpreadMeasure.GetMeasureUnit();
        }
        public TSelf GetSpread(TEnum measureUnit)
        {
            TSMeasure spreadMeasure = SpreadMeasure.GetMeasure(measureUnit);

            return GetFactory().Create(spreadMeasure);
        }

        public TSelf GetSpread(TEnum measureUnit, double quantity)
        {
            return GetFactory().Create(measureUnit, quantity);
        }
        #endregion
        #endregion
    }
}


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
        #endregion

        #region Public methods
        public bool AreValidShapeExtents(params IExtent[] shapeExtents)
        {
            return SpreadMeasures.AreValidShapeExtents(MeasureUnitCode, shapeExtents);
        }

        #region Override methods
        public override ISpreadFactory GetFactory()
        {
            return (ISpreadFactory)Factory;
        }

        public override void ValidateMeasureUnit(Enum measureUnit, string paramName)
        {
            MeasureUnitCode measureUnitCode = MeasureUnitTypes.GetMeasureUnitCode(measureUnit);

            if (IsValidMeasureUnitCode(measureUnitCode)) return;

            throw InvalidMeasureUnitEnumArgumentException(measureUnit, paramName);
        }

        #region Sealed methods
        public override sealed ISpread? ExchangeTo(Enum measureUnit)
        {
            IRateComponent? exchanged = (GetSpreadMeasure() as IRateComponent)?.ExchangeTo(measureUnit);

            if (exchanged is not ISpreadMeasure spreadMeasure) return null;

            return (ISpread)GetFactory().CreateBaseSpread(spreadMeasure);
        }

        public override sealed void ValidateMeasureUnitCode(MeasureUnitCode measureUnitCode, string paramName)
        {
            if (IsValidMeasureUnitCode(measureUnitCode)) return;

            throw InvalidMeasureUnitCodeEnumArgumentException(measureUnitCode, paramName);
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

    internal abstract class Spread<TSelf, TSMeasure> : Spread, ISpread<TSelf, TSMeasure>
        where TSelf : class, ISpread
        where TSMeasure : class, IMeasure<TSMeasure, double>, ISpreadMeasure
    {
        #region Constructors
        private protected Spread(ISpread<TSelf, TSMeasure> other) : base(other)
        {
            SpreadMeasure = other.SpreadMeasure;
        }

        private protected Spread(ISpreadFactory<TSelf, TSMeasure> factory, TSMeasure spreadMeasure) : base(factory, spreadMeasure)
        {
            SpreadMeasure = spreadMeasure;
        }
        #endregion

        #region Properties
        public TSMeasure SpreadMeasure { get; init; }
        #endregion

        #region Public methods
        public TSelf GetNew(TSelf other)
        {
            return GetFactory().CreateNew(other);
        }

        public TSelf GetSpread(TSMeasure spreadMeasure)
        {
            return GetFactory().Create(spreadMeasure);
        }

        #region Override methods
        public override ISpreadFactory<TSelf, TSMeasure> GetFactory()
        {
            return (ISpreadFactory<TSelf, TSMeasure>)Factory;
        }

        #region Sealed methods
        public override sealed TSMeasure GetSpreadMeasure()
        {
            return SpreadMeasure;
        }
        #endregion
        #endregion
        #endregion
    }

    internal abstract class Spread<TSelf, TSMeasure, TEnum> : Spread<TSelf, TSMeasure>, ISpread<TSelf, TSMeasure, TEnum>
        where TSelf : class, ISpread
        where TSMeasure : class, IMeasure<TSMeasure, double, TEnum>, ISpreadMeasure
        where TEnum : struct, Enum
    {
        #region Constructors
        private protected Spread(ISpread<TSelf, TSMeasure, TEnum> other) : base(other)
        {
        }

        private protected Spread(ISpreadFactory<TSelf, TSMeasure, TEnum> factory, TSMeasure spreadMeasure) : base(factory, spreadMeasure)
        {
        }
        #endregion

        #region Public methods
        #region Override methods
        public override ISpreadFactory<TSelf, TSMeasure, TEnum> GetFactory()
        {
            return (ISpreadFactory<TSelf, TSMeasure, TEnum>)Factory;
        }

        #region Sealed methods
        public override sealed TSelf GetBaseSpread(ISpreadMeasure spreadMeasure)
        {
            ValidateSpreadMeasure(spreadMeasure, nameof(spreadMeasure));

            return GetFactory().Create((TSMeasure)spreadMeasure);
        }

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
        public TEnum GetMeasureUnit(IMeasureUnit<TEnum>? other)
        {
            return SpreadMeasure.GetMeasureUnit(other);
        }
        public TSelf GetSpread(TEnum measureUnit)
        {
            return (TSelf)(ExchangeTo(measureUnit) ?? throw InvalidMeasureUnitEnumArgumentException(measureUnit));
        }

        public TSelf GetSpread(TEnum measureUnit, double quantity)
        {
            return GetFactory().Create(measureUnit, quantity);
        }
        #endregion
        #endregion
    }
}


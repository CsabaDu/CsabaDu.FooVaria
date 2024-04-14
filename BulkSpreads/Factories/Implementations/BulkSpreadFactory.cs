
namespace CsabaDu.FooVaria.BulkSpreads.Factories.Implementations
{
    public abstract class BulkSpreadFactory : IBulkSpreadFactory
    {
        #region Constructors
        private protected BulkSpreadFactory(IMeasureFactory measureFactory)
        {
            MeasureFactory = NullChecked(measureFactory, nameof(measureFactory));
        }
        #endregion

        #region Properties
        public IMeasureFactory MeasureFactory { get; init; }
        #endregion

        #region Public methods
        public IQuantifiable CreateQuantifiable(MeasureUnitCode measureUnitCode, decimal defaultQuantity)
        {
            Enum measureUnit = measureUnitCode.GetDefaultMeasureUnit()!;
            object? quantity = defaultQuantity.ToQuantity(TypeCode.Double);

            if (quantity is double doubleQuantity && doubleQuantity > 0)
            {
                ISpreadMeasure spreadMeasure = CreateSpreadMeasure(measureUnit, doubleQuantity)
                    ?? throw InvalidMeasureUnitCodeEnumArgumentException(measureUnitCode);

                return CreateSpread(spreadMeasure);
            }

            throw QuantityArgumentOutOfRangeException(nameof(defaultQuantity), defaultQuantity);
        }

        #region Abstract methods
        public abstract ISpread CreateSpread(ISpreadMeasure spreadMeasure);
        public abstract IBulkSpread Create(params IExtent[] shapeExtents);
        public abstract ISpreadMeasure? CreateSpreadMeasure(Enum measureUnit, double quantity);
        #endregion
        #endregion
    }

    public abstract class BulkSpreadFactory<T, TSMeasure> : BulkSpreadFactory, IBulkSpreadFactory<T, TSMeasure>
        where T : class, IBulkSpread
        where TSMeasure : class, IMeasure, ISpreadMeasure
    {
        #region Constructors
        private protected BulkSpreadFactory(IMeasureFactory measureFactory) : base(measureFactory)
        {
        }
        #endregion

        #region Public methods
        #region Override methods
        #region Sealed methods
        public override sealed T CreateSpread(ISpreadMeasure spreadMeasure)
        {
            if (GetValidSpreadMeasure(spreadMeasure) is TSMeasure measure) return Create(measure);

            throw ArgumentTypeOutOfRangeException(nameof(spreadMeasure), spreadMeasure);
        }
        #endregion
        #endregion

        #region Abstract methods
        public abstract T Create(TSMeasure spreadMeasure);
        public abstract T CreateNew(T other);
        public abstract T? Create(ISpread spread);
        #endregion
        #endregion
    }

    public abstract class BulkSpreadFactory<T, TSMeasure, TEnum> : BulkSpreadFactory<T, TSMeasure>, IBulkSpreadFactory<T, TSMeasure, TEnum>
        where T : class, IBulkSpread
        where TSMeasure : class, IMeasure, ISpreadMeasure
        where TEnum : struct, Enum
    {
        #region Constructors
        private protected BulkSpreadFactory(IMeasureFactory measureFactory) : base(measureFactory)
        {
        }
        #endregion

        #region Public methods
        #region Override methods
        #region Sealed methods
        public override sealed TSMeasure? CreateSpreadMeasure(Enum measureUnit, double quantity)
        {
            if (measureUnit is not TEnum spreadMeasureUnit) return null;

            return quantity > 0 ?
                 (TSMeasure)MeasureFactory.Create(spreadMeasureUnit, quantity)
                 : null;
        }
        #endregion
        #endregion

        #region Abstract methods
        public abstract T Create(TEnum measureUnit, double quantity);
        #endregion
        #endregion
    }
}

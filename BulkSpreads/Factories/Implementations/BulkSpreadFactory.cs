using CsabaDu.FooVaria.BaseTypes.Quantifiables.Types;

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
            if (!measureUnitCode.IsSpreadMeasureUnitCode()) throw InvalidMeasureUnitCodeEnumArgumentException(measureUnitCode);

            IMeasure measure = MeasureFactory.Create(measureUnitCode, defaultQuantity);

            return CreateSpread((ISpreadMeasure)measure);
        }

        #region Abstract methods
        public abstract ISpread CreateSpread(ISpreadMeasure spreadMeasure);
        public abstract IBulkSpread Create(params IExtent[] shapeExtents);
        #endregion
        #endregion
    }

    public abstract class BulkSpreadFactory<T, TSMeasure> : BulkSpreadFactory, IBulkSpreadFactory<T, TSMeasure>
        where T : class, IBulkSpread
        where TSMeasure : class, IMeasure, ISpreadMeasure, ILimitable
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
        #region Abstract methods
        public abstract T Create(TEnum measureUnit, double quantity);
        #endregion
        #endregion
    }
}

using CsabaDu.FooVaria.RateComponents.Factories;

namespace CsabaDu.FooVaria.Spreads.Factories.Implementations
{
    public abstract class SpreadFactory : ISpreadFactory
    {
        #region Constructors
        private protected SpreadFactory(IMeasureFactory measureFactory)
        {
            MeasureFactory = NullChecked(measureFactory, nameof(measureFactory));
        }
        #endregion

        #region Properties
        public IMeasureFactory MeasureFactory { get; init; }
        #endregion

        #region Public methods
        #region Abstract methods
        public abstract IBaseSpread CreateBaseSpread(ISpreadMeasure spreadMeasure);
        public abstract ISpread Create(params IExtent[] shapeExtents);
        #endregion
        #endregion
    }

    public abstract class SpreadFactory<T, TSMeasure> : SpreadFactory, ISpreadFactory<T, TSMeasure>
        where T : class, ISpread
        where TSMeasure : class, IMeasure, ISpreadMeasure
    {
        #region Constructors
        private protected SpreadFactory(IMeasureFactory measureFactory) : base(measureFactory)
        {
        }
        #endregion

        #region Public methods
        #region Override methods
        #region Sealed methods
        public override sealed T CreateBaseSpread(ISpreadMeasure spreadMeasure)
        {
            if (SpreadMeasures.GetValidSpreadMeasure(spreadMeasure) is TSMeasure measure) return Create(measure);

            throw ArgumentTypeOutOfRangeException(nameof(spreadMeasure), spreadMeasure);
        }
        #endregion
        #endregion

        #region Abstract methods
        public abstract T Create(TSMeasure spreadMeasure);
        public abstract T CreateNew(T other);
        public abstract T? Create(IBaseSpread baseSpread);
        #endregion
        #endregion
    }

    public abstract class SpreadFactory<T, TSMeasure, TEnum> : SpreadFactory<T, TSMeasure>, ISpreadFactory<T, TSMeasure, TEnum>
        where T : class, ISpread
        where TSMeasure : class, IMeasure, ISpreadMeasure
        where TEnum : struct, Enum
    {
        #region Constructors
        private protected SpreadFactory(IMeasureFactory measureFactory) : base(measureFactory)
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

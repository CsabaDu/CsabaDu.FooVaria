using CsabaDu.FooVaria.Measurables.Factories;

namespace CsabaDu.FooVaria.Spreads.Factories.Implementations
{
    public abstract class SpreadFactory : ISpreadFactory
    {
        #region Constructors
        public SpreadFactory(IMeasureFactory measureFactory)
        {
            MeasureFactory = NullChecked(measureFactory, nameof(measureFactory));
        }
        #endregion

        #region Properties
        public IMeasureFactory MeasureFactory { get; init; }
        #endregion

        #region Public methods
        #region Abstract methods
        public abstract IBaseSpread Create(ISpreadMeasure spreadMeasure);
        public abstract ISpread Create(params IExtent[] shapeExtents);
        #endregion
        #endregion
    }

    public abstract class SpreadFactory<T, U> : SpreadFactory, ISpreadFactory<T, U> where T : class, ISpread where U : class, IMeasure, ISpreadMeasure
    {
        #region Constructors
        public SpreadFactory(IMeasureFactory measureFactory) : base(measureFactory)
        {
        }
        #endregion

        #region Public methods
        #region Override methods
        #region Sealed methods
        public override sealed T Create(ISpreadMeasure spreadMeasure)
        {
            if (SpreadMeasures.GetValidSpreadMeasure(spreadMeasure) is U measure) return Create(measure);

            throw ArgumentTypeOutOfRangeException(nameof(spreadMeasure), spreadMeasure);
        }
        #endregion
        #endregion

        #region Abstract methods
        public abstract T Create(U spreadMeasure);
        public abstract T Create(T other);
        #endregion
        #endregion
    }
}

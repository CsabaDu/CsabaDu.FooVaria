using CsabaDu.FooVaria.Measurables.Factories;

namespace CsabaDu.FooVaria.Spreads.Factories.Implementations
{
    public abstract class SpreadFactory : ISpreadFactory
    {
        #region Public methods
        #region Abstract methods
        public abstract IFactory GetMeasureFactory();
        #endregion
        #endregion
    }


    public abstract class SpreadFactory<T, U> : SpreadFactory, ISpreadFactory<T, U> where T : class, ISpread where U : class, IMeasure, ISpreadMeasure
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
        #region Override methods
        public override sealed IMeasureFactory GetMeasureFactory()
        {
            return MeasureFactory;
        }
        #endregion

        #region Abstract methods
        public abstract T Create(U spreadMeasure);
        public abstract T Create(T other);
        public abstract T Create(params IExtent[] shapeExtents);
        #endregion
        #endregion
    }
}

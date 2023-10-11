namespace CsabaDu.FooVaria.Common.Types.Implementations
{
    public abstract class BaseSpread : BaseMeasurable, IBaseSpread
    {
        protected BaseSpread(IBaseSpread other) : base(other)
        {
        }

        protected BaseSpread(IBaseSpreadFactory factory, IBaseMeasurable baseMeasurable) : base(factory, baseMeasurable)
        {
        }

        public override IBaseSpreadFactory GetFactory()
        {
            return (IBaseSpreadFactory)Factory;
        }

        public abstract ISpreadMeasure GetSpreadMeasure();
    }
}


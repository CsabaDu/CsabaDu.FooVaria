namespace CsabaDu.FooVaria.Common.Types.Implementations
{
    public abstract class Quantifiable : Measurable, IQuantifiable
    {
        protected Quantifiable(IQuantifiable other) : base(other)
        {
        }

        protected Quantifiable(IQuantifiableFactory factory, MeasureUnitTypeCode measureUnitTypeCode) : base(factory, measureUnitTypeCode)
        {
        }

        protected Quantifiable(IQuantifiableFactory factory, Enum measureUnit) : base(factory, measureUnit)
        {
        }

        protected Quantifiable(IQuantifiableFactory factory, IMeasurable measurable) : base(factory, measurable)
        {
        }

        protected Quantifiable(IQuantifiableFactory factory, MeasureUnitTypeCode measureUnitTypeCode, params IMeasurable[] measurables) : base(factory, measureUnitTypeCode, measurables)
        {
        }

        public abstract decimal GetDefaultQuantity();
        public abstract void ValidateQuantity(ValueType? quantity, string paramName);
    }
}


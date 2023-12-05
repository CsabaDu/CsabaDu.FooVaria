﻿namespace CsabaDu.FooVaria.Common.Types.Implementations
{
    public abstract class BaseMeasure : Quantifiable, IBaseMeasure
    {
        protected BaseMeasure(IBaseMeasureFactory factory, MeasureUnitTypeCode measureUnitTypeCode) : base(factory, measureUnitTypeCode)
        {
        }

        protected BaseMeasure(IBaseMeasureFactory factory, Enum measureUnit) : base(factory, measureUnit)
        {
        }

        protected BaseMeasure(IBaseMeasureFactory factory, IMeasurable measurable) : base(factory, measurable)
        {
        }

        protected BaseMeasure(IBaseMeasureFactory factory, MeasureUnitTypeCode measureUnitTypeCode, params IBaseMeasure[] baseMeasures) : base(factory, measureUnitTypeCode, baseMeasures)
        {
        }

        protected BaseMeasure(IBaseMeasure other) : base(other)
        {
        }

        public abstract decimal DefaultQuantity { get; init; }

        public override sealed decimal GetDefaultQuantity()
        {
            return DefaultQuantity;
        }

        public void ValidateQuantifiable(IQuantifiable? quantifiable, string paramName)
        {
            ValidateQuantity(NullChecked(quantifiable, paramName).GetDefaultQuantity(), paramName);
        }
    }

    public abstract class BaseMeasure<T, U> : BaseMeasure, IBaseMeasure<T, U> where T : class, IBaseMeasure<T, U> where U : notnull
    {
        protected BaseMeasure(T other) : base(other)
        {
        }

        protected BaseMeasure(IBaseMeasureFactory factory, MeasureUnitTypeCode measureUnitTypeCode) : base(factory, measureUnitTypeCode)
        {
        }

        protected BaseMeasure(IBaseMeasureFactory factory, Enum measureUnit) : base(factory, measureUnit)
        {
        }

        protected BaseMeasure(IBaseMeasureFactory factory, IMeasurable measurable) : base(factory, measurable)
        {
        }

        protected BaseMeasure(IBaseMeasureFactory factory, MeasureUnitTypeCode measureUnitTypeCode, params IBaseMeasure[] baseMeasures) : base(factory, measureUnitTypeCode, baseMeasures)
        {
        }

        public virtual int CompareTo(T? other)
        {
            if (other == null) return 1;

            other.ValidateMeasureUnitTypeCode(MeasureUnitTypeCode, nameof(other));

            return DefaultQuantity.CompareTo(other.DefaultQuantity);
        }

        public virtual bool Equals(T? other)
        {
            return MeasureUnitTypeCode == other?.MeasureUnitTypeCode
                && DefaultQuantity == other?.DefaultQuantity;
        }

        public virtual decimal ProportionalTo(T other)
        {
            if (NullChecked(other, nameof(other)).HasMeasureUnitTypeCode(MeasureUnitTypeCode)) return DefaultQuantity / other.DefaultQuantity;

            throw InvalidMeasureUnitTypeCodeEnumArgumentException(other.MeasureUnitTypeCode, nameof(other));
        }

        public override bool Equals(object? obj)
        {
            return obj is T other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(MeasureUnitTypeCode, DefaultQuantity);
        }

        public abstract T? ExchangeTo(U context);
        public abstract bool IsExchangeableTo(U? context);
    }
}


﻿namespace CsabaDu.FooVaria.Measurables.Types.Implementations;

internal abstract class Measurable : BaseMeasurable, IMeasurable
{
    #region Enums
    protected enum SummingMode
    {
        Add,
        Subtract,
    }
    #endregion

    #region Constructors
    private protected Measurable(IMeasurableFactory measurableFactory, MeasureUnitTypeCode measureUnitTypeCode) : base(measureUnitTypeCode)
    {
        MeasurableFactory = NullChecked(measurableFactory, nameof(measurableFactory));
    }

    private protected Measurable(IMeasurableFactory factory, Enum measureUnit) : base(measureUnit)
    {
        MeasurableFactory = NullChecked(factory, nameof(factory));
    }

    private protected Measurable(IMeasurableFactory measurableFactory, IMeasurable measurable) : base(measurable)
    {
        MeasurableFactory = NullChecked(measurableFactory, nameof(measurableFactory));
    }

    private protected Measurable(IMeasurable other) : base(other)
    {
        MeasurableFactory = other.MeasurableFactory;
    }
    #endregion

    #region Properties
    public IMeasurableFactory MeasurableFactory { get; init; }
    #endregion

    #region Public methods
    public override bool Equals(object? obj)
    {
        return obj is IMeasurable other
            && MeasureUnitTypeCode == other.MeasureUnitTypeCode
            && MeasurableFactory.Equals(other.MeasurableFactory);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(MeasureUnitTypeCode, MeasurableFactory);
    }

    public IMeasurable GetMeasurable(IMeasurable? measurable = null)
    {
        return MeasurableFactory.Create(measurable ?? this);
    }

    public IMeasurable GetMeasurable(IMeasurableFactory measurableFactory, IMeasurable measurable)
    {
        return NullChecked(measurableFactory, nameof(measurableFactory)).Create(measurable);
    }

    public virtual TypeCode GetQuantityTypeCode(MeasureUnitTypeCode? measureUnitTypeCode = null)
    {
        TypeCode? quantityTypeCode = (measureUnitTypeCode ?? MeasureUnitTypeCode).GetQuantityTypeCode();

        return quantityTypeCode ?? throw InvalidMeasureUnitTypeCodeEnumArgumentException(measureUnitTypeCode!.Value);
    }

    public override sealed IEnumerable<MeasureUnitTypeCode> GetMeasureUnitTypeCodes()
    {
        return base.GetMeasureUnitTypeCodes();
    }

    public override sealed void ValidateMeasureUnit(Enum measureUnit, MeasureUnitTypeCode? measureUnitTypeCode = null)
    {
        base.ValidateMeasureUnit(measureUnit, measureUnitTypeCode ?? MeasureUnitTypeCode);
    }

    public override sealed void ValidateMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode)
    {
        if (measureUnitTypeCode == MeasureUnitTypeCode) return;

        throw InvalidMeasureUnitTypeCodeEnumArgumentException(measureUnitTypeCode);
    }

    #region Abstract methods
    public abstract IMeasurable GetDefault();
    #endregion
    #endregion

    #region Protected methods
    protected static IMeasure GetSum(IMeasure measure, IMeasure? other, SummingMode summingMode)
    {
        if (other == null) return measure.GetMeasure();

        if (other.IsExchangeableTo(measure.MeasureUnitTypeCode))
        {
            decimal quantity = getDefaultQuantitySum() / measure.GetExchangeRate();

            return measure.GetMeasure(quantity);
        }

        throw new ArgumentOutOfRangeException(nameof(other), other.MeasureUnitTypeCode, null);

        #region Local methods
        decimal getDefaultQuantitySum()
        {
            decimal thisQuantity = measure.DefaultQuantity;
            decimal otherQuantity = other!.DefaultQuantity;

            return summingMode switch
            {
                SummingMode.Add => decimal.Add(thisQuantity, otherQuantity),
                SummingMode.Subtract => decimal.Subtract(thisQuantity, otherQuantity),

                _ => throw new InvalidOperationException(null),
            };
        }
        #endregion
    }
    #endregion
}

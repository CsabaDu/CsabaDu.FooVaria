namespace CsabaDu.FooVaria.BaseTypes.Spreads.Types.Implementations;

/// <summary>
/// Represents an abstract base class for Spread, which is a type of Quantifiable object.
/// </summary>
/// <param name="rootObject">The root object associated with the Spread.</param>
/// <param name="paramName">The parameter name associated with the Spread.</param>
public abstract class Spread(IRootObject rootObject, string paramName) : Quantifiable(rootObject, paramName), ISpread
{
    #region Public methods
    #region Override methods
    #region Sealed methods
    /// <summary>
    /// Gets the base quantity of the Spread.
    /// </summary>
    /// <returns>The base quantity as a ValueType.</returns>
    public override sealed ValueType GetBaseQuantity()
    {
        return GetQuantity();
    }

    /// <summary>
    /// Gets the default quantity of the Spread.
    /// </summary>
    /// <returns>The default quantity as a decimal.</returns>
    public override sealed decimal GetDefaultQuantity()
    {
        return (GetSpreadMeasure() as IBaseQuantifiable)!.GetDefaultQuantity();
    }

    /// <summary>
    /// Gets the type code of the quantity.
    /// </summary>
    /// <returns>The type code of the quantity.</returns>
    public override sealed TypeCode GetQuantityTypeCode()
    {
        return base.GetQuantityTypeCode();
    }

    /// <summary>
    /// Determines if the Spread is exchangeable to the specified context.
    /// </summary>
    /// <param name="context">The context to check against.</param>
    /// <returns>True if exchangeable, otherwise false.</returns>
    public override sealed bool IsExchangeableTo(Enum? context)
    {
        return (GetSpreadMeasure() as IBaseMeasure)!.IsExchangeableTo(context);
    }

    /// <summary>
    /// Rounds the Spread to the specified rounding mode.
    /// </summary>
    /// <param name="roundingMode">The rounding mode to use.</param>
    /// <returns>The rounded Spread.</returns>
    public override sealed ISpread Round(RoundingMode roundingMode)
    {
        double quantity = GetQuantity().Round(roundingMode);
        Enum measureUnit = GetBaseMeasureUnit();
        ISpreadFactory factory = GetSpreadFactory();
        ISpreadMeasure spreadMeasure = factory.CreateSpreadMeasure(measureUnit, quantity)!;

        return GetSpread(spreadMeasure);
    }
    #endregion

    /// <summary>
    /// Gets the base measure unit of the Spread.
    /// </summary>
    /// <returns>The base measure unit as an Enum.</returns>
    public override Enum GetBaseMeasureUnit()
    {
        ISpreadMeasure spreadMeasure = GetSpreadMeasure();

        return spreadMeasure.GetBaseMeasureUnit();
    }
    #endregion

    #region Virtual methods
    /// <summary>
    /// Gets the quantity of the Spread.
    /// </summary>
    /// <returns>The quantity as a double.</returns>
    public virtual double GetQuantity()
    {
        return GetSpreadMeasure().GetQuantity();
    }
    #endregion

    #region Abstract methods
    /// <summary>
    /// Gets the spread measure of the Spread.
    /// </summary>
    /// <returns>The spread measure.</returns>
    public abstract ISpreadMeasure GetSpreadMeasure();
    #endregion

    /// <summary>
    /// Gets a new Spread based on the specified spread measure.
    /// </summary>
    /// <param name="spreadMeasure">The spread measure to use.</param>
    /// <returns>The new Spread.</returns>
    public ISpread GetSpread(ISpreadMeasure spreadMeasure)
    {
        ValidateSpreadMeasure(spreadMeasure, nameof(spreadMeasure));

        ISpreadFactory factory = GetSpreadFactory();

        return factory.CreateSpread(NullChecked(spreadMeasure, nameof(spreadMeasure)));
    }

    /// <summary>
    /// Gets the spread measure based on the specified quantifiable.
    /// </summary>
    /// <param name="quantifiable">The quantifiable to use.</param>
    /// <returns>The spread measure if valid, otherwise null.</returns>
    public ISpreadMeasure? GetSpreadMeasure(IQuantifiable? quantifiable)
    {
        return quantifiable is ISpreadMeasure spreadMeasure
            && getSpreadMeasure() is IBaseMeasure
            && IsExchangeableTo(spreadMeasure.GetBaseMeasureUnit())
            && spreadMeasure.GetQuantity() > 0 ?
            getSpreadMeasure()
            : null;

        #region Local methods
        ISpreadMeasure getSpreadMeasure()
        {
            return spreadMeasure.GetSpreadMeasure();
        }
        #endregion
    }

    /// <summary>
    /// Validates the specified spread measure.
    /// </summary>
    /// <param name="spreadMeasure">The spread measure to validate.</param>
    /// <param name="paramName">The parameter name associated with the spread measure.</param>
    /// <exception cref="ArgumentTypeOutOfRangeException">Thrown if the spread measure is not valid.</exception>
    /// <exception cref="InvalidMeasureUnitCodeEnumArgumentException">Thrown if the measure unit code is not valid.</exception>
    /// <exception cref="QuantityArgumentOutOfRangeException">Thrown if the quantity is not valid.</exception>
    public void ValidateSpreadMeasure(ISpreadMeasure? spreadMeasure, string paramName)
    {
        if (NullChecked(spreadMeasure, paramName).GetSpreadMeasure() is not IBaseMeasure baseMeasure)
        {
            throw ArgumentTypeOutOfRangeException(paramName, spreadMeasure!);
        }

        MeasureUnitCode measureUnitCode = baseMeasure.GetMeasureUnitCode();

        if (!IsExchangeableTo(measureUnitCode))
        {
            throw InvalidMeasureUnitCodeEnumArgumentException(measureUnitCode, paramName);
        }

        double quantity = spreadMeasure!.GetQuantity();

        if (quantity > 0) return;

        throw QuantityArgumentOutOfRangeException(paramName, quantity);
    }
    #endregion

    #region Private methods
    /// <summary>
    /// Gets the spread factory associated with the Spread.
    /// </summary>
    /// <returns>The spread factory.</returns>
    private ISpreadFactory GetSpreadFactory()
    {
        return (ISpreadFactory)GetFactory();
    }
    #endregion
}

using CsabaDu.FooVaria.Measurables.Types.Implementations;

namespace CsabaDu.FooVaria.Measurables.Factories.Implementations;

public abstract class RateFactory : Factory, IRateFactory
{
    #region Constructors
    protected RateFactory(IDenominatorFactory denominatorFactory)
    {
        DenominatorFactory = NullChecked(denominatorFactory, nameof(denominatorFactory));
    }
    #endregion

    #region Properties
    public IDenominatorFactory DenominatorFactory { get; init; }
    #endregion

    #region Public methods
    public IRate Create(IRateFactory rateFactory, IRate rate)
    {
        return rateFactory switch
        {
            FlatRateFactory flatRateFactory => CreateFlatRate(flatRateFactory, rate),
            LimitedRateFactory limitedRateFactory => CreateLimitedRate(limitedRateFactory, rate, rate?.GetLimit()),

            _ => throw new InvalidOperationException(null),
        };
    }
    #endregion

    #region Protected methods
    protected static IFlatRate CreateFlatRate(IFlatRateFactory flatRateFactory, IRate rate)
    {
        if (rate is IFlatRate flatRate) return CreateFlatRate(flatRate);

        var (numerator, denominator) = GetRateParams(rate);

        return CreateFlatRate(flatRateFactory, numerator, denominator);
    }

    protected static IFlatRate CreateFlatRate(IFlatRateFactory flatRateFactory, IMeasure numerator, IDenominator denominator)
    {
        return new FlatRate(flatRateFactory, numerator, denominator);
    }

    protected static IFlatRate CreateLimitedRate(ILimitedRateFactory limitedRateFactory, IRate rate, ILimit? limit)
    {
        if (rate is IFlatRate limitedRate) return CreateLimitedRate(limitedRate, limit);

        var (numerator, denominator) = GetRateParams(rate);

        return CreateLimitedRate(limitedRateFactory, numerator, denominator, limit ?? rate.GetLimit());
    }

    protected static IFlatRate CreateLimitedRate(ILimitedRateFactory limitedRateFactory, IMeasure numerator, IDenominator denominator, ILimit? limit)
    {
        return new LimitedRate(limitedRateFactory, numerator, denominator, limit);
    }
    #endregion

    #region Private methods
    private static (IMeasure, IDenominator) GetRateParams(IRate rate)
    {
        IMeasure numerator = NullChecked(rate, nameof(rate)).Numerator;
        IDenominator denominator = rate.Denominator;

        return (numerator, denominator);
    }
    #endregion
}

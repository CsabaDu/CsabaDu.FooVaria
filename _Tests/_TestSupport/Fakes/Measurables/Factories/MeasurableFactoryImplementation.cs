namespace CsabaDu.FooVaria.TestSupport.Fakes.Measurables.Factories;

internal class MeasurableFactoryImplementation : IMeasurableFactory
{
    public IMeasurable Create(IMeasurable other)
    {
        throw new NotImplementedException();

    //    return other.GetFactory() switch
    //    {
    //        MeasurementFactory factory => create(factory),
    //        DenominatorFactory factory => create(factory),
    //        MeasureFactory factory => create(factory),
    //        LimitFactory factory => create(factory),
    //        FlatRateFactory factory => create(factory),
    //        LimitedRateFactory factory => create(factory),

    //        _ => throw new InvalidOperationException(null),
    //    };

    //    #region Local methods
    //    IMeasurable create<T>(T factory) where T : class, IMeasurableFactory
    //    {
    //        return factory.Create(other);
    //    }
    //    #endregion
    }
}

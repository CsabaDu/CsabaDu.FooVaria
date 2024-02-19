using CsabaDu.FooVaria.BaseTypes.Quantifiables.Types;

namespace CsabaDu.FooVaria.BaseTypes.Quantifiables.Factories
{
    //public interface IBaseQuantifiableFactory : IMeasurableFactory
    //{
    //}

    public interface IQuantifiableFactory : IBaseQuantifiableFactory
    {
        IQuantifiable CreateQuantifiable(MeasureUnitCode measureUnitCode, decimal defaultQuantity);
    }
}

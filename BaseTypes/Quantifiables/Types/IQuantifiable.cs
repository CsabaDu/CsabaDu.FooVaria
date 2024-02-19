using CsabaDu.FooVaria.BaseTypes.Quantifiables.Behaviors;

namespace CsabaDu.FooVaria.BaseTypes.Quantifiables.Types
{
    //public interface IBaseQuantifiable : IMeasurable, IDefaultQuantity
    //{
    //    void ValidateQuantity(ValueType? quantity, string paramName);
    //    void ValidateQuantity(IBaseQuantifiable? baseQuantifiable, string paramName);
    //}

    public interface IQuantifiable : IBaseQuantifiable, IExchange<IQuantifiable, Enum>, IFit<IQuantifiable>, IRound<IQuantifiable>
    {
        //MeasureUnitCode MeasureUnitCode { get; init; }

        IQuantifiable GetQuantifiable(MeasureUnitCode measureUnitCode, decimal defaultQuantity);

        void ValidateQuantifiable(IBaseQuantifiable? baseQuantifiable, string paramName);
    }
}

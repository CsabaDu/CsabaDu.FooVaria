namespace CsabaDu.FooVaria.Common.Types;

public interface IBaseSpread : IBaseMeasurable, ISpreadMeasure, IExchange<IBaseSpread, Enum>, IFit<IBaseSpread>
{
    IBaseSpread GetBaseSpread();
    IBaseSpread GetBaseSpread(ISpreadMeasure spreadMeasure);

    void ValidateSpreadMeasure(ISpreadMeasure? spreadMeasure);
}


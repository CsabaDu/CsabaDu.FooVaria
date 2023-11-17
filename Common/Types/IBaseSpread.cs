namespace CsabaDu.FooVaria.Common.Types;

public interface IBaseSpread : IBaseMeasurable, IQuantifiable, ISpreadMeasure, IExchange<IBaseSpread, Enum>, IFit<IBaseSpread>
{
    IBaseSpread GetBaseSpread(ISpreadMeasure spreadMeasure);

    //void ValidateSpreadMeasure(ISpreadMeasure? spreadMeasure, string paramName);
}


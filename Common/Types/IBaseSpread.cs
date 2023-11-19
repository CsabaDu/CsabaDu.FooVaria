namespace CsabaDu.FooVaria.Common.Types;

public interface IBaseSpread : IMeasurable/*, IQuantifiable<double>*/, ISpreadMeasure, IExchange<IBaseSpread, Enum>, IFit<IBaseSpread>
{
    IBaseSpread GetBaseSpread(ISpreadMeasure spreadMeasure);

    //void ValidateSpreadMeasure(ISpreadMeasure? spreadMeasure, string paramName);
}


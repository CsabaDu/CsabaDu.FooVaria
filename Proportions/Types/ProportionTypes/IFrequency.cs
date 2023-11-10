using CsabaDu.FooVaria.Measurables.Types.MeasureTypes;

namespace CsabaDu.FooVaria.Proportions.Types.ProportionTypes;

public interface IFrequency : IProportion<IFrequency, Pieces, TimePeriodUnit>, IMeasuresProportion<IFrequency, IPieceCount, ITimePeriod>
{
}
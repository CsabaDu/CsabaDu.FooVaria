namespace CsabaDu.FooVaria.Common.Factories;

public interface IBaseSpreadFactory : IFactory
{
    IFactory GetMeasureFactory();
}
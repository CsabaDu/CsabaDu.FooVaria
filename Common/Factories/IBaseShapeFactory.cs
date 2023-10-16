namespace CsabaDu.FooVaria.Common.Factories;

public interface IBaseShapeFactory : IBaseSpreadFactory
{
    IFactory GetSpreadFactory();
}

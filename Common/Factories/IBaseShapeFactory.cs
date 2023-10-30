namespace CsabaDu.FooVaria.Common.Factories;

public interface IBaseShapeFactory : IBaseSpreadFactory
{
    IBaseSpreadFactory GetSpreadFactory();
}

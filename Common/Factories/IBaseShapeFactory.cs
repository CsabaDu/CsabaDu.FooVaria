namespace CsabaDu.FooVaria.Common.Factories;

public interface IBaseShapeFactory : IBaseSpreadFactory
{
    IBaseShape Create(params IQuantifiable[] shapeComponents);
}

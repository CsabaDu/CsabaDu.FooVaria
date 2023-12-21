namespace CsabaDu.FooVaria.Common.Factories;

public interface IBaseShapeFactory : IBaseSpreadFactory
{
    IBaseShape CreateBaseShape(params IQuantifiable[] shapeComponents);
}

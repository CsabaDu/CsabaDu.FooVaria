namespace CsabaDu.FooVaria.AbstractTypes.ComplexShapes.Factories;

public interface IComplexShapeFactory : IShapeFactory
{
    ISimpleShapeFactory SimpleShapeFactory { get; init; }
}

namespace CsabaDu.FooVaria.AbstractTypes.SimpleShapes.Behaviors;

public interface IRadius
{
    IExtent GetRadius();
    IExtent GetRadius(ExtentUnit extentUnit);
}

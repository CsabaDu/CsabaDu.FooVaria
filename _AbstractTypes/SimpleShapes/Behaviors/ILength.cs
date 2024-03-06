namespace CsabaDu.FooVaria.AbstractTypes.SimpleShapes.Behaviors;

public interface ILength
{
    IExtent GetLength();
    IExtent GetLength(ExtentUnit extentUnit);
}

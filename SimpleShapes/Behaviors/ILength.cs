namespace CsabaDu.FooVaria.SimpleShapes.Behaviors;

public interface ILength
{
    IExtent GetLength();
    IExtent GetLength(ExtentUnit extentUnit);
}

namespace CsabaDu.FooVaria.Shapes.Behaviors;

public interface ILength
{
    IExtent GetLength();
    IExtent GetLength(ExtentUnit extentUnit);
}

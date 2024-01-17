namespace CsabaDu.FooVaria.Shapes.Behaviors;

public interface IBaseFace
{
    IPlaneShape GetBaseFace();
    IPlaneShape GetBaseFace(ExtentUnit extentUnit);

    void ValidateBaseFace(IPlaneShape planeShape, string paramName);
}

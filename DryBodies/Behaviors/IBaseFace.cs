namespace CsabaDu.FooVaria.DryBodies.Behaviors;

public interface IBaseFace
{
    IPlaneShape GetBaseFace();
    IPlaneShape GetBaseFace(ExtentUnit extentUnit);

    void ValidateBaseFace(IPlaneShape planeShape, [DisallowNull] string paramName);
}

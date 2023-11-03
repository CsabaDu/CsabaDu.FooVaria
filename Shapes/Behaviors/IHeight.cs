using CsabaDu.FooVaria.Shapes.Types;

namespace CsabaDu.FooVaria.Shapes.Behaviors
{
    public interface IHeight
    {
        IExtent GetHeight();
        IExtent GetHeight(ExtentUnit extentUnit);
    }
}

namespace CsabaDu.FooVaria.Common.Types.Implementations;

public abstract class BaseShape : BaseSpread, IBaseShape
{
    protected BaseShape(IBaseShape other) : base(other)
    {
    }

    protected BaseShape(IBaseShapeFactory factory, IBaseShape baseShape) : base(factory, baseShape)
    {
    }
}



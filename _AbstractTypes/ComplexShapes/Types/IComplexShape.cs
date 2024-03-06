using CsabaDu.FooVaria.BaseTypes.BaseQuantifiables.Behaviors;

namespace CsabaDu.FooVaria.AbstractTypes.ComplexShapes.Types;

public interface IComplexShape : IShape
{
    ISimpleShape GetBaseShape();
}

using CsabaDu.FooVaria.BaseTypes.Shapes.Types;

namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.Returns;

public class ShapeReturn : SpreadReturn
{
    public  IEnumerable<IShapeComponent> GetShapeComponents {  get; set; }
    public IShape GetBaseShape {  get; set; }
}

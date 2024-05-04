namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.Returns;

public class ShapeReturn : SpreadReturn
{
    public  IEnumerable<IShapeComponent> GetShapeComponentsValue {  get; set; }
    public IShape GetBaseShapeValue {  get; set; }
}

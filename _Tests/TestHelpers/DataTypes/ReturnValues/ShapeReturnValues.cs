namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ReturnValues;

public class ShapeReturnValues : SpreadReturnValues
{
    public  IEnumerable<IShapeComponent> GetShapeComponentsValue {  get; set; }
    public IShape GetBaseShapeValue {  get; set; }
}

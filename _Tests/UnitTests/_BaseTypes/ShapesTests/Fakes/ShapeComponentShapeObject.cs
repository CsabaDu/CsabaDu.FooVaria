namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.ShapesTests.Fakes;

internal sealed class ShapeComponentShapeObject(IRootObject rootObject, string paramName) : ShapeChild(rootObject, paramName), IShapeComponent;

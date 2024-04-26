namespace CsabaDu.FooVaria.Tests.TestHelpers.Fakes.BaseTypes.Shapes;

public sealed class LimiterShapeObject(IRootObject rootObject, string paramName) : ShapeChild(rootObject, paramName), ILimiter
{
    public static LimiterShapeObject GetLimiterShapeObject(LimitMode limitMode, IShapeComponent shapeComponent, IShapeFactory factory = null)
    {
        IShape baseShape = GetShapeChild(shapeComponent);

        return new(Fields.RootObject, Fields.paramName)
        {
            Return = GetReturn(shapeComponent, baseShape, factory),
            SpreadMeasure = GetSpreadMeasure(shapeComponent),
            LimiterObject = new()
            {
                LimitMode = limitMode,
            },
        };
    }

    private LimiterObject LimiterObject { get; set; }

    public LimitMode? GetLimitMode() => LimiterObject.GetLimitMode();
}

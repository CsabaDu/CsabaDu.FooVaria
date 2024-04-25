namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.ShapesTests;

internal sealed class DynamicDataSource : CommonDynamicDataSource
{
    private IShape shape;
    internal IEnumerable<object[]> GetEqualsArg()
    {
        testCase = "null => false";
        isTrue = false;
        measureUnit = RandomParams.GetRandomSpreadMeasureUnit();
        measureUnitCode = GetMeasureUnitCode();
        defaultQuantity = RandomParams.GetRandomPositiveDecimal();
        shape = null;
        yield return toObjectArray();

        testCase = "Same shape => true";
        isTrue = true;
        shape = GetShapeChild(this);
        yield return toObjectArray();

        testCase = "Different MeasureUnitCode => false";
        isTrue = false;
        measureUnitCode = SampleParams.GetOtherSpreadMeasureUnitCode(measureUnitCode);
        context = measureUnitCode.GetDefaultMeasureUnit();
        measureUnit = RandomParams.GetRandomMeasureUnit(measureUnitCode, context);
        yield return toObjectArray();

        testCase = "Same MeasureUnitCode, different measureUnit => false";
        shape = GetShapeChild(this);
        measureUnit = RandomParams.GetRandomMeasureUnit(measureUnitCode, measureUnit);
        yield return toObjectArray();

        testCase = "Different quantity => false";
        shape = GetShapeChild(this);
        defaultQuantity = RandomParams.GetRandomPositiveDecimal(defaultQuantity);
        yield return toObjectArray();

        testCase = "Equals when exchanged => true";
        isTrue = true;
        defaultQuantity *= GetExchangeRate();
        measureUnit = RandomParams.GetRandomSameTypeValidMeasureUnit(measureUnit);
        defaultQuantity /= GetExchangeRate();
        shape = GetShapeChild(this);
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            TestCase_Enum_decimal_bool_IShape args = new(testCase, measureUnit, defaultQuantity, isTrue, shape);

            return args.ToObjectArray();
        }
        #endregion
    }
}
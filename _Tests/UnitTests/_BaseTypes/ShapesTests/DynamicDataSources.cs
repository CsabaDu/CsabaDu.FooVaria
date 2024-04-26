using CsabaDu.FooVaria.BaseTypes.BaseMeasures.Types.Implementations;

namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.ShapesTests;

internal sealed class DynamicDataSource : CommonDynamicDataSource
{
    private IShape shape;
    private IShape other;

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

    internal IEnumerable<object[]> GetEqualsArgs()
    {
        testCase = "null, null => true";
        isTrue = true;
        shape = null;
        other = null;
        yield return toObjectArray();

        testCase = "not null, null => false";
        isTrue = false;
        measureUnit = RandomParams.GetRandomSpreadMeasureUnit();
        measureUnitCode = GetMeasureUnitCode();
        defaultQuantity = RandomParams.GetRandomPositiveDecimal();
        shape = GetShapeChild(this);
        yield return toObjectArray();

        testCase = "null, not null => false";
        shape = null;
        other = GetCompleteShapeChild(this);
        yield return toObjectArray();

        testCase = "Same shapes, same baseShapes => true";
        isTrue = true;
        measureUnitCode = SampleParams.GetOtherSpreadMeasureUnitCode(measureUnitCode);
        measureUnit = RandomParams.GetRandomMeasureUnit(measureUnitCode);
        shape = other
            = GetShapeChild(this, other);
        yield return toObjectArray();

        testCase = "Different shapes, same baseShapes => true";
        measureUnitCode = SampleParams.GetOtherSpreadMeasureUnitCode(GetMeasureUnitCode());
        measureUnit = RandomParams.GetRandomMeasureUnit(measureUnitCode);
        defaultQuantity = RandomParams.GetRandomPositiveDecimal(defaultQuantity);
        other = GetShapeChild(this, shape.GetBaseShape());
        yield return toObjectArray();

        testCase = "Different baseShapes => false";
        isTrue = false;
        shape = GetShapeChild(this, other.GetBaseShape());
        other = GetShapeChild(this, other);
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            TestCase_IShape_bool_IShape args = new(testCase, shape, isTrue, other);

            return args.ToObjectArray();
        }
        #endregion
    }
}
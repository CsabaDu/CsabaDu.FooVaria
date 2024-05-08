namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.ShapesTests;

internal sealed class DynamicDataSource : CommonDynamicDataSource
{
    #region Fields
    private IShape shape;
    private IShape other;
    private IShapeComponent shapeComponent;
    private IQuantifiable quantifiable;
    #endregion

    #region Methods
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
        measureUnit = RandomParams.GetRandomMeasureUnit(measureUnitCode);
        shape = other = GetShapeChild(this, other);
        yield return toObjectArray();

        testCase = "Different shapes, same baseShapes => true";
        measureUnit = RandomParams.GetRandomMeasureUnit(measureUnitCode);
        defaultQuantity = RandomParams.GetRandomPositiveDecimal(defaultQuantity);
        other = GetShapeChild(this, shape);
        yield return toObjectArray();

        testCase = "Different baseShapes => false";
        isTrue = false;
        shapeComponent = shape.GetShapeComponents().First();
        defaultQuantity = RandomParams.GetRandomPositiveDecimal(defaultQuantity);
        other = GetShapeChild(shapeComponent, GetShapeChild(this));
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            TestCase_IShape_bool_IShape args = new(testCase, shape, isTrue, other);

            return args.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetFitsInILimiterArgs()
    {
        testCase = "Not IShape";
        measureUnit = RandomParams.GetRandomSpreadMeasureUnit();
        limiter = new LimiterObject();
        yield return toObjectArray();

        testCase = "Different MeasureUnitCode";
        measureUnitCode = SampleParams.GetOtherSpreadMeasureUnitCode(GetMeasureUnitCode());
        limitMode = RandomParams.GetRandomLimitMode();
        limiter = GetLimiterQuantifiableObject(limitMode.Value, RandomParams.GetRandomMeasureUnit(measureUnitCode), defaultQuantity);
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            TestCase_Enum_ILimiter args = new(testCase, measureUnit, limiter);

            return args.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetFitsInIShapeLimitModeArgs()
    {
        testCase = "IShape, Not defined LimitMode";
        limitMode = SampleParams.NotDefinedLimitMode;
        measureUnit = RandomParams.GetRandomSpreadMeasureUnit();
        defaultQuantity = RandomParams.GetRandomPositiveDecimal();
        shape = GetShapeChild(this);
        yield return toObjectArray();

        testCase = "Different IShape, valid LimitMode";
        measureUnitCode = GetMeasureUnitCode();
        measureUnitCode = SampleParams.GetOtherSpreadMeasureUnitCode(measureUnitCode);
        measureUnit = RandomParams.GetRandomMeasureUnit(measureUnitCode);
        limitMode = RandomParams.GetRandomLimitMode();
        yield return toObjectArray();

        testCase = "null, valid LimitMode";
        shape = null;
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            TestCase_Enum_LimitMode_IShape args = new(testCase, measureUnit, limitMode, shape);

            return args.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetGetValidShapeComponentArgs()
    {
        testCase = "null => null";
        quantifiable = null;
        shapeComponent = null;
        yield return toObjectArray();

        testCase = "Not IShapeComponent IQuantifiable => null";
        measureUnit = RandomParams.GetRandomSpreadMeasureUnit();
        defaultQuantity = RandomParams.GetRandomPositiveDecimal();
        quantifiable = GetQuantifiableChild(this);
        shapeComponent = null;
        yield return toObjectArray();

        testCase = "IShapeComponent => same IShapeComponent";
        var shapeComponentQuantifiableObject = GetShapeComponentQuantifiableObject(this);
        quantifiable = shapeComponentQuantifiableObject;
        shapeComponent = shapeComponentQuantifiableObject;
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            TestCase_IQuantifiable_IShapeComponent args = new(testCase, quantifiable, shapeComponent);

            return args.ToObjectArray();
        }
        #endregion
    }

    public new IEnumerable<object[]> GetHasMeasureUnitCodeArgs()
    {
        testCase = "Shape MeasureUnitCode => true";
        isTrue = true;
        measureUnit = RandomParams.GetRandomSpreadMeasureUnit();
        measureUnitCode = GetMeasureUnitCode();
        context = RandomParams.GetRandomSpreadMeasureUnit(measureUnitCode);
        yield return toObjectArray();

        testCase = "BaseShape MeasureUnitCode => true";
        measureUnitCode = Measurable.GetMeasureUnitCode(context);
        yield return toObjectArray();

        testCase = "Different MeasureUnitCode => false";
        isTrue= false;
        measureUnitCode = RandomParams.GetRandomMeasureUnitCode([measureUnitCode, GetMeasureUnitCode()]);
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            TestCase_Enum_MeasureUnitCode_bool_Enum args = new(testCase, measureUnit, measureUnitCode, isTrue, context);

            return args.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> ValidateShapeComponent()
    {
        testCase = "null => null";
        quantifiable = null;
        yield return toObjectArray();

        testCase = "Not IShapeComponent IQuantifiable";
        measureUnit = RandomParams.GetRandomSpreadMeasureUnit();
        defaultQuantity = RandomParams.GetRandomPositiveDecimal();
        quantifiable = GetQuantifiableChild(this);
        yield return toObjectArray();

        testCase = "IShapeComponent => same IShapeComponent";
        quantifiable = GetShapeComponentQuantifiableObject(this);
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            TestCase_IQuantifiable args = new(testCase, quantifiable);

            return args.ToObjectArray();
        }
        #endregion
    }
    #endregion
}
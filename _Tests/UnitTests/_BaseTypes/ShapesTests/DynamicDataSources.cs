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
        yield return argsToObjectArray();

        testCase = "Same shape => true";
        isTrue = true;
        shape = GetShapeChild(this);
        yield return argsToObjectArray();

        testCase = "Different MeasureUnitCode => false";
        isTrue = false;
        measureUnitCode = SampleParams.GetOtherSpreadMeasureUnitCode(measureUnitCode);
        context = measureUnitCode.GetDefaultMeasureUnit();
        measureUnit = RandomParams.GetRandomMeasureUnit(measureUnitCode, context);
        yield return argsToObjectArray();

        testCase = "Same MeasureUnitCode, different measureUnit => false";
        shape = GetShapeChild(this);
        measureUnit = RandomParams.GetRandomMeasureUnit(measureUnitCode, measureUnit);
        yield return argsToObjectArray();

        testCase = "Different quantity => false";
        shape = GetShapeChild(this);
        defaultQuantity = RandomParams.GetRandomPositiveDecimal(defaultQuantity);
        yield return argsToObjectArray();

        testCase = "Equals when exchanged => true";
        isTrue = true;
        defaultQuantity *= GetExchangeRate();
        measureUnit = RandomParams.GetRandomSameTypeValidMeasureUnit(measureUnit);
        defaultQuantity /= GetExchangeRate();
        shape = GetShapeChild(this);
        yield return argsToObjectArray();

        #region argsToObjectArray method
        object[] argsToObjectArray()
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
        yield return argsToObjectArray();

        testCase = "not null, null => false";
        isTrue = false;
        measureUnit = RandomParams.GetRandomSpreadMeasureUnit();
        measureUnitCode = GetMeasureUnitCode();
        defaultQuantity = RandomParams.GetRandomPositiveDecimal();
        shape = GetShapeChild(this);
        yield return argsToObjectArray();

        testCase = "null, not null => false";
        shape = null;
        other = GetCompleteShapeChild(this);
        yield return argsToObjectArray();

        testCase = "Same shapes, same baseShapes => true";
        isTrue = true;
        measureUnit = RandomParams.GetRandomMeasureUnit(measureUnitCode);
        shape = other = GetShapeChild(this, other);
        yield return argsToObjectArray();

        testCase = "Different shapes, same baseShapes => true";
        measureUnit = RandomParams.GetRandomMeasureUnit(measureUnitCode);
        defaultQuantity = RandomParams.GetRandomPositiveDecimal(defaultQuantity);
        other = GetShapeChild(this, shape);
        yield return argsToObjectArray();

        testCase = "Different baseShapes => false";
        isTrue = false;
        shapeComponent = shape.GetShapeComponents().First();
        defaultQuantity = RandomParams.GetRandomPositiveDecimal(defaultQuantity);
        other = GetShapeChild(shapeComponent, GetShapeChild(this));
        yield return argsToObjectArray();

        #region argsToObjectArray method
        object[] argsToObjectArray()
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
        yield return argsToObjectArray();

        testCase = "Different MeasureUnitCode";
        measureUnitCode = SampleParams.GetOtherSpreadMeasureUnitCode(GetMeasureUnitCode());
        limitMode = RandomParams.GetRandomLimitMode();
        limiter = GetLimiterQuantifiableObject(limitMode.Value, RandomParams.GetRandomMeasureUnit(measureUnitCode), defaultQuantity);
        yield return argsToObjectArray();

        #region argsToObjectArray method
        object[] argsToObjectArray()
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
        yield return argsToObjectArray();

        testCase = "Different IShape, valid LimitMode";
        measureUnitCode = GetMeasureUnitCode();
        measureUnitCode = SampleParams.GetOtherSpreadMeasureUnitCode(measureUnitCode);
        measureUnit = RandomParams.GetRandomMeasureUnit(measureUnitCode);
        limitMode = RandomParams.GetRandomLimitMode();
        yield return argsToObjectArray();

        testCase = "null, valid LimitMode";
        shape = null;
        yield return argsToObjectArray();

        #region argsToObjectArray method
        object[] argsToObjectArray()
        {
            TestCase_Enum_LimitMode_IShape args = new(testCase, measureUnit, limitMode, shape);

            return args.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetValidateMeasureUnitInvalidArgs()
    {
        MeasureUnitCode spreadMeasureUnitCode = RandomParams.GetRandomSpreadMeasureUnitCode();
        measureUnit = RandomParams.GetRandomSpreadMeasureUnit(spreadMeasureUnitCode);
        Enum otherMeasureUnit = RandomParams.GetRandomSpreadMeasureUnit(GetMeasureUnitCode());

        testCase = "Different MeassureUnit";
        context = getRandomDifferentMeasureUnit();
        yield return argsToObjectArray();

        testCase = "Not defined MeassureUnit of ShapeComponent";
        context = SampleParams.GetNotDefinedMeasureUnit(GetMeasureUnitCode());
        yield return argsToObjectArray();

        testCase = "Not defined MeassureUnit of BaseShape";
        context = SampleParams.GetNotDefinedMeasureUnit(spreadMeasureUnitCode);
        yield return argsToObjectArray();

        #region argsToObjectArray method
        object[] argsToObjectArray()
        {
            TestCase_Enum_Enum_Enum args = new(testCase, measureUnit, context, otherMeasureUnit);

            return args.ToObjectArray();
        }
        #endregion

        #region Local methods
        Enum getRandomDifferentMeasureUnit()
        {
            Enum measureUnit = RandomParams.GetRandomMeasureUnit(getDifferentMeasureUnitCode());

            while (GetMeasureUnitCode() == MeasureUnitCode.ExtentUnit)
            {
                measureUnit = RandomParams.GetRandomMeasureUnit(getDifferentMeasureUnitCode());
            }

            return measureUnit;
        }

        MeasureUnitCode getDifferentMeasureUnitCode()
        {
            return RandomParams.GetRandomMeasureUnitCode(GetMeasureUnitCode());
        }
        #endregion
    }

    internal IEnumerable<object[]> GetShapeValidateMeasureUnitValidArgs()
    {
        measureUnit = RandomParams.GetRandomSpreadMeasureUnit();
        
        testCase = "Defined MeassureUnit of ShapeComponent";
        context = RandomParams.GetRandomMeasureUnit(GetMeasureUnitCode());
        yield return argsToObjectArray();

        testCase = "Defined MeassureUnit of BaseShape";
        context = RandomParams.GetRandomMeasureUnit(Measurable.GetMeasureUnitCode(context));
        yield return argsToObjectArray();

        #region argsToObjectArray method
        object[] argsToObjectArray()
        {
            TestCase_Enum_Enum args = new(testCase, measureUnit, context);

            return args.ToObjectArray();
        }
        #endregion

    }
    internal IEnumerable<object[]> GetShapeHasMeasureUnitCodeArgs()
    {
        testCase = "Shape MeasureUnitCode => true";
        isTrue = true;
        measureUnit = RandomParams.GetRandomSpreadMeasureUnit();
        measureUnitCode = GetMeasureUnitCode();
        context = RandomParams.GetRandomSpreadMeasureUnit(measureUnitCode);
        yield return argsToObjectArray();

        testCase = "BaseShape MeasureUnitCode => true";
        measureUnitCode = Measurable.GetMeasureUnitCode(context);
        yield return argsToObjectArray();

        testCase = "Different MeasureUnitCode => false";
        isTrue = false;
        measureUnitCode = RandomParams.GetRandomMeasureUnitCode([measureUnitCode, GetMeasureUnitCode()]);
        yield return argsToObjectArray();

        #region argsToObjectArray method
        object[] argsToObjectArray()
        {
            TestCase_Enum_MeasureUnitCode_bool_Enum args = new(testCase, measureUnit, measureUnitCode, isTrue, context);

            return args.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetGetValidShapeComponentArgs()
    {
        testCase = "null => null";
        quantifiable = null;
        shapeComponent = null;
        yield return argsToObjectArray();

        testCase = "Not IShapeComponent IQuantifiable => null";
        measureUnit = RandomParams.GetRandomSpreadMeasureUnit();
        defaultQuantity = RandomParams.GetRandomPositiveDecimal();
        quantifiable = GetQuantifiableChild(this);
        shapeComponent = null;
        yield return argsToObjectArray();

        testCase = "IShapeComponent => same IShapeComponent";
        var shapeComponentQuantifiableObject = GetShapeComponentQuantifiableObject(this);
        quantifiable = shapeComponentQuantifiableObject;
        shapeComponent = shapeComponentQuantifiableObject;
        yield return argsToObjectArray();

        #region argsToObjectArray method
        object[] argsToObjectArray()
        {
            TestCase_IQuantifiable_IShapeComponent args = new(testCase, quantifiable, shapeComponent);

            return args.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> ValidateShapeComponent()
    {
        testCase = "null => null";
        quantifiable = null;
        yield return argsToObjectArray();

        testCase = "Not IShapeComponent IQuantifiable";
        measureUnit = RandomParams.GetRandomSpreadMeasureUnit();
        defaultQuantity = RandomParams.GetRandomPositiveDecimal();
        quantifiable = GetQuantifiableChild(this);
        yield return argsToObjectArray();

        testCase = "IShapeComponent => same IShapeComponent";
        quantifiable = GetShapeComponentQuantifiableObject(this);
        yield return argsToObjectArray();

        #region argsToObjectArray method
        object[] argsToObjectArray()
        {
            TestCase_IQuantifiable args = new(testCase, quantifiable);

            return args.ToObjectArray();
        }
        #endregion
    }
    #endregion
}
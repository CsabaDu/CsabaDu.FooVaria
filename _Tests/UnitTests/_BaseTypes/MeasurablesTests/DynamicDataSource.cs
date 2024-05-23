namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.MeasurablesTests;

internal sealed class DynamicDataSource : CommonDynamicDataSource
{
    #region Methods
    internal IEnumerable<object[]> GetEqualsArgs()
    {
        testCase = "null => false";
        isTrue = false;
        obj = null;
        measureUnit = null;
        yield return argsToObjectArray();

        testCase = "object => false";
        obj = new();
        yield return argsToObjectArray();

        testCase = "IMeasure with same MeasureUnit => true";
        isTrue = true;
        measureUnit = RandomParams.GetRandomMeasureUnit();
        obj = GetMeasurableChild(measureUnit);
        yield return argsToObjectArray();

        testCase = "IMeasure same MeasureUnitCode different MeasureUnit => true";
        measureUnitCode = GetMeasureUnitCode();
        measureUnit = RandomParams.GetRandomMeasureUnit(measureUnitCode, measureUnit);
        obj = GetMeasurableChild(measureUnit);
        yield return argsToObjectArray();

        testCase = "IMeasure with different MeasureUnit => false";
        isTrue = false;
        measureUnitCode = RandomParams.GetRandomMeasureUnitCode(measureUnitCode);
        measureUnit = RandomParams.GetRandomMeasureUnit(measureUnitCode);
        yield return argsToObjectArray();

        #region argsToObjectArray method
        object[] argsToObjectArray()
        {
            TestCase_bool_object_Enum args = new(testCase, isTrue, obj, measureUnit);

            return args.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetValidateMeasureUnitInvalidArgs()
    {
        testCase = "Not MeasureUnit-type Not MeasureUnitCode enum";
        measureUnitCode = RandomParams.GetRandomMeasureUnitCode();
        measureUnit = TypeCode.Empty;
        yield return argsToObjectArray();

        testCase = "Valid type not defined measureUnit";
        measureUnit = SampleParams.GetNotDefinedMeasureUnit(measureUnitCode);
        yield return argsToObjectArray();

        testCase = "Invalid type defined measureUnit";
        measureUnit = RandomParams.GetRandomMeasureUnit(measureUnitCode);
        measureUnitCode = RandomParams.GetRandomMeasureUnitCode(measureUnitCode);
        yield return argsToObjectArray();

        testCase = "Not defined MeasureUnitCode";
        measureUnit = SampleParams.NotDefinedMeasureUnitCode;
        yield return argsToObjectArray();

        #region argsToObjectArray method
        object[] argsToObjectArray()
        {
            TestCase_Enum_MeasureUnitCode args = new(testCase, measureUnit, measureUnitCode);

            return args.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetValidateMeasureUnitValidArgs()
    {
        testCase = "Valid MeasureUnit";
        measureUnit = RandomParams.GetRandomMeasureUnit();
        measureUnitCode = GetMeasureUnitCode();
        yield return argsToObjectArray();

        testCase = "Valid MeasureUnitCode";
        measureUnit = measureUnitCode;
        yield return argsToObjectArray();

        #region argsToObjectArray method
        object[] argsToObjectArray()
        {
            TestCase_Enum_MeasureUnitCode args = new(testCase, measureUnit, measureUnitCode);

            return args.ToObjectArray();
        }
        #endregion
    }
    #endregion
}

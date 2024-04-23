namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes;

public abstract class CommonDynamicDataSource : DataFields
{
    protected string testCase = string.Empty;

    public static string GetDisplayName(MethodInfo methodInfo, object[] args)
    {
        return $"{methodInfo.Name}: {(string)args[0]}";
    }

    public IEnumerable<object[]> GetHasMeasureUnitCodeArgs()
    {
        testCase = "Same MeasureUnitCode => true";
        SetMeasureUnit(RandomParams.GetRandomMeasureUnit());
        isTrue = true;
        yield return toObjectArray();

        testCase = "Different MeasureUnitCode => false";
        measureUnitCode = RandomParams.GetRandomMeasureUnitCode(measureUnitCode);
        isTrue = false;
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            TestCase_Enum_MeasureUnitCode_bool args = new(testCase, measureUnit, measureUnitCode, isTrue);

            return args.ToObjectArray();
        }
        #endregion
    }

    public IEnumerable<object[]> GetValidateMeasureUnitCodeInvalidArgs()
    {
        testCase = "Not defined MeasureUnitCode";
        measureUnit = RandomParams.GetRandomMeasureUnit();
        measureUnitCode = SampleParams.NotDefinedMeasureUnitCode;
        yield return toObjectArray();

        testCase = "Different MeasureUnitCode";
        measureUnitCode = GetMeasureUnitCode();
        measureUnitCode = RandomParams.GetRandomMeasureUnitCode(measureUnitCode);
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            TestCase_Enum_MeasureUnitCode args = new(testCase, measureUnit, measureUnitCode);

            return args.ToObjectArray();
        }
        #endregion
    }

    protected IEnumerable<object[]> GetIsExchangeableToArgs(Enum measureUnit)
    {
        testCase = "null => false";
        this.measureUnit = measureUnit;
        isTrue = false;
        context = null;
        yield return toObjectArray();

        testCase = "Not meaureUnit not MeasureUnitCode Enum => false";
        context = SampleParams.DefaultLimitMode;
        yield return toObjectArray();

        testCase = "Same MeasureUnitCode => true";
        isTrue = true;
        measureUnitCode = GetMeasureUnitCode();
        context = measureUnitCode;
        yield return toObjectArray();

        testCase = "Same type different measureUnit => true";
        context = RandomParams.GetRandomMeasureUnit(measureUnitCode);
        yield return toObjectArray();

        testCase = "Different MeasureUnitCode => false";
        isTrue = false;
        measureUnitCode = RandomParams.GetRandomConstantMeasureUnitCode(measureUnitCode);
        context = measureUnitCode;
        yield return toObjectArray();

        testCase = "Different type measureUnit => false";
        context = RandomParams.GetRandomMeasureUnit(measureUnitCode, measureUnit);
        yield return toObjectArray();

        testCase = "Same type not defined measureUnit => false";
        int count = Enum.GetNames(measureUnit.GetType()).Length;
        context = (Enum)Enum.ToObject(measureUnit.GetType(), count);
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            TestCase_bool_Enum_Enum args = new(testCase, isTrue, measureUnit, context);

            return args.ToObjectArray();
        }
        #endregion
    }

    protected static string GetEnumName<T>(T enumeration)
        where T : struct, Enum
    {
        return $"{enumeration.GetType().Name}.{Enum.GetName(enumeration)}";
    }
}

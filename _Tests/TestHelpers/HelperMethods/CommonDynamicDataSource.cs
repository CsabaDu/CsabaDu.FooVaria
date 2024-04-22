namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes;

public abstract class CommonDynamicDataSource : DataFields
{
    public static string GetDisplayName(MethodInfo methodInfo, object[] args)
    {
        return $"{methodInfo.Name}: {(string)args[0]}";
    }

    public IEnumerable<object[]> GetHasMeasureUnitCodeArgs()
    {
        //measureUnit = RandomParams.GetRandomMeasureUnit();
        //measureUnitCode = GetMeasureUnitCode();

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

    public IEnumerable<object[]> GetIsExchangeableToArgs(Enum measureUnit)
    {
        // 1
        this.measureUnit = measureUnit;
        isTrue = false;
        context = null;
        yield return toObjectArray();

        // 2
        context = SampleParams.DefaultLimitMode;
        yield return toObjectArray();

        // 3
        isTrue = true;
        measureUnitCode = GetMeasureUnitCode();
        context = measureUnitCode;
        yield return toObjectArray();

        // 4
        context = RandomParams.GetRandomMeasureUnit(measureUnitCode);
        yield return toObjectArray();

        // 5
        isTrue = false;
        measureUnitCode = RandomParams.GetRandomConstantMeasureUnitCode(measureUnitCode);
        context = measureUnitCode;
        yield return toObjectArray();

        // 6
        context = RandomParams.GetRandomMeasureUnit(measureUnitCode, measureUnit);
        yield return toObjectArray();

        // 7
        int count = Enum.GetNames(measureUnit.GetType()).Length;
        context = (Enum)Enum.ToObject(measureUnit.GetType(), count);
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            TestCase_bool_Enum_Enum item = new(isTrue, measureUnit, context);

            return item.ToObjectArray();
        }
        #endregion
    }
}

namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes;

public abstract class CommonDynamicDataSource : DataFields
{
    public IEnumerable<object[]> GetHasMeasureUnitCodeArgs()
    {
        //measureUnit = RandomParams.GetRandomMeasureUnit();
        //measureUnitCode = GetMeasureUnitCode();
        SetMeasureUnit(RandomParams.GetRandomMeasureUnit());
        isTrue = true;
        yield return toObjectArray();

        measureUnitCode = RandomParams.GetRandomMeasureUnitCode(measureUnitCode);
        isTrue = false;
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            Args_Enum_MeasureUnitCode_bool item = new(measureUnit, measureUnitCode, isTrue);

            return item.ToObjectArray();
        }
        #endregion
    }

    public IEnumerable<object[]> GetValidateMeasureUnitCodeInvalidArgs()
    {
        measureUnit = RandomParams.GetRandomMeasureUnit();
        measureUnitCode = SampleParams.NotDefinedMeasureUnitCode;
        yield return toObjectArray();

        measureUnitCode = GetMeasureUnitCode();
        measureUnitCode = RandomParams.GetRandomMeasureUnitCode(measureUnitCode);
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            Args_Enum_MeasureUnitCode item = new(measureUnit, measureUnitCode);

            return item.ToObjectArray();
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
            Args_bool_Enum_Enum item = new(isTrue, measureUnit, context);

            return item.ToObjectArray();
        }
        #endregion
    }
}

namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes;

public abstract class CommonDynamicDataSource : DataFields
{
    public IEnumerable<object[]> GetHasMeasureUnitCodeArgs()
    {
        measureUnit = RandomParams.GetRandomMeasureUnit();
        measureUnitCode = GetMeasureUnitCode(measureUnit);
        isTrue = true;
        yield return toObjectArray();

        measureUnitCode = RandomParams.GetRandomMeasureUnitCode(measureUnitCode);
        isTrue = false;
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            Enum_MeasureUnitCode_bool_args item = new(measureUnit, measureUnitCode, isTrue);

            return item.ToObjectArray();
        }
        #endregion
    }

    public IEnumerable<object[]> GetValidateMeasureUnitCodeInvalidArgs()
    {
        measureUnit = RandomParams.GetRandomMeasureUnit();
        measureUnitCode = SampleParams.NotDefinedMeasureUnitCode;
        yield return toObjectArray();

        measureUnitCode = GetMeasureUnitCode(measureUnit);
        measureUnitCode = RandomParams.GetRandomMeasureUnitCode(measureUnitCode);
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            Enum_MeasureUnitCode_args item = new(measureUnit, measureUnitCode);

            return item.ToObjectArray();
        }
        #endregion
    }

}

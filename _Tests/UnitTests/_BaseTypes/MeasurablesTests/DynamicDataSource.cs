namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.MeasurablesTests;

internal sealed class DynamicDataSource : CommonDynamicDataSource
{
    #region Methods
    internal IEnumerable<object[]> GetEqualsArgs()
    {
        // null
        isTrue = false;
        obj = null;
        measureUnit = null;
        yield return toObjectArray();

        // object
        obj = new();
        yield return toObjectArray();

        // IMeasure with same MeasureUnit
        isTrue = true;
        measureUnit = RandomParams.GetRandomMeasureUnit();
        obj = GetMeasurableChild(measureUnit);
        yield return toObjectArray();

        // IMeasure different MeasureUnit with same MeasureUnitCode
        measureUnitCode = GetMeasureUnitCode();
        measureUnit = RandomParams.GetRandomMeasureUnit(measureUnitCode, measureUnit);
        obj = GetMeasurableChild(measureUnit);
        yield return toObjectArray();

        // IMeasure with different MeasureUnitCode
        isTrue = false;
        measureUnitCode = RandomParams.GetRandomMeasureUnitCode(measureUnitCode);
        measureUnit = RandomParams.GetRandomMeasureUnit(measureUnitCode);
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            bool_object_Enum_args item = new(isTrue, obj, measureUnit);

            return item.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetValidateMeasureUnitInvalidArgs()
    {
        // Not MeasureUnit type Not MeasureUnitCode enum
        measureUnitCode = RandomParams.GetRandomMeasureUnitCode();
        measureUnit = TypeCode.Empty;
        yield return toObjectArray();

        // Valid type not defined measureUnit
        measureUnit = SampleParams.GetNotDefinedMeasureUnit(measureUnitCode);
        yield return toObjectArray();

        // Invalid type defined measureUnit
        measureUnit = RandomParams.GetRandomMeasureUnit(measureUnitCode);
        measureUnitCode = RandomParams.GetRandomMeasureUnitCode(measureUnitCode);
        yield return toObjectArray();

        // Not defined MeasureUnitCode enum
        measureUnit = SampleParams.NotDefinedMeasureUnitCode;
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            Enum_MeasureUnitCode_args item = new(measureUnit, measureUnitCode);

            return item.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetValidateMeasureUnitValidArgs()
    {
        measureUnit = RandomParams.GetRandomMeasureUnit();
        measureUnitCode = GetMeasureUnitCode();
        yield return toObjectArray();

        measureUnit = measureUnitCode;
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            Enum_MeasureUnitCode_args item = new(measureUnit, measureUnitCode);

            return item.ToObjectArray();
        }
        #endregion
    }
    #endregion
}

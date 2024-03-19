namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.MeasurablesTests;

internal class DynamicDataSource : DataFields
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
        obj = new MeasurableChild(RootObject, string.Empty);
        measureUnit = RandomParams.GetRandomMeasureUnit();
        (obj as MeasurableChild).Return = new()
        {
            GetBaseMeasureUnit = measureUnit,
        };
        yield return toObjectArray();

        // IMeasure different MeasureUnit with same MeasureUnitCode
        measureUnitCode = GetMeasureUnitCode(measureUnit);
        measureUnit = RandomParams.GetRandomMeasureUnit(measureUnitCode, measureUnit);
        (obj as MeasurableChild).Return = new()
        {
            GetBaseMeasureUnit = measureUnit,
        };
        yield return toObjectArray();

        // IMeasure with different MeasureUnitCode
        isTrue = false;
        measureUnitCode = RandomParams.GetRandomMeasureUnitCode(measureUnitCode);
        measureUnit = RandomParams.GetRandomMeasureUnit(measureUnitCode);
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            Bool_Object_Enum_args item = new(isTrue, obj, measureUnit);

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

    internal IEnumerable<object[]> GetValidateMeasureUnitCodeInvalidArgs()
    {
        measureUnit = RandomParams.GetRandomMeasureUnit();
        measureUnitCode = RandomParams.GetRandomMeasureUnitCode(GetMeasureUnitCode(measureUnit));
        yield return toObjectArray();

        measureUnitCode = SampleParams.NotDefinedMeasureUnitCode;
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            Enum_MeasureUnitCode_args item = new(measureUnit, measureUnitCode);

            return item.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetHasMeasureUnitCodeArgs()
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

    internal IEnumerable<object[]> GetValidMeasureUnitArgs()
    {
        measureUnit = RandomParams.GetRandomMeasureUnit();
        measureUnitCode = GetMeasureUnitCode(measureUnit);
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

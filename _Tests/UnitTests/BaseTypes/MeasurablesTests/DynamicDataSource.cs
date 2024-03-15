namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.MeasurablesTests;

internal class DynamicDataSource
{
    #region Fields
    private bool _isTrue;
    private MeasureUnitCode _measureUnitCode;
    private Enum _measureUnit;
    private object _obj;

    #region Readonly fields
    private readonly RandomParams RandomParams = new();
    private readonly RootObject RootObject = new();
    #endregion
    #endregion

    #region Methods
    internal IEnumerable<object[]> GetMeasurableEqualsArgsArrayList()
    {
        // null
        _isTrue = false;
        _obj = null;
        _measureUnit = null;
        yield return toObjectArray();

        // object
        _obj = new();
        yield return toObjectArray();

        // IMeasure with same MeasureUnit
        _isTrue = true;
        _obj = new MeasurableChild(RootObject, string.Empty);
        _measureUnit = RandomParams.GetRandomMeasureUnit();
        (_obj as MeasurableChild).Return = new()
        {
            GetBaseMeasureUnit = _measureUnit,
        };
        yield return toObjectArray();

        // IMeasure different MeasureUnit with same MeasureUnitCode
        _measureUnitCode = GetMeasureUnitCode(_measureUnit);
        _measureUnit = RandomParams.GetRandomMeasureUnit(_measureUnitCode, _measureUnit);
        (_obj as MeasurableChild).Return = new()
        {
            GetBaseMeasureUnit = _measureUnit,
        };
        yield return toObjectArray();

        // IMeasure with different MeasureUnitCode
        _isTrue = false;
        _measureUnitCode = RandomParams.GetRandomMeasureUnitCode(_measureUnitCode);
        _measureUnit = RandomParams.GetRandomMeasureUnit(_measureUnitCode);
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            Bool_Object_Enum_args item = new(_isTrue, _obj, _measureUnit);

            return item.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetMeasurableValidateMeasureUnitInvalidArgsArrayList()
    {
        // Not MeasureUnit type Not MeasureUnitCode enum
        _measureUnitCode = RandomParams.GetRandomMeasureUnitCode();
        _measureUnit = TypeCode.Empty;
        yield return toObjectArray();

        // Valid type not defined _measureUnit
        _measureUnit = SampleParams.GetNotDefinedMeasureUnit(_measureUnitCode);
        yield return toObjectArray();

        // Invalid type defined _measureUnit
        _measureUnit = RandomParams.GetRandomMeasureUnit(_measureUnitCode);
        _measureUnitCode = RandomParams.GetRandomMeasureUnitCode(_measureUnitCode);
        yield return toObjectArray();

        // Not defined MeasureUnitCode enum
        _measureUnit = SampleParams.NotDefinedMeasureUnitCode;
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            Enum_MeasureUnitCode_args item = new(_measureUnit, _measureUnitCode);

            return item.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetValidateMeasureUnitCodeInvalidArgsArrayList()
    {
        _measureUnit = RandomParams.GetRandomMeasureUnit();
        _measureUnitCode = RandomParams.GetRandomMeasureUnitCode(GetMeasureUnitCode(_measureUnit));
        yield return toObjectArray();

        _measureUnitCode = SampleParams.NotDefinedMeasureUnitCode;
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            Enum_MeasureUnitCode_args item = new(_measureUnit, _measureUnitCode);

            return item.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetHasMeasureUnitCodeArgsArrayList()
    {
        _measureUnit = RandomParams.GetRandomMeasureUnit();
        _measureUnitCode = GetMeasureUnitCode(_measureUnit);
        _isTrue = true;
        yield return toObjectArray();

        _measureUnitCode = RandomParams.GetRandomMeasureUnitCode(_measureUnitCode);
        _isTrue = false;
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            Enum_MeasureUnitCode_bool_args item = new(_measureUnit, _measureUnitCode, _isTrue);

            return item.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetValidMeasureUnitArgsArrayList()
    {
        _measureUnit = RandomParams.GetRandomMeasureUnit();
        _measureUnitCode = GetMeasureUnitCode(_measureUnit);
        yield return toObjectArray();

        _measureUnit = _measureUnitCode;
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            Enum_MeasureUnitCode_args item = new(_measureUnit, _measureUnitCode);

            return item.ToObjectArray();
        }
        #endregion
    }
    #endregion
}

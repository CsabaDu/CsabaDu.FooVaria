namespace CsabaDu.FooVaria.Tests.TestHelpers.DynamicDataSources;

public sealed class DynamicDataSource
{
    #region Fields
    private Enum context;
    private decimal exchangeRate;
    private bool isTrue;
    private MeasureUnitCode measureUnitCode;
    private Enum measureUnit;
    private object obj;

    #region Readonly fields
    private readonly RandomParams RandomParams = new();
    private readonly RootObject RootObject = new();
    #endregion
    #endregion

    #region Methods
    public IEnumerable<object[]> GetInvalidEnumMeasureUnitArgArrayList()
    {
        measureUnit = RandomParams.GetRandomMeasureUnitCode();
        yield return toObjectArray();

        measureUnit = RandomParams.GetRandomNotDefinedMeasureUnit();
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            Enum_arg item = new(measureUnit);

            return item.ToObjectArray();
        }
        #endregion
    }

    public IEnumerable<object[]> GetMeasurableEqualsArgsArrayList()
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

    public IEnumerable<object[]> GetMeasurableIsValidMeasureUnitCodeArgsArrayList()
    {
        measureUnit = RandomParams.GetRandomMeasureUnit();
        measureUnitCode = SampleParams.NotDefinedMeasureUnitCode;
        isTrue = false;
        yield return toObjectArray();

        measureUnitCode = GetMeasureUnitCode(measureUnit);
        isTrue = true;
        yield return toObjectArray();

        measureUnitCode = RandomParams.GetRandomMeasureUnitCode();
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

    public IEnumerable<object[]> GetHasMeasureUnitCodeArgsArrayList()
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

    public IEnumerable<object[]> GetMeasurableValidateMeasureUnitInvalidArgsArrayList()
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

    public IEnumerable<object[]> GetValidMeasureUnitArgsArrayList()
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
    public IEnumerable<object[]> GetMeasurableValidateMeasureUnitCodeInvalidArgsArrayList()
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

    public IEnumerable<object[]> GetMeasurementInvalidEnumMeasureUnitArgArrayList()
    {
        foreach (object[] item in GetInvalidEnumMeasureUnitArgArrayList())
        {
            yield return item;
        }

        measureUnit = RandomParams.GetRandomNotUsedCustomMeasureUnit();
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            Enum_arg item = new(measureUnit);

            return item.ToObjectArray();
        }
        #endregion
    }

    public IEnumerable<object[]> GetBaseMeasurementEqualsObjectArgArrayList()
    {
        // null
        obj = null;
        measureUnit = RandomParams.GetRandomValidMeasureUnit();
        measureUnitCode = GetMeasureUnitCode(measureUnit);
        isTrue = false;
        yield return toObjectArray();

        // object
        obj = new();
        yield return toObjectArray();

        // IBaseMeasurement
        obj = new BaseMeasurementChild(RootObject, null)
        {
            Return = new()
            {
                GetBaseMeasureUnit = RandomParams.GetRandomValidMeasureUnit(),
            }
        };
        isTrue = measureUnit.Equals((obj as IBaseMeasurement).GetBaseMeasureUnit());
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            Bool_Object_Enum_args item = new(isTrue, obj, measureUnit);

            return item.ToObjectArray();
        }
        #endregion
    }

    public IEnumerable<object[]> GetBaseMeasurementEqualsBaseMeasurementArgArrayList()
    {
        // null
        obj = null;
        isTrue = false;
        measureUnit = RandomParams.GetRandomValidMeasureUnit();
        measureUnitCode = GetMeasureUnitCode(measureUnit);
        yield return toObjectArray();

        // Different MeasureUnitCode
        measureUnitCode = RandomParams.GetRandomMeasureUnitCode(measureUnitCode);
        obj = new BaseMeasurementChild(RootObject, null)
        {
            Return = new()
            {
                GetBaseMeasureUnit = RandomParams.GetRandomMeasureUnit(measureUnitCode),
            }
        };
        yield return toObjectArray();

        // Same MeasureUnit
        isTrue = true;
        obj = new BaseMeasurementChild(RootObject, null)
        {
            Return = new()
            {
                GetBaseMeasureUnit = measureUnit,
            }
        };
        yield return toObjectArray();

        // Different MeasureUnit, same MeasureUnitCode and same ExhchangeRate
        measureUnit = RandomParams.GetRandomNotUsedCustomMeasureUnit();
        _ = TrySetCustomMeasureUnit(measureUnit, decimal.One, RandomParams.GetRandomParamName());
        measureUnitCode = GetMeasureUnitCode(measureUnit);
        obj = new BaseMeasurementChild(RootObject, null)
        {
            Return = new()
            {
                GetBaseMeasureUnit = measureUnitCode.GetDefaultMeasureUnit(),
            }
        };
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            IBaseMeasurement baseMeasurement = (IBaseMeasurement)obj;
            Bool_Enum_IBaseMeasurement_args item = new(isTrue, measureUnit, baseMeasurement);

            return item.ToObjectArray();
        }
        #endregion
    }

    public IEnumerable<object[]> GetExchangeRateCollectionArgArrayList()
    {
        measureUnit = RandomParams.GetRandomMeasureUnit();
        measureUnitCode = GetMeasureUnitCode(measureUnit);
        yield return toObjectArray();

        measureUnit = RandomParams.GetRandomNotUsedCustomMeasureUnit();
        _ = TrySetCustomMeasureUnit(measureUnit, RandomParams.GetRandomPositiveDecimal(), RandomParams.GetRandomParamName());
        measureUnitCode = GetMeasureUnitCode(measureUnit);
        measureUnit = measureUnitCode.GetDefaultMeasureUnit();
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            Enum_MeasureUnitCode_args item = new(measureUnit, measureUnitCode);

            return item.ToObjectArray();
        }
        #endregion
    }

    public IEnumerable<object[]> GetBaseMeasurementIsExchangeableToArgArrayList()
    {
        // null -  false
        isTrue = false;
        measureUnit = RandomParams.GetRandomValidMeasureUnit();
        context = null;
        yield return toObjectArray();

        // Not measureUnit not MeasureUnitCode Enum - false
        context = TypeCode.Empty;
        yield return toObjectArray();

        // other MeasureUnitCode - false
        measureUnitCode = GetMeasureUnitCode(measureUnit);
        context = RandomParams.GetRandomMeasureUnitCode(measureUnitCode);
        yield return toObjectArray();

        // same type not defined measureUnit - false
        context = SampleParams.GetNotDefinedMeasureUnit(measureUnitCode);
        yield return toObjectArray();

        // same MeasureUnitCode - true
        isTrue = true;
        context = measureUnitCode;
        yield return toObjectArray();

        // same type valid measureUnit - true
        context = RandomParams.GetRandomValidMeasureUnit(measureUnitCode);
        yield return toObjectArray();

        // other type measureUnit - false
        isTrue = false;
        measureUnitCode = RandomParams.GetRandomMeasureUnitCode(measureUnitCode);
        context = RandomParams.GetRandomMeasureUnit(measureUnitCode);
        yield return toObjectArray();

        // same type invalid measureUnit - false
        context = RandomParams.GetRandomNotUsedCustomMeasureUnit();
        measureUnitCode = GetMeasureUnitCode(measureUnit);
        measureUnit = measureUnitCode.GetDefaultMeasureUnit();
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            Bool_Enum_Enum_args item = new(isTrue, measureUnit, context);

            return item.ToObjectArray();
        }
        #endregion
    }

    public IEnumerable<object[]> GetValidateExchangeRateArgArrayList()
    {
        measureUnit = RandomParams.GetRandomValidMeasureUnit();
        exchangeRate = 0;
        yield return toObjectArray();

        exchangeRate = RandomParams.GetRandomNegativeDecimal();
        yield return toObjectArray();

        exchangeRate = RandomParams.GetRandomPositiveDecimal(GetExchangeRate(measureUnit, null));
        #region toObjectArray method
        object[] toObjectArray()
        {
            Enum_Decimal_args item = new(measureUnit, exchangeRate);

            return item.ToObjectArray();
        }
        #endregion
    }

    public IEnumerable<object[]> GetBaseMeasurementValidateMeasureUnitInvalidArgsArrayList()
    {
        foreach (object[] item in GetMeasurableValidateMeasureUnitInvalidArgsArrayList())
        {
            yield return item;
        }

        // Different type valid type different measureUnit
        measureUnitCode = RandomParams.GetRandomConstantMeasureUnitCode();
        measureUnit = RandomParams.GetRandomConstantMeasureUnit(measureUnitCode.GetDefaultMeasureUnit());
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

    #endregion
}
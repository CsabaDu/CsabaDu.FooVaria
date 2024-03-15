namespace CsabaDu.FooVaria.Tests.TestHelpers.DynamicDataSources;

public sealed class DynamicDataSource
{
    #region Fields
    private Enum _context;
    private decimal _exchangeRate;
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
    public IEnumerable<object[]> GetInvalidEnumMeasureUnitArgArrayList()
    {
        _measureUnit = RandomParams.GetRandomMeasureUnitCode();
        yield return toObjectArray();

        _measureUnit = RandomParams.GetRandomNotDefinedMeasureUnit();
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            Enum_arg item = new(_measureUnit);

            return item.ToObjectArray();
        }
        #endregion
    }

    public IEnumerable<object[]> GetMeasurableIsValidMeasureUnitCodeArgsArrayList()
    {
        _measureUnit = RandomParams.GetRandomMeasureUnit();
        _measureUnitCode = SampleParams.NotDefinedMeasureUnitCode;
        _isTrue = false;
        yield return toObjectArray();

        _measureUnitCode = GetMeasureUnitCode(_measureUnit);
        _isTrue = true;
        yield return toObjectArray();

        _measureUnitCode = RandomParams.GetRandomMeasureUnitCode();
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

    public IEnumerable<object[]> GetMeasurementInvalidEnumMeasureUnitArgArrayList()
    {
        foreach (object[] item in GetInvalidEnumMeasureUnitArgArrayList())
        {
            yield return item;
        }

        _measureUnit = RandomParams.GetRandomNotUsedCustomMeasureUnit();
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            Enum_arg item = new(_measureUnit);

            return item.ToObjectArray();
        }
        #endregion
    }

    public IEnumerable<object[]> GetBaseMeasurementEqualsObjectArgArrayList()
    {
        // null
        _obj = null;
        _measureUnit = RandomParams.GetRandomValidMeasureUnit();
        _measureUnitCode = GetMeasureUnitCode(_measureUnit);
        _isTrue = false;
        yield return toObjectArray();

        // object
        _obj = new();
        yield return toObjectArray();

        // IBaseMeasurement
        _obj = new BaseMeasurementChild(RootObject, null)
        {
            Return = new()
            {
                GetBaseMeasureUnit = RandomParams.GetRandomValidMeasureUnit(),
            }
        };
        _isTrue = _measureUnit.Equals((_obj as IBaseMeasurement).GetBaseMeasureUnit());
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            Bool_Object_Enum_args item = new(_isTrue, _obj, _measureUnit);

            return item.ToObjectArray();
        }
        #endregion
    }

    public IEnumerable<object[]> GetBaseMeasurementEqualsBaseMeasurementArgArrayList()
    {
        // null
        _obj = null;
        _isTrue = false;
        _measureUnit = RandomParams.GetRandomValidMeasureUnit();
        _measureUnitCode = GetMeasureUnitCode(_measureUnit);
        yield return toObjectArray();

        // Different MeasureUnitCode
        _measureUnitCode = RandomParams.GetRandomMeasureUnitCode(_measureUnitCode);
        _obj = new BaseMeasurementChild(RootObject, null)
        {
            Return = new()
            {
                GetBaseMeasureUnit = RandomParams.GetRandomMeasureUnit(_measureUnitCode),
            }
        };
        yield return toObjectArray();

        // Same MeasureUnit
        _isTrue = true;
        _obj = new BaseMeasurementChild(RootObject, null)
        {
            Return = new()
            {
                GetBaseMeasureUnit = _measureUnit,
            }
        };
        yield return toObjectArray();

        // Different MeasureUnit, same MeasureUnitCode and same ExhchangeRate
        _measureUnit = RandomParams.GetRandomNotUsedCustomMeasureUnit();
        _ = TrySetCustomMeasureUnit(_measureUnit, decimal.One, RandomParams.GetRandomParamName());
        _measureUnitCode = GetMeasureUnitCode(_measureUnit);
        _obj = new BaseMeasurementChild(RootObject, null)
        {
            Return = new()
            {
                GetBaseMeasureUnit = _measureUnitCode.GetDefaultMeasureUnit(),
            }
        };
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            IBaseMeasurement baseMeasurement = (IBaseMeasurement)_obj;
            Bool_Enum_IBaseMeasurement_args item = new(_isTrue, _measureUnit, baseMeasurement);

            return item.ToObjectArray();
        }
        #endregion
    }

    public IEnumerable<object[]> GetExchangeRateCollectionArgArrayList()
    {
        _measureUnit = RandomParams.GetRandomMeasureUnit();
        _measureUnitCode = GetMeasureUnitCode(_measureUnit);
        yield return toObjectArray();

        _measureUnit = RandomParams.GetRandomNotUsedCustomMeasureUnit();
        _ = TrySetCustomMeasureUnit(_measureUnit, RandomParams.GetRandomPositiveDecimal(), RandomParams.GetRandomParamName());
        _measureUnitCode = GetMeasureUnitCode(_measureUnit);
        _measureUnit = _measureUnitCode.GetDefaultMeasureUnit();
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            Enum_MeasureUnitCode_args item = new(_measureUnit, _measureUnitCode);

            return item.ToObjectArray();
        }
        #endregion
    }

    public IEnumerable<object[]> GetBaseMeasurementIsExchangeableToArgArrayList()
    {
        // null -  false
        _isTrue = false;
        _measureUnit = RandomParams.GetRandomValidMeasureUnit();
        _context = null;
        yield return toObjectArray();

        // Not _measureUnit not MeasureUnitCode Enum - false
        _context = TypeCode.Empty;
        yield return toObjectArray();

        // other MeasureUnitCode - false
        _measureUnitCode = GetMeasureUnitCode(_measureUnit);
        _context = RandomParams.GetRandomMeasureUnitCode(_measureUnitCode);
        yield return toObjectArray();

        // same type not defined _measureUnit - false
        _context = SampleParams.GetNotDefinedMeasureUnit(_measureUnitCode);
        yield return toObjectArray();

        // same MeasureUnitCode - true
        _isTrue = true;
        _context = _measureUnitCode;
        yield return toObjectArray();

        // same type valid _measureUnit - true
        _context = RandomParams.GetRandomValidMeasureUnit(_measureUnitCode);
        yield return toObjectArray();

        // other type _measureUnit - false
        _isTrue = false;
        _measureUnitCode = RandomParams.GetRandomMeasureUnitCode(_measureUnitCode);
        _context = RandomParams.GetRandomMeasureUnit(_measureUnitCode);
        yield return toObjectArray();

        // same type invalid _measureUnit - false
        _context = RandomParams.GetRandomNotUsedCustomMeasureUnit();
        _measureUnitCode = GetMeasureUnitCode(_measureUnit);
        _measureUnit = _measureUnitCode.GetDefaultMeasureUnit();
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            Bool_Enum_Enum_args item = new(_isTrue, _measureUnit, _context);

            return item.ToObjectArray();
        }
        #endregion
    }

    public IEnumerable<object[]> GetValidateExchangeRateArgArrayList()
    {
        _measureUnit = RandomParams.GetRandomValidMeasureUnit();
        _exchangeRate = 0;
        yield return toObjectArray();

        _exchangeRate = RandomParams.GetRandomNegativeDecimal();
        yield return toObjectArray();

        _exchangeRate = RandomParams.GetRandomPositiveDecimal(GetExchangeRate(_measureUnit, null));
        #region toObjectArray method
        object[] toObjectArray()
        {
            Enum_Decimal_args item = new(_measureUnit, _exchangeRate);

            return item.ToObjectArray();
        }
        #endregion
    }

    public IEnumerable<object[]> GetBaseMeasurementValidateMeasureUnitValidArgsArrayList()
    {
        // MeasureUnitCode
        _measureUnit = RandomParams.GetRandomValidMeasureUnit();
        _context = GetMeasureUnitCode(_measureUnit);
        yield return toObjectArray();

        // _measureUnit
        _context = RandomParams.GetRandomSameTypeValidMeasureUnit(_measureUnit);
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            Enum_Enum_args item = new(_measureUnit, _context);

            return item.ToObjectArray();
        }
        #endregion
    }

    #endregion
}
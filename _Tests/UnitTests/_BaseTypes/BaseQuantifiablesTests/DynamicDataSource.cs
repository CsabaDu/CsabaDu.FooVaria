using CsabaDu.FooVaria.BaseTypes.BaseQuantifiables.Statics;
using static CsabaDu.FooVaria.BaseTypes.BaseQuantifiables.Statics.Extensions;

namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.BaseQuantifiablesTests;

internal class DynamicDataSource : DynamicDataFields
{
    #region Methods
    internal IEnumerable<object[]> GetEqualsArgsArrayList()
    {
        // null
        _obj = null;
        _measureUnit = RandomParams.GetRandomValidMeasureUnit();
        _defaultQuantity = RandomParams.GetRandomDecimal();
        _isTrue = false;
        yield return toObjectArray();

        // object
        _obj = new();
        yield return toObjectArray();

        // Different MeasureUnitCode
        _measureUnitCode = RandomParams.GetRandomMeasureUnitCode(GetMeasureUnitCode(_measureUnit));
        _obj = new BaseQuantifiableChild(RootObject, null)
        {
            Return = new()
            {
                GetBaseMeasureUnit = RandomParams.GetRandomMeasureUnit(_measureUnitCode),
                GetDefaultQuantity = _defaultQuantity,
            }
        };
        yield return toObjectArray();

        // Same MeasureUnit, different defaultQuantity
        _obj = new BaseQuantifiableChild(RootObject, null)
        {
            Return = new()
            {
                GetBaseMeasureUnit = _measureUnit,
                GetDefaultQuantity = RandomParams.GetRandomDecimal(_defaultQuantity),            }
        };
        yield return toObjectArray();

        // IBaseMeasurement
        _obj = new BaseQuantifiableChild(RootObject, null)
        {
            Return = new()
            {
                GetBaseMeasureUnit = _measureUnit,
                GetDefaultQuantity = _defaultQuantity,
            }
        };
        _isTrue = true;
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            Bool_Object_Enum_Decimal_args item = new(_isTrue, _obj, _measureUnit, _defaultQuantity);

            return item.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetFitsInArgsArrayList()
    {
        // null
        _measureUnit = null;
        _limiter = null;
        yield return toObjectArray();

        // Not IBaseQuantifiable
        _limiter = new LimiterObject();
        yield return toObjectArray();

        // Different MeasureUnitCode
        _measureUnit = RandomParams.GetRandomMeasureUnit();
        _measureUnitCode = RandomParams.GetRandomMeasureUnitCode(GetMeasureUnitCode(_measureUnit));
        _limiter = new LimiterBaseQuantifiableOblect(RootObject, null)
        {
            Return = new()
            {
                GetBaseMeasureUnit = RandomParams.GetRandomMeasureUnit(GetMeasureUnitCode(_measureUnitCode)),
            }
        };

        #region toObjectArray method
        object[] toObjectArray()
        {
            Enum_ILimiter_args item = new(_measureUnit, _limiter);

            return item.ToObjectArray();
        }
        #endregion
    }


    //internal IEnumerable<object[]> GetExchangeRateCollectionArgArrayList()
    //{
    //    _measureUnit = RandomParams.GetRandomMeasureUnit();
    //    _measureUnitCode = GetMeasureUnitCode(_measureUnit);
    //    yield return toObjectArray();

    //    _measureUnit = RandomParams.GetRandomNotUsedCustomMeasureUnit();
    //    _ = TrySetCustomMeasureUnit(_measureUnit, RandomParams.GetRandomPositiveDecimal(), RandomParams.GetRandomParamName());
    //    _measureUnitCode = GetMeasureUnitCode(_measureUnit);
    //    _measureUnit = _measureUnitCode.GetDefaultMeasureUnit();
    //    yield return toObjectArray();

    //    #region toObjectArray method
    //    object[] toObjectArray()
    //    {
    //        Enum_MeasureUnitCode_args item = new(_measureUnit, _measureUnitCode);

    //        return item.ToObjectArray();
    //    }
    //    #endregion
    //}

    //internal IEnumerable<object[]> GetIsExchangeableToArgArrayList()
    //{
    //    // null -  false
    //    _isTrue = false;
    //    _measureUnit = RandomParams.GetRandomValidMeasureUnit();
    //    _context = null;
    //    yield return toObjectArray();

    //    // Not _measureUnit not MeasureUnitCode Enum - false
    //    _context = TypeCode.Empty;
    //    yield return toObjectArray();

    //    // other MeasureUnitCode - false
    //    _measureUnitCode = GetMeasureUnitCode(_measureUnit);
    //    _context = RandomParams.GetRandomMeasureUnitCode(_measureUnitCode);
    //    yield return toObjectArray();

    //    // same type not defined _measureUnit - false
    //    _context = SampleParams.GetNotDefinedMeasureUnit(_measureUnitCode);
    //    yield return toObjectArray();

    //    // same MeasureUnitCode - true
    //    _isTrue = true;
    //    _context = _measureUnitCode;
    //    yield return toObjectArray();

    //    // same type valid _measureUnit - true
    //    _context = RandomParams.GetRandomValidMeasureUnit(_measureUnitCode);
    //    yield return toObjectArray();

    //    // other type _measureUnit - false
    //    _isTrue = false;
    //    _measureUnitCode = RandomParams.GetRandomMeasureUnitCode(_measureUnitCode);
    //    _context = RandomParams.GetRandomMeasureUnit(_measureUnitCode);
    //    yield return toObjectArray();

    //    // same type invalid _measureUnit - false
    //    _context = RandomParams.GetRandomNotUsedCustomMeasureUnit();
    //    _measureUnitCode = GetMeasureUnitCode(_measureUnit);
    //    _measureUnit = _measureUnitCode.GetDefaultMeasureUnit();
    //    yield return toObjectArray();

    //    #region toObjectArray method
    //    object[] toObjectArray()
    //    {
    //        Bool_Enum_Enum_args item = new(_isTrue, _measureUnit, _context);

    //        return item.ToObjectArray();
    //    }
    //    #endregion
    //}

    //internal IEnumerable<object[]> GetValidateExchangeRateArgArrayList()
    //{
    //    _measureUnit = RandomParams.GetRandomValidMeasureUnit();
    //    _exchangeRate = 0;
    //    yield return toObjectArray();

    //    _exchangeRate = RandomParams.GetRandomNegativeDecimal();
    //    yield return toObjectArray();

    //    _exchangeRate = RandomParams.GetRandomPositiveDecimal(GetExchangeRate(_measureUnit, null));
    //    #region toObjectArray method
    //    object[] toObjectArray()
    //    {
    //        Enum_Decimal_args item = new(_measureUnit, _exchangeRate);

    //        return item.ToObjectArray();
    //    }
    //    #endregion
    //}

    //internal IEnumerable<object[]> GetValidateMeasureUnitValidArgsArrayList()
    //{
    //    // MeasureUnitCode
    //    _measureUnit = RandomParams.GetRandomValidMeasureUnit();
    //    _context = GetMeasureUnitCode(_measureUnit);
    //    yield return toObjectArray();

    //    // _measureUnit
    //    _context = RandomParams.GetRandomSameTypeValidMeasureUnit(_measureUnit);
    //    yield return toObjectArray();

    //    #region toObjectArray method
    //    object[] toObjectArray()
    //    {
    //        Enum_Enum_args item = new(_measureUnit, _context);

    //        return item.ToObjectArray();
    //    }
    //    #endregion
    //}
    #endregion
}

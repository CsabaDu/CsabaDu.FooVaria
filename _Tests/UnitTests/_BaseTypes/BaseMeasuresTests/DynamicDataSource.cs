namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.BaseMeasuresTests;

internal class DynamicDataSource : CommonDynamicDataSource
{
    #region Fields
    IBaseMeasure baseMeasure;
    IBaseMeasure other;
    IBaseMeasurement baseMeasurement;
    #endregion

    #region Methods
    #region Private methods
    private decimal GetExchangeRate()
    {
        return ExchangeRateCollection[measureUnit];
    }

    private BaseMeasureChild GetBaseMeasureChild()
    {
        return BaseMeasureChild.GetBaseMeasureChild(measureUnit, quantity);
    }
    #endregion

    internal IEnumerable<object[]> GetEqualsArg()
    {
        // null
        isTrue = false;
        measureUnit = RandomParams.GetRandomConstantMeasureUnit();
        quantity = (ValueType)RandomParams.GetRandomQuantity();
        baseMeasure = null;
        yield return toObjectArray();

        // Same
        isTrue = true;
        baseMeasure = GetBaseMeasureChild();
        yield return toObjectArray();

        // Different MeasureUnitCode
        isTrue = false;
        measureUnitCode = RandomParams.GetRandomConstantMeasureUnitCode(measureUnitCode);
        measureUnit = measureUnitCode.GetDefaultMeasureUnit();
        measureUnit = RandomParams.GetRandomMeasureUnit(measureUnitCode, measureUnit);
        yield return toObjectArray();

        // Same MeasureUnitCode, different measureUnit
        baseMeasure = GetBaseMeasureChild();
        measureUnit = RandomParams.GetRandomMeasureUnit(measureUnitCode, measureUnit);
        yield return toObjectArray();

        // Different quantity
        baseMeasure = GetBaseMeasureChild();
        quantityTypeCode = Type.GetTypeCode(quantity.GetType());
        quantity = (ValueType)RandomParams.GetRandomQuantity(quantityTypeCode, quantity);
        yield return toObjectArray();

        // Equals when exchanged
        isTrue = true;
        decimalQuantity = RandomParams.GetRandomDecimal();
        quantity = decimalQuantity;
        baseMeasure = GetBaseMeasureChild();
        decimalQuantity *= GetExchangeRate();
        measureUnit = RandomParams.GetRandomSameTypeValidMeasureUnit(measureUnit);
        decimalQuantity /= GetExchangeRate();
        quantity = decimalQuantity;
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            bool_Enum_ValueType_IQuantifiable_args item = new(isTrue, measureUnit, quantity, baseMeasure);

            return item.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetEqualsArgs()
    {
        // null - null
        isTrue = true;
        baseMeasure = null;
        other = null;
        yield return toObjectArray();

        // not null - null
        isTrue = false;
        measureUnit = RandomParams.GetRandomValidMeasureUnit();
        quantity = (ValueType)RandomParams.GetRandomQuantity();
        rateComponentCode = RandomParams.GetRandomRateComponentCode();
        limitMode = RandomParams.GetRandomNullableLimitMode();
        baseMeasure = getBaseMeasureChild();
        yield return toObjectArray();

        // null - not null
        baseMeasure = null;
        other = getBaseMeasureChild();
        yield return toObjectArray();

        // Same
        isTrue = true;
        baseMeasure = getBaseMeasureChild();
        yield return toObjectArray();

        // Different LimitMode
        isTrue = false;
        limitMode = RandomParams.GetRandomNullableLimitMode(limitMode);
        other = getBaseMeasureChild();
        yield return toObjectArray();

        // Different RateComponentCode
        baseMeasure = getBaseMeasureChild();
        rateComponentCode = RandomParams.GetRandomRateComponentCode(rateComponentCode);
        other = getBaseMeasureChild();
        yield return toObjectArray();

        // Different quantity
        baseMeasure = getBaseMeasureChild();
        quantityTypeCode = Type.GetTypeCode(quantity.GetType());
        quantity = (ValueType)RandomParams.GetRandomQuantity(quantityTypeCode, quantity);
        other = getBaseMeasureChild();
        yield return toObjectArray();

        // Different measureUnit
        baseMeasure = getBaseMeasureChild();
        measureUnit = RandomParams.GetRandomValidMeasureUnit(measureUnit);
        other = getBaseMeasureChild();
        yield return toObjectArray();

        // Equals when exchanged
        isTrue = true;
        decimalQuantity = RandomParams.GetRandomDecimal();
        quantity = decimalQuantity;
        baseMeasure = getBaseMeasureChild();
        decimalQuantity *= GetExchangeRate();
        measureUnit = RandomParams.GetRandomSameTypeValidMeasureUnit(measureUnit);
        decimalQuantity /= GetExchangeRate();
        quantity = decimalQuantity;
        other = getBaseMeasureChild();
        yield return toObjectArray();

        #region Local methods
        #region toObjectArray method
        object[] toObjectArray()
        {
            IBaseMeasure_bool_IBaseMeasure_arg item = new(baseMeasure, isTrue, other);

            return item.ToObjectArray();
        }
        #endregion

        BaseMeasureChild getBaseMeasureChild()
        {
            return BaseMeasureChild.GetBaseMeasureChild(measureUnit, quantity, rateComponentCode, limitMode);
        }
        #endregion
    }

    internal IEnumerable<object[]> GetFitsInILimiterArgs()
    {
        // Not IBaseQuantifiable
        measureUnit = RandomParams.GetRandomMeasureUnit();
        limiter = new LimiterObject();
        yield return toObjectArray();

        // Different MeasureUnitCode
        measureUnitCode = GetMeasureUnitCode(measureUnit);
        measureUnitCode = RandomParams.GetRandomCustomMeasureUnitCode(measureUnitCode);
        limitMode = RandomParams.GetRandomLimitMode();
        limiter = LimiterQuantifiableObject.GetLimiterQuantifiableObject(limitMode.Value, RandomParams.GetRandomMeasureUnit(measureUnitCode), defaultQuantity);
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            Enum_ILimiter_args item = new(measureUnit, limiter);

            return item.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetFitsInIQuantifiableLimitModeArgs()
    {
        // IQuantifiable, Not defined limitMode
        limitMode = SampleParams.NotDefinedLimitMode;
        measureUnit = RandomParams.GetRandomValidMeasureUnit();
        measureUnitCode = GetMeasureUnitCode(measureUnit);
        quantity = (ValueType)RandomParams.GetRandomQuantity();
        baseMeasure = GetBaseMeasureChild();
        yield return toObjectArray();

        // Different IQuantifiable, valid LimitMode
        measureUnitCode = RandomParams.GetRandomConstantMeasureUnitCode(measureUnitCode);
        measureUnit = measureUnitCode.GetDefaultMeasureUnit();
        measureUnit = RandomParams.GetRandomMeasureUnit(measureUnitCode, measureUnit);
        limitMode = RandomParams.GetRandomLimitMode();
        yield return toObjectArray();

        // null, valid LimitMode
        baseMeasure = null;
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            Enum_LimitMode_IQuantifiable_arg item = new(measureUnit, limitMode, baseMeasure);

            return item.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetGetBaseMeasureNullCheckArgs()
    {
        paramName = ParamNames.baseMeasurement;
        baseMeasurement = null;
        yield return toObjectArray();

        paramName = ParamNames.quantity;
        measureUnit = RandomParams.GetRandomMeasureUnit();
        baseMeasurement = BaseMeasurementChild.GetBaseMeasurementChild(measureUnit);
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            string_IBaseMeasurement_args item = new(paramName, baseMeasurement);

            return item.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetGetQuantityTypeCodeArgs()
    {
        typeCode = TypeCode.Empty;
        obj = null;
        yield return toObjectArray();

        typeCode = TypeCode.Object;
        obj = new();
        yield return toObjectArray();

        foreach (TypeCode item in SampleParams.InvalidValueTypeCodes)
        {
            obj = RandomParams.GetRandomValueType(item);
            yield return toObjectArray();
        }

        foreach (var item in BaseQuantifiable.QuantityTypeCodes)
        {
            typeCode = item;
            obj = RandomParams.GetRandomValueType(item);
            yield return toObjectArray();
        }

        #region toObjectArray method
        object[] toObjectArray()
        {
            TypeCode_object_arg item = new(typeCode, obj);

            return item.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetIsExchangeableToArgs()
    {
        // 1
        measureUnit = RandomParams.GetRandomConstantMeasureUnit();
        isTrue = false;
        context = null;
        yield return toObjectArray();

        // 2
        context = SampleParams.DefaultLimitMode;
        yield return toObjectArray();

        // 3
        isTrue = true;
        measureUnitCode = GetMeasureUnitCode(measureUnit);
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
            bool_Enum_Enum_args item = new(isTrue, measureUnit, context);

            return item.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetValidateExchangeRateArgs()
    {
        decimalQuantity = 0;
        measureUnit = RandomParams.GetRandomValidMeasureUnit();
        yield return toObjectArray();

        decimalQuantity = RandomParams.GetRandomDecimal();
        paramName = RandomParams.GetRandomParamName();
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            Enum_decimal_args item = new(measureUnit, decimalQuantity);

            return item.ToObjectArray();
        }
        #endregion
    }
    #endregion
}

namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.BaseMeasuresTests;

internal sealed class DynamicDataSource : CommonDynamicDataSource
{
    #region Fields
    IBaseMeasure baseMeasure;
    IBaseMeasure other;
    IBaseMeasurement baseMeasurement;
    #endregion

    #region Methods
    #region Private methods
    //private decimal GetExchangeRate()
    //{
    //    return ExchangeRateCollection[measureUnit];
    //}

    private BaseMeasureChild GetBaseMeasureChild()
    {
        return BaseMeasureChild.GetBaseMeasureChild(measureUnit, quantity);
    }
    #endregion

    internal IEnumerable<object[]> GetEqualsArg()
    {
        testCase = "null => false";
        isTrue = false;
        measureUnit = RandomParams.GetRandomConstantMeasureUnit();
        quantity = (ValueType)RandomParams.GetRandomQuantity();
        baseMeasure = null;
        yield return toObjectArray();

        testCase = "Same baseMeasure => true";
        isTrue = true;
        baseMeasure = GetBaseMeasureChild();
        yield return toObjectArray();

        testCase = "Different MeasureUnitCode => false";
        isTrue = false;
        measureUnitCode = RandomParams.GetRandomConstantMeasureUnitCode(measureUnitCode);
        context = measureUnitCode.GetDefaultMeasureUnit();
        measureUnit = RandomParams.GetRandomMeasureUnit(measureUnitCode, context);
        yield return toObjectArray();

        testCase = "Same MeasureUnitCode, different measureUnit => false";
        baseMeasure = GetBaseMeasureChild();
        measureUnit = RandomParams.GetRandomMeasureUnit(measureUnitCode, measureUnit);
        yield return toObjectArray();

        testCase = "Different quantity => false";
        baseMeasure = GetBaseMeasureChild();
        quantityTypeCode = Type.GetTypeCode(quantity.GetType());
        quantity = (ValueType)RandomParams.GetRandomQuantity(quantityTypeCode, quantity);
        yield return toObjectArray();

        testCase = "Equals when exchanged => true";
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
            TestCase_bool_Enum_ValueType_IQuantifiable args = new(testCase, isTrue, measureUnit, quantity, baseMeasure);

            return args.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetEqualsArgs()
    {
        testCase = "null, null => true";
        isTrue = true;
        baseMeasure = null;
        other = null;
        yield return toObjectArray();

        testCase = "not null, null => false";
        isTrue = false;
        measureUnit = RandomParams.GetRandomValidMeasureUnit();
        quantity = (ValueType)RandomParams.GetRandomQuantity();
        rateComponentCode = RandomParams.GetRandomRateComponentCode();
        limitMode = RandomParams.GetRandomNullableLimitMode();
        baseMeasure = GetCompleteBaseMeasureChild(this);
        yield return toObjectArray();

        testCase = "null, not null => false";
        baseMeasure = null;
        other = GetCompleteBaseMeasureChild(this);
        yield return toObjectArray();

        testCase = "same baseMeasures, same LimitModes => true";
        isTrue = true;
        baseMeasure = GetCompleteBaseMeasureChild(this);
        yield return toObjectArray();

        testCase = "Different LimitMode => false";
        isTrue = false;
        limitMode = RandomParams.GetRandomNullableLimitMode(limitMode);
        other = GetCompleteBaseMeasureChild(this);
        yield return toObjectArray();

        testCase = "Different RateComponentCode => false";
        baseMeasure = GetCompleteBaseMeasureChild(this);
        rateComponentCode = RandomParams.GetRandomRateComponentCode(rateComponentCode);
        other = GetCompleteBaseMeasureChild(this);
        yield return toObjectArray();

        testCase = "Different quantity => false";
        baseMeasure = GetCompleteBaseMeasureChild(this);
        quantityTypeCode = Type.GetTypeCode(quantity.GetType());
        quantity = (ValueType)RandomParams.GetRandomQuantity(quantityTypeCode, quantity);
        other = GetCompleteBaseMeasureChild(this);
        yield return toObjectArray();

        testCase = "Different measureUnit => false";
        baseMeasure = GetCompleteBaseMeasureChild(this);
        measureUnit = RandomParams.GetRandomValidMeasureUnit(measureUnit);
        other = GetCompleteBaseMeasureChild(this);
        yield return toObjectArray();

        testCase = "Equals when exchanged";
        isTrue = true;
        decimalQuantity = RandomParams.GetRandomDecimal();
        quantity = decimalQuantity;
        baseMeasure = GetCompleteBaseMeasureChild(this);
        decimalQuantity *= GetExchangeRate();
        measureUnit = RandomParams.GetRandomSameTypeValidMeasureUnit(measureUnit);
        decimalQuantity /= GetExchangeRate();
        quantity = decimalQuantity;
        other = GetCompleteBaseMeasureChild(this);
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            TestCase_IBaseMeasure_bool_IBaseMeasure args = new(testCase, baseMeasure, isTrue, other);

            return args.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetFitsInILimiterArgs()
    {
        testCase = "Not IBaseQuantifiable";
        measureUnit = RandomParams.GetRandomMeasureUnit();
        limiter = new LimiterObject();
        yield return toObjectArray();

        testCase = "Different MeasureUnitCode";
        measureUnitCode = GetMeasureUnitCode();
        measureUnitCode = RandomParams.GetRandomCustomMeasureUnitCode(measureUnitCode);
        limitMode = RandomParams.GetRandomLimitMode();
        limiter = GetLimiterQuantifiableObject(limitMode.Value, RandomParams.GetRandomMeasureUnit(measureUnitCode), defaultQuantity);
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            TestCase_Enum_ILimiter args = new(testCase, measureUnit, limiter);

            return args.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetFitsInIQuantifiableLimitModeArgs()
    {
        testCase = "IQuantifiable, Not defined limitMode";
        limitMode = SampleParams.NotDefinedLimitMode;
        measureUnit = RandomParams.GetRandomValidMeasureUnit();
        measureUnitCode = GetMeasureUnitCode();
        quantity = (ValueType)RandomParams.GetRandomQuantity();
        baseMeasure = GetBaseMeasureChild();
        yield return toObjectArray();

        testCase = "Different IQuantifiable, valid LimitMode";
        measureUnitCode = RandomParams.GetRandomConstantMeasureUnitCode(measureUnitCode);
        measureUnit = measureUnitCode.GetDefaultMeasureUnit();
        measureUnit = RandomParams.GetRandomMeasureUnit(measureUnitCode, measureUnit);
        limitMode = RandomParams.GetRandomLimitMode();
        yield return toObjectArray();

        testCase = "null, valid LimitMode";
        baseMeasure = null;
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            TestCase_Enum_LimitMode_IQuantifiable args = new(testCase, measureUnit, limitMode, baseMeasure);

            return args.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetGetBaseMeasureNullCheckArgs()
    {
        testCase = "null, null";
        paramName = ParamNames.baseMeasurement;
        baseMeasurement = null;
        yield return toObjectArray();

        testCase = "baseMeasurement, null";
        paramName = ParamNames.quantity;
        measureUnit = RandomParams.GetRandomMeasureUnit();
        baseMeasurement = GetBaseMeasurementChild(measureUnit);
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            TestCase_string_IBaseMeasurement args = new(testCase, paramName, baseMeasurement);

            return args.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetGetQuantityTypeCodeArgs()
    {
        testCase = "TypeCode.Empty => null";
        typeCode = TypeCode.Empty;
        obj = null;
        yield return toObjectArray();

        testCase = "TypeCode.Object => null";
        typeCode = TypeCode.Object;
        obj = new();
        yield return toObjectArray();

        foreach (TypeCode item in SampleParams.InvalidValueTypeCodes)
        {
            testCase = GetEnumName(item);
            obj = RandomParams.GetRandomValueType(item);
            yield return toObjectArray();
        }

        foreach (var item in BaseQuantifiable.QuantityTypeCodes)
        {
            testCase = GetEnumName(item);
            typeCode = item;
            obj = RandomParams.GetRandomValueType(item);
            yield return toObjectArray();
        }


        #region toObjectArray method
        object[] toObjectArray()
        {
            TestCase_TypeCode_object args = new(testCase, typeCode, obj);

            return args.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetIsExchangeableToArgs()
    {
        return GetIsExchangeableToArgs(RandomParams.GetRandomConstantMeasureUnit());
    }
    #endregion
}

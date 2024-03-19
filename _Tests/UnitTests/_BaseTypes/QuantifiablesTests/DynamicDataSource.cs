﻿namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.QuantifiablesTests;

internal class DynamicDataSource : DataFields
{
    #region Methods
    internal IEnumerable<object[]> GetEqualsArgsArrayList()
    {
        // null
        IQuantifiable quantifiable = null;
        measureUnit = RandomParams.GetRandomValidMeasureUnit();
        defaultQuantity = RandomParams.GetRandomDecimal();
        isTrue = false;
        yield return toObjectArray();

        // Different MeasureUnitCode
        measureUnitCode = RandomParams.GetRandomMeasureUnitCode(GetMeasureUnitCode(measureUnit));
        quantifiable = new QuantifiableChild(RootObject, null)
        {
            Return = new()
            {
                GetBaseMeasureUnit = RandomParams.GetRandomMeasureUnit(measureUnitCode),
                GetDefaultQuantity = defaultQuantity,
            }
        };
        yield return toObjectArray();

        // Same MeasureUnit, different defaultQuantity
        quantifiable = new QuantifiableChild(RootObject, null)
        {
            Return = new()
            {
                GetBaseMeasureUnit = measureUnit,
                GetDefaultQuantity = RandomParams.GetRandomDecimal(defaultQuantity),
            }
        };
        yield return toObjectArray();

        // Same MeasureUnit, same defaultQuantity
        quantifiable = new QuantifiableChild(RootObject, null)
        {
            Return = new()
            {
                GetBaseMeasureUnit = measureUnit,
                GetDefaultQuantity = defaultQuantity,
            }
        };
        isTrue = true;
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            Enum_Decimal_Bool_IQuantifiable_args item = new(measureUnit, defaultQuantity, isTrue, quantifiable);

            return item.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetExchangeToArgsArrayList()
    {
        // null
        measureUnit = RandomParams.GetRandomMeasureUnit();
        measureUnitCode = GetMeasureUnitCode(measureUnit);
        context = null;
        IQuantifiable quantifiable = null;
        yield return toObjectArray();

        // not measureUnit Enum
        context = TypeCode.Empty;
        yield return toObjectArray();

        // same type measureUnit
        context = RandomParams.GetRandomMeasureUnit(measureUnitCode);
        quantifiable = new QuantifiableChild(RootObject, paramName)
        {
            Return = new()
            {
                GetBaseMeasureUnit = context,
                GetDefaultQuantity = RandomParams.GetRandomDecimal(),
            }
        };
        yield return toObjectArray();

        // different type measureUnit
        measureUnitCode = RandomParams.GetRandomMeasureUnitCode(measureUnitCode);
        context = RandomParams.GetRandomMeasureUnit(measureUnitCode);
        quantifiable = null;
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            Enum_Enum_IQuantifiable_args item = new(measureUnit, context, quantifiable);

            return item.ToObjectArray();
        }
        #endregion
    }
    #endregion
}

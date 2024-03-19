namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.QuantifiablesTests;

internal class DynamicDataSource : DataFields
{
    #region Methods
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

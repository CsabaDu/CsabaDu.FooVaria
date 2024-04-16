namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.SpreadsTests;

internal sealed class DynamicDataSource : CommonDynamicDataSource
{
    #region Methods
    internal IEnumerable<object[]> GetIsExchangeableToArgs()
    {
        measureUnitCode = RandomParams.GetRandomSpreadMeasureUnitCode();

        return GetIsExchangeableToArgs(RandomParams.GetRandomMeasureUnit(measureUnitCode));
    }

    internal IEnumerable<object[]> GetGetSpreadMeasureArgs()
    {
        measureUnit = RandomParams.GetRandomSpreadMeasureUnit();
        spreadMeasure = null;
        IQuantifiable quantifiable = null;
        yield return toObjectArray();


        doubleQuantity = 0;
        quantifiable = GetSpreadMeasureBaseMeasureObject(measureUnit, doubleQuantity);
        yield return toObjectArray();

        doubleQuantity = RandomParams.GetRandomNegativeDouble();
        quantifiable = GetSpreadMeasureBaseMeasureObject(measureUnit, doubleQuantity);
        yield return toObjectArray();

        doubleQuantity = RandomParams.GetRandomPositiveDouble();
        quantifiable = BaseMeasureChild.GetBaseMeasureChild(measureUnit, doubleQuantity);
        yield return toObjectArray();

        spreadMeasure = GetSpreadMeasureBaseMeasureObject(measureUnit, doubleQuantity);
        quantifiable = spreadMeasure as IQuantifiable;
        yield return toObjectArray();

        spreadMeasure = null;
        measureUnitCode = GetMeasureUnitCode(measureUnit);
        measureUnitCode = RandomParams.GetRandomMeasureUnitCode(measureUnitCode);
        measureUnit = RandomParams.GetRandomMeasureUnit(measureUnitCode);
        quantifiable = GetSpreadMeasureBaseMeasureObject(measureUnit, doubleQuantity);
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            Enum_ISpreadMeasure_IQuantifiable_args item = new(measureUnit, spreadMeasure, quantifiable);

            return item.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetValidateSpreadMeasureArgs()
    {
        measureUnit = RandomParams.GetRandomSpreadMeasureUnit();
        spreadMeasure = new SpreadMeasureObject();
        yield return toObjectArray();

        doubleQuantity = 0;
        spreadMeasure = GetSpreadMeasureBaseMeasureObject(measureUnit, doubleQuantity);
        yield return toObjectArray();

        doubleQuantity = RandomParams.GetRandomNegativeDouble();
        spreadMeasure = GetSpreadMeasureBaseMeasureObject(measureUnit, doubleQuantity);
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            Enum_ISpreadMeasure_args item = new(measureUnit, spreadMeasure);

            return item.ToObjectArray();
        }
        #endregion
    }
    #endregion
}

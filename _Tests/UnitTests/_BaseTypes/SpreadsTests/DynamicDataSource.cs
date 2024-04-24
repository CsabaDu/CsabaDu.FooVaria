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
        testCase = "null => null";
        spreadMeasure = null;
        measureUnit = RandomParams.GetRandomSpreadMeasureUnit();
        IQuantifiable quantifiable = null;
        yield return toObjectArray();

        testCase = "Zero quantity => null";
        doubleQuantity = 0;
        quantifiable = SpreadMeasureBaseMeasureObject.GetSpreadMeasureBaseMeasureObject(measureUnit, doubleQuantity);
        yield return toObjectArray();

        testCase = "Negative quantity => null";
        doubleQuantity = RandomParams.GetRandomNegativeDouble();
        quantifiable = SpreadMeasureBaseMeasureObject.GetSpreadMeasureBaseMeasureObject(measureUnit, doubleQuantity);
        yield return toObjectArray();

        testCase = "Not ISpreadMeasure IQuantifiable => null";
        doubleQuantity = RandomParams.GetRandomPositiveDouble();
        quantifiable = BaseMeasureChild.GetBaseMeasureChild(measureUnit, doubleQuantity);
        yield return toObjectArray();

        testCase = "ISpreadMeasure IQuantifiable => SpreadMeasure";
        spreadMeasure = SpreadMeasureBaseMeasureObject.GetSpreadMeasureBaseMeasureObject(measureUnit, doubleQuantity);
        quantifiable = spreadMeasure as IQuantifiable;
        yield return toObjectArray();

        testCase = "Different MeasureUnitCode => null";
        spreadMeasure = null;
        measureUnitCode = GetMeasureUnitCode();
        measureUnitCode = RandomParams.GetRandomMeasureUnitCode(measureUnitCode);
        measureUnit = RandomParams.GetRandomMeasureUnit(measureUnitCode);
        quantifiable = SpreadMeasureBaseMeasureObject.GetSpreadMeasureBaseMeasureObject(measureUnit, doubleQuantity);
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            TestCase_Enum_ISpreadMeasure_IQuantifiable args = new(testCase, measureUnit, spreadMeasure, quantifiable);

            return args.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetValidateSpreadMeasureArgs()
    {
        testCase = "Not IBaseMeasure";
        measureUnit = RandomParams.GetRandomSpreadMeasureUnit();
        spreadMeasure = new SpreadMeasureObject();
        yield return toObjectArray();

        testCase = "Zero quantity";
        doubleQuantity = 0;
        spreadMeasure = SpreadMeasureBaseMeasureObject.GetSpreadMeasureBaseMeasureObject(measureUnit, doubleQuantity);
        yield return toObjectArray();

        testCase = "Negative quantity";
        doubleQuantity = RandomParams.GetRandomNegativeDouble();
        spreadMeasure = SpreadMeasureBaseMeasureObject.GetSpreadMeasureBaseMeasureObject(measureUnit, doubleQuantity);
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            TestCase_Enum_ISpreadMeasure args = new(testCase, measureUnit, spreadMeasure);

            return args.ToObjectArray();
        }
        #endregion
    }
    #endregion
}

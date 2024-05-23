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
        yield return argsToObjectArray();

        testCase = "Zero quantity => null";
        quantity = 0.0;
        quantifiable = GetSpreadMeasureBaseMeasureObject(this);
        yield return argsToObjectArray();

        testCase = "Negative quantity => null";
        quantity = RandomParams.GetRandomNegativeDouble();
        quantifiable = GetSpreadMeasureBaseMeasureObject(this);
        yield return argsToObjectArray();

        testCase = "Not ISpreadMeasure IBaseMeasure => null";
        quantity = RandomParams.GetRandomPositiveDouble();
        quantifiable = GetBaseMeasureChild(this);
        yield return argsToObjectArray();

        testCase = "Not ISpreadMeasure IQuantifiable => null";
        defaultQuantity = (decimal)quantity.ToQuantity(TypeCode.Decimal);
        quantifiable = GetQuantifiableChild(this);
        yield return argsToObjectArray();

        testCase = "ISpreadMeasure IQuantifiable => SpreadMeasure";
        spreadMeasure = GetSpreadMeasureBaseMeasureObject(this);
        quantifiable = spreadMeasure as IQuantifiable;
        yield return argsToObjectArray();

        testCase = "Different MeasureUnitCode => null";
        spreadMeasure = null;
        measureUnit = RandomParams.GetRandomSpreadMeasureUnit(GetMeasureUnitCode());
        yield return argsToObjectArray();

        #region argsToObjectArray method
        object[] argsToObjectArray()
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
        yield return argsToObjectArray();

        testCase = "Zero quantity";
        quantity = 0.0;
        spreadMeasure = GetSpreadMeasureBaseMeasureObject(this);
        yield return argsToObjectArray();

        testCase = "Negative quantity";
        quantity = RandomParams.GetRandomNegativeDouble();
        spreadMeasure = GetSpreadMeasureBaseMeasureObject(this);
        yield return argsToObjectArray();

        #region argsToObjectArray method
        object[] argsToObjectArray()
        {
            TestCase_Enum_ISpreadMeasure args = new(testCase, measureUnit, spreadMeasure);

            return args.ToObjectArray();
        }
        #endregion
    }
    #endregion
}

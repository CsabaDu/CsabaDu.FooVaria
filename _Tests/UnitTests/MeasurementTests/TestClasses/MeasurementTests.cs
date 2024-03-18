namespace CsabaDu.FooVaria.Tests.UnitTests.MeasurementsTests.TestClasses;

[TestClass, TestCategory("UnitTest")]
public sealed class MeasurementTests
{
    //#region Initialize
    //[ClassInitialize]
    //public static void InitializeBaseMeasurementTestsClass(TestContext context)
    //{
    //    DynamicDataSource = new();
    //}

    //[TestInitialize]
    //public void InitializeBaseMeasurementTests()
    //{
    //    measureUnit = RandomParams.GetRandomValidMeasureUnit();
    //    measureUnitCode = GetMeasureUnitCode(measureUnit);
    //    measureUnitType = measureUnit.GetType();
    //}

    //[TestCleanup]
    //public void CleanupBaseMeasurementTests()
    //{
    //    paramName = null;
    //    _baseMeasurement = null;

    //    TestSupport.RestoreConstantExchangeRates();
    //}
    //#endregion

    #region Private fields
    private MeasurementChild _Measurement;
    private Enum _measureUnit;
    private MeasureUnitCode _measureUnitCode;
    private Type _measureUnitType;
    private string _paramName;

    #region Readonly fields
    private readonly RandomParams RandomParams = new();
    private readonly RootObject RootObject = new();
    #endregion

    #region Static fields
    //private static DynamicDataSource DynamicDataSource;
    #endregion
    #endregion

    #region Test methods

    #region Tested in parent classes' tests

    // int IComparable<IBaseMeasurement>.CompareTo(IBaseMeasurement? other)
    // bool IEquatable<IBaseMeasurement>.Equals(IBaseMeasurement other)
    // IBaseMeasurement IBaseMeasurement.GetBaseMeasurement(Enum context)
    // IDictionary<object, decimal> IConstantExchangeRateCollection.GetConstantExchangeRateCollection()
    // Enum IDefaultMeasureUnit.GetDefaultMeasureUnit()
    // IEnumerable<string> IDefaultMeasureUnit.GetDefaultMeasureUnitNames()
    // decimal IExchangeRate.GetExchangeRate()
    // IDictionary<object, decimal> IExchangeRateCollection.GetExchangeRateCollection()
    // Type IMeasureUnit.GetMeasureUnitType()
    // TypeCode IQuantityType.GetQuantityTypeCode()
    // bool IMeasureUnitCode.HasMeasureUnitCode(MeasureUnitCode measureUnitCode)
    // bool IExchangeable<Enum>.IsExchangeableTo(Enum context)
    // decimal IProportional<IBaseMeasurement>.ProportionalTo(IBaseMeasurement other)
    // void IExchangeRate.ValidateExchangeRate(decimal exchangeRate, string paramName)
    // void IDefaultMeasureUnit.ValidateMeasureUnit(Enum measureUnit, string paramName)
    // void IMeasurable.ValidateMeasureUnitCode(IMeasurable measurable, string paramName)
    // void IMeasureUnitCode.ValidateMeasureUnitCode(MeasureUnitCode measureUnitCode, string paramName)

    #endregion

    // Measurement(IMeasurementFactory factory, Enum measureUnit)
    // Enum IMeasureUnit.GetBaseMeasureUnit()
    // string? ICustomNameCollection.GetCustomName()
    // IDictionary<object, string> ICustomNameCollection.GetCustomNameCollection()
    // IMeasurement IDefaultMeasurable<IMeasurement>.GetDefault()
    // IMeasurable? IDefaultMeasurable.GetDefault(MeasureUnitCode measureUnitCode)
    // string IMeasureUnitCollection.GetDefaultName()
    // IFactory ICommonBase.GetFactory()
    // IMeasurement IMeasurement.GetMeasurement(Enum measureUnit)
    // IMeasurement IMeasurement.GetMeasurement(IMeasurement other)
    // IMeasurement? IMeasurement.GetMeasurement(string customName, MeasureUnitCode measureUnitCode, decimal exchangeRate)
    // IMeasurement? IMeasurement.GetMeasurement(Enum measureUnit, decimal exchangeRate, string customName)
    // IMeasurement IMeasurement.GetMeasurement(string name)
    // MeasureUnitCode IMeasureUnitCode.GetMeasureUnitCode()
    // string INamed.GetName()
    // void ICustomNameCollection.SetCustomName(string customName)
    // void ICustomNameCollection.SetOrReplaceCustomName(string customName)
    // bool IMeasurement.TryGetMeasurement(decimal exchangeRate, out IMeasurement? measurement)
    // bool ICustomNameCollection.TrySetCustomName(string? customName)

    #endregion

    //#region Private methods
    //private void SetBaseMeasurementChild(Enum measureUnit, IBaseMeasurementFactory factory, string measureUnitName)
    //{
    //    _baseMeasurement = new(RootObject, paramName)
    //    {
    //        Return = new()
    //        {
    //            GetBaseMeasureUnit = measureUnit,
    //            GetFactory = factory,
    //            GetName = measureUnitName,
    //        }
    //    };
    //}

    //#region DynamicDataSource
    //private static IEnumerable<object[]> GetEqualsObjectArgArrayList()
    //{
    //    return DynamicDataSource.GetEqualsObjectArgArrayList();
    //}

    //private static IEnumerable<object[]> GetEqualsBaseMeasurementArgArrayList()
    //{
    //    return DynamicDataSource.GetEqualsBaseMeasurementArgArrayList();
    //}

    //private static IEnumerable<object[]> GetExchangeRateCollectionArgArrayList()
    //{
    //    return DynamicDataSource.GetExchangeRateCollectionArgArrayList();
    //}

    //private static IEnumerable<object[]> GetIsExchangeableToArgArrayList()
    //{
    //    return DynamicDataSource.GetIsExchangeableToArgArrayList();
    //}

    //private static IEnumerable<object[]> GetValidateExchangeRateArgArrayList()
    //{
    //    return DynamicDataSource.GetValidateExchangeRateArgArrayList();
    //}

    //private static IEnumerable<object[]> GetValidateMeasureUnitValidArgsArrayList()
    //{
    //    return DynamicDataSource.GetValidateMeasureUnitValidArgsArrayList();
    //}
    //#endregion
    //#endregion
}
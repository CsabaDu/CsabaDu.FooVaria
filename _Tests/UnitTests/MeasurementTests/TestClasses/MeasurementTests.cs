//namespace CsabaDu.FooVaria.Tests.UnitTests.MeasurementsTests.TestClasses;

//[TestClass, TestCategory("UnitTest")]
//public sealed class MeasurementTests
//{
//    //#region Initialize
//    //[ClassInitialize]
//    //public static void InitializeBaseMeasurementTestsClass(TestContext context)
//    //{
//    //    DynamicDataSource = new();
//    //}

//    //[TestInitialize]
//    //public void InitializeBaseMeasurementTests()
//    //{
//    //    measureUnit = RandomParams.GetRandomValidMeasureUnit();
//    //    measureUnitCode = GetMeasureUnitCode(measureUnit);
//    //    measureUnitType = measureUnit.GetType();
//    //}

//    //[TestCleanup]
//    //public void CleanupBaseMeasurementTests()
//    //{
//    //    paramName = null;
//    //    _baseMeasurement = null;

//    //    TestSupport.RestoreConstantExchangeRates();
//    //}
//    //#endregion

//    #region Private fields
//    private MeasurementChild _measurement;

//    #region Readonly fields
//    private readonly Fields Fields = new();
//    #endregion

//    #region Static fields
//    //private static DynamicDataSource DynamicDataSource;
//    #endregion
//    #endregion

//    #region Test methods

//    #region Tested in parent classes' tests

//    // int IComparable<IBaseMeasurement>.CompareTo(IBaseMeasurement? other)
//    // bool IEquatable<IBaseMeasurement>.Equals(IBaseMeasurement other)
//    // IBaseMeasurement IBaseMeasurement.GetBaseMeasurementReturnValue(Enum context)
//    // IDictionary<object, decimal> IConstantExchangeRateCollection.GetConstantExchangeRateCollection()
//    // Enum IDefaultMeasureUnit.GetDefaultMeasureUnit()
//    // IEnumerable<string> IDefaultMeasureUnit.GetDefaultMeasureUnitNames()
//    // decimal IExchangeRate.GetExchangeRate()
//    // IDictionary<object, decimal> IExchangeRateCollection.GetExchangeRateCollection()
//    // Type IMeasureUnit.GetMeasureUnitType()
//    // TypeCode IQuantityType.GetQuantityTypeCode()
//    // bool IMeasureUnitCode.HasMeasureUnitCode(RateComponentCode measureUnitCode)
//    // bool IExchangeable<Enum>.IsExchangeableTo(Enum context)
//    // decimal IProportional<IBaseMeasurement>.ProportionalTo(IBaseMeasurement other)
//    // void IExchangeRate.ValidateExchangeRate(decimal decimalQuantity, string paramName)
//    // void IDefaultMeasureUnit.ValidateMeasureUnit(Enum measureUnit, string paramName)
//    // void IMeasurable.ValidateMeasureUnitCode(IMeasurable measurable, string paramName)
//    // void IMeasureUnitCode.ValidateMeasureUnitCode(RateComponentCode measureUnitCode, string paramName)

//    #endregion

//    // Measurement(IMeasurementFactory factory, Enum measureUnit)
//    // Enum IMeasureUnit.GetBaseMeasureUnitReturnValue()
//    // string? ICustomNameCollection.GetCustomName()
//    // IDictionary<object, string> ICustomNameCollection.GetCustomNameCollection()
//    // IMeasurement IDefaultMeasurable<IMeasurement>.GetDefault()
//    // IMeasurable? IDefaultMeasurable.GetDefault(RateComponentCode measureUnitCode)
//    // string IMeasureUnitCollection.GetDefaultName()
//    // IFactory ICommonBase.GetFactoryReturnValue()
//    // IMeasurement IMeasurement.GetMeasurement(Enum measureUnit)
//    // IMeasurement IMeasurement.GetMeasurement(IMeasurement other)
//    // IMeasurement? IMeasurement.GetMeasurement(string customName, RateComponentCode measureUnitCode, decimal decimalQuantity)
//    // IMeasurement? IMeasurement.GetMeasurement(Enum measureUnit, decimal decimalQuantity, string customName)
//    // IMeasurement IMeasurement.GetMeasurement(string name)
//    // RateComponentCode IMeasureUnitCode.GetMeasureUnitCode()
//    // string INamed.GetNameReturnValue()
//    // void ICustomNameCollection.SetCustomName(string customName)
//    // void ICustomNameCollection.SetOrReplaceCustomName(string customName)
//    // bool IMeasurement.TryGetMeasurement(decimal decimalQuantity, out IMeasurement? measurement)
//    // bool ICustomNameCollection.TrySetCustomName(string? customName)

//    #endregion

//    //#region Private methods
//    //private void SetBaseMeasurementChild(Enum measureUnit, IBaseMeasurementFactory factory, string measureUnitName)
//    //{
//    //    _baseMeasurement = new(RootObject, paramName)
//    //    {
//    //        ReturnValues = new()
//    //        {
//    //            GetBaseMeasureUnitReturnValue = measureUnit,
//    //            GetFactoryReturnValue = factory,
//    //            GetNameReturnValue = measureUnitName,
//    //        }
//    //    };
//    //}

//    //#region DynamicDataSource
//    //private static IEnumerable<object[]> GetEqualsObjectArg()
//    //{
//    //    return DynamicDataSource.GetEqualsObjectArg();
//    //}

//    //private static IEnumerable<object[]> GetEqualsBaseMeasurementArg()
//    //{
//    //    return DynamicDataSource.GetEqualsBaseMeasurementArg();
//    //}

//    //private static IEnumerable<object[]> GetExchangeRateCollectionArg()
//    //{
//    //    return DynamicDataSource.GetExchangeRateCollectionArg();
//    //}

//    //private static IEnumerable<object[]> GetIsExchangeableToArg()
//    //{
//    //    return DynamicDataSource.GetIsExchangeableToArg();
//    //}

//    //private static IEnumerable<object[]> GetValidateExchangeRateArg()
//    //{
//    //    return DynamicDataSource.GetValidateExchangeRateArg();
//    //}

//    //private static IEnumerable<object[]> GetValidateMeasureUnitValidArgs()
//    //{
//    //    return DynamicDataSource.GetValidateMeasureUnitValidArgs();
//    //}
//    //#endregion
//    //#endregion
//}
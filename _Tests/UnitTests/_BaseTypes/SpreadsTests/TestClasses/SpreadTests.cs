using CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.BehaviorObjects;
using CsabaDu.FooVaria.Tests.TestHelpers.Params;
using CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.BaseMeasuresTests.Fakes;
using System.ComponentModel;

namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.SpreadsTests.TestClasses;

[TestClass, TestCategory("UnitTest")]
public sealed class SpreadTests
{
    #region Tested in parent classes' tests

    // int IComparable<IQuantifiable>.CompareTo(IQuantifiable? other)
    // bool IEquatable<IQuantifiable>.Equals(IQuantifiable? other)
    // bool? ILimitable.FitsIn(ILimiter? limiter)
    // bool? IFit<IQuantifiable>.FitsIn(IQuantifiable? other, LimitMode? limitMode)
    // decimal IDecimalQuantity.GetDecimalQuantity()
    // Enum IDefaultMeasureUnit.GetDefaultMeasureUnit()
    // IEnumerable<string> IDefaultMeasureUnit.GetDefaultMeasureUnitNames()
    // decimal IDefaultQuantity.GetDefaultQuantity()
    // IFactory ICommonBase.GetFactory()
    // MeasureUnitCode IMeasureUnitCode.GetMeasureUnitCode()
    // Type IMeasureUnit.GetMeasureUnitType()
    // IQuantifiable IQuantifiable.GetQuantifiable(MeasureUnitCode measureUnitCode, decimal defaultQuantity)
    // object IRound<IQuantifiable>.GetQuantity(RoundingMode roundingMode)
    // object IQuantity.GetQuantity(TypeCode quantityTypeCode)
    // bool IMeasureUnitCode.HasMeasureUnitCode(MeasureUnitCode measureUnitCode)
    // decimal IProportional<IQuantifiable>.ProportionalTo(IQuantifiable? other)
    // bool ITryExchange<IQuantifiable, Enum>.TryExchangeTo(Enum context, out IQuantifiable? exchanged)
    // void IDefaultMeasureUnit.ValidateMeasureUnit(Enum? measureUnit, string paramName)
    // void IMeasureUnitCode.ValidateMeasureUnitCode(MeasureUnitCode measureUnitCode, string paramName)
    // void IMeasurable.ValidateMeasureUnitCode(IMeasurable? measurable, string paramName)
    // void IBaseQuantifiable.ValidateQuantity(ValueType? quantity, string paramName)

    #endregion

    #region Private fields
    private SpreadChild _spread;
    private ISpreadMeasure _spreadMeasure;

    #region Readonly fields
    private readonly DataFields Fields = new();
    #endregion

    #region Static fields
    private static DynamicDataSource DynamicDataSource;
    #endregion
    #endregion

    #region Initialize
    [ClassInitialize]
    public static void ClassInitialize(TestContext context)
    {
        DynamicDataSource = new();
    }

    [TestInitialize]
    public void TestInitialize()
    {
        Fields.SetBaseMeasureFields(
            Fields.RandomParams.GetRandomSpreadMeasureUnitCode(),
            Fields.RandomParams.GetRandomPositiveDouble());
    }

    [TestCleanup]
    public void TestCleanup()
    {
        Fields.paramName = null;
        _spreadMeasure = null;
    }
    #endregion

    #region Test methods
    #region Enum GetBaseMeasureUnit
    #region override IMeasureUnit.GetBaseMeasureUnit()
    [TestMethod, TestCategory("UnitTest")]
    public void GetBaseMeasureUnit_returns_expected()
    {
        // Arrange
        SetSpreadChild();

        Enum expected = Fields.measureUnit;

        // Act
        var actual = _spread.GetBaseMeasureUnit();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region ValueType GetBaseQuantity
    #region override sealed IQuantity.GetBaseQuantity()
    [TestMethod, TestCategory("UnitTest")]
    public void GetBaseQuantity_returns_expected()
    {
        // Arrange
        SetSpreadChild();

        ValueType expected = Fields.quantity;

        // Act
        var actual = _spread.GetBaseQuantity();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region double GetQuantity
    #region virtual IQuantity<double>.GetQuantity()
    [TestMethod, TestCategory("UnitTest")]
    public void GetQuantity_returns_expected()
    {
        // Arrange
        SetSpreadChild();

        double expected = Fields.doubleQuantity;

        // Act
        var actual = _spread.GetQuantity();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region TypeCode GetQuantityTypeCode
    #region IQuantityType.GetQuantityTypeCode()
    [TestMethod, TestCategory("UnitTest")]
    public void GetQuantityTypeCode_returns_expected()
    {
        // Arrange
        SetSpreadChild();

        TypeCode expected = Fields.quantityTypeCode;

        // Act
        var actual = _spread.GetQuantityTypeCode();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region ISpread GetSpread
    #region ISpread.GetSpread(ISpreadMeasure)
    [TestMethod, TestCategory("UnitTest")]
    public void GetSpread_nullArg_ISpreadMeasure_throws_ArgumentNullException()
    {
        // Arrange
        SetSpreadChild();

        _spreadMeasure = null;

        // Act
        void attempt() =>_ = _spread.GetSpread(_spreadMeasure);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.spreadMeasure, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void GetSpread_invalidArg_ISpreadMeasure_throws_ArgumentNullException() // TODO
    {
        // Arrange
        SetSpreadChild();

        _spreadMeasure = null;

        // Act
        void attempt() => _ = _spread.GetSpread(_spreadMeasure);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.spreadMeasure, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void GetSpread_validArg_ISpreadMeasure_returns_expected()
    {
        // Arrange
        SetCompleteSpreadChild();

        Fields.measureUnit = Fields.RandomParams.GetRandomMeasureUnit(Fields.measureUnitCode);
        Fields.quantity = Fields.RandomParams.GetRandomPositiveDouble();
        _spreadMeasure = GetSpreadMeasureBaseMeasureObject();
        ISpread expected = GetSpreadChild(new SpreadFactoryObject());

        // Act
        var actual = _spread.GetSpread(_spreadMeasure);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region ISpreadMeasure GetSpreadMeasure
    #region abstract ISpreadMeasure.GetSpreadMeasure()
    [TestMethod, TestCategory("UnitTest")]
    public void GetSpreadMeasure_returns_expected()
    {
        // Arrange
        SetSpreadChild();

        ISpreadMeasure expected = GetSpreadMeasureBaseMeasureObject();

        // Act
        var actual = _spread.GetSpreadMeasure();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion

    #region ISpread.GetSpreadMeasure(IQuantifiable?)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetGetSpreadMeasureArgs), DynamicDataSourceType.Method)]
    public void GetSpreadMeasure_arg_ISpreadMeasure_returns_expected(Enum measureUnit, ISpreadMeasure expected, IQuantifiable quantifiable)
    {
        // Arrange
        SetSpreadChild(measureUnit, Fields.quantity);

        Fields.paramName = Fields.RandomParams.GetRandomParamName();

        // Act
        var actual = _spread.GetSpreadMeasure(quantifiable);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region bool IsExchangeableTo
    #region override sealed IExchangeable<Enum>.IsExchangeableTo(Enum?)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetIsExchangeableToArgs), DynamicDataSourceType.Method)]
    public void IsExchangeableTo_arg_Enum_returns_expected(bool expected, Enum measureUnit, Enum context)
    {
        // Arrange
        SetSpreadChild(measureUnit, Fields.quantity);

        // Act
        var actual = _spread.IsExchangeableTo(context);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region IQuantifiable Round
    #region override sealed IRound<IQuantifiable>.Round(RoundingMode)
    [TestMethod, TestCategory("UnitTest")]
    public void Round_invalidArg_RoundingMode_throws_InvalidEnumArgumentException()
    {
        // Arrange
        SetSpreadChild();

        Fields.roundingMode = SampleParams.NotDefinedRoundingMode;

        // Act
        void attempt() => _ = _spread.Round(Fields.roundingMode);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(ParamNames.roundingMode, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Round_validArg_RoundingMode_returns_expected()
    {
        // Arrange
        SetCompleteSpreadChild();

        Fields.roundingMode = Fields.RandomParams.GetRandomRoundingMode();
        Fields.quantity = (ValueType)Fields.quantity.Round(Fields.roundingMode);
        IQuantifiable expected = GetCompleteSpreadChild();

        // Act
        var actual = _spread.Round(Fields.roundingMode);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region void ValidateSpreadMeasure
    #region ISpreadMeasure.ValidateSpreadMeasure(ISpreadMeasure?, string)
    [TestMethod, TestCategory("UnitTest")]
    public void ValidateSpreadMeasure_nullArg_ISpreadMeasure_throws_ArgumentNullException()
    {
        // Arrange
        SetSpreadChild();

        _spreadMeasure = null;
        Fields.paramName = Fields.RandomParams.GetRandomParamName();

        // Act
        void attempt() => _spread.ValidateSpreadMeasure(_spreadMeasure, Fields.paramName);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(Fields.paramName, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetValidateSpreadMeasureArgs), DynamicDataSourceType.Method)]
    public void ValidateSpreadMeasure_invalidArg_ISpreadMeasure_throws_ArgumentOutOfRangeException(Enum measureUnit, ISpreadMeasure spreadMeasure)
    {
        // Arrange
        SetSpreadChild(measureUnit, Fields.quantity);

        Fields.paramName = Fields.RandomParams.GetRandomParamName();

        // Act
        void attempt() => _spread.ValidateSpreadMeasure(spreadMeasure, Fields.paramName);

        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(attempt);
        Assert.AreEqual(Fields.paramName, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void ValidateSpreadMeasure_invalidArg_ISpreadMeasure_throws_InvalidEnumArgumentException()
    {
        // Arrange
        SetSpreadChild();

        Fields.measureUnitCode = SampleParams.GetOtherSpreadMeasureUnitCode(Fields.measureUnitCode);
        Fields.measureUnit = Fields.RandomParams.GetRandomMeasureUnit(Fields.measureUnitCode);
        _spreadMeasure = GetSpreadMeasureBaseMeasureObject(); 
        Fields.paramName = Fields.RandomParams.GetRandomParamName();

        // Act
        void attempt() => _spread.ValidateSpreadMeasure(_spreadMeasure, Fields.paramName);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(Fields.paramName, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void ValidateSpreadMeasure_validArg_ISpreadMeasure_returns()
    {
        // Arrange
        SetSpreadChild();

        Fields.measureUnit = Fields.RandomParams.GetRandomMeasureUnit(Fields.measureUnitCode);
        Fields.quantity = Fields.RandomParams.GetRandomPositiveDouble();
        _spreadMeasure = GetSpreadMeasureBaseMeasureObject();

        // Act
        void attempt() => _spread.ValidateSpreadMeasure(_spreadMeasure, Fields.paramName);

        // Assert
        Assert.IsTrue(DoesNotThrowException(attempt));
    }
    #endregion
    #endregion
    #endregion
    //    //#region Static methods

    //    //#endregion
    //    #endregion

    #region Private methods
    private void SetSpreadChild(Enum measureUnit, ValueType quantity, ISpreadFactory factory = null, RateComponentCode? rateComponentCode = null)
    {
        _spread = SpreadChild.GetSpreadChild(measureUnit, quantity, factory, rateComponentCode);
    }

    private void SetSpreadChild(ISpreadMeasure spreadMeasure, ISpreadFactory factory = null)
    {
        _spread = SpreadChild.GetSpreadChild(spreadMeasure, factory);
    }


    private void SetSpreadChild()
    {
        SetSpreadChild(Fields.measureUnit, Fields.quantity);
    }

    private void SetCompleteSpreadChild()
    {
        Fields.rateComponentCode = Fields.RandomParams.GetRandomRateComponentCode();

        SetSpreadChild(Fields.measureUnit, Fields.quantity, new SpreadFactoryObject(), Fields.rateComponentCode);
    }

    private SpreadChild GetSpreadChild(ISpreadFactory factory = null)
    {
        return SpreadChild.GetSpreadChild(Fields.measureUnit, Fields.quantity, factory);
    }


    private SpreadChild GetCompleteSpreadChild(RateComponentCode? rateComponentCode = null)
    {
        return SpreadChild.GetSpreadChild(Fields.measureUnit, Fields.quantity, new SpreadFactoryObject(), rateComponentCode ?? RateComponentCode.Numerator);
    }

    private ISpreadMeasure GetSpreadMeasureBaseMeasureObject()
    {
        return SpreadMeasureBaseMeasureObject.GetSpreadMeasureBaseMeasureObject(Fields.measureUnit, Fields.quantity);
    }

    #region DynamicDataSource
    private static IEnumerable<object[]> GetIsExchangeableToArgs()
    {
        return DynamicDataSource.GetIsExchangeableToArgs();
    }

    private static IEnumerable<object[]> GetGetSpreadMeasureArgs()
    {
        return DynamicDataSource.GetGetSpreadMeasureArgs();
    }

    private static IEnumerable<object[]> GetValidateSpreadMeasureArgs()
    {
        return DynamicDataSource.GetValidateSpreadMeasureArgs();
    }
    #endregion
    #endregion
}
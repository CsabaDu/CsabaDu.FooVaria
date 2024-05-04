namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.BaseQuantifiablesTests.TestClasses;

[TestClass, TestCategory("UnitTest")]
public sealed class BaseQuantifiableTests
{
    #region Tested in parent classes' tests

    // BaseQuantifiable(IRootObject rootObject, string paramName)
    // Enum IMeasureUnit.GetBaseMeasureUnitValue()
    // Enum IDefaultMeasureUnit.GetDefaultMeasureUnit()
    // IEnumerable<string> IDefaultMeasureUnit.GetDefaultMeasureUnitNames()
    // IFactory ICommonBase.GetFactoryValue()
    // MeasureUnitCode IMeasureUnitCode.GetMeasureUnitCode()
    // Type IMeasureUnit.GetMeasureUnitType()
    // TypeCode IQuantityType.GetQuantityTypeCode()
    // bool IMeasureUnitCode.HasMeasureUnitCode(MeasureUnitCode measureUnitCode)
    // void IDefaultMeasureUnit.ValidateMeasureUnit(Enum measureUnit, string paramName)
    // void IMeasurable.ValidateMeasureUnitCode(IMeasurable measurable, string paramName)
    // void IMeasureUnitCode.ValidateMeasureUnitCode(MeasureUnitCode measureUnitCode, string paramName)

    #endregion

    #region Private fields
    private BaseQuantifiableChild _baseQuantifiable;
    private RandomParams _randomParams;

    #region Readonly fields
    private readonly DataFields Fields = new();
    #endregion

    #region Static fields
    private static DynamicDataSource DynamicDataSource = new();
    private const string DisplayName = nameof(GetDisplayName);
    #endregion
    #endregion

    #region Initialize
    [TestInitialize]
    public void TestInitialize()
    {
        _randomParams = Fields.RandomParams;

        Fields.defaultQuantity = _randomParams.GetRandomDecimal();

        Fields.SetMeasureUnit(_randomParams.GetRandomMeasureUnit());
    }

    [TestCleanup]
    public void TestCleanup()
    {
        Fields.paramName = null;
    }

    public static string GetDisplayName(MethodInfo methodInfo, object[] args)
    {
        return CommonDynamicDataSource.GetDisplayName(methodInfo, args);
    }
    #endregion

    #region Test methods
    #region bool Equals
    #region override BaseQuantifiable.Equals(object?)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetEqualsArgs), DynamicDataSourceType.Method, DynamicDataDisplayName = DisplayName)]
    public void Equals_arg_object_returns_expected(string testCase, bool expected, object obj, Enum measureUnit, decimal defaultQuantity)
    {
        // Arrange
        SetBaseQuantifiableChild(measureUnit, defaultQuantity);

        // Act
        var actual = _baseQuantifiable.Equals(obj);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region bool? FitsIn
    #region virtual ILimitable.FitsIn(ILimiter?)
    [TestMethod, TestCategory("UnitTest")]
    public void FitsIn_nullArg_ILimiter_returns_expected()
    {
        // Arrange
        SetBaseQuantifiableChild();

        ILimiter limiter = null;

        // Act
        var actual = _baseQuantifiable.FitsIn(limiter);

        // Assert
        Assert.IsTrue(actual);
    }

    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetFitsInArgs), DynamicDataSourceType.Method, DynamicDataDisplayName = DisplayName)]
    public void FitsIn_invalidArg_ILimiter_returns_null(string testCase, Enum measureUnit, ILimiter limiter)
    {
        // Arrange
        SetBaseQuantifiableChild(measureUnit, Fields.defaultQuantity);

        // Act
        var actual = _baseQuantifiable.FitsIn(limiter);

        // Assert
        Assert.IsNull(actual);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void FitsIn_validArg_ILimiter_returns_expected()
    {
        // Arrange
        SetBaseQuantifiableChild();

        Fields.limitMode = _randomParams.GetRandomLimitMode();
        decimal otherQuantity = _randomParams.GetRandomDecimal();
        Fields.measureUnit = _randomParams.GetRandomMeasureUnit(Fields.measureUnitCode);
        ILimiter limiter = GetLimiterBaseQuantifiableObject(Fields.limitMode, Fields.measureUnit, otherQuantity);
        bool? expected = Fields.defaultQuantity.FitsIn(otherQuantity, Fields.limitMode);

        // Act
        var actual = _baseQuantifiable.FitsIn(limiter);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region decimal GetDefaultQuantity
    #region abstract IDefaultQuantity.GetDefaultQuantity()
    [TestMethod, TestCategory("UnitTest")]
    public void GetDefaultQuantity_returns_expected()
    {
        // Arrange
        decimal expected = Fields.defaultQuantity;

        SetBaseQuantifiableChild(null, expected);

        // Act
        var actual = _baseQuantifiable.GetDefaultQuantity();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region int GetHashCode
    #region override BaseQuantifiable.GetHashCode()
    [TestMethod, TestCategory("UnitTest")]
    public void GetHashCode_returns_expected()
    {
        // Arrange
        SetBaseQuantifiableChild();

        var expected = HashCode.Combine(Fields.measureUnitCode, Fields.defaultQuantity);

        // Act
        var actual = _baseQuantifiable.GetHashCode();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region void ValidateQuantity
    #region virtual IBaseQuantifiable.ValidateQuantity(ValueType?, string)
    [TestMethod, TestCategory("UnitTest")]
    public void ValidateQuantity_nullArg_ValueType_arg_string_throws_ArgumentNullException()
    {
        // Arrange
        SetBaseQuantifiableChild();

        Fields.paramName = _randomParams.GetRandomParamName();
        ValueType quantity = null;

        // Act
        void attempt() => _baseQuantifiable.ValidateQuantity(quantity, Fields.paramName);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(Fields.paramName, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetInvalidQuantityTypeCodeArg), DynamicDataSourceType.Method, DynamicDataDisplayName = DisplayName)]
    public void ValidateQuantity_invalidArg_ValueType_arg_string_throws_ArgumentOutOfRangeException(string testCase, TypeCode typeCode) // negative!
    {
        // Arrange
        SetBaseQuantifiableChild();

        ValueType quantity = _randomParams.GetRandomValueType(typeCode);
        Fields.paramName = _randomParams.GetRandomParamName();

        // Act
        void attempt() => _baseQuantifiable.ValidateQuantity(quantity, Fields.paramName);

        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(attempt);
        Assert.AreEqual(Fields.paramName, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetValidQuantityTypeCodeArg), DynamicDataSourceType.Method, DynamicDataDisplayName = DisplayName)]
    public void ValidateQuantity_validArg_ValueType_arg_string_returns_expected(string testCase, TypeCode typeCode)
    {
        // Arrange
        if (typeCode == TypeCode.UInt64 && Fields.defaultQuantity < 0)
        {
            Fields.defaultQuantity = _randomParams.GetRandomNotNegativeDecimal();
        }
        SetBaseQuantifiableChild();

        ValueType quantity = _randomParams.GetRandomValueType(typeCode);

        // Act
        void attempt() => _baseQuantifiable.ValidateQuantity(quantity, null);

        // Assert
        Assert.IsTrue(DoesNotThrowException(attempt));
    }
    #endregion
    #endregion
    #endregion

    #region Private methods
    #region DynamicDataSource
    private static IEnumerable<object[]> GetEqualsArgs()
    {
        return DynamicDataSource.GetEqualsArgs();
    }

    private static IEnumerable<object[]> GetFitsInArgs()
    {
        return DynamicDataSource.GetFitsInArgs();
    }

    private static IEnumerable<object[]> GetInvalidQuantityTypeCodeArg()
    {
        return DynamicDataSource.GetInvalidQuantityTypeCodeArg();
    }

    private static IEnumerable<object[]> GetValidQuantityTypeCodeArg()
    {
        return DynamicDataSource.GetValidQuantityTypeCodeArg();
    }
    #endregion

    private void SetBaseQuantifiableChild(Enum measureUnit, decimal defaultQuantity, IBaseQuantifiableFactory factory = null)
    {
        _baseQuantifiable = GetBaseQuantifiableChild(measureUnit, defaultQuantity, factory);
    }

    private void SetBaseQuantifiableChild()
    {
        _baseQuantifiable = GetBaseQuantifiableChild(Fields);
    }
    #endregion
}

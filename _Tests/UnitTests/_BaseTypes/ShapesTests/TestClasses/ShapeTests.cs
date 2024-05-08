namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.ShapesTests.TestClasses;

[TestClass, TestCategory("UnitTest")]
public sealed class ShapeTests
{
    #region Tested in parent classes' tests

    // Shape(IRootObject rootObject, string paramName)
    // int IComparable<IQuantifiable>.CompareTo(IQuantifiable? _other)
    // bool IEquatable<IQuantifiable>.Equals(IQuantifiable? _other)
    // bool? IFit<IQuantifiable>.FitsIn(IQuantifiable? _other, LimitMode? limitMode)
    // Enum IMeasureUnit.GetBaseMeasureUnitValue()
    // ValueType IQuantity.GetBaseQuantityValue()
    // decimal IDecimalQuantity.GetDecimalQuantity()
    // Enum IDefaultMeasureUnit.GetDefaultMeasureUnit()
    // IEnumerable<string> IDefaultMeasureUnit.GetDefaultMeasureUnitNames()
    // decimal IDefaultQuantity.GetDefaultQuantityValue()
    // IFactory ICommonBase.GetFactoryValue()
    // MeasureUnitCode IMeasureUnitCode.GetMeasureUnitCode()
    // Type IMeasureUnit.GetMeasureUnitType()
    // IQuantifiable IQuantifiable.GetQuantifiable(MeasureUnitCode measureUnitCode, decimal defaultQuantity)
    // object IRound<IQuantifiable>.GetQuantity(RoundingMode roundingMode)
    // double IQuantity<double>.GetQuantity()
    // object IQuantity.GetQuantity(TypeCode quantityTypeCode)
    // TypeCode IQuantityType.GetQuantityTypeCode()
    // ISpread ISpread.GetSpread(ISpreadMeasure spreadMeasure)
    // ISpreadMeasure? ISpread.GetSpreadMeasure(IQuantifiable? quantifiable)
    // ISpreadMeasure ISpreadMeasure.GetSpreadMeasure()
    // bool IExchangeable<Enum>.IsExchangeableTo(Enum? context)
    // decimal IProportional<IQuantifiable>.ProportionalTo(IQuantifiable? _other)
    // IQuantifiable IRound<IQuantifiable>.Round(RoundingMode roundingMode)
    // bool ITryExchange<IQuantifiable, Enum>.TryExchangeTo(Enum context, out IQuantifiable? exchanged)
    // void IDefaultMeasureUnit.ValidateMeasureUnit(Enum? measureUnit, string paramName)
    // void IMeasurable.ValidateMeasureUnitCode(IMeasurable? measurable, string paramName)
    // void IMeasureUnitCode.ValidateMeasureUnitCode(MeasureUnitCode measureUnitCode, string paramName)
    // void IBaseQuantifiable.ValidateQuantity(ValueType? defaultQuantity, string paramName)
    // void ISpreadMeasure.ValidateSpreadMeasure(ISpreadMeasure? spreadMeasure, string paramName)

    #endregion

    #region Private fields
    #region Static fields
    private static readonly DynamicDataSource DynamicDataSource = new();
    private const string DisplayName = nameof(GetDisplayName);
    #endregion

    private ShapeChild _shape;
    private ShapeChild _other;
    private IShapeComponent _shapeComponent;
    private ILimiter _limiter;
    private IBaseQuantifiable _baseQuantifiable;
    private IQuantifiable _quantifiable;
    private RandomParams _randomParams;
    private DataFields _fields;
    #endregion

    #region Initialize
    [TestInitialize]
    public void TestInitialize()
    {
        _fields = new();
        _randomParams = _fields.RandomParams;
        _fields.measureUnit = _randomParams.GetRandomSpreadMeasureUnit();
        _fields.measureUnitCode = _fields.GetMeasureUnitCode();
        _fields.defaultQuantity = _randomParams.GetRandomPositiveDecimal();
        _shapeComponent = GetShapeComponentQuantifiableObject(_fields);
    }

    [TestCleanup]
    public void TestCleanup()
    {
        _shape = _other = null;
        _shapeComponent = null;
        _limiter = null;
        _baseQuantifiable = null;
        _quantifiable = null;
    }

    public static string GetDisplayName(MethodInfo methodInfo, object[] args)
    {
        return CommonDynamicDataSource.GetDisplayName(methodInfo, args);
    }
    #endregion

    #region Test methods
    #region int CompareTo
    #region IComparable<IShape>.CompareTo(IShape?)
    [TestMethod, TestCategory("UnitTest")]
    public void CompareTo_nullArg_IShape_returns_expected()
    {
        // Arrange
        SetShapeChild();

        IShape other = null;
        const int expected = 1;

        // Act
        var actual = _shape.CompareTo(other);

        // Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void CompareTo_invalidArg_IShape_throws_InvalidEnumArgumentException()
    {
        // Arrange
        SetCompleteShapeChild();

        _fields.measureUnitCode = SampleParams.GetOtherSpreadMeasureUnitCode(_fields.measureUnitCode);
        _fields.measureUnit = _randomParams.GetRandomMeasureUnit(_fields.measureUnitCode);
        _other = GetCompleteShapeChild(_fields);

        // Act
        void attempt() => _ = _shape.CompareTo(_other);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(ParamNames.other, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void CompareTo_validArg_IQuantifiable_returns_expected()
    {
        // Arrange
        SetCompleteShapeChild();

        _fields.measureUnit = _randomParams.GetRandomSameTypeValidMeasureUnit(_fields.measureUnit);
        _fields.defaultQuantity = _randomParams.GetRandomPositiveDecimal(_fields.defaultQuantity);
        _other = GetCompleteShapeChild(_fields);
        int expected = _shape.GetDefaultQuantity().CompareTo(_other.GetDefaultQuantity());

        // Act
        var actual = _shape.CompareTo(_other);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region bool Equals
    #region virtual IEquatable<IShape>.Equals(IShape?)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetEqualsArg), DynamicDataSourceType.Method, DynamicDataDisplayName = DisplayName)]
    public void Equals_arg_IQuantifiable_returns_expected(string testCase, Enum measureUnit, decimal defaultQuantity, bool expected, IShape other)
    {
        // Arrange
        SetShapeChild(measureUnit, defaultQuantity);

        // Act
        var actual = _shape.Equals(other);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion

    #region IEqualityComparer<IShape>.Equals(IShape?, IShape?)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetEqualsArgs), DynamicDataSourceType.Method, DynamicDataDisplayName = DisplayName)]
    public void Equals_args_IShape_IShape_returns_expected(string testCase, IShape left, bool expected, IShape right)
    {
        // Arrange
        SetShapeChild();

        // Act
        var actual = _shape.Equals(left, right);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region bool? FitsIn
    #region override sealed ILimitable.FitsIn(ILimiter?)
    [TestMethod, TestCategory("UnitTest")]
    public void FitsIn_nullArg_ILimiter_returns_expected()
    {
        // Arrange
        SetShapeChild();

        _limiter = null;

        // Act
        var actual = _shape.FitsIn(_limiter);

        // Assert
        Assert.IsTrue(actual);
    }

    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetFitsInILimiterArgs), DynamicDataSourceType.Method, DynamicDataDisplayName = DisplayName)]
    public void FitsIn_invalidArg_ILimiter_returns_null(string testCase, Enum measureUnit, ILimiter limiter)
    {
        // Arrange
        SetCompleteShapeChild(measureUnit, _fields.defaultQuantity);

        // Act
        var actual = _shape.FitsIn(limiter);

        // Assert
        Assert.IsNull(actual);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void FitsIn_validArg_ILimiter_returns_expected()
    {
        // Arrange
        SetCompleteShapeChild();

        _fields.limitMode = _randomParams.GetRandomLimitMode();
        _fields.defaultQuantity = _randomParams.GetRandomDecimal();
        _fields.measureUnit = _randomParams.GetRandomMeasureUnit(_fields.measureUnitCode);
        _shapeComponent = GetShapeComponentQuantifiableObject(_fields);
        _limiter = GetLimiterShapeObject(_fields.limitMode.Value, _shapeComponent);
        bool? expected = ShapeFitsIn((_limiter as LimiterShapeObject).GetDefaultQuantity(), _limiter.GetLimitMode().Value);

        // Act
        var actual = _shape.FitsIn(_limiter);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion

    #region abstract IFit<IShape>.FitsIn(IShape?, LimitMode?)
    [TestMethod, TestCategory("UnitTest")]
    public void FitsIn_nullArgs_IShape_LimitMode_returns_true()
    {
        // Arrange
        SetShapeChild();

        _fields.limitMode = null;

        // Act
        var actual = _shape.FitsIn(null, _fields.limitMode);

        // Assert
        Assert.IsTrue(actual);
    }

    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetFitsInIShapeLimitModeArgs), DynamicDataSourceType.Method, DynamicDataDisplayName = DisplayName)]
    public void FitsIn_invalidArgs_IShape_LimitMode_returns_null(string testCase, Enum measureUnit, LimitMode? limitMode, IShape other)
    {
        // Arrange
        SetCompleteShapeChild(measureUnit, _fields.defaultQuantity);

        // Act
        var actual = _shape.FitsIn(other, limitMode);

        // Assert
        Assert.IsNull(actual);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void FitsIn_validArgs_IShape_LimitMode_returns_expected()
    {
        // Arrange
        SetCompleteShapeChild();

        _fields.defaultQuantity = _randomParams.GetRandomPositiveDecimal();
        _fields.measureUnit = _randomParams.GetRandomMeasureUnit(_fields.measureUnitCode);
        _other = GetShapeChild(_fields);
        _fields.limitMode = _randomParams.GetRandomLimitMode();
        bool? expected = ShapeFitsIn(_other.GetDefaultQuantity(), _fields.limitMode.Value);

        // Act
        var actual = _shape.FitsIn(_other, _fields.limitMode);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region int GetHashCode
    #region override Shape.GetHashCode()
    [TestMethod, TestCategory("UnitTest")]
    public void GetHashCode_returns_expected()
    {
        // Arrange
        SetCompleteShapeChild();

        HashCode hashCode = new();
        hashCode.Add(_shape.GetMeasureUnitCode());
        hashCode.Add(_shape.GetDefaultQuantity());
        hashCode.Add(_shapeComponent);
        int expected = hashCode.ToHashCode();

        // Act
        var actual = _shape.GetHashCode();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion

    #region IEqualityComparer<IShape>.GetHashCode(IShape)
    [TestMethod, TestCategory("UnitTest")]
    public void GetHashCode_arg_IShape_returns_expected()
    {
        // Arrange
        SetCompleteShapeChild();

        _fields.measureUnitCode = SampleParams.GetOtherSpreadMeasureUnitCode(_fields.measureUnitCode);
        _other = GetCompleteShapeChild(_fields);
        HashCode hashCode = new();
        hashCode.Add(_other.GetMeasureUnitCode());
        hashCode.Add(_other.GetBaseShapeComponents().First());
        int expected = hashCode.ToHashCode();

        // Act
        var actual = _shape.GetHashCode(_other);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region IEnumerable<MeasureUnitCode> GetMeasureUnitCodes
    #region IMeasureUnitCodes.GetMeasureUnitCodes()
    [TestMethod, TestCategory("UnitTest")]
    public void GetMeasureUnitCodes_returns_expected()
    {
        // Arrange
        _other = GetShapeChild(_randomParams.GetRandomSpreadMeasureUnit(_fields.measureUnitCode), _fields.defaultQuantity);

        SetShapeChild(_shapeComponent, _other);

        IEnumerable<MeasureUnitCode> expected =
        [
            _shape.GetMeasureUnitCode(),
            _other.GetMeasureUnitCode(),
        ];

        // Act
        var actual = _shape.GetMeasureUnitCodes();

        // Assert
        Assert.IsTrue(expected.SequenceEqual(actual));
    }
    #endregion
    #endregion

    #region IShape GetBaseShape
    #region abstract IShape.GetBaseShape()
    [TestMethod, TestCategory("UnitTest")]
    public void GetBaseShape_returns_expected()
    {
        // Arrange
        _other = GetShapeChild(_randomParams.GetRandomSpreadMeasureUnit(_fields.measureUnitCode), _fields.defaultQuantity);

        SetShapeChild(_shapeComponent, _other);

        IShape expected = _other;

        // Act
        var actual = _shape.GetBaseShape();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region IShape GetShape
    #region IShape.GetShape(params IShapeComponent[] shapeComponents)
    [TestMethod, TestCategory("UnitTest")]
    public void GetShape_arg_params_IShapeComponent_returns_expected()
    {
        // Arrange
        _other = GetShapeChild(_shapeComponent);

        SetShapeChild(_shapeComponent, _other);

        IShape expected = new ShapeFactoryObject().CreateShape(_shapeComponent);

        // Act
        var actual = _shape.GetBaseShape();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region int GetShapeComponentCount
    #region IShapeComponentCount.GetShapeComponentCount()
    [TestMethod, TestCategory("UnitTest")]
    public void GetShapeComponentCount_returns_expected()
    {
        // Arrange
        SetShapeChild();

        int expected = 1;

        // Act
        var actual = _shape.GetShapeComponentCount();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region IEnumerable<IShapeComponent> GetShapeComponents
    #region abstract IShapeComponents.GetShapeComponents()
    [TestMethod, TestCategory("UnitTest")]
    public void GetShapeComponents_returns_expected()
    {
        // Arrange
        SetShapeChild();

        IEnumerable<IShapeComponent> expected = [_shapeComponent];

        // Act
        var actual = _shape.GetShapeComponents();

        // Assert
        Assert.IsTrue(expected.SequenceEqual(actual));
    }
    #endregion
    #endregion

    #region IShapeComponent? GetValidShapeComponent
    #region abstract IShapeComponents.GetValidShapeComponent(IQuantifiable?)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetGetValidShapeComponentArgs), DynamicDataSourceType.Method, DynamicDataDisplayName = DisplayName)]
    public void GetValidShapeComponent_arg_IQuantifiable_returns_expected(string testCase, IQuantifiable quantifiable, IShapeComponent expected)
    {
        // Arrange
        SetShapeChild();

        // Act
        var actual = _shape.GetValidShapeComponent(quantifiable);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region bool HasMeasureUnitCode
    #region IMeasureUnitCode.HasMeasureUnitCode(MeasureUnitCode)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetHasMeasureUnitCodeArgs), DynamicDataSourceType.Method, DynamicDataDisplayName = DisplayName)]
    public void HasMeasureUnitCode_arg_MeasureUnitCode_returns_expected(string testCase, Enum measureUnit, MeasureUnitCode measureUnitCode, bool expected, Enum otherMeasureUnit)
    {
        // Arrange
        _other = GetCompleteShapeChild(measureUnit, _fields.defaultQuantity);
        SetShapeChild(otherMeasureUnit, _fields.defaultQuantity, _other);

        // Act
        var actual = _shape.HasMeasureUnitCode(measureUnitCode);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    // ValidateMeasureUnit

    #region void ValidateMeasureUnitCodes
    #region IMeasureUnitCodes.ValidateMeasureUnitCodes(IBaseQuantifiable?, string)
    [TestMethod, TestCategory("UnitTest")]
    public void ValidateMeasureUnitCodes_nullArg_IBaseQuantifiable_arg_string_throws_ArgumentNullException()
    {
        // Arrange
        SetShapeChild();

        _baseQuantifiable = null;
        _fields.paramName = _randomParams.GetRandomParamName();

        // Act
        void attempt() => _shape.ValidateMeasureUnitCodes(_baseQuantifiable, _fields.paramName);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(_fields.paramName, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void ValidateMeasureUnitCodes_invalidArg_IBaseQuantifiable_arg_string_throws_InvalidEnumArgumentException()
    {
        // Arrange
        SetCompleteShapeChild();

        _fields.measureUnitCode = _randomParams.GetRandomMeasureUnitCode(_fields.measureUnitCode);
        _fields.measureUnit = _randomParams.GetRandomMeasureUnit(_fields.measureUnitCode);
        _baseQuantifiable = GetBaseQuantifiableChild(_fields);
        _fields.paramName = _randomParams.GetRandomParamName();

        // Act
        void attempt() => _shape.ValidateMeasureUnitCodes(_baseQuantifiable, _fields.paramName);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(_fields.paramName, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void ValidateMeasureUnitCodes_validArg_IBaseQuantifiable_arg_string_returns()
    {
        // Arrange
        _other = GetCompleteShapeChild(_fields);
        _fields.measureUnit = _randomParams.GetRandomSpreadMeasureUnit(_fields.measureUnitCode);
        SetShapeChild(_fields.measureUnit, _fields.defaultQuantity, _other);
        _baseQuantifiable = _shape;

        // Act
        void attempt() => _shape.ValidateMeasureUnitCodes(_baseQuantifiable, _fields.paramName);

        // Assert
        Assert.IsTrue(DoesNotThrowException(attempt));
    }
    #endregion
    #endregion

    #region void ValidateShapeComponent
    #region IShape.ValidateShapeComponent(IQuantifiable?, string)
    [TestMethod, TestCategory("UnitTest")]
    public void ValidateShapeComponent_nullArg_IQuantifiable_arg_string_throws_ArgumentNullException()
    {
        // Arrange
        SetShapeChild();

        _quantifiable = null;
        _fields.paramName = _randomParams.GetRandomParamName();

        // Act
        void attempt() => _shape.ValidateShapeComponent(_quantifiable, _fields.paramName);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(_fields.paramName, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void ValidateShapeComponent_invalidArg_IQuantifiable_arg_string_throws_ArgumentOutOfRangeException()
    {
        // Arrange
        SetShapeChild();

        _quantifiable = GetQuantifiableChild(_fields);
        _fields.paramName = _randomParams.GetRandomParamName();

        // Act
        void attempt() => _shape.ValidateShapeComponent(_quantifiable, _fields.paramName);

        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(attempt);
        Assert.AreEqual(_fields.paramName, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void ValidateShapeComponent_invalidArg_IQuantifiable_arg_string_throws_InvalidEnumArgumentException()
    {
        // Arrange
        SetCompleteShapeChild();

        _fields.measureUnit = _randomParams.GetRandomSpreadMeasureUnit(_fields.measureUnitCode);
        _quantifiable = GetShapeComponentQuantifiableObject(_fields);
        _fields.paramName = _randomParams.GetRandomParamName();

        // Act
        void attempt() => _shape.ValidateShapeComponent(_quantifiable, _fields.paramName);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(_fields.paramName, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void ValidateShapeComponent_validArg_IQuantifiable_arg_string_returns()
    {
        SetCompleteShapeChild();

        _quantifiable = GetShapeComponentQuantifiableObject(_fields);
        _fields.paramName = _randomParams.GetRandomParamName();

        // Act
        void attempt() => _shape.ValidateShapeComponent(_quantifiable, _fields.paramName);

        // Assert
        Assert.IsTrue(DoesNotThrowException(attempt));
    }

    #endregion
    #endregion
    #endregion

    //#region Static methods

    //#endregion

    #region Private methods
    #region DynamicDataSource
    private static IEnumerable<object[]> GetEqualsArg()
    {
        return DynamicDataSource.GetEqualsArg();
    }

    private static IEnumerable<object[]> GetEqualsArgs()
    {
        return DynamicDataSource.GetEqualsArgs();
    }

    private static IEnumerable<object[]> GetFitsInILimiterArgs()
    {
        return DynamicDataSource.GetFitsInILimiterArgs();
    }

    private static IEnumerable<object[]> GetFitsInIShapeLimitModeArgs()
    {
        return DynamicDataSource.GetFitsInIShapeLimitModeArgs();
    }

    private static IEnumerable<object[]> GetGetValidShapeComponentArgs()
    {
        return DynamicDataSource.GetGetValidShapeComponentArgs();
    }

    private static IEnumerable<object[]> GetHasMeasureUnitCodeArgs()
    {
        return DynamicDataSource.GetHasMeasureUnitCodeArgs();
    }
    #endregion

    private void SetShapeChild(Enum measureUnit, decimal defaultQuantity, IShape baseShape = null, IShapeFactory factory = null)
    {
        _shape = GetShapeChild(measureUnit, defaultQuantity, baseShape, factory);
    }

    private void SetShapeChild(IShapeComponent shapeComponent, IShape baseShape = null, IShapeFactory factory = null)
    {
        _shape = GetShapeChild(shapeComponent, baseShape, factory);
    }

    private void SetShapeChild(IShape baseShape = null, IShapeFactory factory = null)
    {
        _shape = GetShapeChild(_fields, baseShape, factory);
    }

    private void SetCompleteShapeChild(IShapeFactory factory = null)
    {
        _shape = GetCompleteShapeChild(_fields, factory);
    }

    private void SetCompleteShapeChild(Enum measureUnit, decimal defaultQuantity, IShapeFactory factory = null)
    {
        _shape = GetCompleteShapeChild(measureUnit, defaultQuantity, factory);
    }

    private bool? ShapeFitsIn(decimal defaultQuantity, LimitMode limitMode)
    {
        return _shape.GetDefaultQuantity().FitsIn(defaultQuantity, limitMode);
    }
    #endregion
}
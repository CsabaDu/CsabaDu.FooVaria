using CsabaDu.FooVaria.BaseTypes.Shapes.Types.Implementations;

namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.ShapesTests.TestClasses;

[TestClass, TestCategory("UnitTest")]
public sealed class ShapeTests
{
    #region Tested in parent classes' tests

    // Shape(IRootObject rootObject, string paramName)
    // int IComparable<IQuantifiable>.CompareTo(IQuantifiable? _other)
    // bool IEquatable<IQuantifiable>.Equals(IQuantifiable? _other)
    // bool? IFit<IQuantifiable>.FitsIn(IQuantifiable? _other, LimitMode? limitMode)
    // Enum IMeasureUnit.GetBaseMeasureUnit()
    // ValueType IQuantity.GetBaseQuantity()
    // decimal IDecimalQuantity.GetDecimalQuantity()
    // Enum IDefaultMeasureUnit.GetDefaultMeasureUnit()
    // IEnumerable<string> IDefaultMeasureUnit.GetDefaultMeasureUnitNames()
    // decimal IDefaultQuantity.GetDefaultQuantity()
    // IFactory ICommonBase.GetFactory()
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
    private ShapeChild _shape;
    private ShapeChild _other;
    private IShapeComponent _shapeComponent;
    private ILimiter _limiter;

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
        Fields.measureUnit = Fields.RandomParams.GetRandomSpreadMeasureUnit();
        Fields.measureUnitCode = Fields.GetMeasureUnitCode();
        Fields.defaultQuantity = Fields.RandomParams.GetRandomPositiveDecimal();
        _shapeComponent = GetShapeComponentQuantifiableObject(Fields);
    }

    [TestCleanup]
    public void TestCleanup()
    {
        Fields.paramName = null;
        Fields.limitMode = null;
        _other = null;
        _shapeComponent = null;
        _limiter = null;
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

        Fields.measureUnitCode = SampleParams.GetOtherSpreadMeasureUnitCode(Fields.measureUnitCode);
        Fields.measureUnit = Fields.RandomParams.GetRandomMeasureUnit(Fields.measureUnitCode);
        _other = GetCompleteShapeChild(Fields);

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

        Fields.measureUnit = Fields.RandomParams.GetRandomSameTypeValidMeasureUnit(Fields.measureUnit);
        Fields.defaultQuantity = Fields.RandomParams.GetRandomPositiveDecimal(Fields.defaultQuantity);
        _other = GetCompleteShapeChild(Fields);
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
        SetCompleteShapeChild(measureUnit, Fields.defaultQuantity);

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

        Fields.limitMode = Fields.RandomParams.GetRandomLimitMode();
        Fields.defaultQuantity = Fields.RandomParams.GetRandomDecimal();
        Fields.measureUnit = Fields.RandomParams.GetRandomMeasureUnit(Fields.measureUnitCode);
        _shapeComponent = GetShapeComponentQuantifiableObject(Fields);
        _limiter = GetLimiterShapeObject(Fields.limitMode.Value, _shapeComponent);
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

        Fields.limitMode = null;

        // Act
        var actual = _shape.FitsIn(null, Fields.limitMode);

        // Assert
        Assert.IsTrue(actual);
    }

    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetFitsInIShapeLimitModeArgs), DynamicDataSourceType.Method, DynamicDataDisplayName = DisplayName)]
    public void FitsIn_invalidArgs_IShape_LimitMode_returns_null(string testCase, Enum measureUnit, LimitMode? limitMode, IShape other)
    {
        // Arrange
        SetCompleteShapeChild(measureUnit, Fields.defaultQuantity);

        // Act
        var actual = _shape.FitsIn(other, limitMode);

        // Assert
        Assert.IsNull(actual);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void FitsIn_validArgs_IShape_LimitMode_returns_expected()
    {
        // Arrange
        SetShapeChild();

        Fields.defaultQuantity = Fields.RandomParams.GetRandomPositiveDecimal();
        Fields.measureUnit = Fields.RandomParams.GetRandomMeasureUnit(Fields.measureUnitCode);
        _other = GetShapeChild(Fields);
        Fields.limitMode = Fields.RandomParams.GetRandomLimitMode();
        bool? expected = ShapeFitsIn(_other.GetDefaultQuantity(), Fields.limitMode.Value);

        // Act
        var actual = _shape.FitsIn(_other, Fields.limitMode);

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

        var expected = hashCode.ToHashCode();

        // Act
        var actual = _shape.GetHashCode();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion

    #region IEqualityComparer<IShape>.GetHashCode(IShape)

    #endregion
    #endregion


    // IEnumerable<MeasureUnitCode> IMeasureUnitCodes.GetMeasureUnitCodes()

    // IShape IShape.GetBaseShape()
    // IShape? IShape.GetBaseShape(params IShapeComponent[] shapeComponents)

    // int IShapeComponentCount.GetShapeComponentCount()

    // IEnumerable<IShapeComponent> IShapeComponents.GetShapeComponents()

    // IShapeComponent? IShapeComponents.GetValidShapeComponent(IQuantifiable?)

    // bool IMeasureUnitCode.HasMeasureUnitCode(MeasureUnitCode)

    // bool IMeasureUnitCodes.IsValidMeasureUnitCode(MeasureUnitCode)

    // void IMeasureUnitCodes.ValidateMeasureUnitCodes(IBaseQuantifiable?, string)

    // void IShape.ValidateShapeComponent(IBaseQuantifiable?, string)

    #endregion

    //    //    //#region Static methods

    //    //    //#endregion

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
        _shape = GetShapeChild(Fields, baseShape, factory);
    }

    private void SetCompleteShapeChild(IShapeFactory factory = null)
    {
        _shape = GetCompleteShapeChild(Fields, factory);
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
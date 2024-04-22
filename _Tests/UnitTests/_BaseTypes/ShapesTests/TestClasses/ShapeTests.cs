namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.ShapesTests.TestClasses;

[TestClass, TestCategory("UnitTest")]
public sealed class ShapeTests
{
    #region Tested in parent classes' tests

    // int IComparable<IQuantifiable>.CompareTo(IQuantifiable? other)
    // bool IEquatable<IQuantifiable>.Equals(IQuantifiable? other)
    // bool? IFit<IQuantifiable>.FitsIn(IQuantifiable? other, LimitMode? limitMode)
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
    // decimal IProportional<IQuantifiable>.ProportionalTo(IQuantifiable? other)
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
    private IShapeComponent _shapeComponent;

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
        Fields.measureUnit = Fields.RandomParams.GetRandomSpreadMeasureUnit();
        Fields.measureUnitCode = Fields.GetMeasureUnitCode();
        Fields.defaultQuantity = Fields.RandomParams.GetRandomPositiveDecimal();
        _shapeComponent = GetShapeComponentQuantifiableObject();
    }

    [TestCleanup]
    public void TestCleanup()
    {
        Fields.paramName = null;
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
        SetShapeChild();

        Fields.measureUnitCode = SampleParams.GetOtherSpreadMeasureUnitCode(Fields.measureUnitCode);
        Fields.measureUnit = Fields.RandomParams.GetRandomMeasureUnit(Fields.measureUnitCode);
        IShape other = GetShapeChild();

        // Act
        void attempt() => _ = _shape.CompareTo(other);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(ParamNames.other, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void CompareTo_validArg_IQuantifiable_returns_expected()
    {
        // Arrange
        SetShapeChild();

        Fields.measureUnit = Fields.RandomParams.GetRandomSameTypeValidMeasureUnit(Fields.measureUnit);
        Fields.defaultQuantity = Fields.RandomParams.GetRandomPositiveDecimal(Fields.defaultQuantity);
        IShape other = GetShapeChild();
        int expected = _shape.GetDefaultQuantity().CompareTo(other.GetDefaultQuantity());

        // Act
        var actual = _shape.CompareTo(other);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    // bool IEquatable<IShape>.Equals(IShape?)
    // bool IEqualityComparer<IShape>.Equals(IShape?, IShape?)

    // bool? ILimitable.FitsIn(ILimiter? limiter)
    // bool? IFit<IShape>.FitsIn(IShape?, LimitMode?)

    // int Shape.GetHashCode()
    // int IEqualityComparer<IShape>.GetHashCode(IShape)

    // IEnumerable<MeasureUnitCode> IMeasureUnitCodes.GetMeasureUnitCodes()

    // IShape IShape.GetShape()
    // IShape? IShape.GetShape(params IShapeComponent[] shapeComponents)

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
    private void SetShapeChild(Enum measureUnit, decimal defaultQuantity, IShapeFactory factory = null)
    {
        _shapeComponent = ShapeComponentQuantifiableObject.GetShapeComponentQuantifiableObject(measureUnit, defaultQuantity);

        SetShapeChild(_shapeComponent, factory);
    }

    private void SetShapeChild(IShapeComponent shapeComponent, IShapeFactory factory = null)
    {
        _shape = ShapeChild.GetShapeChild(shapeComponent, factory);
    }

    private void SetShapeChild()
    {
        _shapeComponent = GetShapeComponentQuantifiableObject();

        SetShapeChild(_shapeComponent);
    }

    private ShapeChild GetShapeChild(IShapeFactory factory = null)
    {
        _shapeComponent = GetShapeComponentQuantifiableObject();

        return ShapeChild.GetShapeChild(_shapeComponent, factory);
    }

    private ShapeComponentQuantifiableObject GetShapeComponentQuantifiableObject()
    {
        return ShapeComponentQuantifiableObject.GetShapeComponentQuantifiableObject(Fields.measureUnit, Fields.defaultQuantity);
    }

    //    #region DynamicDataSource
    //    private static IEnumerable<object[]> GetIsExchangeableToArgs()
    //    {
    //        return DynamicDataSource.GetIsExchangeableToArgs();
    //    }

    //    private static IEnumerable<object[]> GetGetSpreadMeasureArgs()
    //    {
    //        return DynamicDataSource.GetGetSpreadMeasureArgs();
    //    }

    //    private static IEnumerable<object[]> GetValidateSpreadMeasureArgs()
    //    {
    //        return DynamicDataSource.GetValidateSpreadMeasureArgs();
    //    }
    //    #endregion
    #endregion
}
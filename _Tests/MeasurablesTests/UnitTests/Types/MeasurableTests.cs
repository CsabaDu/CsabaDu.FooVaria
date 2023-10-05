using CsabaDu.FooVaria.Common.Types;
using CsabaDu.FooVaria.Common.Types.Implementations;
using CsabaDu.FooVaria.Tests.TestSupport.Fakes.Common.Types;

namespace CsabaDu.FooVaria.Tests.MeasurablesTests.UnitTests.Types;

[TestClass]
public class MeasurableTests
{
    #region Initialize
    [ClassInitialize]
    public static void InitializeMeasurableTestsClass(TestContext context)
    {
        DynamicDataSources = new();
    }

    [TestInitialize]
    public void InitializeMeasurableTests()
    {
        RandomParams = new();
        MeasureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();
        Factory = new MeasurableFactoryImplementation();
        Measurable = new MeasurableChild(Factory, MeasureUnitTypeCode);
    }
    #endregion

    #region Private fields
    private IMeasurable Measurable;
    private IMeasurableFactory Factory;
    private MeasureUnitTypeCode MeasureUnitTypeCode;
    private RandomParams RandomParams;

    #region Static fields
    private static DynamicDataSources DynamicDataSources;
    #endregion
    #endregion

    #region Test methods
    #region Constructors
    #region Measurable(IMeasurableFactory, MeasureUnitTypeCode)
    [TestMethod, TestCategory("UnitTest")]
    public void Measurable_nullArg_IMeasurableFactory_arg_MeasureunitTypeCode_throws_ArgumentNullException()
    {
        // Arrange
        IMeasurableFactory factory = null;
        MeasureUnitTypeCode measureUnitTypeCode = SampleParams.NotDefinedMeasureUnitTypeCode;

        // Act
        void attempt() => _ = new MeasurableChild(factory, measureUnitTypeCode);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.factory, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Measurable_validArg_IMeasurableFactory_invalidArg_MeasureunitTypeCode_throws_InvalidEnumArgumentException()
    {
        // Arrange
        IMeasurableFactory factory = new MeasurableFactoryImplementation();
        MeasureUnitTypeCode measureUnitTypeCode = SampleParams.NotDefinedMeasureUnitTypeCode;

        // Act
        void attempt() => _ = new MeasurableChild(factory, measureUnitTypeCode);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(ParamNames.measureUnitTypeCode, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Measurable_validArgs_IMeasurableFactory_MeasureunitTypeCode_createsInstance()
    {
        // Arrange
        IMeasurableFactory factory = new MeasurableFactoryImplementation();
        MeasureUnitTypeCode expected_MeasureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();

        // Act
        var actual = new MeasurableChild(factory, expected_MeasureUnitTypeCode);

        // Assert
        Assert.IsInstanceOfType(actual, typeof(IMeasurable));
        Assert.AreEqual(expected_MeasureUnitTypeCode, actual.MeasureUnitTypeCode);
    }
    #endregion

    #region Measurable(IMeasurableFactory, Enum)
    [TestMethod, TestCategory("UnitTest")]
    public void Measurable_nullArg_IMeasurableFactory_arg_Enum_throws_ArgumentNullException()
    {
        // Arrange
        IMeasurableFactory factory = null;
        Enum measureUnit = null;

        // Act
        void attempt() => _ = new MeasurableChild(factory, measureUnit);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.factory, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Measurable_validArg_IMeasurableFactory_nullArg_Enum_throws_ArgumentNullException()
    {
        // Arrange
        IMeasurableFactory factory = new MeasurableFactoryImplementation();
        Enum measureUnit = null;

        // Act
        void attempt() => _ = new MeasurableChild(factory, measureUnit);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetInvalidEnumMeasureUnitArgArrayList), DynamicDataSourceType.Method)]
    public void Measurable_validArg_IMeasurableFactory_invalidArg_Enum_throws_InvalidEnumArgumentException(Enum measureUnit)
    {
        // Arrange
        IMeasurableFactory factory = new MeasurableFactoryImplementation();

        // Act
        void attempt() => _ = new MeasurableChild(factory, measureUnit);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Measurable_validArgs_IMeasurableFactory_Enum_createsInstance()
    {
        // Arrange
        IMeasurableFactory factory = new MeasurableFactoryImplementation();
        MeasureUnitTypeCode expected_MeasureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();
        Enum measureUnit = RandomParams.GetRandomMeasureUnit(expected_MeasureUnitTypeCode);

        // Act
        var actual = new MeasurableChild(factory, measureUnit);

        // Assert
        Assert.IsInstanceOfType(actual, typeof(IMeasurable));
        Assert.AreEqual(expected_MeasureUnitTypeCode, actual.MeasureUnitTypeCode);
    }
    #endregion

    #region Measurable(IMeasurableFactory, IBaseMeasurable)
    [TestMethod, TestCategory("UnitTest")]
    public void Measurable_nullArg_IMeasurableFactory_arg_IBaseMeasurable_throws_ArgumentNullException()
    {
        // Arrange
        IMeasurableFactory factory = null;
        IBaseMeasurable baseMeasurable = null;

        // Act
        void attempt() => _ = new MeasurableChild(factory, baseMeasurable);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.factory, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Measurable_validArg_IMeasurableFactory_nullArg_IBaseMeasurable_throws_ArgumentNullException()
    {
        // Arrange
        IMeasurableFactory factory = new MeasurableFactoryImplementation();
        IBaseMeasurable baseMeasurable = null;

        // Act
        void attempt() => _ = new MeasurableChild(factory, baseMeasurable);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.baseMeasurable, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Measurable_validArgs_IMeasurableFactory_IBaseMeasurable_createsInstance()
    {
        // Arrange
        IMeasurableFactory factory = new MeasurableFactoryImplementation();
        MeasureUnitTypeCode expected_MeasureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();
        IBaseMeasurable baseMeasurable = new BaseMeasurableChild(factory, expected_MeasureUnitTypeCode);

        // Act
        var actual = new MeasurableChild(factory, baseMeasurable);

        // Assert
        Assert.IsInstanceOfType(actual, typeof(IMeasurable));
        Assert.AreEqual(expected_MeasureUnitTypeCode, actual.MeasureUnitTypeCode);
    }
    #endregion

    #region Measurable(IMeasurable)
    [TestMethod, TestCategory("UnitTest")]
    public void Measurable_nullArg_Measurable_throws_ArgumentNullException()
    {
        // Arrange
        IMeasurable other = null;

        // Act
        void attempt() => _ = new MeasurableChild(other);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.other, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Measurable_validArg_IMeasurable_createsInstance()
    {
        // Arrange
        IMeasurableFactory factory = new MeasurableFactoryImplementation();
        MeasureUnitTypeCode measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();
        IMeasurable expected = new MeasurableChild(factory, measureUnitTypeCode);

        // Act
        var actual = new MeasurableChild(expected);

        // Assert
        Assert.IsInstanceOfType(actual, typeof(IMeasurable));
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region Equals
    #region Equals(object?)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetMeasurableEqualsArgsArrayList), DynamicDataSourceType.Method)]
    public void Equals_arg_object_returnsExpected(bool expected, object other, MeasureUnitTypeCode measureUnitTypeCode)
    {
        // Arrange
        Measurable = new MeasurableChild(Factory, measureUnitTypeCode);

        // Act
        var actual = Measurable.Equals(other);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region GetFactory
    #region GetFactory()
    [TestMethod, TestCategory("UnitTest")]
    public void GetFactory_returnsExpected()
    {
        // Arrange
        // Act
        var actual = Measurable.GetFactory();

        // Assert
        Assert.IsNotNull(actual);
        Assert.IsInstanceOfType(Measurable.GetFactory(), typeof(IMeasurableFactory));
    }
    #endregion
    #endregion

    #region GetHashCode
    #region GetHashCode()
    [TestMethod, TestCategory("UnitTest")]
    public void GetHashCode_returnsExpected()
    {
        // Arrange
        int expected = HashCode.Combine(typeof(IMeasurable), MeasureUnitTypeCode);

        // Act
        var actual = Measurable.GetHashCode();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region GetMeasurable
    #region GetMeasurable(IMeasurable)
    [TestMethod, TestCategory("UnitTest")]
    public void GetMeasurable_nullArg_IMeasurable_throws_ArgumentNullException()
    {
        // Arrange
        IMeasurable measurable = null;

        // Act
        void attempt() => _ = Measurable.GetMeasurable(measurable);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.other, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void GetMeasurable_validArg_IMeasurable_returnsExpected()
    {
        // Arrange
        IMeasurable expected = RandomParams.GetRandomDefaultMeasurable();
        MeasureUnitTypeCode = expected.MeasureUnitTypeCode;
        Factory = (IMeasurableFactory)expected.GetFactory();
        Measurable = new MeasurableChild(Factory, MeasureUnitTypeCode);

        // Act
        var actual = Measurable.GetMeasurable(expected);

        // Assert
        Assert.AreEqual(expected, actual);
    }

    #endregion
    #endregion

    #endregion

    #region ArrayList methods
    private static IEnumerable<object[]> GetInvalidEnumMeasureUnitArgArrayList()
    {
        return DynamicDataSources.GetInvalidEnumMeasureUnitArgArrayList();
    }

    private static IEnumerable<object[]> GetMeasurableEqualsArgsArrayList()
    {
        return DynamicDataSources.GetMeasurableEqualsArgsArrayList();
    }
    #endregion
}
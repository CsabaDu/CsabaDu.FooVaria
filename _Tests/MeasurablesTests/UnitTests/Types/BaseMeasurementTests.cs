using CsabaDu.FooVaria.Common.Statics;
using CsabaDu.FooVaria.TestSupport.Variants.Measurables;

namespace CsabaDu.FooVaria.Tests.MeasurablesTests.UnitTests.Types
{
    [TestClass, TestCategory("UnitTest")]
    public sealed class BaseMeasurementTests
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
            measureUnit = RandomParams.GetRandomValidMeasureUnit();
            measureUnitTypeCode = MeasureUnitTypes.GetMeasureUnitTypeCode(measureUnit);
            factory = new BaseMeasurementFactoryChild();
            baseMeasurement = new BaseMeasurementChild(factory, measureUnit);
            baseMeasurement.RestoreConstantExchangeRates();
        }
        #endregion

        #region Private fields
        private IBaseMeasurement baseMeasurement;
        private IBaseMeasurementFactory factory;
        private Enum measureUnit;
        string name;
        MeasureUnitTypeCode measureUnitTypeCode;

        #region Readonly fields
        private readonly BaseMeasurementVariant BaseMeasurementVariant = new();
        private readonly RandomParams RandomParams = new();
        #endregion

        #region Static fields
        private static DynamicDataSources DynamicDataSources;
        #endregion
        #endregion

        #region Test methods
        #region Constructors
        #region BaseMeasurement(IBaseMeasurementFactory, Enum)
        [TestMethod, TestCategory("UnitTest")]
        public void BaseMeasurement_nullArg_IBaseMeasurementFactory_arg_Enum_throws_ArgumentNullException()
        {
            // Arrange
            factory = null;
            measureUnit = null;

            // Act
            void attempt() => _ = new BaseMeasurementChild(factory, measureUnit);

            // Assert
            var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
            Assert.AreEqual(ParamNames.factory, ex.ParamName);
        }

        [TestMethod, TestCategory("UnitTest")]
        public void BaseMeasurement_validArg_IBaseMeasurementFactory_nullArg_Enum_throws_ArgumentNullException()
        {
            // Arrange
            measureUnit = null;

            // Act
            void attempt() => _ = new BaseMeasurementChild(factory, measureUnit);

            // Assert
            var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
            Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
        }

        [TestMethod, TestCategory("UnitTest")]
        [DynamicData(nameof(GetInvalidBaseMeasurementEnumMeasureUnitArgArrayList), DynamicDataSourceType.Method)]
        public void BaseMeasurement_validArg_IBaseMeasurementFactory_invalidArg_Enum_throws_InvalidEnumArgumentException(Enum measureUnit)
        {
            // Arrange
            // Act
            void attempt() => _ = new BaseMeasurementChild(factory, measureUnit);

            // Assert
            var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
            Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
        }

        [TestMethod, TestCategory("UnitTest")]
        public void BaseMeasurement_validArgs_IBaseMeasurementFactory_Enum_creates()
        {
            // Arrange
            measureUnit = RandomParams.GetRandomValidMeasureUnit();
            measureUnitTypeCode = MeasureUnitTypes.GetMeasureUnitTypeCode(measureUnit);

            // Act
            var actual = new BaseMeasurementChild(factory, measureUnit);

            // Assert
            Assert.IsInstanceOfType(actual, typeof(IBaseMeasurement));
            Assert.AreEqual(measureUnitTypeCode, actual.MeasureUnitTypeCode);
        }
        #endregion
        #endregion

        #region GetConstantExchangeRateCollection
        #region GetConstantExchangeRateCollection()
        [TestMethod, TestCategory("UnitTest")]
        public void GetConstantExchangeRateCollection_returns_expected()
        {
            // Arrange
            measureUnit = RandomParams.GetRandomValidMeasureUnit();
            measureUnitTypeCode = MeasureUnitTypes.GetMeasureUnitTypeCode(measureUnit);
            baseMeasurement = new BaseMeasurementChild(factory, measureUnit);
            IDictionary<object, decimal> expected = BaseMeasurementVariant.GetConstantExchangeRateCollection(measureUnitTypeCode);

            // Act
            var actual = baseMeasurement.GetConstantExchangeRateCollection();

            // Assert
            Assert.IsTrue(expected.SequenceEqual(actual));
        }
        #endregion

        #region GetConstantExchangeRateCollection(MeasureUnitTypeCode)
        [TestMethod, TestCategory("UnitTest")]
        public void GetConstantExchangeRateCollection_invalidArg_MeasureUnitTypeCode_throws_InvalidEnumArgumentException()
        {
            // Arrange
            measureUnitTypeCode = SampleParams.NotDefinedMeasureUnitTypeCode;

            // Act
            void attempt() => _ = baseMeasurement.GetConstantExchangeRateCollection(measureUnitTypeCode);

            // Assert
            var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
            Assert.AreEqual(ParamNames.measureUnitTypeCode, ex.ParamName);
        }

        [TestMethod, TestCategory("UnitTest")]
        public void GetConstantExchangeRateCollection_validArg_MeasureUnitTypeCode_returns_expected()
        {
            measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();
            IDictionary<object, decimal> expected = BaseMeasurementVariant.GetConstantExchangeRateCollection(measureUnitTypeCode);

            // Act
            var actual = baseMeasurement.GetConstantExchangeRateCollection(measureUnitTypeCode);

            // Assert
            Assert.IsTrue(expected.SequenceEqual(actual));
        }
        #endregion
        #endregion

        #region GetCustomName
        #region GetCustomName(Enum)
        [TestMethod, TestCategory("UnitTest")]
        [DynamicData(nameof(GetInvalidGetCustomNameArgArrayList), DynamicDataSourceType.Method)]
        public void GetCustomName_nullOrInvalidArg_Enum_returns_null(Enum measureUnit)
        {
            // Arrange
            // Act
            var actual = baseMeasurement.GetCustomName(measureUnit);

            // Assert
            Assert.IsNull(actual);
        }

        [TestMethod, TestCategory("UnitTest")]
        public void GetCustomName_validArg_Enum_returns_expected()
        {
            // Arrange
            measureUnit = RandomParams.GetRandomValidMeasureUnit();
            baseMeasurement = new BaseMeasurementChild(factory, measureUnit);
            name = Guid.NewGuid().ToString();
            baseMeasurement.SetCustomName(measureUnit, name);

            // Act
            var actual = baseMeasurement.GetCustomName(measureUnit);

            // Assert
            Assert.AreEqual(name, actual);
        }
        #endregion
        #endregion

        #region GetCustomNameCollection
        #region GetCustomNameCollection()
        [TestMethod, TestCategory("UnitTest")]
        [DynamicData(nameof(GetCustomNameCollectionArgArrayList), DynamicDataSourceType.Method)]
        public void GetCustomNameCollection_returns_expected(IDictionary<object, string> expected)
        {
            // Arrange
            foreach (KeyValuePair<object, string> item in expected)
            {
                baseMeasurement.SetCustomName((Enum)item.Key, item.Value);
            }

            // Act
            var actual = baseMeasurement.GetCustomNameCollection();

            // Assert
            Assert.IsTrue(expected.SequenceEqual(actual));
        }
        #endregion

        #region GetCustomNameCollection(MeasureUnitTypeCode)
        [TestMethod, TestCategory("UnitTest")]
        public void GetCustomNameCollection_invalidArg_MeasureUnitTypeCode_throws_InvalidEnumArgumentException()
        {
            // Arrange
            measureUnitTypeCode = SampleParams.NotDefinedMeasureUnitTypeCode;

            // Act
            void attempt() => _ = baseMeasurement.GetCustomNameCollection(measureUnitTypeCode);

            // Assert
            var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
            Assert.AreEqual(ParamNames.measureUnitTypeCode, ex.ParamName);
        }

        [TestMethod, TestCategory("UnitTest")]
        [DynamicData(nameof(GetCustomNameCollectionMeasureUnitTypeCodeArgArrayList), DynamicDataSourceType.Method)]
        public void GetCustomNameCollection_validArg_MeasureUnitTypeCode_returns_expected(IDictionary<object, string> expected, MeasureUnitTypeCode measureUnitTypeCode)
        {
            // Arrange
            measureUnit = MeasureUnitTypes.GetDefaultMeasureUnit(RandomParams.GetRandomMeasureUnitTypeCode(measureUnitTypeCode));
            name = Guid.NewGuid().ToString();
            baseMeasurement.SetCustomName(measureUnit, name);

            foreach (KeyValuePair<object, string> item in expected)
            {
                baseMeasurement.SetCustomName((Enum)item.Key, item.Value);
            }

            // Act
            var actual = baseMeasurement.GetCustomNameCollection(measureUnitTypeCode);

            // Assert
            Assert.IsTrue(expected.SequenceEqual(actual));
        }
        #endregion
        #endregion

        #region GetDefaultName
        #region GetDefaultName(Enum)
        [TestMethod, TestCategory("UnitTest")]
        public void GetDefaultName_nullArg_Enum_throws_ArgumentNullException()
        {
            // Arrange
            measureUnit = null;

            // Act
            void attempt() => baseMeasurement.GetDefaultName(measureUnit);

            // Assert
            var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
            Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
        }

        #endregion
        #endregion

        #endregion

        #region ArrayList methods
        private static IEnumerable<object[]> GetInvalidBaseMeasurementEnumMeasureUnitArgArrayList()
        {
            return DynamicDataSources.GetInvalidBaseMeasurementEnumMeasureUnitArgArrayList();
        }

        private static IEnumerable<object[]> GetInvalidGetCustomNameArgArrayList()
        {
            return DynamicDataSources.GetInvalidGetCustomNameArgArrayList();
        }

        private static IEnumerable<object[]> GetCustomNameCollectionArgArrayList()
        {
            return DynamicDataSources.GetCustomNameCollectionArgArrayList();
        }

        private static IEnumerable<object[]> GetCustomNameCollectionMeasureUnitTypeCodeArgArrayList()
        {
            return DynamicDataSources.GetCustomNameCollectionMeasureUnitTypeCodeArgArrayList();
        }
        #endregion
    }
}


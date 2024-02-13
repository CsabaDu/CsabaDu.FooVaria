//using CsabaDu.FooVaria.Common.Statics;
//using CsabaDu.FooVaria.TestSupport.Variants.Measurables;

//namespace CsabaDu.FooVaria.Tests.MeasurablesTests.UnitTests.Types
//{
//    [TestClass, TestCategory("UnitTest")]
//    public sealed class BaseMeasurementTests
//    {
//        #region Initialize
//        [ClassInitialize]
//        public static void InitializeMeasurableTestsClass(TestContext context)
//        {
//            DynamicDataSources = new();
//        }

//        [TestInitialize]
//        public void InitializeMeasurableTests()
//        {
//            measureUnit = RandomParams.GetRandomValidMeasureUnit();
//            measureUnitCode = MeasureUnitTypes.GetMeasureUnitCode(measureUnit);
//            factoryObject = new BaseMeasurementFactoryClass();
//            baseMeasurement = new BaseMeasurementChild(factoryObject, measureUnit);
//            baseMeasurement.RestoreConstantExchangeRates();
//        }
//        #endregion

//        #region Private fields
//        private IBaseMeasurement baseMeasurement;
//        private IBaseMeasurementFactory factoryObject;
//        private Enum measureUnit;
//        string name;
//        MeasureUnitCode measureUnitCode;

//        #region Readonly fields
//        private readonly BaseMeasurementVariant BaseMeasurementVariant = new();
//        private readonly RandomParams RandomParams = new();
//        #endregion

//        #region Static fields
//        private static DynamicDataSources DynamicDataSources;
//        #endregion
//        #endregion

//        #region Test methods
//        #region Constructors
//        #region BaseMeasurement(IBaseMeasurementFactory, Enum)
//        [TestMethod, TestCategory("UnitTest")]
//        public void BaseMeasurement_nullArg_IBaseMeasurementFactory_arg_Enum_throws_ArgumentNullException()
//        {
//            // Arrange
//            factoryObject = null;
//            measureUnit = null;

//            // Act
//            void attempt() => _ = new BaseMeasurementChild(factoryObject, measureUnit);

//            // Assert
//            var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
//            Assert.AreEqual(ParamNames.factoryObject, ex.ParamName);
//        }

//        [TestMethod, TestCategory("UnitTest")]
//        public void BaseMeasurement_validArg_IBaseMeasurementFactory_nullArg_Enum_throws_ArgumentNullException()
//        {
//            // Arrange
//            measureUnit = null;

//            // Act
//            void attempt() => _ = new BaseMeasurementChild(factoryObject, measureUnit);

//            // Assert
//            var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
//            Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
//        }

//        [TestMethod, TestCategory("UnitTest")]
//        [DynamicData(nameof(GetInvalidBaseMeasurementEnumMeasureUnitArgArrayList), DynamicDataSourceType.Method)]
//        public void BaseMeasurement_validArg_IBaseMeasurementFactory_invalidArg_Enum_throws_InvalidEnumArgumentException(Enum measureUnit)
//        {
//            // Arrange
//            // Act
//            void attempt() => _ = new BaseMeasurementChild(factoryObject, measureUnit);

//            // Assert
//            var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
//            Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
//        }

//        [TestMethod, TestCategory("UnitTest")]
//        public void BaseMeasurement_validArgs_IBaseMeasurementFactory_Enum_creates()
//        {
//            // Arrange
//            measureUnit = RandomParams.GetRandomValidMeasureUnit();
//            measureUnitCode = MeasureUnitTypes.GetMeasureUnitCode(measureUnit);

//            // Act
//            var actual = new BaseMeasurementChild(factoryObject, measureUnit);

//            // Assert
//            Assert.IsInstanceOfType(actual, typeof(IBaseMeasurement));
//            Assert.AreEqual(measureUnitCode, actual.MeasureUnitCode);
//        }
//        #endregion
//        #endregion

//        #region GetConstantExchangeRateCollection
//        #region GetConstantExchangeRateCollection()
//        [TestMethod, TestCategory("UnitTest")]
//        public void GetConstantExchangeRateCollection_returns_expected()
//        {
//            // Arrange
//            measureUnit = RandomParams.GetRandomValidMeasureUnit();
//            measureUnitCode = MeasureUnitTypes.GetMeasureUnitCode(measureUnit);
//            baseMeasurement = new BaseMeasurementChild(factoryObject, measureUnit);
//            IDictionary<object, decimal> expected = BaseMeasurementVariant.GetConstantExchangeRateCollection(measureUnitCode);

//            // Act
//            var actual = baseMeasurement.GetConstantExchangeRateCollection();

//            // Assert
//            Assert.IsTrue(expected.SequenceEqual(actual));
//        }
//        #endregion

//        #region GetConstantExchangeRateCollection(MeasureUnitCode)
//        [TestMethod, TestCategory("UnitTest")]
//        public void GetConstantExchangeRateCollection_invalidArg_MeasureUnitCode_throws_InvalidEnumArgumentException()
//        {
//            // Arrange
//            measureUnitCode = SampleParams.NotDefinedMeasureUnitCode;

//            // Act
//            void attempt() => _ = baseMeasurement.GetConstantExchangeRateCollection(measureUnitCode);

//            // Assert
//            var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
//            Assert.AreEqual(ParamNames.measureUnitCode, ex.ParamName);
//        }

//        [TestMethod, TestCategory("UnitTest")]
//        public void GetConstantExchangeRateCollection_validArg_MeasureUnitCode_returns_expected()
//        {
//            measureUnitCode = RandomParams.GetRandomMeasureUnitCode();
//            IDictionary<object, decimal> expected = BaseMeasurementVariant.GetConstantExchangeRateCollection(measureUnitCode);

//            // Act
//            var actual = baseMeasurement.GetConstantExchangeRateCollection(measureUnitCode);

//            // Assert
//            Assert.IsTrue(expected.SequenceEqual(actual));
//        }
//        #endregion
//        #endregion

//        #region GetCustomName
//        #region GetCustomName(Enum)
//        [TestMethod, TestCategory("UnitTest")]
//        [DynamicData(nameof(GetInvalidGetCustomNameArgArrayList), DynamicDataSourceType.Method)]
//        public void GetCustomName_nullOrInvalidArg_Enum_returns_null(Enum measureUnit)
//        {
//            // Arrange
//            // Act
//            var actual = baseMeasurement.GetCustomName(measureUnit);

//            // Assert
//            Assert.IsNull(actual);
//        }

//        [TestMethod, TestCategory("UnitTest")]
//        public void GetCustomName_validArg_Enum_returns_expected()
//        {
//            // Arrange
//            measureUnit = RandomParams.GetRandomValidMeasureUnit();
//            baseMeasurement = new BaseMeasurementChild(factoryObject, measureUnit);
//            name = Guid.NewGuid().ToString();
//            baseMeasurement.SetCustomName(measureUnit, name);

//            // Act
//            var actual = baseMeasurement.GetCustomName(measureUnit);

//            // Assert
//            Assert.AreEqual(name, actual);
//        }
//        #endregion
//        #endregion

//        #region GetCustomNameCollection
//        #region GetCustomNameCollection()
//        [TestMethod, TestCategory("UnitTest")]
//        [DynamicData(nameof(GetCustomNameCollectionArgArrayList), DynamicDataSourceType.Method)]
//        public void GetCustomNameCollection_returns_expected(IDictionary<object, string> expected)
//        {
//            // Arrange
//            foreach (KeyValuePair<object, string> item in expected)
//            {
//                baseMeasurement.SetCustomName((Enum)item.Key, item.Value);
//            }

//            // Act
//            var actual = baseMeasurement.GetCustomNameCollection();

//            // Assert
//            Assert.IsTrue(expected.SequenceEqual(actual));
//        }
//        #endregion

//        #region GetCustomNameCollection(MeasureUnitCode)
//        [TestMethod, TestCategory("UnitTest")]
//        public void GetCustomNameCollection_invalidArg_MeasureUnitCode_throws_InvalidEnumArgumentException()
//        {
//            // Arrange
//            measureUnitCode = SampleParams.NotDefinedMeasureUnitCode;

//            // Act
//            void attempt() => _ = baseMeasurement.GetCustomNameCollection(measureUnitCode);

//            // Assert
//            var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
//            Assert.AreEqual(ParamNames.measureUnitCode, ex.ParamName);
//        }

//        [TestMethod, TestCategory("UnitTest")]
//        [DynamicData(nameof(GetCustomNameCollectionMeasureUnitCodeArgArrayList), DynamicDataSourceType.Method)]
//        public void GetCustomNameCollection_validArg_MeasureUnitCode_returns_expected(IDictionary<object, string> expected, MeasureUnitCode measureUnitCode)
//        {
//            // Arrange
//            measureUnit = MeasureUnitTypes.GetDefaultMeasureUnit(RandomParams.GetRandomMeasureUnitCode(measureUnitCode));
//            name = Guid.NewGuid().ToString();
//            baseMeasurement.SetCustomName(measureUnit, name);

//            foreach (KeyValuePair<object, string> item in expected)
//            {
//                baseMeasurement.SetCustomName((Enum)item.Key, item.Value);
//            }

//            // Act
//            var actual = baseMeasurement.GetCustomNameCollection(measureUnitCode);

//            // Assert
//            Assert.IsTrue(expected.SequenceEqual(actual));
//        }
//        #endregion
//        #endregion

//        #region GetDefaultName
//        #region GetDefaultName(Enum)
//        [TestMethod, TestCategory("UnitTest")]
//        public void GetDefaultName_nullArg_Enum_throws_ArgumentNullException()
//        {
//            // Arrange
//            measureUnit = null;

//            // Act
//            void attempt() => baseMeasurement.GetDefaultName(measureUnit);

//            // Assert
//            var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
//            Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
//        }

//        #endregion
//        #endregion

//        #endregion

//        #region ArrayList methods
//        private static IEnumerable<object[]> GetInvalidBaseMeasurementEnumMeasureUnitArgArrayList()
//        {
//            return DynamicDataSources.GetInvalidBaseMeasurementEnumMeasureUnitArgArrayList();
//        }

//        private static IEnumerable<object[]> GetInvalidGetCustomNameArgArrayList()
//        {
//            return DynamicDataSources.GetInvalidGetCustomNameArgArrayList();
//        }

//        private static IEnumerable<object[]> GetCustomNameCollectionArgArrayList()
//        {
//            return DynamicDataSources.GetCustomNameCollectionArgArrayList();
//        }

//        private static IEnumerable<object[]> GetCustomNameCollectionMeasureUnitCodeArgArrayList()
//        {
//            return DynamicDataSources.GetCustomNameCollectionMeasureUnitCodeArgArrayList();
//        }
//        #endregion
//    }
//}


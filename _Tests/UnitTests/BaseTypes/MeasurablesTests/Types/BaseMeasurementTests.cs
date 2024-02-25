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
//            _measureUnit = RandomParams.GetRandomValidMeasureUnit();
//            _measureUnitCode = MeasureUnitTypes.GetMeasureUnitCode(_measureUnit);
//            _factory = new BaseMeasurementFactoryClass();
//            baseMeasurement = new BaseMeasurementChild(_factory, _measureUnit);
//            baseMeasurement.RestoreConstantExchangeRates();
//        }
//        #endregion

//        #region Private fields
//        private IBaseMeasurement baseMeasurement;
//        private IBaseMeasurementFactory _factory;
//        private Enum _measureUnit;
//        string name;
//        MeasureUnitCode _measureUnitCode;

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
//            _factory = null;
//            _measureUnit = null;

//            // Act
//            void attempt() => _ = new BaseMeasurementChild(_factory, _measureUnit);

//            // Assert
//            var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
//            Assert.AreEqual(ParamNames._factory, ex.ParamName);
//        }

//        [TestMethod, TestCategory("UnitTest")]
//        public void BaseMeasurement_validArg_IBaseMeasurementFactory_nullArg_Enum_throws_ArgumentNullException()
//        {
//            // Arrange
//            _measureUnit = null;

//            // Act
//            void attempt() => _ = new BaseMeasurementChild(_factory, _measureUnit);

//            // Assert
//            var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
//            Assert.AreEqual(ParamNames._measureUnit, ex.ParamName);
//        }

//        [TestMethod, TestCategory("UnitTest")]
//        [DynamicData(nameof(GetInvalidBaseMeasurementEnumMeasureUnitArgArrayList), DynamicDataSourceType.Method)]
//        public void BaseMeasurement_validArg_IBaseMeasurementFactory_invalidArg_Enum_throws_InvalidEnumArgumentException(Enum _measureUnit)
//        {
//            // Arrange
//            // Act
//            void attempt() => _ = new BaseMeasurementChild(_factory, _measureUnit);

//            // Assert
//            var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
//            Assert.AreEqual(ParamNames._measureUnit, ex.ParamName);
//        }

//        [TestMethod, TestCategory("UnitTest")]
//        public void BaseMeasurement_validArgs_IBaseMeasurementFactory_Enum_creates()
//        {
//            // Arrange
//            _measureUnit = RandomParams.GetRandomValidMeasureUnit();
//            _measureUnitCode = MeasureUnitTypes.GetMeasureUnitCode(_measureUnit);

//            // Act
//            var actual = new BaseMeasurementChild(_factory, _measureUnit);

//            // Assert
//            Assert.IsInstanceOfType(actual, typeof(IBaseMeasurement));
//            Assert.AreEqual(_measureUnitCode, actual.MeasureUnitCode);
//        }
//        #endregion
//        #endregion

//        #region GetConstantExchangeRateCollection
//        #region GetConstantExchangeRateCollection()
//        [TestMethod, TestCategory("UnitTest")]
//        public void GetConstantExchangeRateCollection_returns_expected()
//        {
//            // Arrange
//            _measureUnit = RandomParams.GetRandomValidMeasureUnit();
//            _measureUnitCode = MeasureUnitTypes.GetMeasureUnitCode(_measureUnit);
//            baseMeasurement = new BaseMeasurementChild(_factory, _measureUnit);
//            IDictionary<object, decimal> expected = BaseMeasurementVariant.GetConstantExchangeRateCollection(_measureUnitCode);

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
//            _measureUnitCode = SampleParams.NotDefinedMeasureUnitCode;

//            // Act
//            void attempt() => _ = baseMeasurement.GetConstantExchangeRateCollection(_measureUnitCode);

//            // Assert
//            var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
//            Assert.AreEqual(ParamNames._measureUnitCode, ex.ParamName);
//        }

//        [TestMethod, TestCategory("UnitTest")]
//        public void GetConstantExchangeRateCollection_validArg_MeasureUnitCode_returns_expected()
//        {
//            _measureUnitCode = RandomParams.GetRandomMeasureUnitCode();
//            IDictionary<object, decimal> expected = BaseMeasurementVariant.GetConstantExchangeRateCollection(_measureUnitCode);

//            // Act
//            var actual = baseMeasurement.GetConstantExchangeRateCollection(_measureUnitCode);

//            // Assert
//            Assert.IsTrue(expected.SequenceEqual(actual));
//        }
//        #endregion
//        #endregion

//        #region GetCustomName
//        #region GetCustomName(Enum)
//        [TestMethod, TestCategory("UnitTest")]
//        [DynamicData(nameof(GetInvalidGetCustomNameArgArrayList), DynamicDataSourceType.Method)]
//        public void GetCustomName_nullOrInvalidArg_Enum_returns_null(Enum _measureUnit)
//        {
//            // Arrange
//            // Act
//            var actual = baseMeasurement.GetCustomName(_measureUnit);

//            // Assert
//            Assert.IsNull(actual);
//        }

//        [TestMethod, TestCategory("UnitTest")]
//        public void GetCustomName_validArg_Enum_returns_expected()
//        {
//            // Arrange
//            _measureUnit = RandomParams.GetRandomValidMeasureUnit();
//            baseMeasurement = new BaseMeasurementChild(_factory, _measureUnit);
//            name = Guid.NewGuid().ToString();
//            baseMeasurement.SetCustomName(_measureUnit, name);

//            // Act
//            var actual = baseMeasurement.GetCustomName(_measureUnit);

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
//            _measureUnitCode = SampleParams.NotDefinedMeasureUnitCode;

//            // Act
//            void attempt() => _ = baseMeasurement.GetCustomNameCollection(_measureUnitCode);

//            // Assert
//            var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
//            Assert.AreEqual(ParamNames._measureUnitCode, ex.ParamName);
//        }

//        [TestMethod, TestCategory("UnitTest")]
//        [DynamicData(nameof(GetCustomNameCollectionMeasureUnitCodeArgArrayList), DynamicDataSourceType.Method)]
//        public void GetCustomNameCollection_validArg_MeasureUnitCode_returns_expected(IDictionary<object, string> expected, MeasureUnitCode _measureUnitCode)
//        {
//            // Arrange
//            _measureUnit = MeasureUnitTypes.GetDefaultMeasureUnit(RandomParams.GetRandomMeasureUnitCode(_measureUnitCode));
//            name = Guid.NewGuid().ToString();
//            baseMeasurement.SetCustomName(_measureUnit, name);

//            foreach (KeyValuePair<object, string> item in expected)
//            {
//                baseMeasurement.SetCustomName((Enum)item.Key, item.Value);
//            }

//            // Act
//            var actual = baseMeasurement.GetCustomNameCollection(_measureUnitCode);

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
//            _measureUnit = null;

//            // Act
//            void attempt() => baseMeasurement.GetDefaultName(_measureUnit);

//            // Assert
//            var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
//            Assert.AreEqual(ParamNames._measureUnit, ex.ParamName);
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


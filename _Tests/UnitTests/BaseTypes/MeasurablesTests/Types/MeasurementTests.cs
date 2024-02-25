//using CsabaDu.FooVaria.Measurables.Factories.Implementations;
//using CsabaDu.FooVaria.Measurables.Types.Implementations;

//namespace CsabaDu.FooVaria.Tests.MeasurablesTests.UnitTests.Types
//{
//    [TestClass, TestCategory("UnitTest")]
//    public sealed class MeasurementTests
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
//            _factory = new MeasurementFactory();
//            measurement = _factory.Create(_measureUnit);
//        }
//        #endregion

//        #region Private fields
//        private IMeasurement measurement;
//        private IMeasurementFactory _factory;
//        private Enum _measureUnit;

//        #region Readonly fields
//        private readonly RandomParams RandomParams = new();
//        #endregion

//        #region Static fields
//        private static DynamicDataSources DynamicDataSources;
//        #endregion
//        #endregion

//        #region Test methods
//        #region Constructors
//        #region Measurement(IMeasurementFactory, Enum)
//        [TestMethod, TestCategory("UnitTest")]
//        [DynamicData(nameof(GetMeasurementFactoryNameArgsArrayList), DynamicDataSourceType.Method)]
//        public void Measurement_nullArgs_IMeasurementFactory_Enum_throws_ArgumentNullException(string _paramName, IMeasurementFactory _factory)
//        {
//            // Arrange
//            _measureUnit = null;

//            // Act
//            void attempt() => _ = new Measurement(_factory, _measureUnit);

//            // Assert
//            var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
//            Assert.AreEqual(_paramName, ex.ParamName);
//        }

//        [TestMethod, TestCategory("UnitTest")]
//        [DynamicData(nameof(GetInvalidEnumMeasureUnitArgArrayList), DynamicDataSourceType.Method)]
//        public void Measurement_validArg_IMeasurementFactory_invalidArg_Enum_throws_InvalidEnumArgumentException(Enum _measureUnit)
//        {
//            // Arrange
//            // Act
//            void attempt() => _ = new Measurement(_factory, _measureUnit);

//            // Assert
//            var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
//            Assert.AreEqual(ParamNames._measureUnit, ex.ParamName);
//        }

//        [TestMethod, TestCategory("UnitTest")]
//        public void Measurement_validArgs_IMeasurementFactory_Enum_creates()
//        {
//            // Arrange
//            _measureUnit = RandomParams.GetRandomValidMeasureUnit();
//            decimal exchangeRate = BaseMeasurement.ExchangeRateCollection.FirstOrDefault(x => x.Key.Equals(_measureUnit)).Value;

//            // Act
//            var actual = new Measurement(_factory, _measureUnit);

//            // Assert
//            Assert.IsInstanceOfType(actual, typeof(IMeasurement));
//            Assert.AreEqual(_measureUnit, actual.MeasureUnit);
//            Assert.AreEqual(exchangeRate, actual.ExchangeRate);
//        }
//        #endregion
//        #endregion

//        #region GetCustomName
//        #region GetCustomName()
//        //[TestMethod, TestCategory("UnitTest")]
//        //public void GetCustomName_returns_null()
//        //{
//        //    // Arrange
//        //    // Act
//        //    var actual = baseMeasurement.GetCustomName(_measureUnit);

//        //    // Assert
//        //    Assert.AreEqual(name, actual);
//        //}

//        // TODO measurement
//        //[TestMethod, TestCategory("UnitTest")]
//        //public void GetCustomName_returns_expected()
//        //{
//        //    // Arrange
//        //    _measureUnit = RandomParams.GetRandomValidMeasureUnit();
//        //    baseMeasurement = new BaseMeasurementChild(_factory, _measureUnit);
//        //    name = Guid.NewGuid().ToString();
//        //    baseMeasurement.SetCustomName(_measureUnit, name);

//        //    // Act
//        //    var actual = baseMeasurement.GetCustomName();

//        //    // Assert
//        //    Assert.AreEqual(name, actual);
//        //}
//        #endregion
//        #endregion

//        #region GetDefaultName
//        #region GetDefaultName()

//        #endregion
//        #endregion

//        //[TestMethod, TestCategory("UnitTest")]
//        //[TestMethod, TestCategory("UnitTest")]
//        //[TestMethod, TestCategory("UnitTest")]

//        #endregion

//        #region ArrayList methods
//        private static IEnumerable<object[]> GetInvalidEnumMeasureUnitArgArrayList()
//        {
//            return DynamicDataSources.GetInvalidEnumMeasureUnitArgArrayList();
//        }

//        private static IEnumerable<object[]> GetMeasurementFactoryNameArgsArrayList()
//        {
//            return DynamicDataSources.GetMeasurementFactoryNameArgsArrayList();
//        }
//        #endregion
//    }
//}


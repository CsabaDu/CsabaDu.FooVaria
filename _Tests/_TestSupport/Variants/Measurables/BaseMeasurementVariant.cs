//namespace CsabaDu.FooVaria.TestSupport.Variants.Measurables
//{
//    internal class BaseMeasurementVariant
//    {
//        internal IDictionary<object, decimal> GetConstantExchangeRateCollection(MeasureUnitCode measureUnitCode)
//        {
//            return GetMeasureUnitBasedCollection(BaseMeasurement.ConstantExchangeRateCollection, measureUnitCode);
//        }

//        internal IDictionary<object, string> GetCustomNameCollection(MeasureUnitCode measureUnitCode)
//        {
//            return GetMeasureUnitBasedCollection(BaseMeasurement.CustomNameCollection, measureUnitCode);
//        }

//        internal IDictionary<object, decimal> GetExchangeRateCollection(MeasureUnitCode measureUnitCode)
//        {
//            return GetMeasureUnitBasedCollection(BaseMeasurement.ExchangeRateCollection, measureUnitCode);
//        }

//        private static IDictionary<object, T> GetMeasureUnitBasedCollection<T>(IDictionary<object, T> measureUnitBasedCollection, MeasureUnitCode measureUnitCode) where T : notnull
//        {
//            _ = ExceptionMethods.Defined(measureUnitCode, nameof(measureUnitCode));

//            return getMeasureUnitBasedList()
//                .OrderBy(x => x.Key)
//                .ToDictionary(x => x.Key, x => x.Value);

//            #region Local methods
//            IEnumerable<KeyValuePair<object, T>> getMeasureUnitBasedList()
//            {
//                foreach (KeyValuePair<object, T> item in measureUnitBasedCollection)
//                {
//                    if (item.Key.GetType().Equals(MeasureUnitTypes.GetMeasureUnitType(measureUnitCode)))
//                    {
//                        yield return item;
//                    }
//                }
//            }
//            #endregion
//        }
//    }
//}

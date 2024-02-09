//namespace CsabaDu.FooVaria.TestSupport.Variants.Measurables
//{
//    internal class BaseMeasurementVariant
//    {
//        internal IDictionary<object, decimal> GetConstantExchangeRateCollection(MeasureUnitTypeCode measureUnitTypeCode)
//        {
//            return GetMeasureUnitBasedCollection(BaseMeasurement.ConstantExchangeRateCollection, measureUnitTypeCode);
//        }

//        internal IDictionary<object, string> GetCustomNameCollection(MeasureUnitTypeCode measureUnitTypeCode)
//        {
//            return GetMeasureUnitBasedCollection(BaseMeasurement.CustomNameCollection, measureUnitTypeCode);
//        }

//        internal IDictionary<object, decimal> GetExchangeRateCollection(MeasureUnitTypeCode measureUnitTypeCode)
//        {
//            return GetMeasureUnitBasedCollection(BaseMeasurement.ExchangeRateCollection, measureUnitTypeCode);
//        }

//        private static IDictionary<object, T> GetMeasureUnitBasedCollection<T>(IDictionary<object, T> measureUnitBasedCollection, MeasureUnitTypeCode measureUnitTypeCode) where T : notnull
//        {
//            _ = ExceptionMethods.Defined(measureUnitTypeCode, nameof(measureUnitTypeCode));

//            return getMeasureUnitBasedList()
//                .OrderBy(x => x.Key)
//                .ToDictionary(x => x.Key, x => x.Value);

//            #region Local methods
//            IEnumerable<KeyValuePair<object, T>> getMeasureUnitBasedList()
//            {
//                foreach (KeyValuePair<object, T> item in measureUnitBasedCollection)
//                {
//                    if (item.Key.GetType().Equals(MeasureUnitTypes.GetMeasureUnitType(measureUnitTypeCode)))
//                    {
//                        yield return item;
//                    }
//                }
//            }
//            #endregion
//        }
//    }
//}

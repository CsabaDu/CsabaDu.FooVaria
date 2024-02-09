//namespace CsabaDu.FooVaria.Tests.TestSupport.Common.Variants;

//public class BaseMeasurableVariant
//{
//    #region Public methods
//    internal Enum GetDefaultMeasureUnit(MeasureUnitTypeCode measureUnitTypeCode)
//    {
//        Type measureUnitType = MeasureUnitTypes.GetMeasureUnitType(measureUnitTypeCode)!;

//        return MeasureUnitTypes.GetDefaultMeasureUnit(measureUnitType);
//    }

//    internal IEnumerable<string> GetDefaultNames(MeasureUnitTypeCode? measureUnitTypeCode)
//    {
//        if (measureUnitTypeCode.HasValue) return getDefaultNames(measureUnitTypeCode.Value);

//        IEnumerable<MeasureUnitTypeCode> measureUnitTypeCodes = MeasureUnitTypes.GetMeasureUnitTypeCodes();
//        IEnumerable<string> defaultNames = getDefaultNames(measureUnitTypeCodes.First());

//        for (int i = 1; i < measureUnitTypeCodes.Count(); i++)
//        {
//            IEnumerable<string> next = getDefaultNames(measureUnitTypeCodes.ElementAt(i));
//            defaultNames = defaultNames.Union(next);
//        }

//        return defaultNames;

//        #region Local methods
//        static IEnumerable<string> getDefaultNames(MeasureUnitTypeCode measureUnitTypeCode)
//        {
//            Type measureUnitType = MeasureUnitTypes.GetMeasureUnitType(measureUnitTypeCode);

//            foreach (string item in Enum.GetNames(measureUnitType))
//            {
//                yield return item + measureUnitType.Name;
//            }
//        }
//        #endregion
//    }

//    internal Type GetMeasureUnitType(MeasureUnitTypeCode measureUnitTypeCode)
//    {
//        Enum measureUnit = MeasureUnitTypes.GetDefaultMeasureUnit(measureUnitTypeCode);

//        return measureUnit.GetType();
//    }

//    //internal MeasureUnitTypeCode GetMeasureUnitTypeCode(Enum measureUnit)
//    //{
//    //    Type measureUnitType = measureUnit.GetType();

//    //    string measureUnitTypeName = measureUnitType.Name;

//    //    return (MeasureUnitTypeCode)Enum.Parse(typeof(MeasureUnitTypeCode), measureUnitTypeName);
//    //}

//    //internal IEnumerable<MeasureUnitTypeCode> GetMeasureUnitTypeCodes()
//    //{
//    //    return Enum.GetValues<MeasureUnitTypeCode>();
//    //}
//    #endregion
//}

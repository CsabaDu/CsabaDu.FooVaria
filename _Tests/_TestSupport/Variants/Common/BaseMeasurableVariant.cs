//namespace CsabaDu.FooVaria.Tests.TestSupport.Common.Variants;

//public class BaseMeasurableVariant
//{
//    #region Public methods
//    internal Enum GetDefaultMeasureUnit(MeasureUnitCode measureUnitCode)
//    {
//        Type measureUnitType = MeasureUnitTypes.GetMeasureUnitType(measureUnitCode)!;

//        return MeasureUnitTypes.GetDefaultMeasureUnit(measureUnitType);
//    }

//    internal IEnumerable<string> GetDefaultNames(MeasureUnitCode? measureUnitCode)
//    {
//        if (measureUnitCode.HasValue) return getDefaultNames(measureUnitCode.Value);

//        IEnumerable<MeasureUnitCode> measureUnitTypeCodes = MeasureUnitTypes.GetMeasureUnitCodes();
//        IEnumerable<string> defaultNames = getDefaultNames(measureUnitTypeCodes.First());

//        for (int i = 1; i < measureUnitTypeCodes.Count(); i++)
//        {
//            IEnumerable<string> next = getDefaultNames(measureUnitTypeCodes.ElementAt(i));
//            defaultNames = defaultNames.Union(next);
//        }

//        return defaultNames;

//        #region Local methods
//        static IEnumerable<string> getDefaultNames(MeasureUnitCode measureUnitCode)
//        {
//            Type measureUnitType = MeasureUnitTypes.GetMeasureUnitType(measureUnitCode);

//            foreach (string item in Enum.GetNames(measureUnitType))
//            {
//                yield return item + measureUnitType.Name;
//            }
//        }
//        #endregion
//    }

//    internal Type GetMeasureUnitType(MeasureUnitCode measureUnitCode)
//    {
//        Enum measureUnit = MeasureUnitTypes.GetDefaultMeasureUnit(measureUnitCode);

//        return measureUnit.GetType();
//    }

//    //internal MeasureUnitCode GetMeasureUnitCode(Enum measureUnit)
//    //{
//    //    Type measureUnitType = measureUnit.GetType();

//    //    string measureUnitTypeName = measureUnitType.Name;

//    //    return (MeasureUnitCode)Enum.Parse(typeof(MeasureUnitCode), measureUnitTypeName);
//    //}

//    //internal IEnumerable<MeasureUnitCode> GetMeasureUnitCodes()
//    //{
//    //    return Enum.GetValues<MeasureUnitCode>();
//    //}
//    #endregion
//}

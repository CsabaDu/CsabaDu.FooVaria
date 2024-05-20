﻿namespace CsabaDu.FooVaria.Tests.TestHelpers.Fakes.BaseTypes.BaseRates
{
    public class BaseRateChild(IRootObject rootObject, string paramName) : BaseRate(rootObject, paramName)
    {
        #region Members

        // BaseRate(IRootObject rootObject, string paramName)
        // RateComponentCode IBaseRate.GetNumeratorCode()
        // RateComponentCode IBaseRate.GetMeasureUnitCode(RateComponentCode rateComponentCode)
        // IBaseRate IBaseRate.GetBaseRate(IQuantifiable numerator, Enum denominator)
        // IBaseRate IBaseRate.GetBaseRate(IQuantifiable numerator, IMeasurable denominator)
        // IBaseRate IBaseRate.GetBaseRate(IQuantifiable numerator, IQuantifiable denominator)
        // void IBaseRate.ValidateRateComponentCode(RateComponentCode rateComponentCode, string paramName)
        // void IBaseQuantifiable.ValidateQuantity(ValueType? quantity, string paramName)
        // void IMeasurable.ValidateMeasureUnitCode(IMeasurable? measurable, string paramName)
        // IFactory ICommonBase.GetFactoryValue()
        // Enum IDefaultMeasureUnit.GetDefaultMeasureUnit()
        // IEnumerable<string> IDefaultMeasureUnit.GetDefaultMeasureUnitNames()
        // void IDefaultMeasureUnit.ValidateMeasureUnit(Enum? measureUnit, string paramName)
        // bool IMeasureUnitCode.HasMeasureUnitCode(RateComponentCode measureUnitCode)
        // RateComponentCode IMeasureUnitCode.GetMeasureUnitCode()
        // void IMeasureUnitCode.ValidateMeasureUnitCode(RateComponentCode measureUnitCode, string paramName)
        // TypeCode IQuantityType.GetQuantityTypeCode()
        // Enum IMeasureUnit.GetBaseMeasureUnitValue()
        // Type IMeasureUnit.GetMeasureUnitType()
        // decimal IDefaultQuantity.GetDefaultQuantityValue()
        // bool? ILimitable.FitsIn(ILimiter? limiter)
        // decimal IQuantity<decimal>.GetQuantity()
        // ValueType IQuantity.GetBaseQuantityValue()
        // object IQuantity.GetQuantity(TypeCode quantityTypeCode)
        // decimal IProportional<IBaseRate>.ProportionalTo(IBaseRate? other)
        // bool IExchangeable<IBaseRate>.IsExchangeableTo(IBaseRate? context)
        // bool? IFit<IBaseRate>.FitsIn(IBaseRate? other, LimitMode? limitMode)
        // int IComparable<IBaseRate>.CompareTo(IBaseRate? other)
        // bool IEquatable<IBaseRate>.Equals(IBaseRate? other)
        // bool BaseRate.Equals(object? obj)
        // int BaseRate.GetHashCode()
        // RateComponentCode IDenominate.GetDenominatorCodeValue()
        // object? IValidRateComponent.GetRateComponent(RateComponentCode rateComponentCode)
        // bool IValidRateComponent.IsValidRateComponent(object? rateComponent, RateComponentCode rateComponentCode)
        // LimitMode? ILimitMode.GetLimitMode()
        // IEnumerable<RateComponentCode> IMeasureUnitCodes.GetMeasureUnitCodes()
        // void IMeasureUnitCodes.ValidateMeasureUnitCodes(IBaseQuantifiable? baseQuantifiable, string paramName)

        #endregion

        #region Test helpers
        public BaseRateReturnValues ReturnValues { private get; set; }
        protected MeasureUnitCode DenominatorCode { private get; set; }
        protected static BaseRateReturnValues GetReturnValues(Enum measureUnit, decimal defaultQuantity, IBaseRateFactory factory = null)
        {
            return new()
            {
                GetBaseMeasureUnitValue = measureUnit,
                GetDefaultQuantityValue = defaultQuantity,
                GetFactoryValue = factory,
            };
        }

        public static BaseRateChild GetBaseRateChild(Enum measureUnit, decimal defaultQuantity, MeasureUnitCode denominatorCode, IBaseRateFactory factory = null)
        {
            return new(Fields.RootObject, Fields.paramName)
            {
                ReturnValues = GetReturnValues(measureUnit, defaultQuantity, factory),
                DenominatorCode = denominatorCode,
            };
        }

        public static BaseRateChild GetBaseRateChild(DataFields fields, IBaseRateFactory factory = null)
        {
            return GetBaseRateChild(fields.measureUnit, fields.defaultQuantity, fields.denominatorCode, factory);
        }

        public static BaseRateChild GetBaseRateChild(IQuantifiable quantifiable, MeasureUnitCode denominatorCode, IBaseRateFactory factory = null)
        {
            return GetBaseRateChild(quantifiable.GetBaseMeasureUnit(), quantifiable.GetDefaultQuantity(), denominatorCode, factory);
        }
        #endregion

        public override sealed object GetRateComponent(RateComponentCode rateComponentCode)
        {
            return rateComponentCode switch
            {
                RateComponentCode.Numerator => GetMeasureUnitCode(),
                RateComponentCode.Denominator => DenominatorCode,

                _ => null,
            };
        }

        public override sealed decimal GetDefaultQuantity() => ReturnValues.GetDefaultQuantityValue;

        public override sealed Enum GetBaseMeasureUnit() => ReturnValues.GetBaseMeasureUnitValue;

        public override sealed IFactory GetFactory() => ReturnValues.GetFactoryValue;
    }
}
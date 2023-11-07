﻿namespace CsabaDu.FooVaria.Common.Types.Implementations
{
    public abstract class BaseShape : BaseSpread, IBaseShape
    {
        protected BaseShape(IBaseShape other) : base(other)
        {
        }

        protected BaseShape(IBaseShapeFactory factory, IBaseShape baseShape) : base(factory, baseShape)
        {
        }

        protected BaseShape(IBaseSpreadFactory factory, MeasureUnitTypeCode measureUnitTypeCode, params IBaseMeasurable[] baseMeasurables) : base(factory, measureUnitTypeCode, baseMeasurables)
        {
        }

        public abstract int CompareTo(IBaseShape? other);
        public abstract bool Equals(IBaseShape? other);
        public abstract bool? FitsIn(IBaseShape? comparable, LimitMode? limitMode);

        public override IEnumerable<MeasureUnitTypeCode> GetMeasureUnitTypeCodes()
        {
            return base.GetMeasureUnitTypeCodes().Append(MeasureUnitTypeCode.ExtentUnit);
        }

        public override IBaseShapeFactory GetFactory()
        {
            return (IBaseShapeFactory)Factory;
        }

        public abstract int GetShapeExtentCount();
        public abstract void ValidateShapeExtent(IQuantifiable shapeExtent, string paramName);
    }
}

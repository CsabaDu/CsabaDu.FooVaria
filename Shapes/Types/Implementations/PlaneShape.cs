using CsabaDu.FooVaria.Spreads.Types;


namespace CsabaDu.FooVaria.Shapes.Types.Implementations
{
    internal abstract class PlaneShape : Shape, IPlaneShape
    {
        private protected PlaneShape(IPlaneShape other) : base(other)
        {
            Area = other.Area;
        }

        private protected PlaneShape(IPlaneShapeFactory factory, params IExtent[] shapeExtents) : base(factory, MeasureUnitTypeCode.AreaUnit, shapeExtents)
        {
            Area = GetArea(shapeExtents, nameof(shapeExtents));
        }

        public IArea Area { get; init; }

        public override sealed IArea GetSpreadMeasure()
        {
            return Area;
        }

        private IArea GetArea(object arg, string paramName)
        {
            if (arg is IBaseShape baseShape)
            {
                return getArea(getBaseShape());
            }
            else if (arg is IExtent[] shapeExtents)
            {
                return getArea(getSpread(shapeExtents));
            }
            else
            {
                throw new InvalidOperationException(null);
            }

            #region Local methods
            IArea getArea(IBaseSpread baseSpread)
            {
                return (IArea)baseSpread.GetSpreadMeasure();
            }

            IBaseShape getBaseShape()
            {
                Validate(baseShape, paramName);

                return baseShape;
            }

            ISpread getSpread(IExtent[] shapeExtents)
            {
                return GetSpreadFactory().Create(shapeExtents);
            }
            #endregion
        }
    }
}

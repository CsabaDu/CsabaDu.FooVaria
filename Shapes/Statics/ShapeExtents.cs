namespace CsabaDu.FooVaria.Shapes.Statics
{
    public static class ShapeExtents
    {
        #region Public methods
        public static IExtent GetDiagonal(IShape shape, ExtentUnit extentUnit = default)
        {
            return NullChecked(shape, nameof(shape)) switch
            {
                Circle circle => getCircleDiagonal(circle),
                Cuboid cuboid => getCuboidDiagonal(cuboid),
                Cylinder cylinder => getCylinderDiagonal(cylinder),
                Rectangle rectangle => getRectangleDiagonal(rectangle),

                _ => throw new InvalidOperationException(null)
            };

            #region Local methods
            IExtent getCircleDiagonal(ICircle circle)
            {
                ValidateMeasureUnit(extentUnit, nameof(extentUnit));

                IRateComponent radius = circle.Radius;
                decimal quantity = radius.DefaultQuantity * 2;
                quantity = IsDefaultMeasureUnit(extentUnit) ?
                    quantity
                    : quantity / GetSpreadMeasureUnitExchangeRate(shape, extentUnit);


                return (IExtent)radius.GetRateComponent(extentUnit, quantity);
            }

            IExtent getCuboidDiagonal(ICuboid cuboid)
            {
                return getRectangularShapeDiagonal(cuboid);
            }

            IExtent getCylinderDiagonal(ICylinder cylinder)
            {
                IRectangle verticalProjection = cylinder.GetVerticalProjection();

                return getRectangleDiagonal(verticalProjection);
            }

            IExtent getRectangleDiagonal(IRectangle rectangle)
            {
                return getRectangularShapeDiagonal(rectangle);
            }

            IExtent getRectangularShapeDiagonal<T>(T shape)
                where T : class, IShape, IRectangularShape
            {
                ValidateMeasureUnit(extentUnit, nameof(extentUnit));

                IEnumerable<ShapeExtentTypeCode> shapeExtentTypeCodes = shape.GetShapeExtentTypeCodes();
                int i = 0;
                decimal quantitySquares = getDefaultQuantitySquare();

                for (i = 1; i < shapeExtentTypeCodes.Count(); i++)
                {
                    quantitySquares += getDefaultQuantitySquare();
                }

                double quantity = GetExchangedQuantitySqrt(shape, extentUnit, quantitySquares);
                IExtent edge = getShapeExtent();

                return edge.GetMeasure(extentUnit, quantity);

                #region Local methods
                IExtent getShapeExtent()
                {
                    return shape.GetShapeExtent(shapeExtentTypeCodes.ElementAt(i));
                }

                decimal getDefaultQuantitySquare()
                {
                    return GetDefaultQuantitySquare(getShapeExtent());
                }
                #endregion
            }
            #endregion
        }

        public static IExtent GetInnerTangentRectangleSide(ICircle circle, ExtentUnit extentUnit = default)
        {
            IExtent diagonal = NullChecked(circle, nameof(circle)).GetDiagonal();
            decimal quantitySquare = GetDefaultQuantitySquare(diagonal) / 2;
            double quantity = GetExchangedQuantitySqrt(circle, extentUnit, quantitySquare);

            return diagonal.GetMeasure(extentUnit, quantity);
        }

        public static IExtent GetInnerTangentRectangleSide(ICircle circle, IExtent tangentRectangleSide, ExtentUnit extentUnit = default)
        {
            NullChecked(circle, nameof(circle)).ValidateShapeComponent(tangentRectangleSide, nameof(tangentRectangleSide));
            ValidateMeasureUnit(extentUnit, nameof(extentUnit));

            decimal sideQuantitySquare = GetDefaultQuantitySquare(tangentRectangleSide);
            IExtent diagonal = circle.GetDiagonal();
            double quantity;

            if (tangentRectangleSide.CompareTo(diagonal) <= 0)
            {
                quantity = tangentRectangleSide.GetQuantity();

                throw QuantityArgumentOutOfRangeException(nameof(tangentRectangleSide), quantity);
            }

            decimal quantitySquare = GetDefaultQuantitySquare(diagonal) - sideQuantitySquare;
            quantity = GetExchangedQuantitySqrt(circle, extentUnit, quantitySquare);

            return diagonal.GetMeasure(extentUnit, quantity);
        }
        #endregion

        #region Private methods
        private static decimal GetDefaultQuantitySquare(IExtent extent)
        {
            decimal quantity = extent.DefaultQuantity;

            return quantity * quantity;
        }

        private static double GetExchangedQuantitySqrt(IShape shape, ExtentUnit extentUnit, decimal quantitySquare)
        {
            double quantity = decimal.ToDouble(quantitySquare);
            quantity = Math.Sqrt(quantity);

            return IsDefaultMeasureUnit(extentUnit) ?
                quantity
                : quantity / decimal.ToDouble(GetSpreadMeasureUnitExchangeRate(shape, extentUnit));

        }

        private static decimal GetSpreadMeasureUnitExchangeRate(IShape shape, Enum measureUnit)
        {
            IRateComponent spreadMeasure = (IRateComponent)shape.GetSpreadMeasure();

            return spreadMeasure.Measurement.GetExchangeRate(measureUnit);
        }
        #endregion
    }
}

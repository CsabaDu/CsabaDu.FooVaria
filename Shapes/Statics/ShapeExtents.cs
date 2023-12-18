using CsabaDu.FooVaria.Common.Enums.MeasureUnits;
using CsabaDu.FooVaria.Shapes.Types;
using CsabaDu.FooVaria.Shapes.Types.Implementations;

namespace CsabaDu.FooVaria.Shapes.Statics
{
    public static class ShapeExtents
    {
        public static IExtent GetDiagonal(IShape shape, ExtentUnit extentUnit = default)
        {
            return NullChecked(shape, nameof(shape)) switch
            {
                Circle circle => GetCircleDiagonal(circle, extentUnit),
                Cuboid cuboid => GetCuboidDiagonal(cuboid, extentUnit),
                Cylinder cylinder => GetCylinderDiagonal(cylinder, extentUnit),
                Rectangle rectangle => GetRectangleDiagonal(rectangle, extentUnit),

                _ => throw new InvalidOperationException(null)
            };
        }

        public static IExtent GetInnerTangentRectangleSide(ICircle circle, IExtent innerTangentRectangleSide, ExtentUnit extentUnit = default)
        {
            NullChecked(circle, nameof(circle)).ValidateShapeExtent(innerTangentRectangleSide, nameof(innerTangentRectangleSide));
            ValidateMeasureUnit(extentUnit, nameof(extentUnit));

            decimal sideQuantitySquare = GetDefaultQuantitySquare(innerTangentRectangleSide);
            IExtent diagonal = circle.GetDiagonal();
            double quantity;

            if (innerTangentRectangleSide.CompareTo(diagonal) <= 0)
            {
                quantity = innerTangentRectangleSide.GetQuantity();

                throw QuantityArgumentOutOfRangeException(nameof(innerTangentRectangleSide), quantity);
            }

            decimal quantitySquare = GetDefaultQuantitySquare(diagonal) - sideQuantitySquare;
            quantity = GetExchangedQuantitySqrt(circle, extentUnit, quantitySquare);

            return innerTangentRectangleSide.GetMeasure(extentUnit, quantity);
        }

        private static decimal GetDefaultQuantitySquare(IExtent extent)
        {
            return Square(extent.DefaultQuantity);
        }

        public static IExtent GetShapeExtent(IVolume volume, IPlaneShape planeShape, ExtentUnit extentUnit = default)
        {
            decimal quantity = NullChecked(volume, nameof(volume)).DefaultQuantity;
            quantity /= NullChecked(planeShape, nameof(planeShape)).Area.DefaultQuantity;
            quantity = Exchange(planeShape, extentUnit, quantity);

            return GetExtent(volume, extentUnit, quantity);
        }

        private static IExtent GetRectangleDiagonal(IRectangle rectangle, ExtentUnit extentUnit)
        {
            return GetRectangularShapeDiagonal(rectangle, extentUnit);
        }

        private static IExtent GetCircleDiagonal(ICircle circle, ExtentUnit extentUnit)
        {
            ValidateMeasureUnit(extentUnit, nameof(extentUnit));

            IRateComponent radius = circle.Radius;
            decimal quantity = radius.DefaultQuantity * 2;
            quantity = Exchange(circle, extentUnit, quantity);

            return GetExtent(radius, extentUnit, quantity);
        }

        private static IExtent GetCuboidDiagonal(ICuboid cuboid, ExtentUnit extentUnit)
        {
            return GetRectangularShapeDiagonal(cuboid, extentUnit);
        }

        private static IExtent GetCylinderDiagonal(ICylinder cylinder, ExtentUnit extentUnit)
        {
            IRectangle verticalProjection = cylinder.GetVerticalProjection();

            return GetRectangleDiagonal(verticalProjection, extentUnit);
        }

        private static IExtent GetExtent(IRateComponent rateComponent, ExtentUnit extentUnit, ValueType quantity)
        {
            return (IExtent)rateComponent.GetRateComponent(extentUnit, quantity);
        }

        private static IExtent GetRectangularShapeDiagonal<T>(T shape, ExtentUnit extentUnit) where T : class, IShape, IRectangularShape
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

        private static double GetExchangedQuantitySqrt(IShape shape, ExtentUnit extentUnit, decimal quantitySquare)
        {
            double quantity = decimal.ToDouble(quantitySquare);
            quantity = Math.Sqrt(quantity);

            return Exchange(shape, extentUnit, quantity);

        }
        private static decimal Exchange(IShape shape, Enum measureUnit,  decimal quantity)
        {
            return IsDefaultMeasureUnit(measureUnit) ?
                quantity
                : quantity / GetSpreadMeasureUnitExchangeRate(shape, measureUnit);
        }

        private static double Exchange(IShape shape, Enum measureUnit, double quantity)
        {
            return IsDefaultMeasureUnit(measureUnit) ?
                quantity
                : quantity / decimal.ToDouble(GetSpreadMeasureUnitExchangeRate(shape, measureUnit));
        }

        private static decimal GetSpreadMeasureUnitExchangeRate(IShape shape, Enum measureUnit)
        {
            IMeasure spreadMeasure = (IMeasure)shape.GetSpreadMeasure();

            return spreadMeasure.Measurement.GetExchangeRate(measureUnit);
        }

        private static decimal Square(decimal quantity)
        {
            return quantity * quantity;
        }

    }
}

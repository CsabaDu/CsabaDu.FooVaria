using CsabaDu.FooVaria.Measurables.Behaviors;
using CsabaDu.FooVaria.Measurables.Statics;
using CsabaDu.FooVaria.Measurables.Types;
using CsabaDu.FooVaria.Shapes.Types;
using CsabaDu.FooVaria.Shapes.Types.Implementations;
using Rectangle = CsabaDu.FooVaria.Shapes.Types.Implementations.Rectangle;

namespace CsabaDu.FooVaria.Shapes.Statics
{
    public static class ShapeExtents
    {
        public static IExtent GetDiagonal(IShape shape, ExtentUnit extentUnit = default)
        {
            IExtent shapeExtent = NullChecked(shape, nameof(shape)).GetShapeExtents().First();

            shapeExtent.ValidateMeasureUnit(extentUnit, nameof(extentUnit));

            return shape switch
            {
                Rectangle rectangle => GetRectangleDiagonal(rectangle, extentUnit),
                Circle circle => GetCircleDiagonal(circle, extentUnit),

                _ => throw new InvalidOperationException(null)
            };

            throw new NotImplementedException();
        }

        public static IExtent GetInnerTangentRectangleSide(ICircle circle, IExtent innerTangentRectangleSide, ExtentUnit extentUnit = default)
        {
            string paramName = nameof(innerTangentRectangleSide);

            NullChecked(circle, nameof(circle)).ValidateShapeExtent(innerTangentRectangleSide, paramName);
            _ = Defined(extentUnit, nameof(extentUnit));

            decimal sideQuantity = innerTangentRectangleSide.DefaultQuantity;
            IExtent diagonal = circle.GetDiagonal();

            if (innerTangentRectangleSide.CompareTo(diagonal) <= 0) throw QuantityArgumentOutOfRangeException(paramName, sideQuantity);

            decimal diagonalQuantity = diagonal.DefaultQuantity;
            diagonalQuantity *= diagonalQuantity;
            sideQuantity *= sideQuantity;
            sideQuantity = diagonalQuantity - sideQuantity;
            double otherSideQuantity = Math.Sqrt(decimal.ToDouble(sideQuantity));
            otherSideQuantity = Exchange(circle, otherSideQuantity, extentUnit);

            return innerTangentRectangleSide.GetMeasure(otherSideQuantity, extentUnit);
        }

        public static IExtent GetShapeExtent(IVolume volume, IPlaneShape planeShape, ExtentUnit extentUnit = default)
        {
            decimal quantity = NullChecked(volume, nameof(volume)).DefaultQuantity;
            quantity /= NullChecked(planeShape, nameof(planeShape)).Area.DefaultQuantity;
            quantity = Exchange(planeShape, quantity, extentUnit);

            return (IExtent)volume.GetMeasure(quantity, extentUnit);
        }

        private static IExtent GetRectangleDiagonal(IRectangle rectangle, ExtentUnit extentUnit = default)
        {
            IExtent length = rectangle.Length;
            decimal quantity = square(length.DefaultQuantity);
            quantity += square(rectangle.Width.DefaultQuantity);
            double diagonalQuantity = Math.Sqrt(decimal.ToDouble(quantity));
            diagonalQuantity = Exchange(rectangle, diagonalQuantity, extentUnit);

            return length.GetMeasure(diagonalQuantity, extentUnit);

            #region Local methods
            static decimal square(decimal quantity)
            {
                return quantity * quantity;
            }
            #endregion
        }

        private static IExtent GetCircleDiagonal(ICircle circle, ExtentUnit extentUnit = default)
        {
            IMeasure radius = circle.Radius;
            decimal diagonalQuantity = radius.DefaultQuantity * 2;
            diagonalQuantity = Exchange(circle, diagonalQuantity, extentUnit);

            return (IExtent)radius.GetMeasure(diagonalQuantity, extentUnit);
        }

        private static decimal Exchange(IShape shape, decimal quantity, Enum measureUnit)
        {
            if (!IsDefaultMeasureUnit(measureUnit))
            {
                quantity /= GetExchangeRate(shape, measureUnit);
            }

            return quantity;
        }

        private static double Exchange(IShape shape, double quantity, Enum measureUnit)
        {
            if (!IsDefaultMeasureUnit(measureUnit))
            {
                quantity /= decimal.ToDouble(GetExchangeRate(shape, measureUnit));
            }

            return quantity;
        }

        private static decimal GetExchangeRate(IShape shape, Enum measureUnit)
        {
            IMeasure spreadMeasure = (IMeasure)shape.GetSpreadMeasure();

            return spreadMeasure.Measurement.GetExchangeRate(measureUnit);
        }

        private static bool IsDefaultMeasureUnit(Enum measureUnit)
        {
            return (int)(object)measureUnit == default;
        }
    }
}

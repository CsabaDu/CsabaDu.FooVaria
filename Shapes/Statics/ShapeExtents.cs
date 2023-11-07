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

        public static IExtent GetInnerTangentRectangleSide(ICircle circle, IExtent innerTangentRectangleSide
            
            )
        {
            string paramName = nameof(innerTangentRectangleSide);
            decimal sideQuantity = innerTangentRectangleSide.DefaultQuantity;

            circle.ValidateShapeExtent(innerTangentRectangleSide, paramName);

            if (innerTangentRectangleSide.CompareTo(circle.GetDiagonal()) <= 0) throw QuantityArgumentOutOfRangeException(paramName, sideQuantity);

            decimal diagonalQuantity = circle.GetDiagonal().DefaultQuantity;
            diagonalQuantity *= diagonalQuantity;
            sideQuantity *= sideQuantity;
            sideQuantity = diagonalQuantity - sideQuantity;
            double otherSideQuantity = Math.Sqrt(decimal.ToDouble(sideQuantity));

            return innerTangentRectangleSide.GetMeasure(otherSideQuantity, default(ExtentUnit));
        }

        private static IExtent GetRectangleDiagonal(IRectangle rectangle, ExtentUnit extentUnit = default)
        {
            IExtent length = rectangle.Length;
            decimal quantity = square(length.DefaultQuantity);
            quantity += square(rectangle.Width.DefaultQuantity);
            double diagonalQuantity = Math.Sqrt(decimal.ToDouble(quantity));
            diagonalQuantity = Exchange(diagonalQuantity, extentUnit);

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
            diagonalQuantity = Exchange(diagonalQuantity, extentUnit);

            return (IExtent)radius.GetMeasure(diagonalQuantity, extentUnit);
        }

        private static decimal Exchange(decimal quantity, Enum measureUnit)
        {
            return quantity / MeasureUnits.GetExchangeRate(measureUnit);
        }

        private static double Exchange(double quantity, Enum measureUnit)
        {
            return quantity / decimal.ToDouble(MeasureUnits.GetExchangeRate(measureUnit));
        }

    }

}

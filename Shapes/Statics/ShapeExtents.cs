using CsabaDu.FooVaria.Common.Enums;
using CsabaDu.FooVaria.Common.Statics;
using CsabaDu.FooVaria.Measurables.Statics;
using CsabaDu.FooVaria.Shapes.Behaviors;
using CsabaDu.FooVaria.Shapes.Types;
using CsabaDu.FooVaria.Shapes.Types.Implementations;
using System.ComponentModel;
using System.Drawing;
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

                _ => throw new InvalidOperationException(null)
            };

            throw new NotImplementedException();
        }

        private static IExtent GetRectangleDiagonal(IRectangle rectangle, ExtentUnit extentUnit = default)
        {
            IExtent length = rectangle.Length;

            length.ValidateMeasureUnit(extentUnit, nameof(extentUnit));

            decimal lengthQuantity = length.DefaultQuantity;
            lengthQuantity *= lengthQuantity;
            decimal widthQuantity = rectangle.Width.DefaultQuantity;
            widthQuantity *= widthQuantity;
            double diagonalQuantity = Math.Sqrt(Convert.ToDouble(lengthQuantity + widthQuantity));
            diagonalQuantity /= Convert.ToDouble(MeasureUnits.GetExchangeRate(extentUnit)); 

            return length.GetMeasure(diagonalQuantity, extentUnit);
        }
    }

    public static class ExceptionMethods
    {
        public static InvalidEnumArgumentException InvalidShapeExtentTypeCodeEnumArgumentException(ShapeExtentTypeCode shapeExtentTypeCode)
        {
            return new InvalidEnumArgumentException(nameof(shapeExtentTypeCode), (int)shapeExtentTypeCode, shapeExtentTypeCode.GetType());
        }
    }

}

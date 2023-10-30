using CsabaDu.FooVaria.Common.Enums;
using CsabaDu.FooVaria.Shapes.Types;
using System.ComponentModel;

namespace CsabaDu.FooVaria.Shapes.Statics
{
    public static class ShapeExtents
    {
        public static IExtent GetDiagonal(IShape shape, ExtentUnit extentUnit = default)
        {
            throw new NotImplementedException();
        }

        //public static IExtent GetCircleDiagonal(ICircle circle, ExtentUnit extentUnit)
        //{

        //}
    }

    public static class ExceptionMethods
    {
        public static InvalidEnumArgumentException InvalidShapeExtentTypeCodeEnumArgumentException(ShapeExtentTypeCode shapeExtentTypeCode)
        {
            return new InvalidEnumArgumentException(nameof(shapeExtentTypeCode), (int)shapeExtentTypeCode, shapeExtentTypeCode.GetType());
        }
    }

}

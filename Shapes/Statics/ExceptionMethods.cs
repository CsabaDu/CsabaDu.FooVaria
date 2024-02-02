using System.ComponentModel;

namespace CsabaDu.FooVaria.Shapes.Statics
{
    public static class ExceptionMethods
    {
        public static InvalidEnumArgumentException InvalidShapeExtentCodeEnumArgumentException(ShapeExtentCode shapeExtentCode)
        {
            return InvalidShapeExtentCodeEnumArgumentException(shapeExtentCode, nameof(shapeExtentCode));
        }

        public static InvalidEnumArgumentException InvalidShapeExtentCodeEnumArgumentException(ShapeExtentCode shapeExtentCode, string paramName)
        {
            return new InvalidEnumArgumentException(paramName, (int)shapeExtentCode, shapeExtentCode.GetType());
        }

    }

}

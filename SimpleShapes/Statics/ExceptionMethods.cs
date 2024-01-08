using System.ComponentModel;

namespace CsabaDu.FooVaria.SimpleShapes.Statics
{
    public static class ExceptionMethods
    {
        public static InvalidEnumArgumentException InvalidShapeExtentTypeCodeEnumArgumentException(ShapeExtentTypeCode shapeExtentTypeCode)
        {
            return InvalidShapeExtentTypeCodeEnumArgumentException(shapeExtentTypeCode, nameof(shapeExtentTypeCode));
        }

        public static InvalidEnumArgumentException InvalidShapeExtentTypeCodeEnumArgumentException(ShapeExtentTypeCode shapeExtentTypeCode, string paramName)
        {
            return new InvalidEnumArgumentException(paramName, (int)shapeExtentTypeCode, shapeExtentTypeCode.GetType());
        }

    }

}

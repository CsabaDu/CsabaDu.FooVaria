using System.ComponentModel;

namespace CsabaDu.FooVaria.SimpleShapes.Statics
{
    public static class ExceptionMethods
    {
        public static InvalidEnumArgumentException InvalidShapeExtentCodeEnumArgumentException(ShapeExtentCode simpleShapeExtentCode)
        {
            return InvalidShapeExtentCodeEnumArgumentException(simpleShapeExtentCode, nameof(simpleShapeExtentCode));
        }

        public static InvalidEnumArgumentException InvalidShapeExtentCodeEnumArgumentException(ShapeExtentCode simpleShapeExtentCode, string paramName)
        {
            return new InvalidEnumArgumentException(paramName, (int)simpleShapeExtentCode, simpleShapeExtentCode.GetType());
        }

    }

}

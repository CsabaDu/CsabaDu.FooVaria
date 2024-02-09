using System.ComponentModel;

namespace CsabaDu.FooVaria.SimpleShapes.Statics
{
    public static class ExceptionMethods
    {
        public static InvalidEnumArgumentException InvalidSimpleShapeExtentCodeEnumArgumentException(SimpleShapeExtentCode simpleShapeExtentCode)
        {
            return InvalidSimpleShapeExtentCodeEnumArgumentException(simpleShapeExtentCode, nameof(simpleShapeExtentCode));
        }

        public static InvalidEnumArgumentException InvalidSimpleShapeExtentCodeEnumArgumentException(SimpleShapeExtentCode simpleShapeExtentCode, string paramName)
        {
            return new InvalidEnumArgumentException(paramName, (int)simpleShapeExtentCode, simpleShapeExtentCode.GetType());
        }

    }

}

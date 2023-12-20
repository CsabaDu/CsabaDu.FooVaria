using static CsabaDu.FooVaria.Spreads.Statics.SpreadMeasures;

namespace CsabaDu.FooVaria.Shapes.Factories.Implementations
{
    public sealed class RectangleFactory : PlaneShapeFactory, IRectangleFactory
    {
        #region Constructors
        public RectangleFactory(IBulkSurfaceFactory spreadFactory, ICircleFactory tangentShapeFactory) : base(spreadFactory, tangentShapeFactory)
        {
        }
        #endregion

        #region Public methods
        public IRectangle Create(IExtent length, IExtent width)
        {
            return new Rectangle(this, length, width);
        }

        public IRectangle Create(IRectangle other)
        {
            return new Rectangle(other);
        }

        public ICircle CreateInnerTangentShape(IRectangle rectangle, ComparisonCode comparisonCode)
        {
            IExtent diagonal = NullChecked(rectangle, nameof(rectangle)).GetComparedShapeExtent(comparisonCode);

            return CreateCircle(diagonal);
        }

        public ICircle CreateInnerTangentShape(IRectangle rectangle)
        {
            return CreateInnerTangentShape(rectangle, ComparisonCode.Less);
        }

        public ICircle CreateOuterTangentShape(IRectangle rectangle)
        {
            IExtent diagonal = NullChecked(rectangle, nameof(rectangle)).GetDiagonal();

            return CreateCircle(diagonal);
        }

        public ICircle CreateTangentShape(IRectangle rectangle, SideCode sideCode)
        {
            return CreateTangentShape(this, rectangle, sideCode);
        }

        #region Override methods
        public override IRectangle Create(IDryBody dryBody, ShapeExtentTypeCode perpendicular)
        {
            _ = NullChecked(dryBody, nameof(dryBody));
            _ = Defined(perpendicular, nameof(perpendicular));

            return dryBody switch
            {
                Cylinder cylinder => create(getCylinderShapeExtents(cylinder)),
                Cuboid cuboid => create(getCuboidShapeExtents(cuboid)),

                _ => throw new InvalidOperationException(null),
            };

            #region Local methods
            IRectangle create((IExtent Length, IExtent Width) shapeExtents)
            {
                return Create(shapeExtents.Length, shapeExtents.Width);
            }

            (IExtent Length, IExtent Width) getCylinderShapeExtents(ICylinder cylinder)
            {
                IExtent baseFaceDiagonal = cylinder.BaseFace.GetDiagonal();
                return perpendicular switch
                {
                    ShapeExtentTypeCode.Radius => (baseFaceDiagonal, cylinder.Height),
                    ShapeExtentTypeCode.Height => (baseFaceDiagonal, baseFaceDiagonal),

                    _ => throw InvalidShapeExtentTypeCodeEnumArgumentException(perpendicular, nameof(perpendicular)),
                };
            }

            (IExtent Length, IExtent Width) getCuboidShapeExtents(ICuboid cuboid)
            {
                return perpendicular switch
                {
                    ShapeExtentTypeCode.Length => (cuboid.GetWidth(), cuboid.Height),
                    ShapeExtentTypeCode.Width => (cuboid.GetLength(), cuboid.Height),
                    ShapeExtentTypeCode.Height => (cuboid.GetLength(), cuboid.GetWidth()),

                    _ => throw InvalidShapeExtentTypeCodeEnumArgumentException(perpendicular, nameof(perpendicular)),
                };
            }
            #endregion
        }

        public override IRectangle Create(IPlaneShape other)
        {
            return NullChecked(other, nameof(other)) switch
            {
                Circle circle => circle.GetOuterTangentShape(),
                Rectangle rectangle => Create(rectangle),

                _ => throw new InvalidOperationException(null),
            };
        }

        public override ICircleFactory GetTangentShapeFactory()
        {
            return (ICircleFactory)TangentShapeFactory;
        }
        #endregion
        #endregion

        #region Private methods
        private ICircle CreateCircle(IExtent diagonal)
        {
            IExtent radius = (IExtent)diagonal.Divide(2);

            return GetTangentShapeFactory().Create(radius);
        }

        public override IBaseShape Create(params IQuantifiable[] rateComponents)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}

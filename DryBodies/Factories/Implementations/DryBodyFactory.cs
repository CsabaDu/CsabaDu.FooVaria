using CsabaDu.FooVaria.PlaneShapes.Types;

namespace CsabaDu.FooVaria.DryBodies.Factories.Implementations
{
    public abstract class DryBodyFactory(IBulkBodyFactory bulkBodyFactory) : SimpleShapeFactory, IDryBodyFactory
    {
        #region Properties
        public IBulkBodyFactory BulkBodyFactory { get; init; } = NullChecked(bulkBodyFactory, nameof(bulkBodyFactory));
        #endregion

        #region Public methods
        public IPlaneShape? CreateProjection(IDryBody dryBody, ShapeExtentCode perpendicular)
        {
            if (dryBody?.IsValidShapeExtentCode(perpendicular) != true) return null;

            return perpendicular switch
            {
                ShapeExtentCode.Radius => createCylinderVerticalProjection(),
                ShapeExtentCode.Length => createCuboidVerticalProjection(),
                ShapeExtentCode.Width => createCuboidVerticalProjection(),
                ShapeExtentCode.Height => createHorizontalProjection(),

                _ => null,
            };

            #region Local methods
            IRectangle createCylinderVerticalProjection()
            {
                IExtent horizontal = dryBody.GetBaseFace().GetDiagonal();
                ICuboidFactory factory = (ICuboidFactory)dryBody.GetTangentShapeFactory();

                return createRectangle(factory, horizontal);
            }

            IRectangle createCuboidVerticalProjection()
            {
                perpendicular = perpendicular == ShapeExtentCode.Length ?
                    ShapeExtentCode.Width
                    : ShapeExtentCode.Length;

                IExtent horizontal = dryBody.GetShapeExtent(perpendicular);
                ICuboidFactory factory = (ICuboidFactory)dryBody.Factory;

                return createRectangle(factory, horizontal);
            }

            IRectangle createRectangle(ICuboidFactory factory, IExtent horizontal)
            {
                IRectangleFactory baseFaceFactory = (IRectangleFactory)factory.GetBaseFaceFactory();

                return baseFaceFactory.Create(horizontal, dryBody.Height);
            }

            IPlaneShape createHorizontalProjection()
            {
                IEnumerable<IExtent> shapeExtents = dryBody.GetShapeExtents().SkipLast(1);
                IPlaneShapeFactory factory = dryBody.GetBaseFaceFactory();

                return (IPlaneShape)factory.CreateShape(shapeExtents.ToArray())!;
            }
            #endregion
        }

        #region Override methods
        #region Sealed methods
        public override sealed IBulkBodyFactory GetBulkSpreadFactory()
        {
            return BulkBodyFactory;
        }
        #endregion
        #endregion

        #region Virtual methods
        public abstract IPlaneShapeFactory GetBaseFaceFactory();
        #endregion

        #region Abstract methods
        public abstract IDryBody Create(IPlaneShape baseFace, IExtent height);
        #endregion
        #endregion

        #region Protected methods
        #region Static methods
        protected static IDryBody? CreateDryBody(IShapeComponent shapeComponent)
        {
            return shapeComponent switch
            {
                Cuboid cuboid => cuboid.GetNew(),
                Cylinder cylinder => cylinder.GetNew(),

                _ => null,

            };
        }

        protected static IDryBody? CreateDryBody(ICuboidFactory cuboidFactory, ICylinderFactory cylinderFactory, params IShapeComponent[] shapeComponents)
        {
            if (GetShapeExtent(shapeComponents[1]) is not IExtent height) return null;

            return shapeComponents[0] switch
            {
                ICircle circle => cylinderFactory.Create(circle, height),
                IRectangle rectangle => cuboidFactory.Create(rectangle, height),
                IExtent radius => cylinderFactory.Create(radius, height),

                _ => null,

            };
        }

        protected static IDryBody? CreateDryBody(ICuboidFactory cuboidFactory, params IShapeComponent[] shapeComponents)
        {
            IEnumerable<IExtent>? shapeExtents = GetShapeExtents(shapeComponents);

            return shapeExtents is not null ?
                cuboidFactory.Create(shapeExtents.First(), shapeExtents.ElementAt(1), shapeExtents.Last())
                : null;
        }

        protected static IRectangle CreateVerticalProjection(IRectangleFactory factory, IExtent horizontal, IDryBody dryBody)
        {
            return factory.Create(horizontal, dryBody.Height)!;
        }
        #endregion
        #endregion
    }

    public abstract class DryBodyFactory<T, TBFace>(IBulkBodyFactory bulkBodyFactory) : DryBodyFactory(bulkBodyFactory), IDryBodyFactory<T, TBFace>
        where T : class, IDryBody, ITangentShape
        where TBFace : class, IPlaneShape, ITangentShape
    {
        #region Public methods
        #region Abstract methods
        public abstract T Create(TBFace baseFace, IExtent height);
        #endregion
        #endregion

        #region Protected methods
        protected static TTangent CreateTangentShape<TTangent>(ISimpleShapeFactory<T, TTangent> factory, IPlaneShape baseFace, T dryBody)
            where TTangent : class, IDryBody, ITangentShape
        {
            IDryBodyFactory tangentShapeFactory = (factory.GetTangentShapeFactory() as IDryBodyFactory)!;

            return (TTangent)tangentShapeFactory.Create(baseFace, dryBody.Height);
        }
        #endregion
    }
}

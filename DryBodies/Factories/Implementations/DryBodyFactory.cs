namespace CsabaDu.FooVaria.DryBodies.Factories.Implementations
{
    public abstract class DryBodyFactory : SimpleShapeFactory, IDryBodyFactory
    {
        #region Constructors
        private protected DryBodyFactory(IBulkBodyFactory spreadFactory, ITangentShapeFactory tangentShapeFactory, IPlaneShapeFactory baseFaceFactory) : base(spreadFactory, tangentShapeFactory)
        {
            BaseFaceFactory = NullChecked(baseFaceFactory, nameof(baseFaceFactory));
        }
        #endregion

        #region Properties
        public IPlaneShapeFactory BaseFaceFactory { get; init; }
        #endregion

        #region Public methods
        public IPlaneShape? CreateProjection(IDryBody dryBody, SimpleShapeExtentCode perpendicular)
        {
            if (dryBody?.IsValidSimpleShapeExtentCode(perpendicular) != true) return null;

            return perpendicular switch
            {
                SimpleShapeExtentCode.Radius => createCylinderVerticalProjection(),
                SimpleShapeExtentCode.Length => createCuboidVerticalProjection(),
                SimpleShapeExtentCode.Width => createCuboidVerticalProjection(),
                SimpleShapeExtentCode.Height => createHorizontalProjection(),

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
                perpendicular = perpendicular == SimpleShapeExtentCode.Length ?
                    SimpleShapeExtentCode.Width
                    : SimpleShapeExtentCode.Length;

                IExtent horizontal = dryBody.GetSimpleShapeExtent(perpendicular);
                ICuboidFactory factory = (ICuboidFactory)dryBody.GetFactory();

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
        public override sealed IBulkBodyFactory GetSpreadFactory()
        {
            return (IBulkBodyFactory)SpreadFactory;
        }
        #endregion
        #endregion

        #region Virtual methods
        public virtual IPlaneShapeFactory GetBaseFaceFactory()
        {
            return BaseFaceFactory;
        }
        #endregion

        #region Abstract methods
        public abstract IDryBody Create(IPlaneShape baseFace, IExtent height);
        #endregion
        #endregion

        #region Protected methods
        protected IDryBody? CreateDryBody(ICuboidFactory cuboidFactory, ICylinderFactory cylinderFactory, ISimpleShapeComponent[] shapeComponents)
        {
            int count = GetShapeComponentsCount(shapeComponents);

            if (count == 0) return null;

            ISimpleShapeComponent firstItem = shapeComponents[0];

            return count switch
            {
                1 => createDryBodyFrom1Param(),
                2 => createDryBodyFrom2Params(),
                3 => createDryBodyFrom3Params(),

                _ => null,
            };

            #region Local methods
            IDryBody? createDryBodyFrom1Param()
            {
                if (firstItem is ICuboid cuboid) return cuboidFactory.CreateNew(cuboid);

                if (firstItem is ICylinder cylinder) return cylinderFactory.CreateNew(cylinder);

                return null;
            }

            IDryBody? createDryBodyFrom2Params()
            {
                if (GetSimpleShapeExtent(shapeComponents[1]) is not IExtent height) return null;

                if (firstItem is IPlaneShape planeShape) return Create(planeShape, height);

                if (firstItem is IExtent radius) return cylinderFactory.Create(radius, height);

                return null;
            }

            IDryBody? createDryBodyFrom3Params()
            {
                IEnumerable<IExtent>? shapeExtents = GetShapeExtents(shapeComponents);

                return shapeExtents != null ?
                    cuboidFactory.Create(shapeExtents.First(), shapeExtents.ElementAt(1), shapeExtents.Last())
                    : null;
            }
            #endregion
        }

        #region Static methods
        protected static IRectangle CreateVerticalProjection(IRectangleFactory factory, IExtent horizontal, IDryBody dryBody)
        {
            return factory.Create(horizontal, dryBody.Height)!;
        }
        #endregion
        #endregion
    }

    public abstract class DryBodyFactory<T, TBFace> : DryBodyFactory, IDryBodyFactory<T, TBFace>
        where T : class, IDryBody, ITangentShape
        where TBFace : class, IPlaneShape, ITangentShape
    {
        #region Constructors
        public DryBodyFactory(IBulkBodyFactory spreadFactory, ITangentShapeFactory tangentShapeFactory, IPlaneShapeFactory baseFaceFactory) : base(spreadFactory, tangentShapeFactory, baseFaceFactory)
        {
        }
        #endregion

        #region Public methods
        #region Abstract methods
        public abstract T Create(TBFace baseFace, IExtent height);
        #endregion
        #endregion

        #region Protected methods
        protected TTangent CreateTangentShape<TTangent>(ISimpleShapeFactory<T, TTangent> factory, IPlaneShape baseFace, T dryBody)
            where TTangent : class, IDryBody, ITangentShape
        {
            IDryBodyFactory tangentShapeFactory = (GetTangentShapeFactory() as IDryBodyFactory)!;

            return (TTangent)tangentShapeFactory.Create(baseFace, dryBody.Height);
        }
        #endregion
    }
}

//namespace CsabaDu.FooVaria.Shapes.Factories.Implementations
//{
//    public abstract class DryBodyFactory : ShapeFactory, IDryBodyFactory
//    {
//        #region Constructors
//        private protected DryBodyFactory(IBulkBodyFactory spreadFactory, ITangentShapeFactory tangentShapeFactory, IPlaneShapeFactory baseFaceFactory) : base(spreadFactory, tangentShapeFactory)
//        {
//            BaseFaceFactory = NullChecked(baseFaceFactory, nameof(baseFaceFactory));
//        }
//        #endregion

//        #region Properties
//        public IPlaneShapeFactory BaseFaceFactory { get; init; }
//        #endregion

//        #region Public methods
//        #region Override methods
//        #region Sealed methods
//        public override sealed IBulkBodyFactory GetSpreadFactory()
//        {
//            return (IBulkBodyFactory)SpreadFactory;
//        }
//        #endregion
//        #endregion

//        #region Virtual methods
//        public virtual IPlaneShapeFactory GetBaseFaceFactory()
//        {
//            return BaseFaceFactory;
//        }
//        #endregion

//        #region Abstract methods
//        public abstract IDryBody Create(IPlaneShape baseFace, IExtent height);
//        #endregion
//        #endregion

//        #region Protected methods
//        protected IDryBody? CreateDryBody(ICuboidFactory cuboidFactory, ICylinderFactory cylinderFactory, IShapeComponent[] shapeComponents)
//        {
//            int count = GetShapeComponentsCount(shapeComponents);

//            if (count == 0) return null;

//            IShapeComponent firstItem = shapeComponents[0];

//            return count switch
//            {
//                1 => createDryBodyFrom1Param(),
//                2 => createDryBodyFrom2Params(),
//                3 => createDryBodyFrom3Params(),

//                _ => null,
//            };

//            #region Local methods
//            IDryBody? createDryBodyFrom1Param()
//            {
//                if (firstItem is ICuboid cuboid) return cuboidFactory.CreateNew(cuboid);

//                if (firstItem is ICylinder cylinder) return cylinderFactory.CreateNew(cylinder);

//                return null;
//            }

//            IDryBody? createDryBodyFrom2Params()
//            {
//                if (GetShapeExtent(shapeComponents[1]) is not IExtent height) return null;

//                if (firstItem is IPlaneShape planeShape) return Create(planeShape, height);

//                if (firstItem is IExtent radius) return cylinderFactory.Create(radius, height);

//                return null;
//            }

//            IDryBody? createDryBodyFrom3Params()
//            {
//                IEnumerable<IExtent>? shapeExtents = GetShapeExtents(shapeComponents);

//                return shapeExtents != null ?
//                    cuboidFactory.Create(shapeExtents.First(), shapeExtents.ElementAt(1), shapeExtents.Last())
//                    : null;
//            }
//            #endregion
//        }

//        #region Static methods
//        protected static IRectangle CreateVerticalProjection(IRectangleFactory factory, IExtent horizontal, IDryBody dryBody)
//        {
//            return factory.Create(horizontal, dryBody.Height)!;
//        }
//        #endregion
//        #endregion
//    }

//    public abstract class DryBodyFactory<T, TBFace> : DryBodyFactory, IDryBodyFactory<T, TBFace>
//        where T : class, IDryBody, ITangentShape
//        where TBFace : class, IPlaneShape, ITangentShape
//    {
//        #region Constructors
//        public DryBodyFactory(IBulkBodyFactory spreadFactory, ITangentShapeFactory tangentShapeFactory, IPlaneShapeFactory baseFaceFactory) : base(spreadFactory, tangentShapeFactory, baseFaceFactory)
//        {
//        }
//        #endregion

//        #region Public methods
//        #region Abstract methods
//        public abstract T Create(TBFace baseFace, IExtent height);
//        #endregion
//        #endregion

//        #region Protected methods
//        protected TTangent CreateTangentShape<TTangent>(IShapeFactory<T, TTangent> factory, IPlaneShape baseFace, T dryBody)
//            where TTangent : class, IDryBody, ITangentShape
//        {
//            IDryBodyFactory tangentShapeFactory = (GetTangentShapeFactory() as IDryBodyFactory)!;

//            return (TTangent)tangentShapeFactory.Create(baseFace, dryBody.Height);
//        }
//        #endregion
//    }
//}

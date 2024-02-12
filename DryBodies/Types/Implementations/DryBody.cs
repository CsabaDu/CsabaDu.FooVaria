namespace CsabaDu.FooVaria.DryBodies.Types.Implementations
{
    internal abstract class DryBody : SimpleShape, IDryBody
    {
        #region Constructors
        private protected DryBody(IDryBody other) : base(other)
        {
            Height = other.Height;
            Volume = other.Volume;
        }

        private protected DryBody(IDryBodyFactory factory, IPlaneShape baseFace, IExtent height) : base(factory, baseFace)
        {
            var (validHeight, volume) = getDryBodyValidParams();

            Height = validHeight;
            Volume = volume;

            #region Local methods
            (IExtent, IVolume) getDryBodyValidParams()
            {
                ValidateShapeExtent(height, nameof(height));

                IExtent[] shapeExtents = baseFace.GetShapeExtents().Append(height).ToArray();
                IVolume volume = (IVolume)GetSpreadMeasure(shapeExtents);

                return (height, volume);
            }
            #endregion
        }

        private protected DryBody(IDryBodyFactory factory, params IExtent[] shapeExtents) : base(factory, MeasureUnitCode.VolumeUnit, shapeExtents)
        {
            Height = shapeExtents.Last();
            Volume = (IVolume)GetSpreadMeasure(shapeExtents);
        }
        #endregion

        #region Properties
        public IVolume Volume { get; init; }
        public IExtent Height { get; init; }
        #endregion

        #region Public methods
        public IPlaneShape GetBaseFace(ExtentUnit extentUnit)
        {
            return (IPlaneShape)GetBaseFace().GetSimpleShape(extentUnit);
        }

        public IBody GetBody()
        {
            return this;
        }

        public IDryBody GetDryBody(IPlaneShape baseFace, IExtent height)
        {
            return GetFactory().Create(baseFace, height);
        }

        public IExtent GetHeight()
        {
            return Height;
        }
        public IExtent GetHeight(ExtentUnit extentUnit)
        {
            return Height.GetMeasure(extentUnit);
        }

        public void ValidateBaseFace(IPlaneShape planeShape, string paramName)
        {
            int baseFaceShapeExtentCout = GetShapeComponentCount() - 1;

            if (NullChecked(planeShape, paramName).GetShapeComponentCount() == baseFaceShapeExtentCout) return;

            throw ArgumentTypeOutOfRangeException(paramName, planeShape);
        }

        #region Override methods
        public override IDryBodyFactory GetFactory()
        {
            return (IDryBodyFactory)Factory;
        }

        #region Sealed methods
        public override sealed IVolume GetSpreadMeasure()
        {
            return Volume;
        }

        public override sealed IShapeComponent? GetValidShapeComponent(IQuantifiable? shapeComponent)
        {
            if (shapeComponent is not IExtent or IPlaneShape) return null;

            return (IShapeComponent)shapeComponent;
        }
        #endregion
        #endregion

        #region Virtual methods
        public virtual IPlaneShapeFactory GetBaseFaceFactory()
        {
            return GetFactory().BaseFaceFactory;
        }
        #endregion

        #region Abstract methods
        public abstract IPlaneShape GetBaseFace();
        public abstract IPlaneShape GetProjection(ShapeExtentCode perpendicular);
        #endregion
        #endregion
    }

    internal abstract class DryBody<TSelf, TBFace> : DryBody, IDryBody<TSelf, TBFace>
        where TSelf : class, IDryBody, ITangentShape
        where TBFace : IPlaneShape, ITangentShape
    {
        #region Constructors
        private protected DryBody(TSelf other) : base(other)
        {
            BaseFace = (TBFace)other.GetBaseFace();
        }

        private protected DryBody(IDryBodyFactory factory, params IExtent[] shapeExtents) : base(factory, shapeExtents)
        {
            BaseFace = (TBFace)GetSimpleShape(shapeExtents.SkipLast(1).ToArray());
        }

        private protected DryBody(IDryBodyFactory factory, TBFace baseFace, IExtent height) : base(factory, baseFace, height)
        {
            BaseFace = baseFace;
        }
        #endregion

        #region Properties
        public TBFace BaseFace { get; init; }
        #endregion

        #region Public methods
        public TSelf GetDryBody(TBFace baseFace, IExtent height)
        {
            return GetFactory().Create(baseFace, height);
        }

        #region Override methods
        public override IDryBodyFactory<TSelf, TBFace> GetFactory()
        {
            return (IDryBodyFactory<TSelf, TBFace>)Factory;
        }

        #region Sealed methods
        public override sealed IPlaneShape GetBaseFace()
        {
            return BaseFace;
        }
        #endregion
        #endregion

        #region Abstract methods
        public abstract TSelf GetNew(TSelf other);
        #endregion
        #endregion
    }
}

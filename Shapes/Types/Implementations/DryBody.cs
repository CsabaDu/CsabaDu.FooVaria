namespace CsabaDu.FooVaria.Shapes.Types.Implementations
{
    internal abstract class DryBody : Shape, IDryBody
    {
        private protected DryBody(IDryBody other) : base(other)
        {
            Height = other.Height;
            Volume = other.Volume;
        }

        private protected DryBody(IDryBodyFactory factory, IPlaneShape baseFace, IExtent height) : base(factory, baseFace)
        {
            Height = getDryBodyProperties().Height;
            Volume = getDryBodyProperties().Volume;

            #region Local methods
            (IExtent Height, IVolume Volume) getDryBodyProperties()
            {
                //ValidateShapeComponent(height, nameof(height));

                IExtent[] shapeExtents = baseFace.GetShapeExtents().Append(height).ToArray();

                return (height, (IVolume)GetSpreadMeasure(shapeExtents));
            }
            #endregion
        }

        private protected DryBody(IDryBodyFactory factory, params IExtent[] shapeExtents) : base(factory, MeasureUnitCode.VolumeUnit, shapeExtents)
        {
            Height = shapeExtents.Last();
            Volume = (IVolume)GetSpreadMeasure(shapeExtents);
        }

        public IVolume Volume { get; init; }
        public IExtent Height { get; init; }

        public IPlaneShape GetBaseFace(ExtentUnit extentUnit)
        {
            return (IPlaneShape)GetBaseFace().GetShape(extentUnit);
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

        public override sealed IVolume GetSpreadMeasure()
        {
            return Volume;
        }

        public override IDryBodyFactory GetFactory()
        {
            return (IDryBodyFactory)Factory;
        }

        public virtual IPlaneShapeFactory GetBaseFaceFactory()
        {
            return GetFactory().BaseFaceFactory;
        }

        public abstract IPlaneShape GetBaseFace();
        public abstract IPlaneShape GetProjection(ShapeExtentCode perpendicular);
    }

    internal abstract class DryBody<TSelf, TBFace> : DryBody, IDryBody<TSelf, TBFace>
        where TSelf : class, IDryBody, ITangentShape
        where TBFace : IPlaneShape, ITangentShape
    {
        private protected DryBody(TSelf other) : base(other)
        {
            BaseFace = (TBFace)other.GetBaseFace();
        }

        private protected DryBody(IDryBodyFactory factory, params IExtent[] shapeExtents) : base(factory, shapeExtents)
        {
            BaseFace = (TBFace)GetShape(shapeExtents.SkipLast(1).ToArray());
        }

        private protected DryBody(IDryBodyFactory factory, TBFace baseFace, IExtent height) : base(factory, baseFace, height)
        {
            BaseFace = baseFace;
        }

        public TBFace BaseFace { get; init; }

        public TSelf GetDryBody(TBFace baseFace, IExtent height)
        {
            return GetFactory().Create(baseFace, height);
        }

        public override IDryBodyFactory<TSelf, TBFace> GetFactory()
        {
            return (IDryBodyFactory<TSelf, TBFace>)Factory;
        }

        public override sealed IPlaneShape GetBaseFace()
        {
            return BaseFace;
        }
    }
}

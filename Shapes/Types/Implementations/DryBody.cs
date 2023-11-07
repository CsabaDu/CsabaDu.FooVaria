using CsabaDu.FooVaria.Measurables.Factories;


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
                ValidateShapeExtent(height, nameof(height));

                IExtent[] shapeExtents = baseFace.GetShapeExtents().Append(height).ToArray();

                return (height, GetVolume(shapeExtents));
            }
            #endregion
        }

        private protected DryBody(IDryBodyFactory factory, params IExtent[] shapeExtents) : base(factory, MeasureUnitTypeCode.VolumeUnit, shapeExtents)
        {
            Height = shapeExtents.Last();
            Volume = GetVolume(shapeExtents);
        }

        private IVolume GetVolume(IExtent[] shapeExtents)
        {
            IMeasureFactory measureFactory = GetFactory().SpreadFactory.MeasureFactory;

            return SpreadMeasures.GetVolume(measureFactory, shapeExtents);
        }

        public IVolume Volume { get; init; }
        public IExtent Height { get; init; }

        public IPlaneShape GetBaseFace(ExtentUnit extentUnit)
        {
            return (IPlaneShape)GetBaseFace().GetShape(extentUnit);
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
            int baseFaceShapeExtentCout = GetShapeExtentCount() - 1;

            if (NullChecked(planeShape, paramName).GetShapeExtentCount() == baseFaceShapeExtentCout) return;

            throw ArgumentTypeOutOfRangeException(paramName, planeShape);
        }

        public override sealed IVolume GetSpreadMeasure()
        {
            return Volume;
        }

        public override sealed IEnumerable<IExtent> GetDimensions()
        {
            return GetBaseFace().GetDimensions().Append(Height);
        }

        public override IDryBodyFactory GetFactory()
        {
            return (IDryBodyFactory)Factory;
        }

        public virtual IPlaneShapeFactory GetBaseFaceFactory()
        {
            return GetFactory().GetBaseFaceFactory();
        }

        public abstract IPlaneShape GetBaseFace();
        public abstract IPlaneShape GetProjection(ShapeExtentTypeCode perpendicular);
        public abstract IExtent GetShapeExtent(IPlaneShape projection, IVolume volume);
    }

    internal abstract class DryBody<T, U> : DryBody, IDryBody<T, U> where T : class, IDryBody, ITangentShape where U : IPlaneShape, ITangentShape
    {
        private protected DryBody(T other) : base(other)
        {
            BaseFace = (U)other.GetBaseFace();
        }

        private protected DryBody(IDryBodyFactory factory, params IExtent[] shapeExtents) : base(factory, shapeExtents)
        {
            BaseFace = (U)GetShape(shapeExtents.SkipLast(1));
        }

        private protected DryBody(IDryBodyFactory factory, U baseFace, IExtent height) : base(factory, baseFace, height)
        {
            BaseFace = baseFace;
        }

        public U BaseFace { get; init; }

        public override sealed IPlaneShape GetBaseFace()
        {
            return BaseFace;
        }

        public override sealed IExtent GetShapeExtent(IPlaneShape projection, IVolume volume)
        {
            decimal shapeExtentQuantity = volume.DefaultQuantity / projection.DefaultQuantity;

            return Height.GetMeasure(decimal.ToDouble(shapeExtentQuantity), default(ExtentUnit));
            throw new NotImplementedException();
        }

        public override IDryBodyFactory<T, U> GetFactory()
        {
            return (IDryBodyFactory<T, U>)Factory;
        }
        public T GetDryBody(U baseFace, IExtent height)
        {
            return GetFactory().Create(baseFace, height);
        }
    }
}

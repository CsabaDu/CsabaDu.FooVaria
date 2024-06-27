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

        private protected DryBody(IDryBodyFactory factory, IPlaneShape baseFace, IExtent height) : base(factory)
        {
            ValidateShapeExtent(height, nameof(height));

            Height = height;
            Volume = getVolume();

            #region Local methods
            IVolume getVolume()
            {
                IExtent[] shapeExtents = baseFace.GetShapeExtents().Append(height).ToArray();

                return (IVolume)GetSpreadMeasure(shapeExtents);
            }
            #endregion
        }

        private protected DryBody(IDryBodyFactory factory, params IExtent[] shapeExtents) : base(factory)
        {
            Height = shapeExtents[^1];
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
            return GetDryBodyFactory().Create(baseFace, height);
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
        #region Sealed methods
        public override sealed Enum GetBaseMeasureUnit()
        {
            return Volume.GetMeasureUnit();
        }

        public override sealed IVolume GetSpreadMeasure()
        {
            return Volume;
        }

        public override sealed IShapeComponent? GetValidShapeComponent(IQuantifiable? quantifiable)
        {
            if (quantifiable is IExtent extent) return extent;

            if (quantifiable is IPlaneShape planeShape) return planeShape;

            return null;
        }

        public override sealed IBulkBodyFactory GetBulkSpreadFactory()
        {
            return GetDryBodyFactory().BulkBodyFactory;
        }
        #endregion
        #endregion

        #region Abstract methods
        public abstract IPlaneShape GetBaseFace();
        public abstract IPlaneShapeFactory GetBaseFaceFactory();
        public abstract IPlaneShape GetProjection(ShapeExtentCode perpendicular);
        #endregion
        #endregion

        #region Private methods
        private IDryBodyFactory GetDryBodyFactory()
        {
            return (IDryBodyFactory)Factory;
        }

        public override sealed bool TryExchangeTo(Enum context, [NotNullWhen(true)] out IQuantifiable? exchanged)
        {
            exchanged = null;

            if (!IsExchangeableTo(context)) return false;

            MeasureUnitElements measureUnitElements = GetMeasureUnitElements(context, nameof(context));
            exchanged = measureUnitElements.MeasureUnit switch
            {
                ExtentUnit extentUnit => ExchangeTo(extentUnit),
                VolumeUnit volumeUnit => GetSpread(Volume.ExchangeTo(volumeUnit)!),

                _ => null,
            };

            return exchanged is not null;
        }
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

        private protected DryBody(IDryBodyFactory<TSelf, TBFace> factory, params IExtent[] shapeExtents) : base(factory, shapeExtents)
        {
            BaseFace = (TBFace)GetSimpleShape(shapeExtents.SkipLast(1).ToArray());
        }

        private protected DryBody(IDryBodyFactory<TSelf, TBFace> factory, TBFace baseFace, IExtent height) : base(factory, baseFace, height)
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
            IDryBodyFactory<TSelf, TBFace> factory = (IDryBodyFactory<TSelf, TBFace>)Factory;

            return factory.Create(baseFace, height);
        }

        public TSelf GetNew()
        {
            TSelf other = (this as TSelf)!;

            return GetNew(other);
        }

        #region Override methods
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

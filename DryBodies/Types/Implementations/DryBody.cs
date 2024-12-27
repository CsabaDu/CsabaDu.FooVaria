namespace CsabaDu.FooVaria.DryBodies.Types.Implementations
{
    /// <summary>
    /// Represents an abstract base class for dry bodies.
    /// </summary>
    internal abstract class DryBody : SimpleShape, IDryBody
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="DryBody"/> class by copying another dry body.
        /// </summary>
        /// <param name="other">The other dry body to copy.</param>
        private protected DryBody(IDryBody other) : base(other)
        {
            Height = other.Height;
            Volume = other.Volume;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DryBody"/> class using a factory, base face, and height.
        /// </summary>
        /// <param name="factory">The factory to use for initialization.</param>
        /// <param name="baseFace">The base face of the dry body.</param>
        /// <param name="height">The height of the dry body.</param>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="DryBody"/> class using a factory and shape extents.
        /// </summary>
        /// <param name="factory">The factory to use for initialization.</param>
        /// <param name="shapeExtents">The shape extents of the dry body.</param>
        private protected DryBody(IDryBodyFactory factory, params IExtent[] shapeExtents) : base(factory)
        {
            Height = shapeExtents.Last();
            Volume = (IVolume)GetSpreadMeasure(shapeExtents);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the volume of the dry body.
        /// </summary>
        public IVolume Volume { get; init; }

        /// <summary>
        /// Gets the height of the dry body.
        /// </summary>
        public IExtent Height { get; init; }
        #endregion

        #region Public methods
        /// <summary>
        /// Gets the base face of the dry body with the specified extent unit.
        /// </summary>
        /// <param name="extentUnit">The extent unit.</param>
        /// <returns>The base face of the dry body.</returns>
        public IPlaneShape GetBaseFace(ExtentUnit extentUnit)
        {
            return (IPlaneShape)GetBaseFace().GetSimpleShape(extentUnit);
        }

        /// <summary>
        /// Gets the body of the dry body.
        /// </summary>
        /// <returns>The body of the dry body.</returns>
        public IBody GetBody()
        {
            return this;
        }

        /// <summary>
        /// Gets a new dry body with the specified base face and height.
        /// </summary>
        /// <param name="baseFace">The base face of the dry body.</param>
        /// <param name="height">The height of the dry body.</param>
        /// <returns>A new dry body.</returns>
        public IDryBody GetDryBody(IPlaneShape baseFace, IExtent height)
        {
            return GetDryBodyFactory().Create(baseFace, height);
        }

        /// <summary>
        /// Gets the height of the dry body.
        /// </summary>
        /// <returns>The height of the dry body.</returns>
        public IExtent GetHeight()
        {
            return Height;
        }

        /// <summary>
        /// Gets the height of the dry body with the specified extent unit.
        /// </summary>
        /// <param name="extentUnit">The extent unit.</param>
        /// <returns>The height of the dry body.</returns>
        public IExtent GetHeight(ExtentUnit extentUnit)
        {
            return Height.GetMeasure(extentUnit);
        }

        /// <summary>
        /// Validates the base face of the dry body.
        /// </summary>
        /// <param name="planeShape">The plane shape to validate.</param>
        /// <param name="paramName">The parameter name.</param>
        public void ValidateBaseFace(IPlaneShape planeShape, string paramName)
        {
            int baseFaceShapeExtentCout = GetShapeComponentCount() - 1;

            if (NullChecked(planeShape, paramName).GetShapeComponentCount() == baseFaceShapeExtentCout) return;

            throw ArgumentTypeOutOfRangeException(paramName, planeShape);
        }

        #region Override methods
        #region Sealed methods
        /// <summary>
        /// Gets the base measure unit of the dry body.
        /// </summary>
        /// <returns>The base measure unit.</returns>
        public override sealed Enum GetBaseMeasureUnit()
        {
            return Volume.GetMeasureUnit();
        }

        /// <summary>
        /// Gets the spread measure of the dry body.
        /// </summary>
        /// <returns>The spread measure.</returns>
        public override sealed IVolume GetSpreadMeasure()
        {
            return Volume;
        }

        /// <summary>
        /// Gets a valid shape component for the specified quantifiable.
        /// </summary>
        /// <param name="quantifiable">The quantifiable to validate.</param>
        /// <returns>The valid shape component.</returns>
        public override sealed IShapeComponent? GetValidShapeComponent(IQuantifiable? quantifiable)
        {
            if (quantifiable is IExtent extent) return extent;

            if (quantifiable is IPlaneShape planeShape) return planeShape;

            return null;
        }

        /// <summary>
        /// Gets the bulk body factory of the dry body.
        /// </summary>
        /// <returns>The bulk body factory.</returns>
        public override sealed IBulkBodyFactory GetBulkSpreadFactory()
        {
            return GetDryBodyFactory().BulkBodyFactory;
        }
        #endregion
        #endregion

        #region Abstract methods
        /// <summary>
        /// Gets the base face of the dry body.
        /// </summary>
        /// <returns>The base face of the dry body.</returns>
        public abstract IPlaneShape GetBaseFace();

        /// <summary>
        /// Gets the base face factory of the dry body.
        /// </summary>
        /// <returns>The base face factory.</returns>
        public abstract IPlaneShapeFactory GetBaseFaceFactory();

        /// <summary>
        /// Gets the projection of the dry body for the specified shape extent code.
        /// </summary>
        /// <param name="perpendicular">The shape extent code.</param>
        /// <returns>The projection of the dry body.</returns>
        public abstract IPlaneShape GetProjection(ShapeExtentCode perpendicular);
        #endregion
        #endregion

        #region Private methods
        /// <summary>
        /// Gets the dry body factory.
        /// </summary>
        /// <returns>The dry body factory.</returns>
        private IDryBodyFactory GetDryBodyFactory()
        {
            return (IDryBodyFactory)GetFactory();
        }

        /// <summary>
        /// Tries to exchange the dry body to the specified context.
        /// </summary>
        /// <param name="context">The context to exchange to.</param>
        /// <param name="exchanged">The exchanged quantifiable.</param>
        /// <returns>True if the exchange was successful, otherwise false.</returns>
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

    /// <summary>
    /// Represents an abstract base class for dry bodies with a specific base face type.
    /// </summary>
    /// <typeparam name="TSelf">The type of the dry body.</typeparam>
    /// <typeparam name="TBFace">The type of the base face.</typeparam>
    internal abstract class DryBody<TSelf, TBFace> : DryBody, IDryBody<TSelf, TBFace>
        where TSelf : class, IDryBody, ITangentShape
        where TBFace : IPlaneShape, ITangentShape
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="DryBody{TSelf, TBFace}"/> class by copying another dry body.
        /// </summary>
        /// <param name="other">The other dry body to copy.</param>
        private protected DryBody(TSelf other) : base(other)
        {
            BaseFace = (TBFace)other.GetBaseFace();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DryBody{TSelf, TBFace}"/> class using a factory and shape extents.
        /// </summary>
        /// <param name="factory">The factory to use for initialization.</param>
        /// <param name="shapeExtents">The shape extents of the dry body.</param>
        private protected DryBody(IDryBodyFactory<TSelf, TBFace> factory, params IExtent[] shapeExtents) : base(factory, shapeExtents)
        {
            BaseFace = (TBFace)GetSimpleShape(shapeExtents.SkipLast(1).ToArray());
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DryBody{TSelf, TBFace}"/> class using a factory, base face, and height.
        /// </summary>
        /// <param name="factory">The factory to use for initialization.</param>
        /// <param name="baseFace">The base face of the dry body.</param>
        /// <param name="height">The height of the dry body.</param>
        private protected DryBody(IDryBodyFactory<TSelf, TBFace> factory, TBFace baseFace, IExtent height) : base(factory, baseFace, height)
        {
            BaseFace = baseFace;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the base face of the dry body.
        /// </summary>
        public TBFace BaseFace { get; init; }
        #endregion

        #region Public methods
        /// <summary>
        /// Gets a new dry body with the specified base face and height.
        /// </summary>
        /// <param name="baseFace">The base face of the dry body.</param>
        /// <param name="height">The height of the dry body.</param>
        /// <returns>A new dry body.</returns>
        public TSelf GetDryBody(TBFace baseFace, IExtent height)
        {
            IDryBodyFactory<TSelf, TBFace> factory = (IDryBodyFactory<TSelf, TBFace>)GetFactory();

            return factory.Create(baseFace, height);
        }

        #region Override methods
        #region Sealed methods
        /// <summary>
        /// Gets the base face of the dry body.
        /// </summary>
        /// <returns>The base face of the dry body.</returns>
        public override sealed IPlaneShape GetBaseFace()
        {
            return BaseFace;
        }
        #endregion
        #endregion

        #region Abstract methods
        /// <summary>
        /// Gets a new instance of the dry body by copying another dry body.
        /// </summary>
        /// <param name="other">The other dry body to copy.</param>
        /// <returns>A new instance of the dry body.</returns>
        public abstract TSelf GetNew(TSelf other);
        #endregion
        #endregion
    }
}

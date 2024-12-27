namespace CsabaDu.FooVaria.BulkSpreads.Types.Implementations
{
    internal abstract class BulkSpread : Spread, IBulkSpread
    {
        #region Constants
        /// <summary>
        /// The extent count for a circle shape.
        /// </summary>
        public const int CircleShapeExtentCount = 1;

        /// <summary>
        /// The extent count for a rectangle shape.
        /// </summary>
        public const int RectangleShapeExtentCount = 2;

        /// <summary>
        /// The extent count for a cylinder shape.
        /// </summary>
        public const int CylinderShapeExtentCount = 2;

        /// <summary>
        /// The extent count for a cuboid shape.
        /// </summary>
        public const int CuboidShapeExtentCount = 3;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="BulkSpread"/> class with the specified root object.
        /// </summary>
        /// <param name="other">The root object associated with the BulkSpread.</param>
        private protected BulkSpread(IRootObject other) : base(other, nameof(other))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BulkSpread"/> class with the specified factory.
        /// </summary>
        /// <param name="factory">The factory used to create the BulkSpread.</param>
        private protected BulkSpread(IBulkSpreadFactory factory) : base(factory, nameof(factory))
        {
        }
        #endregion

        #region Public methods
        #region Override methods
        #region Sealed methods
        /// <summary>
        /// Gets the quantity of the BulkSpread.
        /// </summary>
        /// <returns>The quantity as a double.</returns>
        public override sealed double GetQuantity()
        {
            return base.GetQuantity();
        }

        /// <summary>
        /// Validates the specified quantity.
        /// </summary>
        /// <param name="quantity">The quantity to validate.</param>
        /// <param name="paramName">The name of the parameter associated with the quantity.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the quantity is not valid.</exception>
        public override sealed void ValidateQuantity(ValueType? quantity, string paramName)
        {
            _ = NullChecked(quantity, paramName);

            if (quantity!.ToQuantity(TypeCode.Double) is double doubleQuantity
                && doubleQuantity > 0)
            {
                return;
            }

            throw QuantityArgumentOutOfRangeException(paramName, quantity);
        }
        #endregion
        #endregion

        #region Abstract methods
        /// <summary>
        /// Gets a new BulkSpread based on the specified spread measure.
        /// </summary>
        /// <param name="spreadMeasure">The spread measure to use.</param>
        /// <returns>The new BulkSpread.</returns>
        public abstract IBulkSpread GetBulkSpread(ISpreadMeasure spreadMeasure);

        /// <summary>
        /// Gets a new BulkSpread based on the specified shape extents.
        /// </summary>
        /// <param name="shapeExtents">The shape extents to use.</param>
        /// <returns>The new BulkSpread.</returns>
        public abstract IBulkSpread GetBulkSpread(params IExtent[] shapeExtents);

        /// <summary>
        /// Gets a new BulkSpread based on the specified spread.
        /// </summary>
        /// <param name="spread">The spread to use.</param>
        /// <returns>The new BulkSpread.</returns>
        public abstract IBulkSpread GetBulkSpread(ISpread spread);
        #endregion

        #region Static methods
        /// <summary>
        /// Determines if the specified shape extents are valid for the given measure unit code.
        /// </summary>
        /// <param name="measureUnitCode">The measure unit code to validate against.</param>
        /// <param name="shapeExtents">The shape extents to validate.</param>
        /// <returns>True if the shape extents are valid, otherwise false.</returns>
        public static bool AreValidShapeExtents(MeasureUnitCode measureUnitCode, params IExtent[] shapeExtents)
        {
            int count = shapeExtents?.Length ?? 0;
            bool isValidShapeExtentCount = measureUnitCode switch
            {
                MeasureUnitCode.AreaUnit => isValidateShapeExtentsCount(CircleShapeExtentCount, RectangleShapeExtentCount),
                MeasureUnitCode.VolumeUnit => isValidateShapeExtentsCount(CylinderShapeExtentCount, CuboidShapeExtentCount),

                _ => false,
            };

            return isValidShapeExtentCount && shapeExtents!.All(x => x.GetDefaultQuantity() > 0);

            #region Local methods
            bool isValidateShapeExtentsCount(int minValue, int maxValue)
            {
                return count >= minValue && count <= maxValue;
            }
            #endregion
        }

        /// <summary>
        /// Gets the area based on the specified shape extents.
        /// </summary>
        /// <param name="factory">The measure factory to use.</param>
        /// <param name="shapeExtents">The shape extents to use.</param>
        /// <returns>The calculated area.</returns>
        public static IArea GetArea(IMeasureFactory factory, params IExtent[] shapeExtents)
        {
            ValidateShapeExtents(MeasureUnitCode.AreaUnit, nameof(shapeExtents), shapeExtents);

            return shapeExtents.Length switch
            {
                1 => GetCircleArea(factory, shapeExtents[0]),
                2 => GetRectangleArea(factory, shapeExtents[0], shapeExtents[1]),

                _ => throw new InvalidOperationException(null),
            };
        }

        /// <summary>
        /// Gets the area of a circle based on the specified radius.
        /// </summary>
        /// <param name="factory">The measure factory to use.</param>
        /// <param name="radius">The radius of the circle.</param>
        /// <returns>The calculated area of the circle.</returns>
        public static IArea GetCircleArea(IMeasureFactory factory, IExtent radius)
        {
            _ = NullChecked(factory, nameof(factory));

            decimal quantity = GetCircleAreaDefaultQuantity(radius);

            return GetArea(factory, quantity);
        }

        /// <summary>
        /// Gets the volume of a cuboid based on the specified dimensions.
        /// </summary>
        /// <param name="factory">The measure factory to use.</param>
        /// <param name="length">The length of the cuboid.</param>
        /// <param name="width">The width of the cuboid.</param>
        /// <param name="height">The height of the cuboid.</param>
        /// <returns>The calculated volume of the cuboid.</returns>
        public static IVolume GetCuboidVolume(IMeasureFactory factory, IExtent length, IExtent width, IExtent height)
        {
            _ = NullChecked(factory, nameof(factory));

            decimal quantity = GetRectangleAreaDefaultQuantity(length, width);

            return GetVolume(factory, quantity, height);
        }

        /// <summary>
        /// Gets the volume of a cylinder based on the specified radius and height.
        /// </summary>
        /// <param name="factory">The measure factory to use.</param>
        /// <param name="radius">The radius of the cylinder.</param>
        /// <param name="height">The height of the cylinder.</param>
        /// <returns>The calculated volume of the cylinder.</returns>
        public static IVolume GetCylinderVolume(IMeasureFactory factory, IExtent radius, IExtent height)
        {
            _ = NullChecked(factory, nameof(factory));

            decimal quantity = GetCircleAreaDefaultQuantity(radius);

            return GetVolume(factory, quantity, height);
        }

        /// <summary>
        /// Gets the area of a rectangle based on the specified length and width.
        /// </summary>
        /// <param name="factory">The measure factory to use.</param>
        /// <param name="length">The length of the rectangle.</param>
        /// <param name="width">The width of the rectangle.</param>
        /// <returns>The calculated area of the rectangle.</returns>
        public static IArea GetRectangleArea(IMeasureFactory factory, IExtent length, IExtent width)
        {
            _ = NullChecked(factory, nameof(factory));

            decimal quantity = GetRectangleAreaDefaultQuantity(length, width);

            return GetArea(factory, quantity);
        }

        /// <summary>
        /// Gets the spread measure based on the specified measure unit code and shape extents.
        /// </summary>
        /// <param name="factory">The measure factory to use.</param>
        /// <param name="measureUnitCode">The measure unit code to use.</param>
        /// <param name="shapeExtents">The shape extents to use.</param>
        /// <returns>The calculated spread measure.</returns>
        public static IMeasure GetSpreadMeasure(IMeasureFactory factory, MeasureUnitCode measureUnitCode, params IExtent[] shapeExtents)
        {
            return measureUnitCode switch
            {
                MeasureUnitCode.AreaUnit => GetArea(factory, shapeExtents),
                MeasureUnitCode.VolumeUnit => GetVolume(factory, shapeExtents),

                _ => throw InvalidMeasureUnitCodeEnumArgumentException(measureUnitCode),
            };
        }

        /// <summary>
        /// Gets a valid spread measure based on the specified spread measure.
        /// </summary>
        /// <param name="spreadMeasure">The spread measure to validate.</param>
        /// <returns>The valid spread measure.</returns>
        /// <exception cref="ArgumentTypeOutOfRangeException">Thrown if the spread measure is not valid.</exception>
        /// <exception cref="QuantityArgumentOutOfRangeException">Thrown if the quantity is not valid.</exception>
        public static IMeasure GetValidSpreadMeasure(ISpreadMeasure? spreadMeasure)
        {
            const string paramName = nameof(spreadMeasure);
            spreadMeasure = NullChecked(spreadMeasure, nameof(spreadMeasure)).GetSpreadMeasure();

            if (spreadMeasure is not IMeasure measure) throw ArgumentTypeOutOfRangeException(paramName, spreadMeasure!);

            double quantity = (double)measure.GetBaseQuantity();

            if (quantity > 0) return measure;

            throw QuantityArgumentOutOfRangeException(paramName, quantity);
        }

        /// <summary>
        /// Gets a valid spread measure based on the specified measure unit code and spread measure.
        /// </summary>
        /// <param name="measureUnitCode">The measure unit code to validate against.</param>
        /// <param name="spreadMeasure">The spread measure to validate.</param>
        /// <returns>The valid spread measure.</returns>
        /// <exception cref="ArgumentTypeOutOfRangeException">Thrown if the spread measure is not valid.</exception>
        /// <exception cref="InvalidMeasureUnitCodeEnumArgumentException">Thrown if the measure unit code is not valid.</exception>
        /// <exception cref="QuantityArgumentOutOfRangeException">Thrown if the quantity is not valid.</exception>
        public static IMeasure GetValidSpreadMeasure(MeasureUnitCode measureUnitCode, ISpreadMeasure? spreadMeasure)
        {
            const string paramName = nameof(spreadMeasure);

            ValidateMeasureUnitCodeByDefinition(measureUnitCode, nameof(measureUnitCode));

            IMeasure measure = GetValidSpreadMeasure(spreadMeasure);

            if (measure.IsExchangeableTo(measureUnitCode)) return measure;

            throw ArgumentTypeOutOfRangeException(paramName, spreadMeasure!);
        }

        /// <summary>
        /// Gets the volume based on the specified shape extents.
        /// </summary>
        /// <param name="factory">The measure factory to use.</param>
        /// <param name="shapeExtents">The shape extents to use.</param>
        /// <returns>The calculated volume.</returns>
        public static IVolume GetVolume(IMeasureFactory factory, params IExtent[] shapeExtents)
        {
            ValidateShapeExtents(MeasureUnitCode.VolumeUnit, nameof(shapeExtents), shapeExtents);

            return shapeExtents.Length switch
            {
                2 => GetCylinderVolume(factory, shapeExtents[0], shapeExtents[1]),
                3 => GetCuboidVolume(factory, shapeExtents[0], shapeExtents[1], shapeExtents[2]),

                _ => throw new InvalidOperationException(null),
            };
        }

        /// <summary>
        /// Validates the specified shape extents for the given measure unit code.
        /// </summary>
        /// <param name="measureUnitCode">The measure unit code to validate against.</param>
        /// <param name="paramName">The name of the parameter associated with the shape extents.</param>
        /// <param name="shapeExtents">The shape extents to validate.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the shape extents are not valid.</exception>
        /// <exception cref="InvalidMeasureUnitCodeEnumArgumentException">Thrown if the measure unit code is not valid.</exception>
        public static void ValidateShapeExtents(MeasureUnitCode measureUnitCode, string paramName, params IExtent[] shapeExtents)
        {
            int count = shapeExtents?.Length ?? 0;

            switch (measureUnitCode)
            {
                case MeasureUnitCode.AreaUnit:
                    validateShapeExtentsCount(CircleShapeExtentCount, RectangleShapeExtentCount);
                    break;
                case MeasureUnitCode.VolumeUnit:
                    validateShapeExtentsCount(CylinderShapeExtentCount, CuboidShapeExtentCount);
                    break;

                default:
                    throw InvalidMeasureUnitCodeEnumArgumentException(measureUnitCode);
            }

            foreach (IExtent item in shapeExtents!)
            {
                double quantity = item.GetQuantity();

                if (item.GetDefaultQuantity() <= 0) throw QuantityArgumentOutOfRangeException(paramName, quantity);
            }

            #region Local methods
            void validateShapeExtentsCount(int minValue, int maxValue)
            {
                if (count >= minValue && count <= maxValue) return;

                throw CountArgumentOutOfRangeException(count, paramName);
            }
            #endregion
        }
        #endregion

        /// <summary>
        /// Determines if the specified shape extents are valid.
        /// </summary>
        /// <param name="shapeExtents">The shape extents to validate.</param>
        /// <returns>True if the shape extents are valid, otherwise false.</returns>
        public bool AreValidShapeExtents(params IExtent[] shapeExtents)
        {
            return AreValidShapeExtents(GetMeasureUnitCode(), shapeExtents);
        }
        #endregion

        #region Private methods
        #region Static methods
        /// <summary>
        /// Gets the area based on the specified quantity.
        /// </summary>
        /// <param name="factory">The measure factory to use.</param>
        /// <param name="quantity">The quantity to use.</param>
        /// <returns>The calculated area.</returns>
        private static IArea GetArea(IMeasureFactory factory, decimal quantity)
        {
            return (IArea)factory.Create(default(AreaUnit), quantity);
        }

        /// <summary>
        /// Gets the default quantity for a circle area based on the specified radius.
        /// </summary>
        /// <param name="radius">The radius of the circle.</param>
        /// <returns>The default quantity for the circle area.</returns>
        private static decimal GetCircleAreaDefaultQuantity(IExtent radius)
        {
            decimal quantity = GetValidShapeExtentDefaultQuantity(radius, nameof(radius));
            quantity *= quantity;

            return quantity * Convert.ToDecimal(Math.PI);
        }

        /// <summary>
        /// Gets the default quantity for a rectangle area based on the specified length and width.
        /// </summary>
        /// <param name="length">The length of the rectangle.</param>
        /// <param name="width">The width of the rectangle.</param>
        /// <returns>The default quantity for the rectangle area.</returns>
        private static decimal GetRectangleAreaDefaultQuantity(IExtent length, IExtent width)
        {
            decimal quantity = GetValidShapeExtentDefaultQuantity(length, nameof(length));

            return quantity * GetValidShapeExtentDefaultQuantity(width, nameof(width));
        }

        /// <summary>
        /// Gets the default quantity for a shape extent based on the specified name.
        /// </summary>
        /// <param name="shapeExtent">The shape extent to validate.</param>
        /// <param name="name">The name of the parameter associated with the shape extent.</param>
        /// <returns>The default quantity for the shape extent.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the shape extent is not valid.</exception>
        private static decimal GetValidShapeExtentDefaultQuantity(IExtent shapeExtent, string name)
        {
            decimal quantity = NullChecked(shapeExtent, name).GetDefaultQuantity();

            return quantity > 0 ?
                quantity
                : throw new ArgumentOutOfRangeException(name, quantity, null);
        }

        /// <summary>
        /// Gets the volume based on the specified quantity and height.
        /// </summary>
        /// <param name="factory">The measure factory to use.</param>
        /// <param name="quantity">The quantity to use.</param>
        /// <param name="height">The height to use.</param>
        /// <returns>The calculated volume.</returns>
        private static IVolume GetVolume(IMeasureFactory factory, decimal quantity, IExtent height)
        {
            quantity *= GetValidShapeExtentDefaultQuantity(height, nameof(height));

            return (IVolume)factory.Create(default(VolumeUnit), quantity);
        }
        #endregion
        #endregion
    }

    internal abstract class BulkSpread<TSelf, TSMeasure> : BulkSpread, IBulkSpread<TSelf, TSMeasure>
        where TSelf : class, IBulkSpread
        where TSMeasure : class, IMeasure<TSMeasure, double>, ISpreadMeasure
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="BulkSpread{TSelf, TSMeasure}"/> class with the specified other BulkSpread.
        /// </summary>
        /// <param name="other">The other BulkSpread to copy.</param>
        private protected BulkSpread(IBulkSpread<TSelf, TSMeasure> other) : base(other)
        {
            SpreadMeasure = other.SpreadMeasure;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BulkSpread{TSelf, TSMeasure}"/> class with the specified factory and spread measure.
        /// </summary>
        /// <param name="factory">The factory used to create the BulkSpread.</param>
        /// <param name="spreadMeasure">The spread measure to use.</param>
        private protected BulkSpread(IBulkSpreadFactory<TSelf, TSMeasure> factory, TSMeasure spreadMeasure) : base(factory)
        {
            SpreadMeasure = getValidSpreadMeasure(spreadMeasure);

            #region Local methods
            static TSMeasure getValidSpreadMeasure(ISpreadMeasure? spreadMeasure)
            {
                const string paramName = nameof(spreadMeasure);
                TSMeasure measure = TypeChecked<TSMeasure>(spreadMeasure, paramName);
                double quantity = measure.GetQuantity();

                if (quantity > 0) return measure;

                throw QuantityArgumentOutOfRangeException(paramName, quantity);
            }
            #endregion
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the spread measure associated with the BulkSpread.
        /// </summary>
        public TSMeasure SpreadMeasure { get; init; }
        #endregion

        #region Public methods
        /// <summary>
        /// Creates a new instance of the BulkSpread based on the specified other BulkSpread.
        /// </summary>
        /// <param name="other">The other BulkSpread to copy.</param>
        /// <returns>A new instance of the BulkSpread.</returns>
        public TSelf GetNew(TSelf other)
        {
            IBulkSpreadFactory<TSelf, TSMeasure> factory = GetBulkSpreadFactory();

            return factory.CreateNew(other);
        }

        /// <summary>
        /// Creates a new instance of the BulkSpread based on the specified spread measure.
        /// </summary>
        /// <param name="spreadMeasure">The spread measure to use.</param>
        /// <returns>A new instance of the BulkSpread.</returns>
        public TSelf GetBulkSpread(TSMeasure spreadMeasure)
        {
            IBulkSpreadFactory<TSelf, TSMeasure> factory = GetBulkSpreadFactory();

            return factory.Create(spreadMeasure);
        }

        #region Override methods
        #region Sealed methods
        /// <summary>
        /// Gets the spread measure of the BulkSpread.
        /// </summary>
        /// <returns>The spread measure.</returns>
        public override sealed TSMeasure GetSpreadMeasure()
        {
            return SpreadMeasure;
        }
        #endregion
        #endregion
        #endregion

        #region Private methods
        /// <summary>
        /// Gets the BulkSpread factory.
        /// </summary>
        /// <returns>The BulkSpread factory.</returns>
        private IBulkSpreadFactory<TSelf, TSMeasure> GetBulkSpreadFactory()
        {
            return (IBulkSpreadFactory<TSelf, TSMeasure>)GetFactory();
        }
        #endregion
    }

    /// <summary>
    /// Represents an abstract base class for BulkSpread with generic parameters.
    /// </summary>
    /// <typeparam name="TSelf">The type of the derived class.</typeparam>
    /// <typeparam name="TSMeasure">The type of the measure.</typeparam>
    /// <typeparam name="TEnum">The type of the enum.</typeparam>
    internal abstract class BulkSpread<TSelf, TSMeasure, TEnum> : BulkSpread<TSelf, TSMeasure>, IBulkSpread<TSelf, TSMeasure, TEnum>
        where TSelf : class, IBulkSpread
        where TSMeasure : class, IMeasure<TSMeasure, double, TEnum>, ISpreadMeasure
        where TEnum : struct, Enum
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="BulkSpread{TSelf, TSMeasure, TEnum}"/> class with the specified other BulkSpread.
        /// </summary>
        /// <param name="other">The other BulkSpread to copy.</param>
        private protected BulkSpread(IBulkSpread<TSelf, TSMeasure, TEnum> other) : base(other)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BulkSpread{TSelf, TSMeasure, TEnum}"/> class with the specified factory and spread measure.
        /// </summary>
        /// <param name="factory">The factory used to create the BulkSpread.</param>
        /// <param name="spreadMeasure">The spread measure to use.</param>
        private protected BulkSpread(IBulkSpreadFactory<TSelf, TSMeasure, TEnum> factory, TSMeasure spreadMeasure) : base(factory, spreadMeasure)
        {
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Gets the base measure unit as an Enum.
        /// </summary>
        /// <returns>The base measure unit.</returns>
        public override sealed Enum GetBaseMeasureUnit()
        {
            return GetMeasureUnit();
        }

        /// <summary>
        /// Gets the measure unit.
        /// </summary>
        /// <returns>The measure unit.</returns>
        public TEnum GetMeasureUnit()
        {
            return SpreadMeasure.GetMeasureUnit();
        }

        /// <summary>
        /// Gets a new BulkSpread based on the specified measure unit.
        /// </summary>
        /// <param name="measureUnit">The measure unit to use.</param>
        /// <returns>The new BulkSpread.</returns>
        public TSelf GetBulkSpread(TEnum measureUnit)
        {
            return (TSelf)(ExchangeTo(measureUnit) ?? throw InvalidMeasureUnitEnumArgumentException(measureUnit));
        }

        /// <summary>
        /// Gets a new BulkSpread based on the specified measure unit and quantity.
        /// </summary>
        /// <param name="measureUnit">The measure unit to use.</param>
        /// <param name="quantity">The quantity to use.</param>
        /// <returns>The new BulkSpread.</returns>
        public TSelf GetBulkSpread(TEnum measureUnit, double quantity)
        {
            IBulkSpreadFactory<TSelf, TSMeasure, TEnum> factory = GetBulkSpreadFactory();

            return factory.Create(measureUnit, quantity);
        }

        #region Override methods
        #region Sealed methods
        //public override sealed TSelf GetSpread(ISpreadMeasure spreadMeasure)
        //{
        //    ValidateSpreadMeasure(spreadMeasure, nameof(spreadMeasure));

        //    IBulkSpreadFactory<TSelf, TSMeasure, TEnum> factory = GetBulkSpreadFactory();

        //    return factory.Create((TSMeasure)spreadMeasure);
        //}

        /// <summary>
        /// Gets a new BulkSpread based on the specified shape extents.
        /// </summary>
        /// <param name="shapeExtents">The shape extents to use.</param>
        /// <returns>The new BulkSpread.</returns>
        public override sealed TSelf GetBulkSpread(params IExtent[] shapeExtents)
        {
            IBulkSpreadFactory<TSelf, TSMeasure, TEnum> factory = GetBulkSpreadFactory();

            return (TSelf)factory.Create(shapeExtents);
        }

        /// <summary>
        /// Gets a new BulkSpread based on the specified spread measure.
        /// </summary>
        /// <param name="spreadMeasure">The spread measure to use.</param>
        /// <returns>The new BulkSpread.</returns>
        public override sealed TSelf GetBulkSpread(ISpreadMeasure spreadMeasure)
        {
            IBulkSpreadFactory<TSelf, TSMeasure, TEnum> factory = GetBulkSpreadFactory();

            if (spreadMeasure.GetSpreadMeasure() is TSMeasure validSpreadMeasure) return factory.Create(validSpreadMeasure);

            throw ArgumentTypeOutOfRangeException(nameof(spreadMeasure), spreadMeasure!);
        }

        /// <summary>
        /// Validates the specified measure unit.
        /// </summary>
        /// <param name="measureUnit">The measure unit to validate.</param>
        /// <param name="paramName">The name of the parameter associated with the measure unit.</param>
        /// <exception cref="InvalidMeasureUnitEnumArgumentException">Thrown if the measure unit is not valid.</exception>
        public override sealed void ValidateMeasureUnit(Enum? measureUnit, string paramName)
        {
            if (NullChecked(measureUnit, paramName) is TEnum) return;

            throw InvalidMeasureUnitEnumArgumentException(measureUnit!, paramName);
        }
        #endregion
        #endregion
        #endregion

        #region Protected methods
        #region Static methods
        /// <summary>
        /// Exchanges the current BulkSpread to the specified measure unit.
        /// </summary>
        /// <param name="measureUnit">The measure unit to exchange to.</param>
        /// <returns>The exchanged BulkSpread if successful, otherwise null.</returns>
        public TSelf? ExchangeTo(TEnum measureUnit)
        {
            ISpreadMeasure? exchanged = SpreadMeasure.ExchangeTo(measureUnit);

            if (exchanged is null) return null;

            return GetBulkSpread(exchanged);
        }

        /// <summary>
        /// Tries to exchange the current BulkSpread to the specified context.
        /// </summary>
        /// <param name="context">The context to exchange to.</param>
        /// <param name="exchanged">The exchanged quantifiable if successful.</param>
        /// <returns>True if the exchange was successful, otherwise false.</returns>
        public override sealed bool TryExchangeTo(Enum context, [NotNullWhen(true)] out IQuantifiable? exchanged)
        {
            exchanged = null;

            if (!IsExchangeableTo(context)) return false;

            MeasureUnitElements measureUnitElements = GetMeasureUnitElements(context, nameof(context));

            if (measureUnitElements.MeasureUnit is TEnum measureUnit)
            {
                exchanged = ExchangeTo(measureUnit);
            }

            return exchanged is not null;
        }
        #endregion
        #endregion

        #region Private methods
        /// <summary>
        /// Gets the BulkSpread factory.
        /// </summary>
        /// <returns>The BulkSpread factory.</returns>
        private IBulkSpreadFactory<TSelf, TSMeasure, TEnum> GetBulkSpreadFactory()
        {
            return (IBulkSpreadFactory<TSelf, TSMeasure, TEnum>)GetFactory();
        }
        #endregion
    }
}

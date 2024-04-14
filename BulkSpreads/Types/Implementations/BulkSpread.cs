namespace CsabaDu.FooVaria.BulkSpreads.Types.Implementations
{
    internal abstract class BulkSpread : Spread, IBulkSpread
    {
        #region Constants
        public const int CircleShapeExtentCount = 1;
        public const int RectangleShapeExtentCount = 2;
        public const int CylinderShapeExtentCount = 2;
        public const int CuboidShapeExtentCount = 3;
        #endregion

        #region Constructors
        private protected BulkSpread(IRootObject other) : base(other, nameof(other))
        {
        }

        private protected BulkSpread(IBulkSpreadFactory factory) : base(factory, nameof(factory))
        {
        }
        #endregion

        #region Public methods
        #region Override methods
        #region Sealed methods
        public override sealed double GetQuantity()
        {
            return base.GetQuantity();
        }

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
        public abstract IBulkSpread GetBulkSpread(ISpreadMeasure spreadMeasure);
        public abstract IBulkSpread GetBulkSpread(params IExtent[] shapeExtents);
        public abstract IBulkSpread GetBulkSpread(ISpread spread);
        #endregion

        #region Static methods
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

        public static IArea GetCircleArea(IMeasureFactory factory, IExtent radius)
        {
            _ = NullChecked(factory, nameof(factory));

            decimal quantity = GetCircleAreaDefaultQuantity(radius);

            return GetArea(factory, quantity);
        }

        public static IVolume GetCuboidVolume(IMeasureFactory factory, IExtent length, IExtent width, IExtent height)
        {
            _ = NullChecked(factory, nameof(factory));

            decimal quantity = GetRectangleAreaDefaultQuantity(length, width);

            return GetVolume(factory, quantity, height);
        }

        public static IVolume GetCylinderVolume(IMeasureFactory factory, IExtent radius, IExtent height)
        {
            _ = NullChecked(factory, nameof(factory));

            decimal quantity = GetCircleAreaDefaultQuantity(radius);

            return GetVolume(factory, quantity, height);
        }

        public static IArea GetRectangleArea(IMeasureFactory factory, IExtent length, IExtent width)
        {
            _ = NullChecked(factory, nameof(factory));

            decimal quantity = GetRectangleAreaDefaultQuantity(length, width);

            return GetArea(factory, quantity);
        }

        public static IMeasure GetSpreadMeasure(IMeasureFactory factory, MeasureUnitCode measureUnitCode, params IExtent[] shapeExtents)
        {
            return measureUnitCode switch
            {
                MeasureUnitCode.AreaUnit => GetArea(factory, shapeExtents),
                MeasureUnitCode.VolumeUnit => GetVolume(factory, shapeExtents),

                _ => throw InvalidMeasureUnitCodeEnumArgumentException(measureUnitCode),
            };
        }

        public static IMeasure GetValidSpreadMeasure(ISpreadMeasure? spreadMeasure)
        {
            const string paramName = nameof(spreadMeasure);
            spreadMeasure = NullChecked(spreadMeasure, nameof(spreadMeasure)).GetSpreadMeasure();

            if (spreadMeasure is not IMeasure measure) throw ArgumentTypeOutOfRangeException(paramName, spreadMeasure!);

            double quantity = (double)measure.GetBaseQuantity();

            if (quantity > 0) return measure;

            throw QuantityArgumentOutOfRangeException(paramName, quantity);
        }

        public static IMeasure GetValidSpreadMeasure(MeasureUnitCode measureUnitCode, ISpreadMeasure? spreadMeasure)
        {
            const string paramName = nameof(spreadMeasure);

            ValidateMeasureUnitCodeByDefinition(measureUnitCode, nameof(measureUnitCode));

            IMeasure measure = GetValidSpreadMeasure(spreadMeasure);

            if (measure.IsExchangeableTo(measureUnitCode)) return measure;

            throw ArgumentTypeOutOfRangeException(paramName, spreadMeasure!);
        }

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

        public bool AreValidShapeExtents(params IExtent[] shapeExtents)
        {
            return AreValidShapeExtents(GetMeasureUnitCode(), shapeExtents);
        }
        #endregion

        #region Private methods
        #region Static methods
        private static IArea GetArea(IMeasureFactory factory, decimal quantity)
        {
            return (IArea)factory.Create(default(AreaUnit), quantity);
        }

        private static decimal GetCircleAreaDefaultQuantity(IExtent radius)
        {
            decimal quantity = GetValidShapeExtentDefaultQuantity(radius, nameof(radius));
            quantity *= quantity;

            return quantity * Convert.ToDecimal(Math.PI);
        }

        private static decimal GetRectangleAreaDefaultQuantity(IExtent length, IExtent width)
        {
            decimal quantity = GetValidShapeExtentDefaultQuantity(length, nameof(length));

            return quantity * GetValidShapeExtentDefaultQuantity(width, nameof(width));
        }

        private static decimal GetValidShapeExtentDefaultQuantity(IExtent shapeExtent, string name)
        {
            decimal quantity = NullChecked(shapeExtent, name).GetDefaultQuantity();

            return quantity > 0 ?
                quantity
                : throw new ArgumentOutOfRangeException(name, quantity, null);
        }

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
        private protected BulkSpread(IBulkSpread<TSelf, TSMeasure> other) : base(other)
        {
            SpreadMeasure = other.SpreadMeasure;
        }

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
        public TSMeasure SpreadMeasure { get; init; }
        #endregion

        #region Public methods
        public TSelf GetNew(TSelf other)
        {
            IBulkSpreadFactory<TSelf, TSMeasure> factory = GetBulkSpreadFactory();

            return factory.CreateNew(other);
        }

        public TSelf GetBulkSpread(TSMeasure spreadMeasure)
        {
            IBulkSpreadFactory<TSelf, TSMeasure> factory = GetBulkSpreadFactory();

            return factory.Create(spreadMeasure);
        }

        #region Override methods
        #region Sealed methods
        public override sealed TSMeasure GetSpreadMeasure()
        {
            return SpreadMeasure;
        }
        #endregion
        #endregion
        #endregion

        #region Private methods
        private IBulkSpreadFactory<TSelf, TSMeasure> GetBulkSpreadFactory()
        {
            return (IBulkSpreadFactory<TSelf, TSMeasure>)GetFactory();
        }
        #endregion
    }

    internal abstract class BulkSpread<TSelf, TSMeasure, TEnum> : BulkSpread<TSelf, TSMeasure>, IBulkSpread<TSelf, TSMeasure, TEnum>
        where TSelf : class, IBulkSpread
        where TSMeasure : class, IMeasure<TSMeasure, double, TEnum>, ISpreadMeasure
        where TEnum : struct, Enum
    {
        #region Constructors
        private protected BulkSpread(IBulkSpread<TSelf, TSMeasure, TEnum> other) : base(other)
        {
        }

        private protected BulkSpread(IBulkSpreadFactory<TSelf, TSMeasure, TEnum> factory, TSMeasure spreadMeasure) : base(factory, spreadMeasure)
        {
        }
        #endregion

        #region Public methods
        public override sealed Enum GetBaseMeasureUnit()
        {
            return GetMeasureUnit();
        }

        public TEnum GetMeasureUnit()
        {
            return SpreadMeasure.GetMeasureUnit();
        }

        public TSelf GetBulkSpread(TEnum measureUnit)
        {
            return (TSelf)(ExchangeTo(measureUnit) ?? throw InvalidMeasureUnitEnumArgumentException(measureUnit));
        }

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

        public override sealed TSelf GetBulkSpread(params IExtent[] shapeExtents)
        {
            IBulkSpreadFactory<TSelf, TSMeasure, TEnum> factory = GetBulkSpreadFactory();

            return (TSelf)factory.Create(shapeExtents);
        }

        public override sealed TSelf GetBulkSpread(ISpreadMeasure spreadMeasure)
        {
            IBulkSpreadFactory<TSelf, TSMeasure, TEnum> factory = GetBulkSpreadFactory();

            if (spreadMeasure.GetSpreadMeasure() is TSMeasure validSpreadMeasure) return factory.Create(validSpreadMeasure);

            throw ArgumentTypeOutOfRangeException(nameof(spreadMeasure), spreadMeasure!);
        }

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
        public TSelf? ExchangeTo(TEnum measureUnit)
        {
            ISpreadMeasure? exchanged = SpreadMeasure.ExchangeTo(measureUnit);

            if (exchanged is null) return null;

            return GetBulkSpread(exchanged);
        }

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
        private IBulkSpreadFactory<TSelf, TSMeasure, TEnum> GetBulkSpreadFactory()
        {
            return (IBulkSpreadFactory<TSelf, TSMeasure, TEnum>)GetFactory();
        }
        #endregion
    }
}

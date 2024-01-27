namespace CsabaDu.FooVaria.Spreads.Types.Implementations
{
    internal abstract class Spread : BaseSpread, ISpread
    {
        #region Constants
        public const int CircleShapeExtentCount = 1;
        public const int RectangleShapeExtentCount = 2;
        public const int CylinderShapeExtentCount = 2;
        public const int CuboidShapeExtentCount = 3;
        #endregion

        #region Constructors
        private protected Spread(ISpread other) : base(other)
        {
        }

        private protected Spread(ISpreadFactory factory, IMeasure spreadMeasure) : base(factory, spreadMeasure)
        {
        }
        #endregion

        #region Public methods
        public bool AreValidShapeExtents(params IExtent[] shapeExtents)
        {
            return AreValidShapeExtents(MeasureUnitCode, shapeExtents);
        }

        #region Override methods
        public override ISpreadFactory GetFactory()
        {
            return (ISpreadFactory)Factory;
        }

        public override void ValidateMeasureUnit(Enum measureUnit, string paramName)
        {
            MeasureUnitCode measureUnitCode = GetMeasureUnitCode(measureUnit);

            if (IsValidMeasureUnitCode(measureUnitCode)) return;

            throw InvalidMeasureUnitEnumArgumentException(measureUnit, paramName);
        }

        #region Sealed methods
        public override sealed ISpread? ExchangeTo(Enum measureUnit)
        {
            IBaseMeasure? exchanged = (GetSpreadMeasure() as IBaseMeasure)?.ExchangeTo(measureUnit);

            if (exchanged is not ISpreadMeasure spreadMeasure) return null;

            return (ISpread)GetFactory().CreateBaseSpread(spreadMeasure);
        }

        public override sealed void ValidateMeasureUnitCode(MeasureUnitCode measureUnitCode, string paramName)
        {
            if (IsValidMeasureUnitCode(measureUnitCode)) return;

            throw InvalidMeasureUnitCodeEnumArgumentException(measureUnitCode, paramName);
        }

        public override sealed void ValidateQuantity(ValueType? quantity, string paramName)
        {
            _ = NullChecked(quantity, paramName);

            if (quantity!.ToQuantity(TypeCode.Double) is double doubleQuantity
                && doubleQuantity > 0) return;

            throw QuantityArgumentOutOfRangeException(paramName, quantity);
        }

        #endregion
        #endregion

        #region Abstract methods
        public abstract ISpread GetSpread(ISpreadMeasure spreadMeasure);
        public abstract ISpread GetSpread(params IExtent[] shapeExtents);
        public abstract ISpread GetSpread(IBaseSpread baseSpread);
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
            string paramName = nameof(spreadMeasure);
            spreadMeasure = NullChecked(spreadMeasure, nameof(spreadMeasure)).GetSpreadMeasure();

            if (spreadMeasure is not IMeasure measure) throw ArgumentTypeOutOfRangeException(paramName, spreadMeasure!);

            double quantity = (double)measure.Quantity;

            if (quantity > 0) return measure;

            throw QuantityArgumentOutOfRangeException(paramName, quantity);
        }

        public static IMeasure GetValidSpreadMeasure(MeasureUnitCode measureUnitCode, ISpreadMeasure? spreadMeasure)
        {
            string paramName = nameof(spreadMeasure);

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
                decimal quantity = item.GetDecimalQuantity();

                if (item.GetDefaultQuantity() <= 0) throw QuantityArgumentOutOfRangeException(paramName, quantity);
            }

            #region Local methods
            void validateShapeExtentsCount(int minValue, int maxValue)
            {
                if (count >= minValue && count <= maxValue) return;

                throw new ArgumentOutOfRangeException(paramName, count, null);
            }
            #endregion
        }
        #endregion
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

    internal abstract class Spread<TSelf, TSMeasure> : Spread, ISpread<TSelf, TSMeasure>
        where TSelf : class, ISpread
        where TSMeasure : class, IMeasure<TSMeasure, double>, ISpreadMeasure
    {
        #region Constructors
        private protected Spread(ISpread<TSelf, TSMeasure> other) : base(other)
        {
            SpreadMeasure = other.SpreadMeasure;
        }

        private protected Spread(ISpreadFactory<TSelf, TSMeasure> factory, TSMeasure spreadMeasure) : base(factory, spreadMeasure)
        {
            SpreadMeasure = spreadMeasure;
        }
        #endregion

        #region Properties
        public TSMeasure SpreadMeasure { get; init; }
        #endregion

        #region Public methods
        public TSelf GetNew(TSelf other)
        {
            return GetFactory().CreateNew(other);
        }

        public TSelf GetSpread(TSMeasure spreadMeasure)
        {
            return GetFactory().Create(spreadMeasure);
        }

        #region Override methods
        public override ISpreadFactory<TSelf, TSMeasure> GetFactory()
        {
            return (ISpreadFactory<TSelf, TSMeasure>)Factory;
        }

        #region Sealed methods
        public override sealed TSMeasure GetSpreadMeasure()
        {
            return SpreadMeasure;
        }
        #endregion
        #endregion
        #endregion
    }

    internal abstract class Spread<TSelf, TSMeasure, TEnum> : Spread<TSelf, TSMeasure>, ISpread<TSelf, TSMeasure, TEnum>
        where TSelf : class, ISpread
        where TSMeasure : class, IMeasure<TSMeasure, double, TEnum>, ISpreadMeasure
        where TEnum : struct, Enum
    {
        #region Constructors
        private protected Spread(ISpread<TSelf, TSMeasure, TEnum> other) : base(other)
        {
        }

        private protected Spread(ISpreadFactory<TSelf, TSMeasure, TEnum> factory, TSMeasure spreadMeasure) : base(factory, spreadMeasure)
        {
        }
        #endregion

        #region Public methods
        #region Override methods
        public override ISpreadFactory<TSelf, TSMeasure, TEnum> GetFactory()
        {
            return (ISpreadFactory<TSelf, TSMeasure, TEnum>)Factory;
        }

        #region Sealed methods
        public override sealed TSelf GetBaseSpread(ISpreadMeasure spreadMeasure)
        {
            ValidateSpreadMeasure(spreadMeasure, nameof(spreadMeasure));

            return GetFactory().Create((TSMeasure)spreadMeasure);
        }

        public override sealed TSelf GetSpread(params IExtent[] shapeExtents)
        {
            return (TSelf)GetFactory().Create(shapeExtents);
        }

        public override sealed TSelf GetSpread(ISpreadMeasure spreadMeasure)
        {
            if (spreadMeasure.GetSpreadMeasure() is TSMeasure validSpreadMeasure) return GetFactory().Create(validSpreadMeasure);

            throw ArgumentTypeOutOfRangeException(nameof(spreadMeasure), spreadMeasure!);
        }

        public override sealed void ValidateMeasureUnit(Enum measureUnit, string paramName)
        {
            if (NullChecked(measureUnit, paramName) is TEnum) return;

            throw InvalidMeasureUnitEnumArgumentException(measureUnit, paramName);
        }
        #endregion
        #endregion

        #region Abstract methods
        public TEnum GetMeasureUnit(IMeasureUnit<TEnum>? other)
        {
            return SpreadMeasure.GetMeasureUnit(other);
        }
        public TSelf GetSpread(TEnum measureUnit)
        {
            return (TSelf)(ExchangeTo(measureUnit) ?? throw InvalidMeasureUnitEnumArgumentException(measureUnit));
        }

        public TSelf GetSpread(TEnum measureUnit, double quantity)
        {
            return GetFactory().Create(measureUnit, quantity);
        }
        #endregion
        #endregion
    }
}


using System.Diagnostics.CodeAnalysis;

namespace CsabaDu.FooVaria.Shapes.Types.Implementations
{
    internal abstract class Shape : BaseShape, IShape
    {
        #region Constructors
        private protected Shape(IShape other) : base(other)
        {
        }

        private protected Shape(IShapeFactory factory, IBaseShape baseShape) : base(factory, baseShape)
        {
        }

        private protected Shape(IShapeFactory factory, MeasureUnitTypeCode measureUnitTypeCode, params IExtent[] shapeExtents) : base(factory, measureUnitTypeCode, shapeExtents)
        {
            ValidateShapeExtents(shapeExtents, nameof(shapeExtents));
        }
        #endregion

        #region Properties
        #region Abstract properties
        public abstract IExtent? this[ShapeExtentTypeCode shapeExtentTypeCode] { get; }
        #endregion
        #endregion

        #region Public methods
        public IExtent GetDiagonal(ExtentUnit extentUnit)
        {
            return ShapeExtents.GetDiagonal(this, extentUnit);
        }

        public IExtent GetDiagonal()
        {
            return GetDiagonal(default);
        }

        public IShape GetShape(ExtentUnit extentUnit)
        {
            return (IShape?)ExchangeTo(extentUnit) ?? throw InvalidMeasureUnitEnumArgumentException(extentUnit, nameof(extentUnit));
        }

        public IShape GetShape(params IExtent[] shapeExtents)
        {
            return (IShape)GetFactory().Create(shapeExtents);

            //ValidateShapeExtents(shapeExtents, nameof(shapeExtents));

            //return getShape(this, shapeExtents);

            //#region Local methods
            //static IShape getShape<T>(T shape, IExtent[] shapeExtents)
            //    where T : IShape
            //{
            //    return shape switch
            //    {
            //        Circle circle => circle.GetCircle(shapeExtents[0]),
            //        Cuboid cuboid => cuboid.GetCuboid(shapeExtents[0], shapeExtents[1], shapeExtents[2]),
            //        Cylinder cylinder => cylinder.GetCylinder(shapeExtents[0], shapeExtents[1]),
            //        Rectangle rectangle => rectangle.GetRectangle(shapeExtents[0], shapeExtents[1]),

            //        _ => throw new InvalidOperationException(null),
            //    };
            //}
            //#endregion
        }

        public IExtent GetShapeExtent(ShapeExtentTypeCode shapeExtentTypeCode)
        {
            return this[shapeExtentTypeCode] ?? throw InvalidShapeExtentTypeCodeEnumArgumentException(shapeExtentTypeCode);
        }

        public IEnumerable<IExtent> GetShapeExtents()
        {
            foreach (ShapeExtentTypeCode item in GetShapeExtentTypeCodes())
            {
                yield return this[item]!;
            }
        }

        public IEnumerable<ShapeExtentTypeCode> GetShapeExtentTypeCodes()
        {
            foreach (ShapeExtentTypeCode item in Enum.GetValues<ShapeExtentTypeCode>())
            {
                if (this[item] != null)
                {
                    yield return item;
                }
            }
        }

        public IEnumerable<IExtent> GetSortedDimensions()
        {
            return GetDimensions().OrderBy(x => x);
        }

        public virtual ISpreadFactory GetSpreadFactory()
        {
            return GetFactory().SpreadFactory;
        }

        public bool TryGetShapeExtentTypeCode(IExtent shapeExtent, [NotNullWhen(true)] out ShapeExtentTypeCode? shapeExtentTypeCode)
        {
            _ = NullChecked(shapeExtent, nameof(shapeExtent));

            for (int i = 0; i < GetShapeComponentCount(); i++)
            {
                if (GetShapeExtents().ElementAt(i).Equals(shapeExtent))
                {
                    shapeExtentTypeCode = GetShapeExtentTypeCodes().ElementAt(i);

                    return true;
                }
            }

            shapeExtentTypeCode = null;

            return false;
        }

        public void ValidateShapeExtentCount(int count, string name)
        {
            if (count == GetShapeComponentCount()) return;

            throw QuantityArgumentOutOfRangeException(name, count);
        }

        public void ValidateShapeExtents(IEnumerable<IExtent> shapeExtents, string paramName)
        {
            int count = NullChecked(shapeExtents, paramName).Count();

            ValidateShapeExtentCount(count, paramName);

            foreach (IExtent item in shapeExtents)
            {
                ValidateShapeComponent(item, paramName);
            }
        }

        public void ValidateShapeExtentTypeCode(ShapeExtentTypeCode shapeExtentTypeCode, string paramName)
        {
            if (GetShapeExtentTypeCodes().Contains(shapeExtentTypeCode)) return;

            throw InvalidShapeExtentTypeCodeEnumArgumentException(shapeExtentTypeCode, paramName);
        }

        #region Override methods
        public override IShapeFactory GetFactory()
        {
            return (IShapeFactory)Factory;
        }

        #region Sealed methods
        public override sealed int CompareTo(IBaseShape? other)
        {
            if (other == null) return 1;

            if (other is not IShape shape) throw ArgumentTypeOutOfRangeException(nameof(other), other);

            shape.Validate(this, nameof(other));

            return Compare(this, shape) ?? throw new ArgumentOutOfRangeException(nameof(other));
        }

        public override sealed bool Equals(IBaseShape? other)
        {
            return (this as Measurable).Equals(other)
                && other is IShape shape
                && shape.GetShapeExtents().SequenceEqual(GetShapeExtents());
        }

        public override sealed IBaseSpread? ExchangeTo(Enum measureUnit)
        {
            if (measureUnit == null) return null;

            return measureUnit switch
            {
                AreaUnit areaUnit => exchangeToAreaUnit(areaUnit),
                ExtentUnit extentUnit => exchangeToExtentUnit(extentUnit),
                VolumeUnit volumeUnit => exchangeToVolumeUnit(volumeUnit),

                _ => null,
            };

            #region Local methods
            IShape? exchangeToExtentUnit(ExtentUnit extentUnit)
            {
                IEnumerable<IExtent> exchangedShapeExtents = getExchangedShapeExtents(extentUnit);

                if (exchangedShapeExtents.Count() != GetShapeExtents().Count()) return null;

                return GetShape(exchangedShapeExtents.ToArray());
            }

            IEnumerable<IExtent> getExchangedShapeExtents(ExtentUnit extentUnit)
            {
                foreach (IExtent item in GetShapeExtents())
                {
                    IRateComponent? exchanged = item.ExchangeTo(extentUnit);

                    if (exchanged is IExtent extent)
                    {
                        yield return extent;
                    }
                }
            }

            IBulkSurface? exchangeToAreaUnit(AreaUnit areaUnit)
            {
                if (getExchangedSpreadMeasure(areaUnit) is not IArea area) return null;

                return (IBulkSurface)GetFactory().SpreadFactory.Create(area);
            }

            IBulkBody? exchangeToVolumeUnit(VolumeUnit volumeUnit)
            {
                if (getExchangedSpreadMeasure(volumeUnit) is not IVolume volume) return null;

                return (IBulkBody)GetFactory().SpreadFactory.Create(volume);
            }

            IRateComponent? getExchangedSpreadMeasure(Enum measureUnit)
            {
                IRateComponent spreadMeasure = (IRateComponent)GetSpreadMeasure();

                return spreadMeasure.ExchangeTo(measureUnit);
            }
            #endregion
        }

        public override sealed bool? FitsIn(IBaseShape? other, LimitMode? limitMode)
        {
            if (other == null) return null;

            if (!other.IsExchangeableTo(MeasureUnitTypeCode)) return null;

            if (other is not IShape shape) return null;

            if (other is not ITangentShape tangentShape) return null;

            limitMode ??= LimitMode.BeNotGreater;

            SideCode sideCode = limitMode == LimitMode.BeNotLess || limitMode == LimitMode.BeGreater ?
                SideCode.Outer
                : SideCode.Inner;

            if (shape.GetShapeComponentCount() != GetShapeComponentCount())
            {
                shape = tangentShape.GetTangentShape(sideCode);
            }

            return Compare(this, shape)?.FitsIn(limitMode);
        }

        public override sealed IBaseSpread GetBaseSpread(ISpreadMeasure spreadMeasure)
        {
            return GetFactory().SpreadFactory.Create(spreadMeasure);
        }

        public override sealed IEnumerable<MeasureUnitTypeCode> GetMeasureUnitTypeCodes()
        {
            return base.GetMeasureUnitTypeCodes();
        }

        public override sealed IEnumerable<IQuantifiable> GetShapeComponents()
        {
            return GetShapeExtents();
        }

        public override sealed int GetShapeComponentCount()
        {
            return GetShapeExtentTypeCodes().Count();
        }

        public override sealed void Validate(IRootObject? rootObject, string paramName)
        {
            Validate(this, rootObject, validateShape, paramName);

            #region Local methods
            void validateShape()
            {
                base.Validate(rootObject, paramName);

                int shapeExtentCount = GetShapeExtents().Count();

                if (rootObject is IShape shape && shape.GetShapeExtents().Count() == shapeExtentCount) return;

                throw ArgumentTypeOutOfRangeException(paramName, rootObject!);
            }
            #endregion
        }

        public override sealed void ValidateQuantity(ValueType? quantity, string paramName)
        {
            object? converted = NullChecked(quantity, paramName).ToQuantity(TypeCode.Decimal) ?? throw ArgumentTypeOutOfRangeException(paramName, quantity!);

            ValidateDecimalQuantity((decimal)converted, paramName);
        }

        public override sealed void ValidateShapeComponent(IQuantifiable shapeComponent, string paramName)
        {
            decimal defaultQuantity = NullChecked(shapeComponent, paramName).GetDefaultQuantity();

            if (shapeComponent is not IExtent) throw ArgumentTypeOutOfRangeException(paramName, shapeComponent);

            ValidateDecimalQuantity(defaultQuantity, paramName);
        }
        #endregion
        #endregion

        #region Abstract methods
        public abstract IEnumerable<IExtent> GetDimensions();
        public abstract ITangentShapeFactory GetTangentShapeFactory();
        #endregion
        #endregion

        protected ISpreadMeasure GetSpreadMeasure(IExtent[] shapeExtents)
        {
            return GetSpreadFactory().Create(shapeExtents).GetSpreadMeasure();
        }

        #region Private methods
        #region Static methods
        private static int? Compare(IShape shape, IShape? other)
        {
            if (other == null) return null;

            int comparison = 0;

            foreach (ShapeExtentTypeCode item in shape.GetShapeExtentTypeCodes())
            {
                int recentComparison = shape[item]!.CompareTo(other[item]);

                if (recentComparison != 0)
                {
                    if (comparison == 0)
                    {
                        comparison = recentComparison;
                    }

                    if (comparison != recentComparison)
                    {
                        return null;
                    }
                }
            }

            return comparison;
        }

        private static void ValidateDecimalQuantity(decimal quantity, string name)
        {
            if (quantity > 0) return;

            throw QuantityArgumentOutOfRangeException(name, quantity);
        }
        #endregion
        #endregion
    }
}

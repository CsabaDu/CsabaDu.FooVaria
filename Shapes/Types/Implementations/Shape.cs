using CsabaDu.FooVaria.Common;
using CsabaDu.FooVaria.Measurables.Types;
using CsabaDu.FooVaria.Shapes.Statics;
using CsabaDu.FooVaria.Spreads.Factories;
using CsabaDu.FooVaria.Spreads.Types;
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

        //public IShape GetShape(IShape other)
        //{
        //    return GetFactory().Create(other);
        //}

        public IShape GetShape(IEnumerable<IExtent> shapeExtentList)
        {
            return GetShape(shapeExtentList.ToArray());
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

        public ISpreadFactory GetSpreadFactory()
        {
            return GetFactory().SpreadFactory;
        }

        public bool TryGetShapeExtentTypeCode(IExtent shapeExtent, [NotNullWhen(true)] out ShapeExtentTypeCode? shapeExtentTypeCode)
        {
            _ = NullChecked(shapeExtent, nameof(shapeExtent));

            for (int i = 0; i < GetShapeExtents().Count(); i++)
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

        public override sealed void ValidateShapeExtent(IQuantifiable shapeExtent, string paramName)
        {
            decimal defaultQuantity = NullChecked(shapeExtent, paramName).DefaultQuantity;

            if (shapeExtent is not IExtent) throw ArgumentTypeOutOfRangeException(paramName, shapeExtent);

            ValidateDecimalQuantity(defaultQuantity, paramName);
        }

        public void ValidateShapeExtentCount(int count, string name)
        {
            if (count == GetShapeExtentTypeCodes().Count()) return;

            throw QuantityArgumentOutOfRangeException(name, count);
        }

        public void ValidateShapeExtents(IEnumerable<IExtent> shapeExtents, string paramName)
        {
            int count = NullChecked(shapeExtents, paramName).Count();

            ValidateShapeExtentCount(count, paramName);

            foreach (IExtent item in shapeExtents)
            {
                ValidateShapeExtent(item, paramName);
            }
        }

        public virtual void ValidateShapeExtentTypeCode(ShapeExtentTypeCode shapeExtentTypeCode)
        {
            _ = Defined(shapeExtentTypeCode, nameof(shapeExtentTypeCode));
        }

        public override sealed void ValidateQuantity(ValueType? quantity, string paramName)
        {
            object? converted = NullChecked(quantity, paramName).ToQuantity(TypeCode.Decimal) ?? throw ArgumentTypeOutOfRangeException(paramName, quantity!);

            ValidateDecimalQuantity((decimal)converted, paramName);
        }

        #region Override methods
        public override sealed int CompareTo(IBaseShape? other)
        {
            if (other == null) return 1;

            if (other is not IShape shape) throw ArgumentTypeOutOfRangeException(nameof(other), other);

            shape.Validate(this, nameof(other));

            return Compare(this, shape) ?? throw new ArgumentOutOfRangeException(nameof(other));
        }

        public override sealed bool Equals(IBaseShape? other)
        {
            return other is IShape shape
                && shape.MeasureUnitTypeCode == MeasureUnitTypeCode
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

                return GetShape(exchangedShapeExtents);
            }

            IEnumerable<IExtent> getExchangedShapeExtents(ExtentUnit extentUnit)
            {
                foreach (IExtent item in GetShapeExtents())
                {
                    IBaseMeasure? exchanged = item.ExchangeTo(extentUnit);

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

            IBaseMeasure? getExchangedSpreadMeasure(Enum measureUnit)
            {
                IBaseMeasure spreadMeasure = (IBaseMeasure)GetSpreadMeasure();

                return spreadMeasure.ExchangeTo(measureUnit);
            }
            #endregion
        }

        public override bool? FitsIn(IBaseShape? other, LimitMode? limitMode)
        {
            if (other == null) return null;

            if (!other.IsExchangeableTo(MeasureUnitTypeCode)) return null;

            if (other is not IShape shape) return null;

            if (other is not ITangentShape tangentShape) return null;

            limitMode ??= LimitMode.BeNotGreater;

            SideCode sideCode = limitMode == LimitMode.BeNotLess || limitMode == LimitMode.BeGreater ?
                SideCode.Outer
                : SideCode.Inner;

            if (shape.GetShapeExtents().Count() != GetShapeExtents().Count())
            {
                shape = tangentShape.GetTangentShape(sideCode);
            }

            return Compare(this, shape)?.FitsIn(limitMode);
        }

        public override IBaseSpread GetBaseSpread(ISpreadMeasure spreadMeasure)
        {
            return GetFactory().SpreadFactory.Create(spreadMeasure);
        }

        public override IShapeFactory GetFactory()
        {
            return (IShapeFactory)Factory;
        }

        public override sealed void Validate(IRootObject? rootObject, string paramName)
        {
            ValidateCommonBaseAction = () => validateShape();

            Validate(this, rootObject, paramName);

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
        #endregion

        #region Abstract methods
        public abstract IEnumerable<IExtent> GetDimensions();
        //public abstract IShape GetShape(params IExtent[] shapeExtents);
        public virtual ITangentShapeFactory GetTangentShapeFactory()
        {
            return GetFactory().TangentShapeFactory;
        }
        #endregion
        #endregion

        //#region Protected methods
        //#region Static methods
        //protected static T GetTangentShape<T>(ITangentShape<T> shape, SideCode sideCode) where T : class, IShape, ITangentShape
        //{
        //    return sideCode switch
        //    {
        //        SideCode.Inner => shape.GetInnerTangentShape(),
        //        SideCode.Outer => shape.GetOuterTangentShape(),

        //        _ => throw InvalidSideCodeEnumArgumentException(sideCode),
        //    };
        //}
        //#endregion
        //#endregion

        #region Private methods
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

        #region Static methods
        private static void ValidateDecimalQuantity(decimal quantity, string name)
        {
            if (quantity > 0) return;

            throw QuantityArgumentOutOfRangeException(name, quantity);
        }
        #endregion
        #endregion
    }
}

using CsabaDu.FooVaria.Common;
using CsabaDu.FooVaria.Measurables.Types;
using CsabaDu.FooVaria.Shapes.Statics;
using CsabaDu.FooVaria.Spreads.Types;
using System.Diagnostics.CodeAnalysis;

namespace CsabaDu.FooVaria.Shapes.Types.Implementations
{
    internal abstract class Shape : BaseShape, IShape
    {
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

        public override int CompareTo(IBaseShape? other)
        {

            throw new NotImplementedException();
        }

        public override bool Equals(IBaseShape? other)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public override ISpread GetBaseSpread(ISpreadMeasure spreadMeasure)
        {
            throw new NotImplementedException();
        }

        public override IShapeFactory GetFactory()
        {
            return (IShapeFactory)Factory;
        }

        public override void Validate(IFooVariaObject? fooVariaObject, string paramName)
        {
            throw new NotImplementedException();
        }
        public abstract IExtent? this[ShapeExtentTypeCode shapeExtentTypeCode] { get; }

        public IExtent GetDiagonal(ExtentUnit extentUnit = default)
        {
            return ShapeExtents.GetDiagonal(this, extentUnit);
        }
        public abstract IShape GetShape(ExtentUnit extentUnit);
        public abstract IShape GetShape(params IExtent[] shapeExtents);
        public abstract IShape GetShape(IShape other);

        public override sealed void ValidateQuantity(ValueType? quantity, string paramName)
        {
            object? converted = NullChecked(quantity, paramName).ToQuantity(TypeCode.Decimal) ?? throw ArgumentTypeOutOfRangeException(paramName, quantity!);

            ValidateDecimalQuantity((decimal)converted, paramName);
        }

        public IEnumerable<IExtent> GetShapeExtents()
        {
            foreach (ShapeExtentTypeCode item in GetShapeExtentTypeCodes())
            {
                yield return GetShapeExtent(item);
            }
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

        public abstract bool TryGetShapeExtentTypeCode(IExtent shapeExtent, [NotNullWhen(true)] out ShapeExtentTypeCode? shapeExtentTypeCode);

        public virtual void ValidateShapeExtentTypeCode(ShapeExtentTypeCode shapeExtentTypeCode)
        {
            _ = Defined(shapeExtentTypeCode, nameof(shapeExtentTypeCode));
        }

        public IExtent GetShapeExtent(ShapeExtentTypeCode shapeExtentTypeCode)
        {
            return this[shapeExtentTypeCode] ?? throw InvalidShapeExtentTypeCodeEnumArgumentException(shapeExtentTypeCode);
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

        public override sealed void ValidateShapeExtent(IQuantifiable shapeExtent, string paramName)
        {
            decimal defaultQuantity = NullChecked(shapeExtent, paramName).DefaultQuantity;

            if (shapeExtent is not IExtent) throw ArgumentTypeOutOfRangeException(paramName, shapeExtent);

            ValidateDecimalQuantity(defaultQuantity, paramName);
        }

        private static void ValidateDecimalQuantity(decimal quantity, string name)
        {
            if (quantity > 0) return;

            throw QuantityArgumentOutOfRangeException(name, quantity);
        }

        public abstract IShape GetShape(IEnumerable<IExtent> shapeExtentList);
    }

    internal abstract class PlaneShape : Shape, IPlaneShape
    {
        private protected PlaneShape(IPlaneShape other) : base(other)
        {
            Area = other.Area;
        }

        private protected PlaneShape(IShapeFactory factory, IBaseShape baseShape) : base(factory, baseShape)
        {
            Area = GetArea(baseShape, nameof(baseShape));
        }

        private protected PlaneShape(IShapeFactory factory, params IExtent[] shapeExtents) : base(factory, MeasureUnitTypeCode.AreaUnit, shapeExtents)
        {
            Area = GetArea(shapeExtents, nameof(shapeExtents));
        }

        public IArea Area { get; }

        public override IPlaneShape GetShape(ExtentUnit measureUnit)
        {
            return (IPlaneShape?)ExchangeTo(measureUnit) ?? throw new InvalidOperationException(null);
        }

        public override sealed ISpreadMeasure GetSpreadMeasure()
        {
            return Area;
        }

        private IArea GetArea(object arg, string paramName)
        {
            if (arg is IBaseShape baseShape)
            {
                return getArea(getBaseShape());
            }
            else if (arg is IExtent[] shapeExtents)
            {
                return getArea(getSpread(shapeExtents));
            }
            else
            {
                throw new InvalidOperationException(null);
            }

            #region Local methods
            IArea getArea(IBaseSpread baseSpread)
            {
                return (IArea)baseSpread.GetSpreadMeasure();
            }

            IBaseShape getBaseShape()
            {
                Validate(baseShape, paramName); // TODO Mit validál?

                return baseShape;
            }

            ISpread getSpread(IExtent[] shapeExtents)
            {
                return GetFactory().SpreadFactory.Create(shapeExtents);
            }
            #endregion
        }

    }

    internal sealed class Rectangle : PlaneShape, IRectangle
    {
        internal Rectangle(IRectangle other) : base(other)
        {
            Length = other.Length;
            Width = other.Width;
        }

        internal Rectangle(IShapeFactory factory, ICuboid cuboid, ShapeExtentTypeCode perpendicular) : base(factory, cuboid)
        {
            Length = getShapeExtents().Length;
            Width = getShapeExtents().Width;

            #region Local methods
            (IExtent Length, IExtent Width) getShapeExtents()
            {
                return perpendicular switch
                {
                    ShapeExtentTypeCode.Length => (cuboid.GetWidth(), cuboid.Height),
                    ShapeExtentTypeCode.Width => (cuboid.GetLength(), cuboid.Height),
                    ShapeExtentTypeCode.Height => (cuboid.GetLength(), cuboid.GetWidth()),

                    _ => throw new InvalidOperationException(null),
                };
            }
            #endregion
        }

        internal Rectangle(IShapeFactory factory, params IExtent[] shapeExtents) : base(factory, shapeExtents)
        {
            Length = shapeExtents[0];
            Width = shapeExtents[1];
        }

        public override IExtent? this[ShapeExtentTypeCode shapeExtentTypeCode] => shapeExtentTypeCode switch
        {
            ShapeExtentTypeCode.Length => Length,
            ShapeExtentTypeCode.Width => Width,

            _ => null,
        };

        public IExtent Length { get; init; }
        public IExtent Width { get; init; }

        public IExtent GetComparedShapeExtent(ComparisonCode? comparisonCode)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IExtent> GetDimensions()
        {
            return GetShapeExtents();
        }

        public ICircularShape GetInnerTangentShape(ComparisonCode comparisonCode)
        {
            throw new NotImplementedException();
        }

        public ITangentShape GetInnerTangentShape()
        {
            return GetInnerTangentShape(ComparisonCode.Less);
        }

        public IExtent GetLength()
        {
            return Length;
        }

        public IExtent GetLength(ExtentUnit extentUnit)
        {
            return Length.GetMeasure(extentUnit);
        }

        public ITangentShape GetOuterTangentShape()
        {
            throw new NotImplementedException();
        }

        public override IShape GetShape(params IExtent[] shapeExtents)
        {
            throw new NotImplementedException();
        }

        public override IShape GetShape(IShape other)
        {
            throw new NotImplementedException();
        }

        public override IShape GetShape(IEnumerable<IExtent> shapeExtentList)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IExtent> GetSortedDimensions()
        {
            throw new NotImplementedException();
        }

        public IExtent GetWidth()
        {
            return Width;
        }

        public IExtent GetWidth(ExtentUnit extentUnit)
        {
            throw new NotImplementedException();
        }

        public IRectangle RotateHorizontally()
        {
            throw new NotImplementedException();
        }

        public IRectangle RotateHorizontallyWith(IRectangularShape other)
        {
            throw new NotImplementedException();
        }

        public override bool TryGetShapeExtentTypeCode(IExtent shapeExtent, [NotNullWhen(true)] out ShapeExtentTypeCode? shapeExtentTypeCode)
        {
            throw new NotImplementedException();
        }
    }
}

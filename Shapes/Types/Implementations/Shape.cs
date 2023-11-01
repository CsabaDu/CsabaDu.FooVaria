using CsabaDu.FooVaria.Common;
using CsabaDu.FooVaria.Measurables.Types;
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

        public override void Validate(IFooVariaObject? fooVariaObject)
        {
            throw new NotImplementedException();
        }
        public abstract IExtent? this[ShapeExtentTypeCode shapeExtentTypeCode] { get; }

        public abstract IExtent GetDiagonal(ExtentUnit extentUnit = default);
        public abstract IShape GetShape(ExtentUnit extentUnit);
        public abstract IShape GetShape(params IExtent[] shapeExtents);
        public abstract IShape GetShape(IShape other);

        public override sealed void ValidateQuantity(ValueType? quantity, string paramName)
        {
            object? converted = NullChecked(quantity, paramName).ToQuantity(TypeCode.Decimal);

            if (converted == null) throw ArgumentTypeOutOfRangeException(paramName, quantity!);

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
        public void ValidateShapeExtents(IEnumerable<IExtent> shapeExtents, string name)
        {
            int count = NullChecked(shapeExtents, name).Count();

            ValidateShapeExtentCount(count, name);

            foreach (IExtent item in shapeExtents)
            {
                ValidateShapeExtent(item, name);
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

        public override sealed void ValidateShapeExtent(IQuantifiable shapeExtent, string name)
        {
            decimal defaultQuantity = NullChecked(shapeExtent, name).DefaultQuantity;

            if (shapeExtent is not IExtent) throw ArgumentTypeOutOfRangeException(name, shapeExtent);

            ValidateDecimalQuantity(defaultQuantity, name);
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
        }

        private protected PlaneShape(IShapeFactory factory, IBaseShape baseShape) : base(factory, baseShape)
        {
        }

        private protected PlaneShape(IShapeFactory factory, params IExtent[] shapeExtents) : base(factory, MeasureUnitTypeCode.AreaUnit, shapeExtents)
        {
            Area = SpreadMeasures.GetArea(new MeasureFactory(new MeasurementFactory()), shapeExtents);
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
    }
}

using CsabaDu.FooVaria.Common;
using CsabaDu.FooVaria.Measurables.Factories;
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

        public override sealed void Validate(IFooVariaObject? fooVariaObject, string paramName)
        {
            ValidateCommonBaseAction = () => validateShape();

            Validate(this, fooVariaObject, paramName);

            #region Local methods
            void validateShape()
            {
                base.Validate(fooVariaObject, paramName);

                int shapeExtentCount = GetShapeExtents().Count();

                if (fooVariaObject is IShape shape && shape.GetShapeExtents().Count() == shapeExtentCount) return;

                throw ArgumentTypeOutOfRangeException(paramName, fooVariaObject!);
            }
            #endregion
        }
        #endregion

        #region Abstract methods
        public abstract IEnumerable<IExtent> GetDimensions();
        public abstract IShape GetShape(params IExtent[] shapeExtents);
        #endregion
        #endregion

        #region Protected methods
        #region Static methods
        protected static T GetTangentShape<T>(ITangentShape<T> shape, SideCode sideCode) where T : class, IShape, ITangentShape
        {
            return sideCode switch
            {
                SideCode.Inner => shape.GetInnerTangentShape(),
                SideCode.Outer => shape.GetOuterTangentShape(),

                _ => throw InvalidSideCodeEnumArgumentException(sideCode),
            };
        }
        #endregion
        #endregion

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

    internal abstract class PlaneShape : Shape, IPlaneShape
    {
        private protected PlaneShape(IPlaneShape other) : base(other)
        {
            Area = other.Area;
        }

        private protected PlaneShape(IPlaneShapeFactory factory, params IExtent[] shapeExtents) : base(factory, MeasureUnitTypeCode.AreaUnit, shapeExtents)
        {
            Area = GetArea(shapeExtents, nameof(shapeExtents));
        }

        public IArea Area { get; init; }

        public override sealed IArea GetSpreadMeasure()
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
                Validate(baseShape, paramName);

                return baseShape;
            }

            ISpread getSpread(IExtent[] shapeExtents)
            {
                return GetSpreadFactory().Create(shapeExtents);
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

        internal Rectangle(IRectangleFactory factory, IExtent length, IExtent width) : base(factory, length, width)
        {
            Length = length;
            Width = width;
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
            _ = NullChecked(comparisonCode, nameof(comparisonCode));

            IEnumerable<IExtent> shapeExtents = GetSortedDimensions();

            return comparisonCode switch
            {
                ComparisonCode.Greater => shapeExtents.Last(),
                ComparisonCode.Less => shapeExtents.First(),

                _ => throw InvalidComparisonCodeEnumArgumentException(comparisonCode!.Value),
            };
        }

        public override IEnumerable<IExtent> GetDimensions()
        {
            return GetShapeExtents();
        }

        public ICircle GetInnerTangentShape(ComparisonCode comparisonCode)
        {
            throw new NotImplementedException();
        }

        public ICircle GetInnerTangentShape()
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

        public ICircle GetOuterTangentShape()
        {
            throw new NotImplementedException();
        }

        public override IRectangle GetShape(params IExtent[] shapeExtents)
        {
            throw new NotImplementedException();
        }

        public IShape GetTangentShape(SideCode sideCode)
        {
            return GetTangentShape(this, sideCode);
        }

        public IExtent GetWidth()
        {
            return Width;
        }

        public IExtent GetWidth(ExtentUnit extentUnit)
        {
            return Width.GetMeasure(extentUnit);
        }

        public IRectangle RotateHorizontally()
        {
            return (IRectangle)GetShape(GetSortedDimensions());
        }
    }

    internal sealed class Circle : PlaneShape, ICircle
    {
        internal Circle(ICircle other) : base(other)
        {
            Radius = other.Radius;
        }

        internal Circle(ICircleFactory factory, IExtent radius) : base(factory, radius)
        {
            Radius = radius;
        }

        public override IExtent? this[ShapeExtentTypeCode shapeExtentTypeCode] => throw new NotImplementedException();

        public IExtent Radius { get; init; }

        public override IEnumerable<IExtent> GetDimensions()
        {
            for (int i = 0; i < 2; i++)
            {
                yield return GetDiagonal();
            }
        }

        public IRectangle GetInnerTangentShape(IExtent innerTangentRectangleSide)
        {
            throw new NotImplementedException();
        }

        public IRectangle GetInnerTangentShape()
        {
            throw new NotImplementedException();
        }

        public IRectangle GetOuterTangentShape()
        {
            throw new NotImplementedException();
        }

        public IExtent GetRadius()
        {
            return Radius;
        }

        public IExtent GetRadius(ExtentUnit extentUnit)
        {
            return Radius.GetMeasure(extentUnit);
        }

        public override ICircle GetShape(params IExtent[] shapeExtents)
        {
            throw new NotImplementedException();
        }

        public IShape GetTangentShape(SideCode sideCode)
        {
            return GetTangentShape(this, sideCode);
        }
    }

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

        public abstract IPlaneShape GetBaseFace();
        public IPlaneShape GetBaseFace(ExtentUnit extentUnit)
        {
            return (IPlaneShape)GetBaseFace().GetShape(extentUnit);
        }
        public abstract IDryBody GetDryBody(IPlaneShape baseFace, IExtent height);
        public IExtent GetHeight()
        {
            return Height;
        }
        public IExtent GetHeight(ExtentUnit extentUnit)
        {
            return Height.GetMeasure(extentUnit);
        }

        public override sealed IVolume GetSpreadMeasure()
        {
            return Volume;
        }

        //public override IDryBodyFactory GetFactory()
        //{
        //    return (IDryBodyFactory)Factory;
        //}
        public abstract IPlaneShapeFactory GetBaseFaceFactory();

        public abstract IPlaneShape GetProjection(ShapeExtentTypeCode perpendicular);
        public abstract IExtent GetShapeExtent(IPlaneShape projection, IVolume volume);
        public abstract void ValidateBaseFace(IPlaneShape planeShape);
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

        public override sealed T GetDryBody(IPlaneShape baseFace, IExtent height)
        {
            string paramName = nameof(baseFace);

            if (NullChecked(baseFace, paramName) is U validBaseFace) return GetDryBody(validBaseFace, height);

            throw ArgumentTypeOutOfRangeException(paramName, baseFace);
        }

        public abstract T GetDryBody(U baseFace, IExtent height);

        public override IShape GetShape(params IExtent[] shapeExtents)
        {
            throw new NotImplementedException();
        }

        public override IExtent GetShapeExtent(IPlaneShape projection, IVolume volume)
        {
            throw new NotImplementedException();
        }

        public override void ValidateBaseFace(IPlaneShape planeShape)
        {
            throw new NotImplementedException();
        }
    }

    internal sealed class Cuboid : DryBody<ICuboid, IRectangle>, ICuboid
    {
        public Cuboid(ICuboid other) : base(other)
        {
        }

        public Cuboid(ICuboidFactory factory, IExtent length, IExtent width, IExtent height) : base(factory, length, width, height)
        {
        }

        public Cuboid(ICuboidFactory factory, IRectangle baseFace, IExtent height) : base(factory, baseFace, height)
        {
        }

        public override IExtent? this[ShapeExtentTypeCode shapeExtentTypeCode] => shapeExtentTypeCode switch
        {
            ShapeExtentTypeCode.Length => BaseFace.Length,
            ShapeExtentTypeCode.Width => BaseFace.Width,
            ShapeExtentTypeCode.Height => Height,

            _ => null,
        };

        public override IRectangleFactory GetBaseFaceFactory()
        {
            return (IRectangleFactory)GetFactory().BaseFaceFactory;
        }

        public IExtent GetComparedShapeExtent(ComparisonCode? comparisonCode)
        {
            IEnumerable<IExtent> shapeExtents = GetSortedDimensions();

            return comparisonCode switch
            {
                null => shapeExtents.ElementAt(1),
                ComparisonCode.Greater => shapeExtents.Last(),
                ComparisonCode.Less => shapeExtents.First(),

                _ => throw InvalidComparisonCodeEnumArgumentException(comparisonCode!.Value),
            };
        }

        public override IEnumerable<IExtent> GetDimensions()
        {
            return GetShapeExtents();
        }

        public override ICuboid GetDryBody(IRectangle baseFace, IExtent height)
        {
            return GetFactory().Create(baseFace, height);
        }

        public ICylinder GetInnerTangentShape(ComparisonCode comparisonCode)
        {
            throw new NotImplementedException();
        }

        public ICylinder GetInnerTangentShape()
        {
            throw new NotImplementedException();
        }

        public IExtent GetLength()
        {
            throw new NotImplementedException();
        }

        public IExtent GetLength(ExtentUnit extentUnit)
        {
            throw new NotImplementedException();
        }

        public ICylinder GetOuterTangentShape()
        {
            throw new NotImplementedException();
        }

        public override IPlaneShape GetProjection(ShapeExtentTypeCode perpendicular)
        {
            throw new NotImplementedException();
        }

        public IShape GetTangentShape(SideCode sideCode)
        {
            return GetTangentShape(this, sideCode);
        }

        public IRectangle GetVerticalProjection(ComparisonCode comparisonCode)
        {
            throw new NotImplementedException();
        }

        public IExtent GetWidth()
        {
            return BaseFace.Width;
        }

        public IExtent GetWidth(ExtentUnit extentUnit)
        {
            return GetWidth().GetMeasure(extentUnit);
        }

        public ICuboid RotateHorizontally()
        {
            IRectangle baseFace = BaseFace.RotateHorizontally();

            return GetDryBody(baseFace, Height);
        }

        public ICuboid RotateSpatially()
        {
            return (ICuboid)GetShape(GetSortedDimensions());
        }

        public override ICuboidFactory GetFactory()
        {
            return (ICuboidFactory)Factory;
        }
    }

    internal sealed class Cylinder : DryBody<ICylinder, ICircle>, ICylinder
    {
        public Cylinder(ICylinder other) : base(other)
        {
        }

        public Cylinder(ICylinderFactory factory, IExtent radius, IExtent height) : base(factory, radius, height)
        {
        }

        public Cylinder(ICylinderFactory factory, ICircle baseFace, IExtent height) : base(factory, baseFace, height)
        {
        }

        public override IExtent? this[ShapeExtentTypeCode shapeExtentTypeCode] => throw new NotImplementedException();

        public override IEnumerable<IExtent> GetDimensions()
        {
            throw new NotImplementedException();
        }

        public override ICylinder GetDryBody(ICircle baseFace, IExtent height)
        {
            throw new NotImplementedException();
        }

        public ICuboid GetInnerTangentShape(IExtent innerTangentRectangleSide)
        {
            throw new NotImplementedException();
        }

        public ICuboid GetInnerTangentShape()
        {
            throw new NotImplementedException();
        }

        public ICuboid GetOuterTangentShape()
        {
            throw new NotImplementedException();
        }

        public override ICircleFactory GetBaseFaceFactory()
        {
            throw new NotImplementedException();
        }

        public override IPlaneShape GetProjection(ShapeExtentTypeCode perpendicular)
        {
            throw new NotImplementedException();
        }

        public IExtent GetRadius()
        {
            return BaseFace.Radius;
        }

        public IExtent GetRadius(ExtentUnit extentUnit)
        {
            return GetRadius().GetMeasure(extentUnit);
        }

        public IShape GetTangentShape(SideCode sideCode)
        {
            throw new NotImplementedException();
        }

        public IRectangle GetVerticalProjection()
        {
            throw new NotImplementedException();
        }

        public override ICylinderFactory GetFactory()
        {
            return (ICylinderFactory)Factory;
        }
    }
}

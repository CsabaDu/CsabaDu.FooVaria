using CsabaDu.FooVaria.Measurables.Factories;
using CsabaDu.FooVaria.Measurables.Types;
using CsabaDu.FooVaria.Shapes.Types;
using CsabaDu.FooVaria.Shapes.Types.Implementations;
using CsabaDu.FooVaria.Spreads.Factories;
using CsabaDu.FooVaria.Spreads.Types;

namespace CsabaDu.FooVaria.Shapes.Factories.Implementations
{
    public abstract class ShapeFactory : IShapeFactory
    {
        private protected ShapeFactory(ISpreadFactory spreadFactory, ITangentShapeFactory tangentShapeFactory)
        {
            SpreadFactory = NullChecked(spreadFactory, nameof(spreadFactory));
            TangentShapeFactory = NullChecked(tangentShapeFactory, nameof(tangentShapeFactory));
        }

        public ISpreadFactory SpreadFactory { get; init; }
        public ITangentShapeFactory TangentShapeFactory { get; init; }

        public abstract IBaseSpread Create(ISpreadMeasure spreadMeasure);

        public IExtent CreateShapeExtent(ExtentUnit extentUnit, ValueType quantity)
        {
            IMeasure extent = GetMeasureFactory().Create(quantity, extentUnit);

            if (extent.DefaultQuantity > 0) return (IExtent)extent;

            throw QuantityArgumentOutOfRangeException(quantity);
        }

        public IMeasureFactory GetMeasureFactory()
        {
            return SpreadFactory.MeasureFactory;
        }

        //public abstract IShape Create(params IExtent[] shapeExtents);
        //public abstract IShape Create(IShape other);

        public virtual ISpreadFactory GetSpreadFactory()
        {
            return SpreadFactory;
        }

        public virtual ITangentShapeFactory GetTangentShapeFactory()
        {
            return TangentShapeFactory;
        }
    }

    public abstract class PlaneShapeFactory : ShapeFactory, IPlaneShapeFactory
    {
        private protected PlaneShapeFactory(IBulkSurfaceFactory spreadFactory, ITangentShapeFactory tangentShapeFactory) : base(spreadFactory, tangentShapeFactory)
        {
        }

        public abstract IPlaneShape Create(IDryBody dryBody, ShapeExtentTypeCode perpendicular);

        public override IBulkSurface Create(ISpreadMeasure spreadMeasure)
        {
            IMeasure area = SpreadMeasures.GetValidSpreadMeasure(MeasureUnitTypeCode.AreaUnit, spreadMeasure);

            return GetSpreadFactory().Create((IArea)area);
        }

        public override sealed IBulkSurfaceFactory GetSpreadFactory()
        {
            return (IBulkSurfaceFactory)SpreadFactory;
        }

        protected static T CreateTangentShape<T, U>(ITangentShapeFactory<T, U> factory,  U tangentShape, SideCode sideCode) where T : class, IShape, ITangentShape where U : class, IShape, ITangentShape
        {
            return sideCode switch
            {
                SideCode.Outer => factory.CreateOuterTangentShape(tangentShape),
                SideCode.Inner => factory.CreateInnerTangentShape(tangentShape),

                _ => throw InvalidSidenCodeEnumArgumentException(sideCode),
            };
        }

        public abstract IPlaneShape Create(IPlaneShape other);
    }

    public sealed class RectangleFactory : PlaneShapeFactory, IRectangleFactory
    {
        #region Constructors
        public RectangleFactory(IBulkSurfaceFactory spreadFactory, ICircleFactory tangentShapeFactory) : base(spreadFactory, tangentShapeFactory)
        {
        }
        #endregion

        #region Public methods
        public IRectangle Create(IExtent length, IExtent width)
        {
            return new Rectangle(this, length, width);
        }

        public IRectangle Create(IRectangle other)
        {
            return new Rectangle(other);
        }

        public ICircle CreateInnerTangentShape(IRectangle rectangle, ComparisonCode comparisonCode)
        {
            IExtent diagonal = NullChecked(rectangle, nameof(rectangle)).GetComparedShapeExtent(comparisonCode);

            return CreateCircle(diagonal);
        }

        public ICircle CreateInnerTangentShape(IRectangle rectangle)
        {
            return CreateInnerTangentShape(rectangle, ComparisonCode.Less);
        }

        public ICircle CreateOuterTangentShape(IRectangle rectangle)
        {
            IExtent diagonal = NullChecked(rectangle, nameof(rectangle)).GetDiagonal();

            return CreateCircle(diagonal);
        }

        public ICircle CreateTangentShape(IRectangle rectangle, SideCode sideCode)
        {
            return CreateTangentShape(this, rectangle, sideCode);
        }

        #region Override methods
        public override IRectangle Create(IDryBody dryBody, ShapeExtentTypeCode perpendicular)
        {
            _ = NullChecked(dryBody, nameof(dryBody));
            _ = Defined(perpendicular, nameof(perpendicular));

            return dryBody switch
            {
                Cylinder cylinder => create(getCylinderShapeExtents(cylinder)),
                Cuboid cuboid => create(getCuboidShapeExtents(cuboid)),

                _ => throw new InvalidOperationException(null),
            };

            #region Local methods
            IRectangle create((IExtent Length, IExtent Width) shapeExtents)
            {
                return Create(shapeExtents.Length, shapeExtents.Width);
            }

            (IExtent Length, IExtent Width) getCylinderShapeExtents(ICylinder cylinder)
            {
                IExtent baseFaceDiagonal = cylinder.BaseFace.GetDiagonal();
                return perpendicular switch
                {
                    ShapeExtentTypeCode.Radius => (baseFaceDiagonal, cylinder.Height),
                    ShapeExtentTypeCode.Height => (baseFaceDiagonal, baseFaceDiagonal),

                    _ => throw InvalidShapeExtentTypeCodeEnumArgumentException(perpendicular, nameof(perpendicular)),
                };
            }

            (IExtent Length, IExtent Width) getCuboidShapeExtents(ICuboid cuboid)
            {
                return perpendicular switch
                {
                    ShapeExtentTypeCode.Length => (cuboid.GetWidth(), cuboid.Height),
                    ShapeExtentTypeCode.Width => (cuboid.GetLength(), cuboid.Height),
                    ShapeExtentTypeCode.Height => (cuboid.GetLength(), cuboid.GetWidth()),

                    _ => throw InvalidShapeExtentTypeCodeEnumArgumentException(perpendicular, nameof(perpendicular)),
                };
            }
            #endregion
        }

        public override IRectangle Create(IPlaneShape other)
        {
            return NullChecked(other, nameof(other)) switch
            {
                Circle circle => circle.GetOuterTangentShape(),
                Rectangle rectangle => Create(rectangle),

                _ => throw new InvalidOperationException(null),
            };
        }

        public override ICircleFactory GetTangentShapeFactory()
        {
            return (ICircleFactory)TangentShapeFactory;
        }
        #endregion
        #endregion

        #region Private methods
        private ICircle CreateCircle(IExtent diagonal)
        {
            IExtent radius = (IExtent)diagonal.Divide(2);

            return GetTangentShapeFactory().Create(radius);
        }
        #endregion
    }

    public sealed class CircleFactory : PlaneShapeFactory, ICircleFactory
    {
        public CircleFactory(IBulkSurfaceFactory spreadFactory, IRectangleFactory tangentShapeFactory) : base(spreadFactory, tangentShapeFactory)
        {
        }

        public override IPlaneShape Create(IDryBody dryBody, ShapeExtentTypeCode perpendicular)
        {
            throw new NotImplementedException();
        }

        public override ICircle Create(IPlaneShape other)
        {
            return NullChecked(other, nameof(other)) switch
            {
                Circle circle =>  Create(circle),
                Rectangle rectangle => rectangle.GetOuterTangentShape(),

                _ => throw new InvalidOperationException(null),
            };
        }

        public ICircle Create(IExtent radius)
        {
            return new Circle(this, radius);
        }

        public ICircle Create(ICircle other)
        {
            return new Circle(other);
        }

        public IRectangle CreateInnerTangentShape(ICircle circle, IExtent tangentRectangleSide)
        {
            throw new NotImplementedException();
        }

        public IRectangle CreateInnerTangentShape(ICircle circle)
        {
            throw new NotImplementedException();
        }

        public IRectangle CreateOuterTangentShape(ICircle circle)
        {
            return (IRectangle)GetTangentShapeFactory().Create(circle);
        }

        public IRectangle CreateTangentShape(ICircle circle, SideCode sideCode)
        {
            return CreateTangentShape(this, circle, sideCode);
        }

        public override IRectangleFactory GetTangentShapeFactory()
        {
            return (IRectangleFactory)TangentShapeFactory;
        }
    }

    public abstract class DryBodyFactory : ShapeFactory, IDryBodyFactory
    {
        private protected DryBodyFactory(IBulkBodyFactory spreadFactory, ITangentShapeFactory tangentShapeFactory, IPlaneShapeFactory baseFaceFactory) : base(spreadFactory, tangentShapeFactory)
        {
            BaseFaceFactory = NullChecked(baseFaceFactory, nameof(baseFaceFactory));
        }

        public IPlaneShapeFactory BaseFaceFactory { get; init; }

        public override sealed IBulkBody Create(ISpreadMeasure spreadMeasure)
        {
            IMeasure volume = SpreadMeasures.GetValidSpreadMeasure(MeasureUnitTypeCode.VolumeUnit, spreadMeasure);

            return GetSpreadFactory().Create((IVolume)volume);
        }

        public abstract IDryBody Create(IPlaneShape baseFace, IExtent height);
        public abstract IDryBody Create(IDryBody other);
        public abstract IPlaneShapeFactory GetBaseFaceFactory();

        //public override IShape Create(IShape other)
        //{
        //    throw new NotImplementedException();
        //}

        public override sealed IBulkBodyFactory GetSpreadFactory()
        {
            return (IBulkBodyFactory)SpreadFactory;
        }
    }

    public abstract class DryBodyFactory<T, U> : DryBodyFactory, IDryBodyFactory<T, U> where T : class, IDryBody, ITangentShape where U : class, IPlaneShape,ITangentShape
    {
        public DryBodyFactory(IBulkBodyFactory spreadFactory, ITangentShapeFactory tangentShapeFactory, IPlaneShapeFactory baseFaceFactory) : base(spreadFactory, tangentShapeFactory, baseFaceFactory)
        {
        }

        public override T Create(IPlaneShape baseFace, IExtent height)
        {
            string paramName = nameof(baseFace);

            if (NullChecked(baseFace, paramName) is U validBaseFace) return Create(validBaseFace, height);

            throw ArgumentTypeOutOfRangeException(paramName, baseFace);
        }

        public abstract T Create(U baseFace, IExtent height);
        //public abstract U CreateBaseFace(params IExtent[] shapeExtent);
    }

    public sealed class CuboidFactory : DryBodyFactory<ICuboid, IRectangle>, ICuboidFactory
    {
        public CuboidFactory(IBulkBodyFactory spreadFactory, ICylinderFactory tangentShapeFactory, IRectangleFactory baseFaceFactory) : base(spreadFactory, tangentShapeFactory, baseFaceFactory)
        {
        }

        public override IRectangleFactory GetBaseFaceFactory()
        {
            return (IRectangleFactory)BaseFaceFactory;
        }

        public override ICuboid Create(IRectangle baseFace, IExtent height)
        {
            return new Cuboid(this, baseFace, height);
        }

        public ICuboid Create(IExtent length, IExtent width, IExtent height)
        {
            return new Cuboid(this, length, width, height);
        }

        public ICuboid Create(ICuboid other)
        {
            return new Cuboid(other);
        }

        public override ICuboid Create(IDryBody other)
        {
            return NullChecked(other, nameof(other)) switch
            {
                Cylinder cylinder => cylinder.GetOuterTangentShape(),
                Cuboid cuboid => Create(cuboid),

                _ => throw ArgumentTypeOutOfRangeException(nameof(other), other),
            };
        }

        //public override IRectangle CreateBaseFace(params IExtent[] shapeExtent)
        //{
        //    throw new NotImplementedException();
        //}

        public IRectangle CreateBaseFace(IExtent length, IExtent width)
        {
            return GetBaseFaceFactory().Create(length, width);
        }

        public ICylinder CreateInnerTangentShape(ICuboid rectangularShape, ComparisonCode comparisonCode)
        {
            throw new NotImplementedException();
        }

        public ICylinder CreateInnerTangentShape(ICuboid shape)
        {
            throw new NotImplementedException();
        }

        public ICylinder CreateOuterTangentShape(ICuboid shape)
        {
            throw new NotImplementedException();
        }

        public IRectangle CreateProjection(ICuboid cuboid, ShapeExtentTypeCode perpendicular)
        {
            throw new NotImplementedException();
        }

        public ICylinder CreateTangentShape(ICuboid shape, SideCode sideCode)
        {
            throw new NotImplementedException();
        }

        public IRectangle CreateVerticalProjection(ICuboid cuboid, ComparisonCode comparisonCode)
        {
            throw new NotImplementedException();
        }

        public override ICylinderFactory GetTangentShapeFactory()
        {
            return (ICylinderFactory)TangentShapeFactory;
        }
    }
}

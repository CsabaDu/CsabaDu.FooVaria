using System.Collections.Generic;

namespace CsabaDu.FooVaria.Shapes.Factories.Implementations
{
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

        public IRectangle CreateBaseFace(IExtent length, IExtent width)
        {
            return GetBaseFaceFactory().Create(length, width);
        }

        public ICylinder CreateInnerTangentShape(ICuboid rectangularShape, ComparisonCode comparisonCode)
        {
            throw new NotImplementedException();
        }

        public ICylinder CreateInnerTangentShape(ICuboid cuboid)
        {
            throw new NotImplementedException();
        }

        public ICylinder CreateOuterTangentShape(ICuboid cuboid)
        {
            throw new NotImplementedException();
        }

        public IRectangle CreateProjection(ICuboid cuboid, ShapeExtentTypeCode perpendicular)
        {
            throw new NotImplementedException();
        }

        public ICylinder CreateTangentShape(ICuboid cuboid, SideCode sideCode)
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

        public override IBaseShape Create(params IQuantifiable[] shapeComponents)
        {
            int count = GetValidShapeComponentsCount(shapeComponents);

            return count switch
            {
                1 => createCuboidFromOneParam(),
                2 => createCuboidFromTwoParams(),
                3 => createCuboidFromThreeParams(),

                _ => throw CountArgumentOutOfRangeException(count, nameof(shapeComponents)),

            };

            #region Local methods
            ICuboid createCuboidFromOneParam()
            {
                if (shapeComponents[0] is ICuboid cuboid) return Create(cuboid);

                throw ArgumentTypeOutOfRangeException(nameof(shapeComponents), shapeComponents);
            }

            ICuboid createCuboidFromTwoParams()
            {
                IQuantifiable last = shapeComponents[1];

                if (last is not IExtent height) throw ArgumentTypeOutOfRangeException(nameof(shapeComponents), last);

                IQuantifiable first = shapeComponents[0];

                if (first is IRectangle rectangle) return Create(rectangle, height);

                throw ArgumentTypeOutOfRangeException(nameof(shapeComponents), first);

            }

            ICuboid createCuboidFromThreeParams()
            {
                IEnumerable<IExtent> shapeExtents = GetShapeExtents(shapeComponents);

                return Create(shapeExtents.First(), shapeExtents.ElementAt(1), shapeExtents.Last());
            }
            #endregion
        }

        public override IPlaneShape CreateProjection(IDryBody dryBody, ShapeExtentTypeCode perpendicular)
        {
            throw new NotImplementedException();
        }
    }
}

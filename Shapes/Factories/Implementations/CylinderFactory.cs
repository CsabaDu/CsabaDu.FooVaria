using CsabaDu.FooVaria.Shapes.Behaviors;

namespace CsabaDu.FooVaria.Shapes.Factories.Implementations
{
    public sealed class CylinderFactory : DryBodyFactory<ICylinder, ICircle>, ICylinderFactory
    {
        public CylinderFactory(IBulkBodyFactory spreadFactory, ICuboidFactory tangentShapeFactory, ICircleFactory baseFaceFactory) : base(spreadFactory, tangentShapeFactory, baseFaceFactory)
        {
        }

        public override ICylinder Create(ICircle baseFace, IExtent height)
        {
            return new Cylinder(this, baseFace, height);
        }

        public ICylinder Create(IExtent radius, IExtent height)
        {
            return new Cylinder(this, radius, height);
        }

        public ICylinder CreateNew(ICylinder other)
        {
            return new Cylinder(other);
        }

        public override ICylinder CreateBaseShape(params IQuantifiable[] shapeComponents)
        {
            int count = GetValidShapeComponentsCount(shapeComponents);

            return count switch
            {
                1 => createCylinderFromOneParam(),
                2 => createCylinderFromTwoParams(),

                _ => throw CountArgumentOutOfRangeException(count, nameof(shapeComponents)),

            };

            #region Local methods
            ICylinder createCylinderFromOneParam()
            {
                if (shapeComponents[0] is ICylinder cylinder) return CreateNew(cylinder);

                throw ArgumentTypeOutOfRangeException(nameof(shapeComponents), shapeComponents);
            }

            ICylinder createCylinderFromTwoParams()
            {
                IQuantifiable last = shapeComponents[1];

                if (last is not IExtent height) throw ArgumentTypeOutOfRangeException(nameof(shapeComponents), last);

                IQuantifiable first = shapeComponents[0];

                if (first is ICircle circle) return Create(circle, height);

                if (first is IExtent radius) return Create(radius, height);

                throw ArgumentTypeOutOfRangeException(nameof(shapeComponents), first);
            }
            #endregion
        }

        public ICircle CreateBaseFace(IExtent radius)
        {
            return GetBaseFaceFactory().Create(radius);
        }

        public ICuboid CreateInnerTangentShape(ICylinder circularShape, IExtent tangentRectangleSide)
        {
            throw new NotImplementedException();
        }

        public ICuboid CreateInnerTangentShape(ICylinder shape)
        {
            throw new NotImplementedException();
        }

        public ICuboid CreateOuterTangentShape(ICylinder shape)
        {
            throw new NotImplementedException();
        }

        public ICuboid CreateTangentShape(ICylinder shape, SideCode sideCode)
        {
            throw new NotImplementedException();
        }

        public IRectangle CreateVerticalProjection(ICylinder cylinder)
        {
            throw new NotImplementedException();
        }

        public override ICircleFactory GetBaseFaceFactory()
        {
            throw new NotImplementedException();
        }

        public override ICuboidFactory GetTangentShapeFactory()
        {
            throw new NotImplementedException();
        }
    }
}

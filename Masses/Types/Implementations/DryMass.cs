using CsabaDu.FooVaria.Quantifiables.Enums;

namespace CsabaDu.FooVaria.Masses.Types.Implementations
{
    internal sealed class DryMass : Mass, IDryMass
    {
        internal DryMass(IDryMass other) : base(other)
        {
            DryBody = other.DryBody;
        }

        internal DryMass(IDryMassFactory factory, IWeight weight, IDryBody dryBody) : base(factory, weight, dryBody)
        {
            DryBody = dryBody;
        }

        internal DryMass(IDryMassFactory factory, IWeight weight, params IExtent[] shapeExtents) : base(factory, weight, shapeExtents)
        {
            DryBody = getDryBody();

            #region Local methods
            IDryBody getDryBody()
            {
                IShape? baseShape = (IShape?)GetBodyFactory().CreateBaseBaseShape(shapeExtents);

                if (baseShape is IDryBody dryBody) return dryBody;

                throw CountArgumentOutOfRangeException(shapeExtents.Length, nameof(shapeExtents));
            }
            #endregion
        }

        internal DryMass(IDryMassFactory factory, IWeight weight, IPlaneShape baseFace, IExtent height) : base(factory, weight, baseFace, height)
        {
            DryBody = GetBodyFactory().Create(baseFace, height);
        }

        public IDryBody DryBody { get; init; }

        public override IDryBody GetBody()
        {
            return DryBody;
        }

        public IDryMass GetDryMass(IWeight weight, IDryBody dryBody)
        {
            return GetFactory().Create(weight, dryBody);
        }

        public IDryMass GetDryMass(IWeight weight, IPlaneShape baseFace, IExtent height)
        {
            return GetFactory().Create(weight, baseFace, height);
        }

        public IDryMass GetDryMass(IWeight weight, params IExtent[] shapeExtents)
        {
            return GetFactory().Create(weight, shapeExtents);
        }

        public IDryMass GetNew(IDryMass other)
        {
            return GetFactory().CreateNew(other);
        }
        public override IBaseSpread? ExchangeTo(Enum measureUnit)
        {
            if (measureUnit is not ExtentUnit extentUnit) return base.ExchangeTo(measureUnit);

            IBaseSpread? baseSpread = DryBody.ExchangeTo(extentUnit);

            return baseSpread is IDryBody dryBody ?
                GetDryMass(Weight, dryBody)
                : null;
        }

        public override bool? FitsIn(IBaseSpread? baseSpread, LimitMode? limitMode)
        {
            bool? bodyFitsIn = GetBody().FitsIn(baseSpread, limitMode);

            if (baseSpread is not IMass mass) return bodyFitsIn;

            bool? weightFitsIn = Weight.FitsIn(mass.Weight, limitMode);

            if (bodyFitsIn == null || weightFitsIn == null) return null;

            if (bodyFitsIn != weightFitsIn) return false;

            return bodyFitsIn;
        }

        public override IDryBodyFactory GetBodyFactory()
        {
            return (IDryBodyFactory)base.GetBodyFactory();
        }

        public override IDryMassFactory GetFactory()
        {
            return (IDryMassFactory)Factory;
        }

        public override IEnumerable<MeasureUnitCode> GetMeasureUnitCodes()
        {
            return base.GetMeasureUnitCodes().Append(MeasureUnitCode.ExtentUnit);
        }

        public override IMass GetMass(IWeight weight, IBody body)
        {
            string paramName = nameof(body);

            if (NullChecked(body, paramName) is IDryBody dryBody) return GetDryMass(weight, dryBody);

            throw ArgumentTypeOutOfRangeException(paramName, body);
        }
    }
}

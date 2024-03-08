namespace CsabaDu.FooVaria.Masses.Types.Implementations;

internal sealed class DryMass : Mass, IDryMass
{
    #region Constructors
    internal DryMass(IDryMass other) : base(other)
    {
        DryBody = other.DryBody;
        Factory = other.Factory;
    }

    internal DryMass(IDryMassFactory factory, IWeight weight, IDryBody dryBody) : base(factory, weight)
    {
        DryBody = NullChecked(dryBody, nameof(dryBody));
        Factory = factory;
    }

    internal DryMass(IDryMassFactory factory, IWeight weight, params IExtent[] shapeExtents) : base(factory, weight)
    {
        DryBody = getDryBody();
        Factory = factory;

        #region Local methods
        IDryBody getDryBody()
        {
            IShape? shape = GetBodyFactory().CreateShape(shapeExtents);

            if (shape is IDryBody dryBody) return dryBody;

            throw CountArgumentOutOfRangeException(shapeExtents.Length, nameof(shapeExtents));
        }
        #endregion
    }

    internal DryMass(IDryMassFactory factory, IWeight weight, IPlaneShape baseFace, IExtent height) : base(factory, weight)
    {
        DryBody = GetBodyFactory().Create(baseFace, height);
        Factory = factory;
    }
    #endregion

    #region Properties
    public IDryBody DryBody { get; init; }
    public IDryMassFactory Factory { get; init; }
    #endregion

    #region Public methods
    public IDryMass GetDryMass(IWeight weight, IDryBody dryBody)
    {
        return Factory.Create(weight, dryBody);
    }

    public IDryMass GetDryMass(IWeight weight, IPlaneShape baseFace, IExtent height)
    {
        return Factory.Create(weight, baseFace, height);
    }

    public IDryMass GetDryMass(IWeight weight, params IExtent[] shapeExtents)
    {
        return Factory.Create(weight, shapeExtents);
    }

    public IDryMass GetNew(IDryMass other)
    {
        return Factory.CreateNew(other);
    }

    #region Override methods
    public override bool Equals(IMass? other)
    {
        return base.Equals(other)
            && other.GetBody() is IDryBody dryBody
            && DryBody.Equals(dryBody);
    }

    public override IMass? ExchangeTo(Enum? context)
    {
        if (context is not ExtentUnit extentUnit) return base.ExchangeTo(context);

        IQuantifiable? exchanged = (DryBody as ISimpleShape)!.ExchangeTo(extentUnit);

        return exchanged is IDryBody dyBody ?
            GetDryMass(Weight, dyBody)
            : null;
    }

    public override bool? FitsIn(IMass? other, LimitMode? limitMode)
    {
        bool? baseFitsIn = base.FitsIn(other, limitMode);

        if (other is not IDryMass dryMass) return baseFitsIn;

        bool? dryBodyFitsIn = DryBody.FitsIn(dryMass.DryBody, limitMode);

        return BothFitIn(baseFitsIn, dryBodyFitsIn);
    }

    public override bool? FitsIn(ILimiter? limiter)
    {
        if (limiter is not IDryBody dryBody) return base.FitsIn(limiter);

        return DryBody.FitsIn(dryBody, limiter.GetLimitMode());
    }

    public override IDryBodyFactory GetBodyFactory()
    {
        return (IDryBodyFactory)Factory.GetBodyFactory();
    }

    public override IDryMassFactory GetFactory()
    {
        return Factory;
    }

    public override IEnumerable<MeasureUnitCode> GetMeasureUnitCodes()
    {
        return base.GetMeasureUnitCodes().Append(MeasureUnitCode.ExtentUnit);
    }

    public override IDryBody GetBody()
    {
        return DryBody;
    }
    #endregion
    #endregion
}

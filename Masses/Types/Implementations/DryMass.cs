﻿namespace CsabaDu.FooVaria.Masses.Types.Implementations;

internal sealed class DryMass : Mass, IDryMass
{
    #region Constructors
    internal DryMass(IDryMass other) : base(other)
    {
        Body = other.Body;
    }

    internal DryMass(IDryMassFactory factory, IWeight weight, IDryBody dryBody) : base(factory, weight, dryBody)
    {
        Body = dryBody;
    }

    internal DryMass(IDryMassFactory factory, IWeight weight, params IExtent[] shapeExtents) : base(factory, weight, shapeExtents)
    {
        Body = getDryBody();

        #region Local methods
        IDryBody getDryBody()
        {
            IBaseShape? baseShape = GetBodyFactory().CreateBaseShape(shapeExtents);

            if (baseShape is IDryBody dryBody) return dryBody;

            throw CountArgumentOutOfRangeException(shapeExtents.Length, nameof(shapeExtents));
        }
        #endregion
    }

    internal DryMass(IDryMassFactory factory, IWeight weight, IPlaneShape baseFace, IExtent height) : base(factory, weight, baseFace, height)
    {
        Body = GetBodyFactory().Create(baseFace, height);
    }
    #endregion

    #region Properties
    public override IBody Body { get; init; }
    #endregion

    #region Public methods
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

    #region Override methods
    public override bool Equals(IMass? other)
    {
        return base.Equals(other)
            && other.Body is IDryBody dryBody
            && (Body as IDryBody)!.Equals(dryBody);
    }

    public override IMass? ExchangeTo(Enum? context)
    {
        if (context is not ExtentUnit extentUnit) return base.ExchangeTo(context);

        IBaseSpread? exchanged = (Body as IShape)!.ExchangeTo(extentUnit);

        return exchanged is IDryBody dyBody ?
            GetDryMass(Weight, dyBody)
            : null;
    }

    public override bool? FitsIn(IMass? other, LimitMode? limitMode)
    {
        bool? baseFitsIn = base.FitsIn(other, limitMode);

        if (other is not IDryMass dryMass) return baseFitsIn;

        bool? dryBodyFitsIn = GetDryBody().FitsIn(dryMass.GetDryBody(), limitMode);

        return BothFitIn(baseFitsIn, dryBodyFitsIn);
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

    public override IDryMass GetMass(IWeight weight, IBody body)
    {
        string paramName = nameof(body);

        if (NullChecked(body, paramName) is IDryBody dryBody) return GetDryMass(weight, dryBody);

        throw ArgumentTypeOutOfRangeException(paramName, body);
    }

    public IDryBody GetDryBody()
    {
        return (IDryBody)Body;
    }
    #endregion
    #endregion
}

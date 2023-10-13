﻿using CsabaDu.FooVaria.Common.Factories;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace CsabaDu.FooVaria.Common.Types.Implementations;

public abstract class CommonBase : ICommonBase
{
    #region Constructors
    protected CommonBase(IFactory factory)
    {
        Factory = NullChecked(factory, nameof(factory));
    }

    protected CommonBase(IFactory factory, ICommonBase commonBase)
    {
        Factory = NullChecked(factory, nameof(factory));

        _ = NullChecked(commonBase, nameof(commonBase));
    }

    protected CommonBase(ICommonBase other)
    {
        Factory = NullChecked(other, nameof(other)).Factory;
    }
    #endregion

    #region Properties
    public IFactory Factory { get; init; }
    #endregion

    #region Public methods
    #region Virtual methods
    public virtual IFactory GetFactory()
    {
        return Factory;
    }

    public virtual void Validate(IFooVariaObject? fooVariaObject)
    {
        validate(this, fooVariaObject);

        #region Local methods
        void validate<T>(T commonBase, IFooVariaObject? fooVariaObject) where T : class, ICommonBase
        {
            _ = NullChecked(fooVariaObject, nameof(fooVariaObject));

            if (fooVariaObject is IFactory factory)
            {
                ValidateFactory(commonBase, factory);
            }
            else if (fooVariaObject is ICommonBase other)
            {
                _ = GetValidCommonBase(commonBase, other);
            }
            else
            {
                throw new InvalidOperationException(null!);
            }
        }
        #endregion
    }
    #endregion
    #endregion

    #region Protected methods
    #region Static methods
    protected static T GetValidCommonBase<T>(T commonBase, ICommonBase? other) where T : class, ICommonBase
    {
        string name = nameof(other);
        Type type = typeof(T);
        Type commonBaseType = commonBase.GetType();
        Type otherType = NullChecked(other, name).GetType();

        var x = otherType.IsSubclassOf(type);
        if (x) return (T)other!;
        //if (NullChecked(other, name).GetType() == commonBase.GetType()) return (T)other!;

        throw ArgumentTypeOutOfRangeException(name, other!);
    }

    private static void ValidateFactory<T>(T commonBase, IFactory? factory) where T : class, ICommonBase
    {
        string name = nameof(factory);

        if (NullChecked(factory, name).GetType() == commonBase.Factory.GetType()) return;

        throw ArgumentTypeOutOfRangeException(name, factory!);
    }
    #endregion
    #endregion
}

﻿//using System.Diagnostics.CodeAnalysis;

//namespace CsabaDu.FooVaria.Common.Behaviors;

//public interface IExchange<TSelf, in TContext> : IProportional<TSelf>, IExchangeable<TContext>
//    where TSelf : class, IMeasurable, IQuantifiable
//    where TContext : notnull
//{
//    TSelf? ExchangeTo(TContext context);

//    public bool TryExchangeTo(TContext context, [NotNullWhen(true)] out TSelf? exchanged)
//    {
//        exchanged = ExchangeTo(context);

//        return exchanged != null;
//    }
//}
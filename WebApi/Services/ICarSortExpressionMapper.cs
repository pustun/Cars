using System;
using System.Linq.Expressions;
using Model;

namespace WebApi.Services
{
    public interface ICarSortExpressionMapper
    {
        Expression<Func<Car, object>> Map(string sort);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace RecipeMs.CrossCutting.Common.Query
{
    public class ExpressionBuilder
    {
        private static readonly MethodInfo ContainsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
        private static readonly MethodInfo StartsWithMethod = typeof(string).GetMethod("StartsWith", new[] { typeof(string) });
        private static readonly MethodInfo EndsWithMethod = typeof(string).GetMethod("EndsWith", new[] { typeof(string) });

        public static Expression<Func<T, bool>> GetExpression<T>(ICollection<QueryFilter> filterscolleCollection)
        {
            IList<QueryFilter> filters = (IList<QueryFilter>)filterscolleCollection;
            Expression exp = null;
            ParameterExpression param = Expression.Parameter(typeof(T), "parm");

            if (filters.Count == 0)
                return null;

            if (filters.Count != 1)
            {
                if (filters.Count == 2)
                    exp = GetExpression<T>(param, filters[0], filters[1]);
                else
                {
                    while (filters.Count > 0)
                    {
                        var f1 = filters[0];
                        var f2 = filters[1];

                        exp = exp == null ? GetExpression<T>(param, filters[0], filters[1]) : Expression.AndAlso(exp, GetExpression<T>(param, filters[0], filters[1]));

                        filters.Remove(f1);
                        filters.Remove(f2);

                        if (filters.Count != 1) continue;

                        exp = Expression.AndAlso(exp, GetExpression<T>(param, filters[0]));

                        filters.RemoveAt(0);
                    }
                }
            }
            else
                exp = GetExpression<T>(param, filters[0]);

            return Expression.Lambda<Func<T, bool>>(exp, param);
        }

        private static Expression GetExpression<T>(ParameterExpression param, QueryFilter queryFilter)
        {
            MemberExpression member = Expression.Property(param, queryFilter.PropertyName);
            ConstantExpression constant = GetConstant(member.Type, queryFilter.Value);

            switch (queryFilter.Operator)
            {
                case Operator.Equals:
                    return Expression.Equal(member, constant);

                case Operator.Contains:
                    return Expression.Call(member, ContainsMethod, constant);

                case Operator.GreaterThan:
                    return Expression.GreaterThan(member, constant);

                case Operator.GreaterThanOrEqual:
                    return Expression.GreaterThanOrEqual(member, constant);

                case Operator.LessThan:
                    return Expression.LessThan(member, constant);

                case Operator.LessThanOrEqualTo:
                    return Expression.LessThanOrEqual(member, constant);

                case Operator.StartsWith:
                    return Expression.Call(member, StartsWithMethod, constant);

                case Operator.EndsWith:
                    return Expression.Call(member, EndsWithMethod, constant);
            }

            return null;
        }

        private static ConstantExpression GetConstant(Type type, string value)
        {
            ConstantExpression constant = null;
            if (type == typeof(int))
            {
                int num;
                int.TryParse(value, out num);
                constant = Expression.Constant(num);
            }
            else if(type == typeof(string))
            {
                constant = Expression.Constant(value);
            }
            else if (type == typeof(DateTime))
            {
                DateTime date;
                DateTime.TryParse(value, out date);
                constant = Expression.Constant(date);
            }
            else if (type == typeof(bool))
            {
                //TODO: Verify what happens when the try is false, because when fails is false but doesn't mean that false is the wanted value.
                bool flag;
                if (bool.TryParse(value, out flag))
                {
                    flag = true;
                }
                constant = Expression.Constant(flag);
            }
            else if (type == typeof(decimal))
            {
                decimal number;
                decimal.TryParse(value, out number);
                constant = Expression.Constant(number);
            }
            return constant;
        }

        private static BinaryExpression GetExpression<T>(ParameterExpression param, QueryFilter filter1, QueryFilter filter2)
        {
            Expression result1 = GetExpression<T>(param, filter1);
            Expression result2 = GetExpression<T>(param, filter2);
            return Expression.AndAlso(result1, result2);
        }
    }
}

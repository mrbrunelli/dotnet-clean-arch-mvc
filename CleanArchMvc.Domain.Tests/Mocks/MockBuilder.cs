using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Domain.Tests.Mocks
{
    public abstract class MockBuilder<TEntity, TBuilder> where TBuilder : MockBuilder<TEntity, TBuilder>, new()
    {
        protected Faker<TBuilder> _faker;

        public MockBuilder()
        {
            _faker = new Faker<TBuilder>();
        }

        public TEntity Build()
        {
            var mock = _faker.Generate();
            return CreateInstance(mock);
        }

        protected abstract TEntity CreateInstance(TBuilder mock);

        public TBuilder WithProperty<TProperty>(Expression<Func<TBuilder, TProperty>> propertyExpression, TProperty value)
        {
            _faker.RuleFor(propertyExpression, f => value);
            return this as TBuilder;
        }
    }
}

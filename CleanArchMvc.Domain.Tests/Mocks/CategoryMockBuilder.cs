using Bogus;
using CleanArchMvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Domain.Tests.Mocks
{
    public sealed class CategoryMockBuilder : MockBuilder<Category, CategoryMockBuilder>
    {
        public int Id {  get; set; }
        public string Name { get; set; } = string.Empty;

        public CategoryMockBuilder()
        {
            _faker
                .RuleFor(c => c.Id, f => f.IndexFaker)
                .RuleFor(c => c.Name, f => f.Commerce.Categories(1).First());
        }

        protected override Category CreateInstance(CategoryMockBuilder mock)
        {
            return new Category(mock.Id, mock.Name);
        }
    }
}

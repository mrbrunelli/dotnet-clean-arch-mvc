using CleanArchMvc.Domain.Validation;

namespace CleanArchMvc.Domain.Entities
{
    public sealed class Category : Entity
    {
        public string Name { get; private set; }

        public ICollection<Product> Products { get; private set; }

        public Category(string name)
        {
            ValidateDomain(name);
            Name = name;
        }

        public Category(int id, string name)
        {
            DomainExceptionValidation.When(id < 0, "Invalid Id value");
            ValidateDomain(name);

            Id = id;
            Name = name;
        }

        public void Update(string name)
        {
            ValidateDomain(name);
            Name = name;
        }

        private static void ValidateDomain(string name)
        {
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(name), "Invalid name. Name is required");
            DomainExceptionValidation.When(name.Length < 3, "Name too short, minimum 3 characters");
        }

    }
}

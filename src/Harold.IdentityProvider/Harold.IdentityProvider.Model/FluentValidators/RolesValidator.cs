using FluentValidation;
using Harold.IdentityProvider.Model.Models;

namespace Harold.IdentityProvider.Model.FluentValidators
{
    public class RolesValidator : AbstractValidator<Roles>
    {
        public RolesValidator()
        {
            RuleSet("create", () =>
            {
                RuleFor(x => x.RolId).Empty();
            });

            RuleSet("update", () =>
            {
                RuleFor(x => x.RolId).NotEmpty();
            });

            RuleFor(x => x.Name).NotEmpty().MaximumLength(20);            
        }
    }
}

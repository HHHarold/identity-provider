using FluentValidation;
using Harold.IdentityProvider.Model.Requests;

namespace Harold.IdentityProvider.Model.FluentValidators
{
    public class UsersRequestValidator : AbstractValidator<UsersRequest>
    {
        public UsersRequestValidator()
        {
            RuleSet("create", () =>
            {
                RuleFor(x => x.UserId).Empty();
                RuleFor(x => x.Password).NotEmpty();
            });

            RuleSet("update", () =>
            {
                RuleFor(x => x.UserId).NotEmpty();
            });

            RuleSet("authenticate", () =>
            {
                RuleFor(x => x.Password).NotEmpty();
                RuleFor(x => x.Username).NotEmpty();
            });

            RuleFor(x => x.Username).NotEmpty().MaximumLength(20);
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(20);
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(20);            
            RuleFor(x => x.RoleId).NotEmpty();
        }
    }
}

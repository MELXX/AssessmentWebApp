using AssessmentWebApp.Data.Models;
using FluentValidation;
using System;

namespace AssessmentWebApp.Helpers
{
    public class UserValidator: AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.Identifier).NotNull();
            RuleFor(x => x.Username).NotNull();
            RuleFor(x => x.LastName).NotNull();
            RuleFor(x => x.Date).NotNull();
            RuleFor(x => x.Values).NotNull();
            RuleFor(x => x.LoginEmail).EmailAddress().When(x=> x.LoginEmail != String.Empty);
        }
    }
}

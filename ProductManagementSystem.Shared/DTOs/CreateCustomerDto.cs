using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTOs
{
    public class CreateCustomerDto
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }

    public class CreateCustomerDtoValidator : AbstractValidator<CreateCustomerDto>
    {
        public CreateCustomerDtoValidator()
        {
            RuleFor(dto => dto.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(dto => dto.Phone).NotEmpty().WithMessage("Phone number is required.");
            RuleFor(dto => dto.Email).NotEmpty().WithMessage("Email address is required.");
        }
    }
}

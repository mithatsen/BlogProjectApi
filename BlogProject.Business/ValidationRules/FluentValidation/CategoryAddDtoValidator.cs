using BlogProject.DTO.DTOs.CategoryDtos;
using FluentValidation;

using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject.Business.ValidationRules.FluentValidation
{
    public class CategoryAddDtoValidator : AbstractValidator<CategoryAddDto>
    {
        public CategoryAddDtoValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Ad Alanı Boş Geçilemez"); // bu noktadan sonra startupa git addfluent validtrion ekle services.addcontroller a
        }

    }
}

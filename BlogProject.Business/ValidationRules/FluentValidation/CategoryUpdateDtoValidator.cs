using BlogProject.DTO.DTOs.CategoryDtos;
using FluentValidation;

using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject.Business.ValidationRules.FluentValidation
{
    public class CategoryUpdateDtoValidator : AbstractValidator<CategoryUpdateDto>
    {
        public CategoryUpdateDtoValidator()
        {
            RuleFor(p => p.Id).InclusiveBetween(0, int.MaxValue).WithMessage("Id alanı boş geçilemez");
            RuleFor(p => p.Name).NotEmpty().WithMessage("Ad alanı boş bırakılamaz");

        }
    }
}

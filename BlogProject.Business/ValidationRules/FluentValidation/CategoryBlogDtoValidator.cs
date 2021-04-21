using BlogProject.DTO.DTOs.CategoryBlogDtos;
using FluentValidation;

using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject.Business.ValidationRules.FluentValidation
{
    public class CategoryBlogDtoValidator: AbstractValidator<CategoryBlogDto>
    {
        public CategoryBlogDtoValidator()
        {
            RuleFor(p => p.CategoryId).InclusiveBetween(0, int.MaxValue).WithMessage("CategoryId alanı boş geçilemez");
            RuleFor(p => p.BlogId).InclusiveBetween(0, int.MaxValue).WithMessage("BlogId alanı boş geçilemez");
        }

    }
}

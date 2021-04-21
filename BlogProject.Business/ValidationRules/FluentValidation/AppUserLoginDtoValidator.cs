using BlogProject.DTO.DTOs.AppUserDtos;
using FluentValidation;

using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject.Business.ValidationRules.FluentValidation
{
    public class AppUserLoginDtoValidator : AbstractValidator<AppUserLoginDto>
    {
        public AppUserLoginDtoValidator()
        {
            RuleFor(p => p.UserName).NotEmpty().WithMessage("Kullanıcı Adı alanı boş bırakılamaz");
            RuleFor(p => p.Password).NotEmpty().WithMessage("Şifre alanı boş bırakılamaz");
        }

    }
}

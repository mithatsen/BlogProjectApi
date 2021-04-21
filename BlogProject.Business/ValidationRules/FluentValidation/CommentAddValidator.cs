using BlogProject.DTO.DTOs.CommentDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject.Business.ValidationRules.FluentValidation
{
    public class CommentAddValidator : AbstractValidator<CommentAddDto>
    {
        public CommentAddValidator()
        {
            RuleFor(p => p.AuthorName).NotEmpty().WithMessage("Ad Alanı Boş Geçilemez"); // bu noktadan sonra startupa git addfluent validtrion ekle services.addcontroller a
            RuleFor(p => p.AuthorEmail).NotEmpty().WithMessage("Email Alanı Boş Geçilemez"); // bu noktadan sonra startupa git addfluent validtrion ekle services.addcontroller a
            RuleFor(p => p.Description).NotEmpty().WithMessage("Açıklama Alanı Boş Geçilemez"); // bu noktadan sonra startupa git addfluent validtrion ekle services.addcontroller a
           
        }

    }
}

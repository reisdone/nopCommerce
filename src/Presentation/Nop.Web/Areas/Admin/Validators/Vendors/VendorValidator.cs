﻿using FluentValidation;
using Nop.Web.Areas.Admin.Models.Vendors;
using Nop.Core.Domain.Vendors;
using Nop.Data;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;

namespace Nop.Web.Areas.Admin.Validators.Vendors
{
    public partial class VendorValidator : BaseNopValidator<VendorModel>
    {
        public VendorValidator(ILocalizationService localizationService, IDbContext dbContext)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(localizationService.GetResource("Admin.Vendors.Fields.Name.Required"));

            RuleFor(x => x.Email).NotEmpty().WithMessage(localizationService.GetResource("Admin.Vendors.Fields.Email.Required"));
            RuleFor(x => x.Email).EmailAddress().WithMessage(localizationService.GetResource("Admin.Common.WrongEmail"));
            RuleFor(x => x.PageSizeOptions).Must(ValidatorUtilities.PageSizeOptionsValidator).WithMessage(localizationService.GetResource("Admin.Vendors.Fields.PageSizeOptions.ShouldHaveUniqueItems"));
            RuleFor(x => x.PageSize).Must((x, context) =>
            {
                if (!x.AllowCustomersToSelectPageSize && x.PageSize <= 0)
                    return false;

                return true;
            }).WithMessage(localizationService.GetResource("Admin.Vendors.Fields.PageSize.Positive"));
            RuleFor(x => x.SeName).MaximumLength(400).WithMessage(string.Format(localizationService.GetResource("Admin.SEO.SeName.MaxLengthValidation"), 400));

            SetDatabaseValidationRules<Vendor>(dbContext);
        }
    }
}
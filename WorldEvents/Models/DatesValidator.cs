using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WorldEvents.Models
{
    public sealed class DatesValidator : CompareAttribute
    {
        readonly DateTime _startDate;
        public DatesValidator(DateTime startDate) : base(startDate.ToString())
        {
            _startDate = startDate;//DateTime.Parse(startDate);
        }

        protected override ValidationResult IsValid(object currentDate, ValidationContext validationContext)
        {
            if (currentDate == null)
                return ValidationResult.Success; //End date can be undefined
            DateTime endDate = DateTime.Parse(currentDate.ToString());
            return endDate >= _startDate ? ValidationResult.Success : null;//base.IsValid(value, validationContext);

        }
        //public DatesValidator()
        //{
        //    RuleFor(m => m.StartDate)
        //        .NotEmpty()
        //        .WithMessage("Start Date is Required");

        //    RuleFor(m => m.EndDate)
        //        .NotEmpty().WithMessage("End date is required")
        //        .GreaterThan(m => m.StartDate.Value)
        //                        .WithMessage("End date must after Start date")
        //        .When(m => m.StartDate.HasValue);
        //}
    }
}

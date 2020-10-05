using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServerDemoApp.ViewModels
{
    public class DoctorViewModel : IValidatableObject
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Hospital { get; set; }
        [Required]
        [Phone]
        public string TelNo { get; set; }
        [Required]
        public string Speciality { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (String.IsNullOrWhiteSpace(Speciality))
            {
                yield return new ValidationResult("Speciality is required", new[] { "Speciality" });
            }
        }
    }
}

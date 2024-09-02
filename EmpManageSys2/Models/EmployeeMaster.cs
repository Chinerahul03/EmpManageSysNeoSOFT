using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmpManageSys2.Models;

public partial class EmployeeMaster
{
    public int RowId { get; set; }


    public string EmployeeCode { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "First Name is required.")]
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Country is required.")]
    public int? ContryId { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "State is required.")]
    public int? StateId { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "City is required.")]
    public int? CityId { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Enter valid email Id.")]
    public string EmailAddress { get; set; } = null!;

    [Required(ErrorMessage = "Mobile number is required.")]
    [RegularExpression(@"^[789]\d{9}$", ErrorMessage = "Enter valid mobile number.")]
    public string MobileNumber { get; set; } = null!;

    [Required(ErrorMessage = "PAN card number is required.")]
    [RegularExpression(@"^[a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}$", ErrorMessage = "Invalid PAN card format.")]
    public string PanNumber { get; set; } = null!;

    [Required(ErrorMessage = "Passport number is required.")]
    [RegularExpression(@"^[a-zA-Z][1-9]\d\s?\d{4}[1-9]$", ErrorMessage = "Invalid passport number format.")]
    public string PassportNumber { get; set; } = null!;

    public string? ProfileImage { get; set; }

    [NotMapped]
    public IFormFile? image { get; set; }

    public byte? Gender { get; set; }

    public bool IsActive { get; set; }

    public DateOnly DateOfBirth { get; set; }

    public DateOnly DateOfJoinee { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedDate { get; set; }

    public virtual City? City { get; set; }

    public virtual Country? Contry { get; set; }

    public virtual State? State { get; set; }
}

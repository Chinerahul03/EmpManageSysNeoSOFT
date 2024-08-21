using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmpManageSys2.Models;

public partial class EmployeeMaster
{
    public int RowId { get; set; }

    public string EmployeeCode { get; set; } 

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public int? ContryId { get; set; }

    public int? StateId { get; set; }

    public int? CityId { get; set; }

    public string EmailAddress { get; set; } = null!;

    public string MobileNumber { get; set; } = null!;

    public string PanNumber { get; set; } = null!;

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

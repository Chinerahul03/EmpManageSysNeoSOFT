using System;
using System.Collections.Generic;

namespace EmpManageSys2.Models;

public partial class State
{
    public int RowId { get; set; }

    public int? CountryId { get; set; }

    public string? StateName { get; set; }

    public virtual ICollection<City> Cities { get; set; } = new List<City>();

    public virtual Country? Country { get; set; }

    public virtual ICollection<EmployeeMaster> EmployeeMasters { get; set; } = new List<EmployeeMaster>();
}

using System;
using System.Collections.Generic;

namespace EmpManageSys2.Models;

public partial class Country
{
    public int RowId { get; set; }

    public string? CountryName { get; set; }

    public virtual ICollection<EmployeeMaster> EmployeeMasters { get; set; } = new List<EmployeeMaster>();

    public virtual ICollection<State> States { get; set; } = new List<State>();
}

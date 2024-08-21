using System;
using System.Collections.Generic;

namespace EmpManageSys2.Models;

public partial class City
{
    public int RowId { get; set; }

    public int? StateId { get; set; }

    public string? CityName { get; set; }

    public virtual ICollection<EmployeeMaster> EmployeeMasters { get; set; } = new List<EmployeeMaster>();

    public virtual State? State { get; set; }
}

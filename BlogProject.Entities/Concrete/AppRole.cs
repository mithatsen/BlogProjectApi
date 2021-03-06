using BlogProject.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject.Entities.Concrete
{
    public class AppRole : ITable
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<AppUserRole> AppUserRoles { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.PwaEntities.Projects
{
    /// <summary>
    /// پروژه
    /// </summary>
    public class Project
    {
        public Guid ProjectUID { get; set; }
        public string ProjectName { get; set; }
    }
}

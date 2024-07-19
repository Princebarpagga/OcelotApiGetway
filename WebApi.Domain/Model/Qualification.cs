using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Domain.Model
{
    public class Qualification
    {
        [Key]
        public int QualifId { get; set; }

        public string QualificationName { get; set; } = null!;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scadaTestTask.Models
{
    public class AuthorModel
    {
        public int Id { get; set; }
        public int IdBook { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

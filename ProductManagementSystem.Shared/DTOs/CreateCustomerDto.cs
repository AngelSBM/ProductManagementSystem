using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTOs
{
    public class CreateCustomerDto
    {
        public int Id { get; set; }
        private string _name;
        public string Name {
            get => _name;
            set 
            {
                if(value == null)
                {
                    throw new ArgumentNullException(nameof(value), "Argument cannot be null");
                }
                _name = value;
            }
        }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool Active {  get; set; }
    }
}

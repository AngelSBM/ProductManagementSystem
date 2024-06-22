using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        private string _name;
        private string _phone;
        private string _email;
        public string Name
        {
            get => _name;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value), "Argument cannot be null");
                }
                _name = value;
            }
        }


        public string Phone
        {
            get => _phone;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value), "Phone number cannot be null.");
                }
                if (!PhoneNumberRegex.IsMatch(value))
                {
                    throw new ArgumentException("Phone number must be exactly 10 digits with no special characters.", nameof(value));
                }
                _phone = value;
            }
        }


        public string Email
        {
            get => _email;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value), "Email cannot be null.");
                }
                if (!EmailRegex.IsMatch(value))
                {
                    throw new ArgumentException("Email is not valid.", nameof(value));
                }
                _email = value;
            }
        }
        public bool Active { get; set; }
        public List<CustomerItem> CustomerItems { get; set; } = [];

        private static readonly Regex PhoneNumberRegex = new Regex(@"^\d{10}$", RegexOptions.Compiled);
        private static readonly Regex EmailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
    }
}

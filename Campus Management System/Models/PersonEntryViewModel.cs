using Campus_Management_System.Models;

namespace Campus_Management_System.Models
{
    public class PersonEntryViewModel
    {
        public Person Person { get; set; } = new Person();
        public List<Person>? PersonsList { get; set; } = new List<Person>();
    }
}
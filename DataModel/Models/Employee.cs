using Dapper.Contrib.Extensions;

namespace DataModel.Models
{
    [Table("Employee")]
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public byte[] Photo { get; set; }
        public int ParentId { get; set; }
        [Write(false)]
        public Employee ParentEmployee { get; set; }
    }
}
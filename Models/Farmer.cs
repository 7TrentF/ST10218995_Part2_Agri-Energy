using System.ComponentModel.DataAnnotations;

namespace AgriEnergySolution.Models
{
    public class Farmer
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }    
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<FarmerProducts> FarmerProducts { get; set; } = new List<FarmerProducts>();

        
    }

    public class Products
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCategory { get; set; }
        public DateTime ProductionDate { get; set; }


       public ICollection<FarmerProducts> FarmerProducts { get; set; } = new List<FarmerProducts>();
    }

    public class FarmerProducts
    {

        [Key]
        public int FarmerId { get; set; }
        public Farmer Farmer { get; set; }
        public int ProductId { get; set; }
        public Products Products { get; set; }
       

    }

    public class Employee
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

}

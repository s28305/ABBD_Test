using System.ComponentModel.DataAnnotations;

namespace Test.Models;

public class Cpu
{
    [Required]
    [Length(1, 100)]
    public string Name { get; set; }
    
    [Required]
    [Range(double.Epsilon, double.MaxValue)]
    public double Frequency{ get; set; }
    
    [Required]
    [Range(1, int.MaxValue)]
    public int Cores { get; set; }
}
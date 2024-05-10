using System.ComponentModel.DataAnnotations;

namespace Test.Models;

public class Computer
{
    [Required]
    [Range(1, int.MaxValue)]
    public int VideocardId { get; set; }
    
    [Required]
    [Range(1, int.MaxValue)]
    public int CpuId { get; set; }
    
    [Required]
    [Length(1, 200)]
    public string Name { get; set; }
}
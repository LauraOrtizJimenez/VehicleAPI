using System.ComponentModel.DataAnnotations;

namespace VehicleAPI.Models;

public class Vehicle
{
    public Guid Id { get; init; }=Guid.NewGuid();
    
    [Required, StringLength(50)]
    public string Make { get; init; } = null!;
    
    [Required, StringLength(50)]
    public string Model { get; init; } = null!;
    
    [Range(1900, 2100)]
    public int Year { get; init; }
}
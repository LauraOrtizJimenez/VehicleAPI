using Microsoft.AspNetCore.Mvc;
using VehicleAPI.Models;
namespace VehicleAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class VehiclesController: ControllerBase
{
    private static readonly List<Vehicle> Data = new List<Vehicle>();
    
    //GET: api/vehicles?make=Ford&year=2020
    [HttpGet]
    public ActionResult<IEnumerable<Vehicle>> Get(string? make, int? year)
    {
        var result = Data.AsEnumerable();
        if (!string.IsNullOrWhiteSpace(make))
        {

            result = result.Where(v =>
                v.Make.Contains(make, StringComparison.OrdinalIgnoreCase));
        }

        if (year > 0)
        {
            result=result.Where(v=> v.Year==year);
        }

        return Ok(result.ToList());
    }
    //GET: api/vehicles/{id}
    [HttpGet("{id:guid}")]
    public ActionResult<Vehicle> GetById(Guid id)
    {
        var vehicle = Data.FirstOrDefault(v=>v.Id==id);
        if (vehicle == null) return NotFound();
        return Ok(vehicle);
    }
    //POST api/vehicles
    [HttpPost]
    public ActionResult<Vehicle> Create(Vehicle vehicle)
    {
        var newVehicle = new Vehicle
        {
            Id = Guid.NewGuid(),
            Make = vehicle.Make,
            Model = vehicle.Model,
            Year = vehicle.Year
        };
        Data.Add(newVehicle);
        return CreatedAtAction(nameof(GetById), new { id = vehicle.Id }, vehicle);
    }
    //PUT api/vehicles/{id}
    [HttpPut("{id:guid}")]
    public IActionResult Replace(Guid id, Vehicle vehicle)
    {
        var index = Data.FindIndex(v => v.Id == id);
        if (index == -1) return NotFound();

        var newVehicle = new Vehicle
        {
            Id = id,                  // conservamos el mismo Id
            Make = vehicle.Make,
            Model = vehicle.Model,
            Year = vehicle.Year
        };

        Data[index] = newVehicle;     // reemplazamos el objeto en la lista
        return NoContent();
    }

    //DELETE api/vehicles/{id}
    [HttpDelete("{id}")]
    public ActionResult Delete(Guid id)
    {
        var existing = Data.FirstOrDefault(v=>v.Id==id);
        if (existing == null) return NotFound();
        Data.Remove(existing);
        return NoContent();
    }
}
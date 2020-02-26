using System;
using System.Collections.Generic;
using System.Linq;

namespace Garden
{
  public class Gardener
  {

    public int Menu()
    {
      Console.ForegroundColor = ConsoleColor.DarkMagenta;
      Console.WriteLine($"Enter your selection from the choices below:");
      Console.WriteLine($"(1) View all plants. (2) Add a plant. (3) Remove a plant.");
      Console.WriteLine($"(4) Water a plant. (5) View plants that have not been watered today.");
      Console.WriteLine($"(6) Display location summary. (7) Exit.");
      var menuInt = int.Parse(Console.ReadLine());
      Console.ForegroundColor = ConsoleColor.White;
      return menuInt;
    }

    public static void AddPlant()
    {
      var db = new GardenContext();
      Console.WriteLine($"What plant would you like to add? Please enter species.");
      var species = Console.ReadLine();
      Console.WriteLine($"Where is the {species} planted? Please enter a location.");
      var locationPlanted = Console.ReadLine();
      Console.WriteLine($"How much sunlight does this {species} need? Please enter a number.");
      var lightNeeded = double.Parse(Console.ReadLine());
      Console.WriteLine($"How much water does this {species} need? Please enter the recommended number of daily waterings.");
      var waterNeeded = double.Parse(Console.ReadLine());

      var plant = new Plant
      {
        Species = species,
        LocatedPlanted = locationPlanted,
        PlantedDate = DateTime.Now,
        LastWateredDate = DateTime.Now,
        LightNeeded = lightNeeded,
        WaterNeeded = waterNeeded
      };
      db.Plants.Add(plant);
      db.SaveChanges();
    }

    public static void ViewPlants()
    {
      var db = new GardenContext();
      foreach (var p in db.Plants)
      {
        var displayPlants = db.Plants.OrderBy(p => p.LocatedPlanted);
        Console.WriteLine($"{p.Id} {p.Species} is planted in {p.LocatedPlanted}.");
      }
    }
    public static void RemovePlant()
    {
      var removing = true;
      while (removing)
      {
        var db = new GardenContext();
        ViewPlants();
        Console.WriteLine($"Please enter the number of the plant you would like to remove.");
        var deletePlantId = int.Parse(Console.ReadLine());
        var plantToRemove = db.Plants.FirstOrDefault(p => p.Id == deletePlantId);
        if (plantToRemove == null)
        {
          Console.WriteLine($"That is not a valid selection. Please try again.");
        }
        else
        {
          db.Plants.Remove(plantToRemove);
          removing = false;
        }
      }

    }

  }
}

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
      var input = Console.ReadLine();
      var menuInt = TryInt(input);
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
      Console.ForegroundColor = ConsoleColor.Red;
      var removing = true;
      while (removing)
      {
        var db = new GardenContext();
        ViewPlants();
        Console.WriteLine($"Please enter the number of the plant you would like to remove.");
        var input = Console.ReadLine();
        var deletePlantId = TryInt(input);
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
      Console.ForegroundColor = ConsoleColor.White;
    }
    public static void WaterAPlant()
    {
      Console.ForegroundColor = ConsoleColor.Blue;
      var watering = true;
      while (watering)
      {
        var db = new GardenContext();
        ViewPlants();
        Console.WriteLine($"Please enter the number of the plant you would like to water.");
        var input = Console.ReadLine();
        var waterPlantId = TryInt(input);
        var plantToWater = db.Plants.FirstOrDefault(p => p.Id == waterPlantId);
        if (plantToWater == null)
        {
          Console.WriteLine($"That is not a valid selection. Please try again.");
        }
        else
        {
          plantToWater.LastWateredDate = DateTime.Now;
          watering = false;
        }
      }
      Console.ForegroundColor = ConsoleColor.White;
    }
    public static void ViewUnwatered()
    {
      var db = new GardenContext();
      var displayPlants = db.Plants.Where(p => p.LastWateredDate < DateTime.Today);
      if (displayPlants == null)
      {
        Console.WriteLine($"No plants have gone without water today.");
      }
      foreach (var d in displayPlants)
      {
        Console.WriteLine($"{d.Species} was last watered on {d.LastWateredDate}.");
      }

    }
    public static void LocationSummary()
    {
      var db = new GardenContext();
      var displayPlants = db.Plants.Where(p => p.LocatedPlanted != "");
      if (displayPlants == null)
      {
        Console.WriteLine($"You have no plants in your garden.");
      }
      var allPlants = displayPlants.OrderBy(d => d.LocatedPlanted);
      foreach (var d in allPlants)
      {

        Console.WriteLine($"{d.Species} is planted in {d.LocatedPlanted}.");
      }
    }
    public static int TryInt(string input)
    {
      var plantId = 0;
      var trying = true;
      while (trying)
      {
        var intBool = int.TryParse(input, out plantId);
        if (intBool)
        {
          trying = false;

        }
        else
        {
          Console.WriteLine("That is not a valid selection. Please try again.");
          input = Console.ReadLine();
        }
      }
      return plantId;
    }
  }
}

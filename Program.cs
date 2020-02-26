using System;

namespace Garden
{
  class Program
  {
    static void Main(string[] args)
    {
      //create var to access methods in Gardener (UI)
      var garden = new Gardener();
      //create var to access db
      var db = new GardenContext();

      //welcome user to the Garden Database then call menu method
      Console.WriteLine($"Welcome to the Garden Database.");
      Console.WriteLine($"What would you like to do?");
      //set bool to keep menu/options running
      var isRunning = true;
      //start while loop
      while (isRunning)
      {
        var menuInt = garden.Menu();
        if (menuInt == 1)
        {
          Gardener.ViewPlants();
        }
        else if (menuInt == 2)
        {
          Gardener.AddPlant();
        }
        else if (menuInt == 3)
        {
          Gardener.RemovePlant();
        }
        else if (menuInt == 4)
        {
          // Gardener.WaterAPlant;
        }

      }



    }
  }
}

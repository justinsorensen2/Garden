using System;

namespace Garden
{
  public class Plant
  {
    public int Id { get; set; }
    public string Species { get; set; }
    public string LocatedPlanted { get; set; }
    public DateTime PlantedDate { get; set; }
    public DateTime LastWateredDate { get; set; }
    public double LightNeeded { get; set; }
    public double WaterNeeded { get; set; }

  }

}
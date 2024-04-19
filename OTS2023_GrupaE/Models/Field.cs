

namespace OTS2023_GrupaE.Models
{

    public enum Zone
    {
        FarmLand,
        PlantingLand,
        Invalid
    }

    public enum FieldContent
    {
        Empty,
        Seed,
        SeededLand,
        FertileLand
    }

    public class Field
    {
        public FieldContent Content { get; set; }
        public Zone Zone { get; set; }

        public Field(Zone zone)
        {
            Zone = zone;
            Content = FieldContent.Empty;
        }

        public Field(Zone zone, FieldContent content)
        {
            Zone = zone;
            Content = content;
        }
    }
}

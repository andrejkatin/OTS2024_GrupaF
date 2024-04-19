

namespace OTS2023_GrupaE.Models
{
    public class Space
    {
        public Field[,,] Fields { get; set; }
        public static readonly int MapSize = 30;

        public Space()
        {
            Fields = new Field[MapSize, MapSize, MapSize];
        }

        public void InitializeMap()
        {
            for (int i = 0; i < MapSize; i++)
            {
                for (int j = 0; j < MapSize; j++)
                {
                    for (int k = 0; k < MapSize; k++)
                    {
                        Fields[i, j, k] = new Field(Zone.Invalid);
                    }
                }
            }

            CreateRectangleZone(Zone.FarmLand, 15, 0, 0, 15, 10, 10);
            CreateRectangleZone(Zone.FarmLand, 15, 20, 0, 15, 10, 10);
            CreateRectangleZone(Zone.PlantingLand, 0, 5, 0, 15, 20, 10);
         
        }


        private void CreateRectangleZone(Zone zone, int upperLeftCornerX, int upperLeftCornerY, int upperLeftCornerZ, int width, int height, int length)
        {
            for (int i = upperLeftCornerX; i < upperLeftCornerX + width; i++)
            {
                for (int j = upperLeftCornerY; j < upperLeftCornerY + height; j++)
                {
                    for (int k = upperLeftCornerZ; k < upperLeftCornerZ + length; k++)
                    {
                        Fields[i, j, k].Zone = zone;
                    }
                }
            }
        }

        public void AddContentToFieldOnPosition(FieldContent content, Position position)
        {
            Field field = Fields[position.X, position.Y, position.Z];

            if (!field.Zone.Equals(Zone.Invalid))
            {
                if (field.Zone.Equals(Zone.FarmLand) && (content.Equals(FieldContent.SeededLand) || content.Equals(FieldContent.FertileLand)))
                    return;
                else
                    field.Content = content;
            }
        }

        public void EmptyTileOnPosition(Position position)
        { 
            int x = position.X;
            int y = position.Y;
            int z = position.Z;
            Fields[x, y, z].Content = FieldContent.Empty;

        }
    }
}

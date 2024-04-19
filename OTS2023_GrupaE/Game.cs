

using OTS2023_GrupaE.Exceptions;
using OTS2023_GrupaE.Models;

namespace OTS2023_GrupaE
{

    public enum Move
    {
        Up,
        Down,
        Left,
        Right,
        Back,
        Forward
    }

    public enum Income
    {
        Poor,
        Average,
        Good
    }

    public class Game
    {
        public Player Player { get; set; }
        public Space Map { get; set; }


        public Game(Position playerPosition)
        {
            Map = new Space();
            Map.InitializeMap();

            int playerX = playerPosition.X;
            int playerY = playerPosition.Y;
            int playerZ = playerPosition.Z;
          
            if (!ValidatePositionInsideValidZones(playerPosition) || !Map.Fields[playerX, playerY, playerZ].Zone.Equals(Zone.FarmLand))
            {
                throw new InvalidPlayerPositionException("Player must be in the FarmLand zone!");
            }
     
            Player = new Player(playerPosition);
        }

        public void MovePlayer(Move move)
        {
            Position playerPositionAfterMove = Player.GetPositionAfterMove(move);
            
            if (ValidatePosition(playerPositionAfterMove))
            {
                Player.MakeMove(move);
            }
        }

        public bool ValidatePosition(Position position)
        {

            if (position == null)
                return false;

            int x = position.X;
            int y = position.Y;
            int z = position.Z;

            if (!ValidatePositionInsideValidZones(position))
            {
                return false;
            }
            if (Map.Fields[x, y, z].Content.Equals(FieldContent.SeededLand))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool ValidatePositionInsideValidZones(Position position)
        {

            if (position == null)
                return false;

            int x = position.X;
            int y = position.Y;
            int z = position.Z;

            if (x < 0 || x >= Space.MapSize || y < 0 || y >= Space.MapSize)
            {
                return false;
            }
            if (Map.Fields[x, y, z].Zone.Equals(Zone.Invalid))
            {
                return false;
            }
            return true;
        }

        public void ResolvePlayerPosition()
        {
            FieldContent fieldContent = Map.Fields[Player.Position.X, Player.Position.Y, Player.Position.Z].Content;
            Zone fieldZone = Map.Fields[Player.Position.X, Player.Position.Y, Player.Position.Z].Zone;

            if (fieldContent.Equals(FieldContent.Seed))
            {
                Player.AmountOfSeed++;
                Map.EmptyTileOnPosition(Player.Position);
            }
            else if (fieldContent.Equals(FieldContent.FertileLand))
            {
                if(Player.AmountOfSeed > 0)
                {
                    Player.PlantSeed();
                    Map.AddContentToFieldOnPosition(FieldContent.SeededLand, Player.Position);
                }
            }
            if (fieldZone.Equals(Zone.PlantingLand))
            {
                Player.PlantingZoneVisited = true;
            }
            
        }


        public Income CalculateIncome()
        {
            if (Player.AmountOfSeededLand >= 14)
            {
                return Income.Good;
            }
            if (Player.AmountOfSeed > 10 && Player.PlantingZoneVisited)
            {
                if (Player.AmountOfSeededLand > 7)
                {
                    return Income.Good;
                }
                else
                {
                    return Income.Average;
                }
            }
            return Income.Poor;
        }

    }
}

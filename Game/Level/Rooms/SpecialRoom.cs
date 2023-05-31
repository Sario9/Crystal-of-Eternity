using Microsoft.Xna.Framework;
using System;

namespace Crystal_of_Eternity
{
    public class SpecialRoom : Room
    {
        public SpecialRoom(LevelType levelType, Vector2 playerStartPosition) :
            base(levelType, new(25,15), playerStartPosition, 0, new())
        {
        }

        public override void Initialize(Player player, GameState gameState)
        {
            base.Initialize(player, gameState);

            SpawnPlayer(player);
            InitializeSpecialEntity(SpecialRoomTypes.Shop, player);
            AddEntitesToColliders(entities.ToArray());
            AddObstaclesToColliders();

            Complete();
        }

        private void InitializeSpecialEntity(SpecialRoomTypes type, Player player)
        {
            switch (type) 
            {
                case SpecialRoomTypes.Fountain:
                    CreateInteractable(new FountainOfLife(player));
                    break;
                case SpecialRoomTypes.Shop:
                        CreateInteractable(new Merchant(new(400, 250), player.Weapon));
                    break;
                default:
                    throw new ArgumentException();
            }
        }

        protected override void Complete()
        {
            CreateInteractable(new Hatch(player.Position, gameState));
        }
    }
}

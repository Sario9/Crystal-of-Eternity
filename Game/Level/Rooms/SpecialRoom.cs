using Microsoft.Xna.Framework;
using System;

namespace Crystal_of_Eternity
{
    public class SpecialRoom : Room
    {
        private SpecialRoomType roomType;

        public SpecialRoom(LevelType levelType, Vector2 playerStartPosition, SpecialRoomType roomType) :
            base(levelType, new(25,15), playerStartPosition, 0, new())
        {
            this.roomType = roomType;
        }

        public override void Initialize(Player player, GameState gameState)
        {
            base.Initialize(player, gameState);

            SpawnPlayer(player);
            InitializeSpecialEntity(roomType, player);
            AddEntitesToColliders(entities.ToArray());
            AddObstaclesToColliders();

            Complete();
        }

        private void InitializeSpecialEntity(SpecialRoomType type, Player player)
        {
            switch (type) 
            {
                case SpecialRoomType.Fountain:
                    CreateInteractable(new FountainOfLife(player));
                    break;
                case SpecialRoomType.Shop:
                        CreateInteractable(new Merchant(new(400, 250), player.Weapon, player.Invenory));
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

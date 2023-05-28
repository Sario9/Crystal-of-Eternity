using Microsoft.Xna.Framework;

namespace Crystal_of_Eternity
{
    public class SpecialRoom : Room
    {
        public SpecialRoom(LevelType levelType, Vector2 playerStartPosition) :
            base(levelType, new(15,15), playerStartPosition, 0, new())
        {
        }

        public override void Initialize(Player player, GameState gameState)
        {
            base.Initialize(player, gameState);
            CreateInteractable(new FountainOfLife(player));
            SpawnPlayer(player);
            AddEntitesToColliders(entities.ToArray());
            AddObstaclesToColliders();
        }

        protected override void Complete()
        {
            
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using System;
using System.Collections.Generic;

namespace Crystal_of_Eternity
{
    public class Level
    {
        #region Fields
        public TileMap Map => currentRoom.Map;
        public RectangleF bounds => currentRoom.Bounds;

        public Player Player { get; private set; }

        private LevelType levelType;

        public CollisionComponent CollisionComponent => currentRoom.CollisionComponent;

        private List<Room> rooms;
        private int currentRoomIndex = 0;
        public Room currentRoom => rooms[currentRoomIndex];

        private GameState gameState;

        public Level(LevelType levelType, List<Room> rooms)
        {
            this.levelType = levelType;
            this.rooms = rooms;
        } 
        #endregion

        public void Initialize(Player player, GameState gameState)
        {
            Player = player;
            this.gameState = gameState;
            currentRoom.Initialize(player, gameState);
        }

        private void CompleteRoom()
        {
            throw new NotImplementedException();
        }

        public void ChangeRoom(int index)
        {
            currentRoomIndex = index;
            currentRoom.Initialize(Player, gameState);
        }

        public void Update(GameTime gameTime)
        {
            currentRoom.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            currentRoom.Draw(spriteBatch, gameTime, false);
        }

        private Vector2 RandomPosition => Randomizer.NextVector2((int)bounds.Width, (int)bounds.Height);
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace Crystal_of_Eternity
{
    public class Level
    {
        #region Fields
        public TileMap Map => CurrentRoom.Map;
        public RectangleF bounds => CurrentRoom.Bounds;
        public Queue<Room> Rooms { get; private set; }

        public Player Player { get; private set; }

        private LevelType levelType;

        public CollisionComponent CollisionComponent => CurrentRoom.CollisionComponent;

        public Room CurrentRoom { get; private set; }

        private GameState gameState;

        public Level(LevelType levelType, List<Room> rooms)
        {
            this.levelType = levelType;
            Rooms = new(rooms);
        } 
        #endregion

        public void Initialize(Player player, GameState gameState)
        {
            Player = player;
            this.gameState = gameState;
            if (NextRoom(player) == null)
                throw new ArgumentNullException();
        }

        public Room NextRoom(Player player)
        {
            if (Rooms.Count == 0)
                return null;

            Player = player;
            CurrentRoom = Rooms.Dequeue();
            CurrentRoom.Initialize(Player, gameState);
            return CurrentRoom;
        }

        public void Update(GameTime gameTime)
        {
            CurrentRoom.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            CurrentRoom.Draw(spriteBatch, gameTime, false);
        }
    }
}

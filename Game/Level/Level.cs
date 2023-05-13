using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Crystal_of_Eternity
{
    public class Level
    {
        public TileMap Map { get; private set; }
        public Player Player { get; private set; }
        public List<MovableEntity> MovableEntities { get; private set; }
        public RectangleF bounds;

        public CollisionComponent CollisionComponent => currentRoom.CollisionComponent;

        private List<Room> rooms;
        public Room currentRoom;

        public Level(LevelType levelType)
        {
            rooms = new List<Room>()
            {
                new Room(levelType, new(25,25), new(25,25))
            };
            currentRoom = rooms[0];
        }

        public void Initialize()
        {
            currentRoom.Initialize();
            MovableEntities = currentRoom.MovableEntities;
            Player = currentRoom.Player;
            bounds = currentRoom.Bounds;
            Map = currentRoom.Map;
        }

        public void Update(GameTime gameTime)
        {
           currentRoom.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            currentRoom.Draw(spriteBatch, gameTime, false);
        }
    }
}

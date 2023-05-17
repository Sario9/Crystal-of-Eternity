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
        public TileMap Map => currentRoom.Map;
        public Player Player => currentRoom.Player;
        public List<MovableEntity> MovableEntities => currentRoom.MovableEntities;
        public RectangleF bounds => currentRoom.Bounds;

        private LevelType levelType;

        public CollisionComponent CollisionComponent => currentRoom.CollisionComponent;

        private List<Room> rooms;
        private int currentRoomIndex = 0;
        public Room currentRoom => rooms[currentRoomIndex];

        public Level(LevelType levelType)
        {
            this.levelType = levelType;
        }

        public void Initialize(Player player)
        {
            rooms = new List<Room>()
            {
                new Room(levelType, new(25,25), new(400,400)),
                new Room(levelType, new(35,35), new(125,125))
            };
            currentRoom.Initialize(player);
        }

        private void CompleteRoom()
        {
            throw new NotImplementedException();
        }

        public void ChangeRoom(int index)
        {
            currentRoomIndex = index;
            currentRoom.Initialize(Player);
        }

        public void Update(GameTime gameTime)
        {
            currentRoom.Update(gameTime);
            //if (currentRoom.isCompleted)
            //{
            //    if(currentRoomIndex + 1 < rooms.Count)
            //        ChangeRoom(currentRoomIndex + 1);
            //}
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            currentRoom.Draw(spriteBatch, gameTime, false);
        }
    }
}

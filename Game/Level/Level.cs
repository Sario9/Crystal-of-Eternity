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
        public List<MovableEntity> MovableEntities => currentRoom.MovableEntities;
        public RectangleF bounds => currentRoom.Bounds;

        public Player Player;

        private LevelType levelType;

        public CollisionComponent CollisionComponent => currentRoom.CollisionComponent;

        private List<Room> rooms;
        private int currentRoomIndex = 0;
        public Room currentRoom => rooms[currentRoomIndex];

        public Level(LevelType levelType)
        {
            this.levelType = levelType;
        } 
        #endregion

        public void Initialize(Player player)
        {
            Player = player;

            rooms = new List<Room>()
            {
                new Room(levelType, RoomType.Arena, new(25,25), new(400,400), 25,
                () => new Skeleton(RandomPosition, bounds, player),
                () => new Rogue(1, RandomPosition, bounds, player),
                () => new Rogue(2, RandomPosition, bounds, player)),
                new Room(levelType, RoomType.Arena, new(35,35), new(125,125), 30,
                () => new Skeleton(RandomPosition, bounds, player))
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

        private Vector2 RandomPosition => Randomizer.NextVector2((int)bounds.Width, (int)bounds.Height);
    }
}

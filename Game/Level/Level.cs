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
                new DefaultRoom(levelType, new(25,25), new(125,125)),
                new DefaultRoom(levelType, new(25,45), new(75,325)),
            };
            currentRoom.Initialize(player, 25, new()
            {
                new Skeleton(Vector2.One, currentRoom.Bounds, player),
                new Rogue(1, Vector2.One, currentRoom.Bounds, player),
                new Rogue(2, Vector2.One, currentRoom.Bounds, player)
            });
        }

        private void CompleteRoom()
        {
            throw new NotImplementedException();
        }

        public void ChangeRoom(int index)
        {
            currentRoomIndex = index;
            currentRoom.Initialize(Player, 25, new() { new Skeleton(Vector2.One, currentRoom.Bounds, Player) });
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

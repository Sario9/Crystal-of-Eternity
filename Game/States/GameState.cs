using GeonBit.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace Crystal_of_Eternity
{
    public class GameState : State
    {
        #region Fields
        public Level CurrentLevel => Levels[currentLevelIndex];
        public List<Level> Levels { get; private set; }
        public Player Player { get; private set; }
        public MyCamera Camera { get; private set; }

        private GameUI ui;
        private int currentLevelIndex = 0;
        #endregion

        public GameState(MyGame game, ContentManager content, GraphicsDevice graphicsDevice) : base(game, content, graphicsDevice)
        {
            Initialize();
        }

        public void Initialize()
        {
            UserInterface.Active.Clear();
            ui = new GameUI(this);

            Player = new Player(this, 100.0f, 0.7f, 0);
            Player.onHealthChanged += ui.UpdateHealth;
            Player.OnDeath += ui.ShowPlayerDeathText;

            Levels = new List<Level>()
            {
                //Уровнь 1
                new Level(LevelType.Level1,
                new Queue<Room>(new List<Room>() 
                {
                    new DefaultRoom(LevelType.Level1, new(35,35), new(125,125),
                    25, new()
                    {
                        new Skeleton(),
                        new Rogue(1),
                        new Rogue(2),
                    }),
                    new DefaultRoom(LevelType.Level1, new(50,25), new(255,125),
                    50, new()
                    {
                        new Skeleton(),
                    }),
                })),
                //Уровнь 2
                new Level(LevelType.Level2,
                new Queue<Room>(new List<Room>()
                {
                    new DefaultRoom(LevelType.Level2, new(35,35), new(125,125),
                    25, new()
                    {
                        new Skeleton(),
                    }),
                    new DefaultRoom(LevelType.Level2, new(50,25), new(255,125),
                    50, new()
                    {
                        new Skeleton(),
                        new Rogue(1),
                        new Rogue(2),
                    }),
                })),
            };

            CurrentLevel.Initialize(Player, this);

            Camera = new MyCamera(this, graphicsDevice);

            CurrentLevel.CurrentRoom.OnEnemyDie += ui.UpdateEnemies;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend,
               SamplerState.PointClamp, null, null, null, Camera.Main.GetViewMatrix());

            CurrentLevel.Draw(spriteBatch, gameTime);

            spriteBatch.End();
            UserInterface.Active.Draw(spriteBatch);
        }

        public override void PostUpdate(GameTime gameTime)
        {
            //
        }

        public override void Update(GameTime gameTime)
        {
            UserInterface.Active.Update(gameTime);
            UserInput.Update(game, gameTime);
            UserInput.Debug(this);
            Camera.Update(gameTime, Player);
            CurrentLevel.Update(gameTime);
        }

        public void ChangeLevel(int index)
        {
            if (index >= Levels.Count)
            {
                UserInput.ExitToMenu();
                return;
            }

            currentLevelIndex = index;
            CurrentLevel.NextRoom(Player);
            RestartLevel();
        }

        public void NextRoom()
        {
            if (CurrentLevel.NextRoom(Player) == null)
            {
                ChangeLevel(currentLevelIndex + 1);
            }
        }

        public void RestartLevel()
        {
            CurrentLevel.CurrentRoom.OnEnemyDie -= ui.UpdateEnemies;
            Player.Restart();
            CurrentLevel.CurrentRoom.OnEnemyDie += ui.UpdateEnemies;
            Camera = new MyCamera(this, graphicsDevice);
        }
    }
}

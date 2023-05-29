using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.TextureAtlases;
using System.Diagnostics;

namespace Crystal_of_Eternity
{
    public class Player : MovableEntity
    {
        #region Fields
        public InteractableEntity CurrentInteractable 
        {
            get => currentInteractable;
            set
            {
                currentInteractable?.SetNotActive();
                currentInteractable = value;
                currentInteractable?.SetActive();
            }
        }
        public float InteractDistance = 50.0f;
        public PlayerWeapon PlayerAttack { get; private set; }

        private bool isIdle => velocity == Vector2.Zero;
        private GameState gameState;

        private Sprite interactCloud;
        private InteractableEntity currentInteractable;

        public override float CurrentHP
        {
            get => currentHP;
            set
            {
                base.CurrentHP = value;
                if(IsAlive)
                    onHealthChanged?.Invoke(value, maxHP);
            }
        }
        public override float MaxHP
        {
            get { return maxHP; }
            protected set
            {
                var hpCorrelation = currentHP / maxHP;
                maxHP = value;
                CurrentHP = maxHP * hpCorrelation;
                onHealthChanged?.Invoke(currentHP, maxHP);
            }
        }

        public delegate void HitHandler(float currentHealth, float maxHealth);
        public event HitHandler onHealthChanged; 
        #endregion

        public Player(GameState gameState, float maxHP, float moveSpeed, float damage) :
            base("Player", SpriteNames.Character_knight, SpriteNames.Rogue_corpse, "", maxHP,
                moveSpeed, damage, 0.05f)
        {
            this.gameState = gameState;

            UserInput.OnLMBPressed += Attack;
            UserInput.OnMove += Move;
        }

        public void Spawn(Vector2 position, RectangleF mapBounds, CollisionComponent collisionComponent)
        {
            base.Spawn(position, mapBounds);
            PlayerAttack = new Sword(2.0f, collisionComponent);
            onHealthChanged.Invoke(currentHP, maxHP);
        }

        public override void TakeHit(float damage)
        {
            CurrentHP -= damage;
            Debug.Print("{0}/{1}", CurrentHP, maxHP);
        }

        public void ChangeMaxHealth(float value)
        {
            MaxHP = value;
        }

        public override void LoadContent()
        {
            var content = MyGame.Instance.Content;
            base.LoadContent();

            interactCloud = new(content.Load<Texture2D>(SpriteNames.InteractCloud));
        }

        private void Attack()
        {
            if (PlayerAttack.CanAttack && isSpawned)
            {
                var camera = gameState.Camera.Main;
                var mouseWorldPosition = camera.ScreenToWorld(UserInput.GetMousePosition().ToVector2());
                var distance = (mouseWorldPosition - Position).Length();
                PlayerAttack.MakeAttack(mouseWorldPosition - Position, Position, distance);
            }
        }

        protected override void Die()
        {
            base.Die();

            UserInput.OnLMBPressed -= Attack;
            UserInput.OnMove -= Move;
        }

        public void Restart()
        {
            CurrentHP = maxHP;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (CurrentInteractable != null && UserInput.OnInteract == null)
                UserInput.OnInteract += CurrentInteractable.Interact;
            else if (CurrentInteractable == null && UserInput.OnInteract != null)
                UserInput.Clear();

            PlayerAttack.Update(gameTime);
            iTimer.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!isIdle)
                walkAnimation?.Play(gameTime);
            else
                walkAnimation?.Reset();

            if (velocity.X > 0)
                flip = SpriteEffects.FlipHorizontally;
            if (velocity.X < 0)
                flip = SpriteEffects.None;
            Sprite.Effect = flip;

            if (UserInput.OnInteract != null && currentInteractable != null && !currentInteractable.isUsed)
                interactCloud.Draw(spriteBatch, Position + new Vector2(20, -25), 0, Vector2.One);

            if(IsAlive)
                Sprite.Draw(spriteBatch, Position, walkAnimation.SpriteRotation, new(1, 1));
            PlayerAttack.Draw(spriteBatch);
        }

        public override void DrawBounds(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawRectangle((RectangleF)Bounds, Color.Blue, 3);
            PlayerAttack.DrawBounds(spriteBatch);
        }
    }
}

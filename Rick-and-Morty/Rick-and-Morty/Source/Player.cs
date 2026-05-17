using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Rick_and_Morty.Source
{
    public class Player : GameObject
    {
        private float _jumpVelocity = 0f;
        private bool _isJumping = false;
        private const float Gravity = 0.6f; // Сила тяжести
        private float _groundY;

        public Player(Texture2D texture, Vector2 startPosition) : base(texture, startPosition)
        {
            _groundY = startPosition.Y;
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState kState = Keyboard.GetState();

            // Если нажали Пробел и мы на земле — прыгаем
            if ((kState.IsKeyDown(Keys.Space) || kState.IsKeyDown(Keys.Up)) && !_isJumping)
            {
                _isJumping = true;
                _jumpVelocity = -13f; // Летим вверх
            }

            if (_isJumping)
            {
                Position.Y += _jumpVelocity;
                _jumpVelocity += Gravity; // Гравитация тянет вниз

                if (Position.Y >= _groundY) // Приземление
                {
                    Position.Y = _groundY;
                    _isJumping = false;
                }
            }
        }
    }
}
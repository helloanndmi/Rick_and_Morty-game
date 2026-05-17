using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Rick_and_Morty.Source
{
    // Это абстрактный класс. 
    public abstract class GameObject
    {
        public Texture2D Texture;  // Картинка
        public Vector2 Position;   // Координаты (X, Y)

        public GameObject(Texture2D texture, Vector2 startPosition)
        {
            Texture = texture;
            Position = startPosition;
        }

        // Обновление логики (будет свое у каждого)
        public abstract void Update(GameTime gameTime);

        // Отрисовка (одинаковая для всех)
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Rick_and_Morty.Source
{
    public class Obstacle : GameObject
    {
        public float Speed = 7f; // Скорость, с которой враг едет на нас

        public Obstacle(Texture2D texture, Vector2 startPosition) : base(texture, startPosition)
        {
        }

        public override void Update(GameTime gameTime)
        {
            // Враг постоянно двигается влево (уменьшаем X)
            Position.X -= Speed;
        }
    }
}
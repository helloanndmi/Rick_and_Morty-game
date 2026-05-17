using System.Collections.Generic;
using Rick_and_Morty.Source;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Rick_and_Morty
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private List<Obstacle> _obstacles;
        private float _spawnTimer = 0f; // Таймер для появления новых врагов
        Player _morty;
        Texture2D _pixel; // Заглушка вместо картинки

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
            protected override void Initialize()
        {
            // Устанавливаем размер окна ( HD: 1280 на 720)
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges(); // Применяем изменения
            _obstacles = new List<Obstacle>();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Создаем простую белую точку, которую растянем в квадрат
            _pixel = new Texture2D(GraphicsDevice, 1, 1);
            _pixel.SetData(new[] { Color.White });

            // Создаем Морти (текстура, позиция X=100, Y=600)
            _morty = new Player(_pixel, new Vector2(100, 600));
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _morty.Update(gameTime);

            // 2. Таймер для спавна врагов (каждые 2 секунды)
            _spawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_spawnTimer >= 2f)
            {
                // Появляется за правым краем экрана (X = 1300) на уровне земли (Y = 600)
                _obstacles.Add(new Obstacle(_pixel, new Vector2(1300, 600)));
                _spawnTimer = 0f; // Сбрасываем таймер
            }

            // 3. Обновляем всех врагов и проверяем, не ударили ли они Морти
            for (int i = _obstacles.Count - 1; i >= 0; i--)
            {
                _obstacles[i].Update(gameTime);

                // Проверка столкновения: пересекается ли прямоугольник Морти с прямоугольником врага?
                // Мы задаем размер врага 50x50, как и у Морти
                Rectangle mortyRect = new Rectangle((int)_morty.Position.X, (int)_morty.Position.Y, 50, 50);
                Rectangle obstacleRect = new Rectangle((int)_obstacles[i].Position.X, (int)_obstacles[i].Position.Y, 50, 50);

                if (mortyRect.Intersects(obstacleRect))
                {
                    // Пока просто выходим из игры, если врезались (потом сделаем экран GameOver)
                    Exit();
                }

                // Если враг уехал далеко за левый край экрана, удаляем его, чтобы не засорять память
                if (_obstacles[i].Position.X < -100)
                {
                    _obstacles.RemoveAt(i);
                }
            }


            base.Update(gameTime);
            
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            // Рисуем Морти как желтый квадрат 50x50
            _spriteBatch.Draw(_morty.Texture, new Rectangle((int)_morty.Position.X, (int)_morty.Position.Y, 50, 50), Color.Yellow);
            // Рисуем всех врагов красным цветом
            foreach (var obs in _obstacles)
            {
                _spriteBatch.Draw(obs.Texture, new Rectangle((int)obs.Position.X, (int)obs.Position.Y, 50, 50), Color.Red);
            }      

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}



using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame
{
    internal class Sprite
    {
        public Texture2D texture;
        public Vector2 position;

        public int Widht;
        public int Height;
        public Rectangle Rect
        {
            get
            {
                return new Rectangle
                (
                    (int)position.X,
                    (int)position.Y,
                    80,
                    50
                );
            }
        }

        public  Sprite(Texture2D texture , Vector2 position )

        {
            this.texture = texture;
            this.position = position;
        }

        public virtual void Update(GameTime gameTime)
        {

        }
        
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture , Rect, Color.White);
        }
    }

}
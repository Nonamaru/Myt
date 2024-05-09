using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame;

class Ground
{
    private static readonly float SCALE = 1f; 
    public Texture2D texture;
    public Vector2 position;
    public Rectangle Rect
    {
        get
        {
            return new Rectangle(
                (int)position.X,
                (int)position.Y,
                texture.Width ,
                texture.Height
            );
        }
    }
    public int x = 0, y = 0;

    public void GroundInit(Texture2D texture, Vector2 position)
    {
        this.position = position;
        this.texture = texture;
    }
}
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame;

class Ground : Sprite
{
    private static readonly float SCALE = 1f; 

    public int x = 0, y = 0;

    public Ground(Texture2D texture, Vector2 position) : base(texture , position)
    {

    }
}
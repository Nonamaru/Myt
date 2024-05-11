/*
    CHARACTER IS PARAMETR FOR ELEMENT 
    w - WATER;
    F - FIRE;
    E - EARTH;
    A - AIR; 


*/
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace MyGame
{
    class MagickCicle : Sprite
    {
        public char character ;
        
        public Vector2 posithion;
        public MagickCicle(Texture2D texture , Vector2 position , char character):base(texture, position)
        {
            this.texture  = texture;
            this.position = position;
            this.character = character;
        }

    } 
}

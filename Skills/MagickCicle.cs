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

        private int counter;
        private int activeFrame;

    
        public MagickCicle(Texture2D texture , Vector2 position , char character):base(texture, position)
        {
            this.texture  = texture;
            this.position = position;
            this.character = character;
        }

        public void MagickCicleUpdate(){
            if(++counter > 7){          // Костыль для анимации 
                counter = 0;
                activeFrame++;
                if(activeFrame >= 4){
                    activeFrame = 0;
                }
            }
        }

    } 
}

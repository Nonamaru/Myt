/*
    CHARACTER IS PARAMETR FOR ELEMENT 
    w - WATER;
    F - FIRE;
    E - EARTH;
    A - AIR; 


*/
using System.Linq;
using System.Net.Mime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame
{
    class Spell : Sprite
    {
        public int speed = 1;
        
        public Vector2 posithion;
        public Vector2 startPosithion;
        public int index;

        private int counter;
        private int activeFrame;

        public Spell(Texture2D texture , Vector2 position , int speed , int index):base(texture, position)
        {
            this.texture  = texture;
            this.position = position;
            this.speed = speed;
            this.index = index;
        }

        public void SpellAnimate(){
            if(++counter > 7){          // Костыль для анимации 
                counter = 0;
                activeFrame++;
                if(activeFrame >= 5){
                    activeFrame = 0;
                }
            }
        }
        public void Update()
        {
            this.speed = this.speed + (int)startPosithion.X;
        }

        public bool CheckRemove()
        {
            if(posithion.X > 500)
            {
                speed = 2;
                
                return true;
            }
            
            return false;
        }

        public void SetStartPosition(Vector2 startPosithion)
        {
            this.startPosithion = startPosithion;
        }


    } 
}

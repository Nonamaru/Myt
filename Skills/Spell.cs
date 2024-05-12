/*
    CHARACTER IS PARAMETR FOR ELEMENT 
    w - WATER;
    F - FIRE;
    E - EARTH;
    A - AIR; 


*/
using System;
using System.ComponentModel;
using System.Linq;
using System.Net.Mime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame
{
    class Spell
    {
        public int speed = 1;
        

        public Texture2D texture;
        public Vector2 position;
        public Vector2 startPosithion;
        public int index;

        public int timerExist = 0; 
        public int timerAlive = 0; 

        private int counter;
        private int activeFrame;

        public Spell(Texture2D texture , Vector2 position , int speed , int index , int timerAlive)
        {
            this.texture  = texture;
            this.position = position;
            this.speed = speed;
            this.index = index;
            this.timerAlive = timerAlive;
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
        public void UpdateUp()
        {
           position.X += speed;
        }

        public bool CheckRemove()
        {
            if(timerExist      > timerAlive){   timerExist = 0 ; return true;}
            timerExist++;
            return false;
        }

        public void SetStartPosition(Vector2 startPosithion)
        {
            this.startPosithion = startPosithion;
        }


    } 
}

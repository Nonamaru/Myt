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
        
        public bool goToPlayer = false;

        public Spell(Texture2D texture , Vector2 position ,bool goToPlayer, int speed , int index , int timerAlive)
        {
            this.texture  = texture;
            this.position = position;
            this.speed = speed;
            this.index = index;
            this.timerAlive = timerAlive;
            this.goToPlayer = goToPlayer;
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
        public void UpdateUp(Vector2 playerPosithion)
        {
            if(!this.goToPlayer)
            {
                position.X += speed;
            }
            else
            {
                position.X = playerPosithion.X + 15;
                position.Y = playerPosithion.Y - 5;
            } 

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

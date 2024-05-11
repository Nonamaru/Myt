/*
    CHARACTER IS PARAMETR FOR ELEMENT 
    w - WATER;
    F - FIRE;
    E - EARTH;
    A - AIR; 


*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Mime;
using System.Runtime.ExceptionServices;
using System.Runtime.Serialization.Formatters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Sprites;

namespace MyGame
{
    class Skills 
    {
        List<MagickCicle> magickCicles;
        private char[] Buff = {' ' , ' ' , ' '};
        public Skills()    
        {

        }
    
        public char[] getBuffer()
        {
            return Buff;
        }

        public void addBuffer(char character)
        {
            if(Buff.Length < 3)
            {
                for(int g = 0; g < 3 ; g++)
                {
                    if(Buff[g] == ' ')
                    {
                        Buff[g] = character;
                        break;
                    }
                }
            }
            else
            {
                Buff[0] = character;
                for(int g = 1; g < 3 ; g++)
                {
                    Buff[g + 1] = Buff[g];
                }
            }
            
        }
        public void sendSkill()
        {

        }
        public void getSkill()
        {

        } 

        public void Press(MagickCicle magick)
        {
            this.addBuffer(magick.character);
            magickCicles.Add(magick);
        } 

        public virtual void Draw(SpriteBatch spriteBatch , bool flip)
        {
            // spriteBatch.Draw(walkLeftAnime.texture,new Rectangle((int)position.X, (int)position.Y,  80, 90), new Rectangle(activeFrame *48 , 0, 48, 48), Color.White);
            // foreach(var sprite in magickCicles)
            // {   
            //     //spriteBatch.Draw(texture , Rect, Color.White);
            //     //spriteBatch.Draw(sprite.texture , new Vector2(100 , 100), Color.White);
            // }
        }     
    }


}
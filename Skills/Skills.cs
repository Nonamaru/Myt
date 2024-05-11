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
        private String BuffString = "";
        public Skills()    
        {
            magickCicles = new List<MagickCicle>();
        }
        public void addBuffer(MagickCicle magick)
        {
            int counter = 0;
            char[] bufferChar = BuffString.ToCharArray();
            BuffString += magick.character; 
            if(BuffString.Length <= 3)
            {
                magickCicles.Add(magick);
            }
            else
            {
                //magickCicles.RemoveAt(2);
            }
            Console.Write(counter);
        }
        public void addBufferMagick(MagickCicle magick)
        {
            
        }
        public void Press(MagickCicle magick)
        {
            //magickCicles.Add(magick);
            this.addBuffer(magick);
        } 

        public virtual void Draw(SpriteBatch spriteBatch , Vector2 posithion)
        {
            foreach(var sprite in magickCicles )
            {
                spriteBatch.Draw(sprite.texture ,  new Rectangle((int)posithion.X - (int)sprite.posithion.X , (int)posithion.Y - (int)sprite.posithion.Y , 30 , 30),new Rectangle(46, 0, 20 , 20), Color.White);
            }
        }

        public void Update()
        {

        }


        public void sendSkill()
        {

        }
        public void getSkill()
        {

        }
        public char[] getBuffer()
        {
        return Buff;
        } 
    }
}
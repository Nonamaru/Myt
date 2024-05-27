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

        List<Spell> spells;
        List<Spell> activeSpells;
        List<Spell> nonActiveSpells;
        private char[] Buff = {' ' , ' ' , ' '};
        private String BuffString = "";

        public Vector2 playerPosithion;
        public bool playerState; 
        public Skills()    
        {
            magickCicles = new List<MagickCicle>();
        }
        public void Spellinit(Texture2D texture, Texture2D textureEarth, Texture2D textureFire , Texture2D textureWaterBall)
        {
            // F - 70 , W - 87 , A - 65 , E - 69
            spells = new List<Spell>();
            spells.Add(new Spell(texture,           new Vector2(100 , 200) , false ,12 , 205 , 100));
            spells.Add(new Spell(textureEarth,      new Vector2(100 , 200) , false ,0  , 207 , 160));
            spells.Add(new Spell(textureFire,       new Vector2(playerPosithion.X , playerPosithion.Y) , true  ,0  , 210 , 160));
            spells.Add(new Spell(textureWaterBall , new Vector2(100 , 200) , false ,12  , 261 , 160));

            activeSpells = new List<Spell>();
            nonActiveSpells = new List<Spell>();
        }
        public void addBuffer(MagickCicle magick)
        {
            int counter = magickCicles.Count;
            if(counter < 3){
                magickCicles.Insert(0, magick);
            }else{
                magickCicles[2] = magickCicles[1];
                magickCicles[1] = magickCicles[0];
                magickCicles[0] = magick;
            }            
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
            switch (magickCicles.Count){
                case 1:
                {
                    spriteBatch.Draw(magickCicles[0].texture ,  new Rectangle((int)posithion.X - 15, (int)posithion.Y,30,30),new Rectangle(16*this.activeFrame,0,16,16), Color.White);            
                }
                break;
                case 2:
                {
                    spriteBatch.Draw(magickCicles[0].texture ,  new Rectangle((int)posithion.X - 15, (int)posithion.Y,30,30),new Rectangle(16*this.activeFrame,0,16,16), Color.White);            
                    spriteBatch.Draw(magickCicles[1].texture ,  new Rectangle((int)posithion.X + 20, (int)posithion.Y - 30,30,30),new Rectangle(16*this.activeFrame,0,16,16), Color.White);            
                }
                break;
                case 3:
                {
                    spriteBatch.Draw(magickCicles[0].texture ,  new Rectangle((int)posithion.X - 15, (int)posithion.Y  , 30,30),new Rectangle(16*this.activeFrame,0,16,16), Color.White);            
                    spriteBatch.Draw(magickCicles[1].texture ,  new Rectangle((int)posithion.X + 20, (int)posithion.Y  - 30,30,30),new Rectangle(16*this.activeFrame,0,16,16), Color.White);            
                    spriteBatch.Draw(magickCicles[2].texture ,  new Rectangle((int)posithion.X + 60, (int)posithion.Y   ,30,30),new Rectangle(16*this.activeFrame,0,16,16), Color.White);            
                }
                break;
            }

            foreach(var sprite in activeSpells)
            {   
                spriteBatch.Draw(sprite.texture ,  new Rectangle( (int)sprite.position.X , (int)sprite.position.Y ,80,80), Color.White);  
               
                          
            }
        }
        private int counter;
        private int activeFrame;
        bool checkDelete = false; 
        int indexElement = 0;
        public void Update()
        {
            if(++counter > 12){          // Костыль для анимации 
                counter = 0;
                activeFrame++;
                if(activeFrame >= 3){
                    activeFrame = 0;
                }
            }
            
            foreach(var sprite in activeSpells)
            {
                sprite.UpdateUp(playerPosithion);
                
                if(sprite.CheckRemove())
                {
                    //activeSpells = new List<Spell>();
                    // nonActiveSpells = new List<Spell>();
                     nonActiveSpells.Add(sprite);
                } 

            }


            foreach(var sprite in nonActiveSpells)
            {
                activeSpells.Remove(sprite);
            }

            nonActiveSpells.Clear();
        }


        public void SendSkill()
        {
            int indexSpell = 0;
            foreach (var cicle in magickCicles)
            {
                indexSpell += cicle.character;
            }
            magickCicles = new List<MagickCicle>();
            this.AddActiveSpell(indexSpell);
        }

        private void AddActiveSpell(int index)
        {

            foreach(var spell in spells)
            {
              if(spell.index == index)
              {
                
                spell.position.Y       = playerPosithion.Y;
                if(playerState)
                {
                    spell.position.X       = playerPosithion.X + 60;
                }
                else
                {
                    spell.position.X       = playerPosithion.X - 60;
                }
                spell.playerState      = playerState;
                activeSpells.Add(new Spell(spell.texture , spell.position , spell.goToPlayer , spell.speed , spell.index , spell.timerAlive , spell.playerState ));
              }
            }
        } 
    }
}
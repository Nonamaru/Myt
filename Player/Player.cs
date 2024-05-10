using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Serialization;
using MonoGame.Framework.Utilities;

namespace MyGame;

class Player : Sprite
{
    public bool onTheGround = false;
    public SpriteEffects flip = SpriteEffects.FlipHorizontally;

    public int x = 0, y = 0;
    private static readonly float SCALE = 1f; 
    
    private bool doJump = false;
    private int jumpHeigh = 100;
    private float positionYstate = 0;
    List<Sprite> collisionGroup;
    
    


    public  Player(Texture2D texture, Vector2 position, List<Sprite> collisionGroup ) : base(texture , position )
    {
        this.collisionGroup = collisionGroup;
    }


    public virtual void Draw(SpriteBatch spriteBatch , bool flip)
    {
        spriteBatch.Draw(texture, position, null, Color.White ,0.0f, Vector2.Zero, 1, this.flip , 0 ); 
    }
    public override void Update(GameTime gameTime )
    {
        float changeX = 0;
        float changeY = 6;
        
        if (GamePad.GetState(PlayerIndex.One).Buttons.RightStick == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.D))
        {
            changeX += 2;
            this.flip = SpriteEffects.None;
        }
        if (GamePad.GetState(PlayerIndex.One).Buttons.LeftStick == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.A))
        {
            changeX -= 2;
            this.flip = SpriteEffects.FlipHorizontally;
        }
        if(!doJump)
        {
            if ( Keyboard.GetState().IsKeyDown(Keys.W))
            {
                changeY -= 12;
            }
            if ((GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Space)) && this.onTheGround && !this.doJump)
            {
                positionYstate = position.Y;
                this.onTheGround = false;
                //changeY -= 50;
                this.doJump = true;
            }
            if (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.S))
            {
                position.Y += 9;
            }
        } else {
            if((positionYstate - position.Y) <= this.jumpHeigh)
            {
                changeY -= 12;
               
            } 
            else
            {
                this.doJump = false;
            }
        }
       
        position.Y += changeY;
        foreach(var sprite in collisionGroup)
        {
            if(sprite != this && sprite.Rect.Intersects(Rect))
            {
                if(this.doJump) this.doJump = false;
                position.Y -= changeY ;
                position.X += changeX ;
                this.onTheGround = true;
            } 
        }
        
        position.Y += changeY;
        position.X += changeX;


        this.CheckDown();
        base.Update(gameTime);
        
    }

    private void CheckDown(){
        if(position.Y > 500) position.Y = 0;
    }
}
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

namespace MyGame;

class Player : Sprite
{
    AnimationManager playerAnime;
    AnimationManager stayAnime;
    AnimationManager walkLeftAnime;
    AnimationManager walkRightAnime;


    Skills skills;
    MagickCicle earth;
    MagickCicle air;
    MagickCicle water;
    MagickCicle fire;
    public bool onTheGround = false;


    public int speed = 2;
    private static readonly float SCALE = 1f; 
    
    private bool doJump = false;
    private int jumpHeigh = 100;
    private float positionYstate = 0;

    List<Sprite> collisionGroup;
    public  Player(Texture2D texture, Vector2 position, List<Sprite> collisionGroup ) : base(texture , position )
    {
        this.collisionGroup = collisionGroup;

        playerAnime = new AnimationManager(texture , 6 , 0 , new Vector2(50 , 50));
    }

    
    public virtual void Draw(SpriteBatch spriteBatch , bool flip)
    {

        spriteBatch.Draw(playerAnime.texture, new Rectangle((int)position.X, (int)position.Y, 80, 90), playerAnime.getFrame(), Color.White);

        skills.Draw(spriteBatch, position);
    }  
    private bool lastState  = true; // true = значит персонаж смотрит вправо
    public override void Update(GameTime gameTime )
    {
        float changeX = 0;
        float changeY = 6;
        if(!lastState)                  
        {
            playerAnime.setRow(1); // переход на анимацию стойки
        } 
        else
        {
            playerAnime.setRow(0); // переход на анимацию стойки
        }
       
       
        Vector2 leftthumbstick = GamePad.GetState(PlayerIndex.One).ThumbSticks.Left;
        if(GamePad.GetState(PlayerIndex.One).IsConnected)   changeX = leftthumbstick.X * speed;;

        if (GamePad.GetState(PlayerIndex.One).DPad.Right  == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.D))
        {
            changeX += 2 * speed;
            lastState = true;
            
            playerAnime.setRow(3); // переход на анимацию вправо
        }
        if (GamePad.GetState(PlayerIndex.One).DPad.Left == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.A))
        {
            changeX -= 2 * speed;
            lastState = false;

            playerAnime.setRow(2); // переход на анимацию влево
        }
        if (GamePad.GetState(PlayerIndex.One).DPad.Down == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.S))
        {
            playerAnime.setRow(5); // переход на анимацию вниз
        }


        if(!doJump)
        {
            if ( Keyboard.GetState().IsKeyDown(Keys.W))
            {
                changeY -= 12 * speed;
            }
            if ((GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Space)) && this.onTheGround && !this.doJump)
            {
                positionYstate = position.Y;
                this.onTheGround = false;
                this.doJump = true;
            }
            if (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.S))
            {
                position.Y += 9;
            }
            this.SkillsInput();
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
                    //if(this.doJump) this.doJump = false;
                    position.Y -= changeY ;
                    position.X += changeX ;
                    changeY = 0;
                    this.onTheGround = true;
            } 
        }
        
        position.Y += changeY;
        position.X += changeX;

        this.CheckDown();
        stayAnime.Update();
        base.Update(gameTime);
        playerAnime.Update();
        skills.Update();
    }

    private void CheckDown()
    {
        if(position.Y > 700) position.Y = 0;
    }

    public void LoadContent(Texture2D texture, Texture2D eathTower, Texture2D fullFre, Texture2D waterBall)
    {
        skills = new Skills();
        skills.Spellinit(texture , eathTower , fullFre , waterBall);

    }

    //  Установление анимации 
    public void setWalkAnime(Texture2D textureRight,Texture2D textureLeft )
    {
        walkLeftAnime = new AnimationManager(textureLeft , 0 , 0 , new Vector2(48 , 48) );
        walkRightAnime = new AnimationManager(textureRight , 0 , 0 , new Vector2(48 , 48) );
    }
    public void setStayAnime(Texture2D texture)
    {
        stayAnime = new AnimationManager(texture , 0 , 0 , new Vector2(40 , 40) );
    }

    // Загрузка шариков
    public void EarthInit(Texture2D texture )
    {
        earth = new MagickCicle(texture , Vector2.Zero,'E');
    }
    public void AirInit(Texture2D texture )
    {
        air = new MagickCicle(texture , Vector2.Zero,'A');
    }
    public void FireInit(Texture2D texture )
    {
        fire = new MagickCicle(texture , Vector2.Zero,'F');
    }
    public void WaterInit(Texture2D texture )
    {
        water = new MagickCicle(texture , Vector2.Zero,'W');
    }

    // Нажатия на заклинания  
    bool KeyDownH = false;
    bool KeyDownJ = false;
    bool KeyDownU = false;
    bool KeyDownK = false;
    bool KeyDownE = false;
    private void SkillsInput()
    {
        if(GamePad.GetState(PlayerIndex.One).Buttons.X == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.H)) 
        {
            KeyDownH = true;
        }
        if(Keyboard.GetState().IsKeyUp(Keys.H) && KeyDownH)
        {
            Console.WriteLine("UnPress H");
            skills.Press(fire);
            KeyDownH = false;
        }

        if(GamePad.GetState(PlayerIndex.One).Buttons.X == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.J)) 
        {
            KeyDownJ = true;
        }
        if((Keyboard.GetState().IsKeyUp(Keys.J)) && KeyDownJ)
        {
            Console.WriteLine("UnPress J");
            skills.Press(water);
            KeyDownJ = false;
        }

        if(GamePad.GetState(PlayerIndex.One).Buttons.X == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.K))
        {
            KeyDownK = true;
        }
        if((Keyboard.GetState().IsKeyUp(Keys.K)) && KeyDownK)
        {
            Console.WriteLine("Press K");
            skills.Press(earth);
            KeyDownK = false;
        }

        if(GamePad.GetState(PlayerIndex.One).Buttons.X == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.U))  
        {
            KeyDownU = true;
        }
        if(Keyboard.GetState().IsKeyUp(Keys.U) && KeyDownU)
        {
            Console.WriteLine("Unpress U");
            skills.Press(air);
            KeyDownU = false;
        }

        if(Keyboard.GetState().IsKeyDown(Keys.E))
        {
            KeyDownE = true;
        }
        if(Keyboard.GetState().IsKeyUp(Keys.E) && KeyDownE)
        {
            skills.SendSkill();
            KeyDownE = false;
        }
        
        skills.playerPosithion.X = position.X;
        skills.playerPosithion.Y = position.Y;
        skills.playerPosithion.X = position.X;
        skills.playerPosithion.Y = position.Y;
        skills.playerState       = lastState;
    }
}











//spriteBatch.Draw(stayAnime.texture,new Rectangle( (int)position.X , (int)position.Y, 48, 48), stayAnime.getFrame(), Color.White);
// spriteBatch.Draw(texture, position, null, Color.White ,0.0f, Vector2.Zero, 1, this.flip , 0 );  // Версия без анимации 
//spriteBatch.Draw(stayAnime.texture,new Rectangle( (int)position.X , (int)position.Y, 48, 48), new Rectangle( (int)position.X*48 , (int)position.Y, 48, 48), Color.White);
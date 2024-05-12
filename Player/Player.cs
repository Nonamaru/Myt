using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Mime;
using System.Runtime.ExceptionServices;
using System.Runtime.Serialization.Formatters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Sprites;

namespace MyGame;

class Player : Sprite
{

    AnimationManager stayAnime;
    AnimationManager walkLeftAnime;
    AnimationManager walkRightAnime;


    Skills skills;
    MagickCicle earth;
    MagickCicle air;
    MagickCicle water;
    MagickCicle fire;
    public bool onTheGround = false;

    public SpriteEffects flip = SpriteEffects.FlipHorizontally;

    public int speed = 2;
    private static readonly float SCALE = 1f; 
    
    private int playerState = 0;
    private bool doJump = false;
    private int jumpHeigh = 100;
    private float positionYstate = 0;

    List<Sprite> collisionGroup;
    int activeFrame = 0;
    int counter = 0;

    public  Player(Texture2D texture, Vector2 position, List<Sprite> collisionGroup ) : base(texture , position )
    {
        this.collisionGroup = collisionGroup;
        this.LoadContent();
    }


    public virtual void Draw(SpriteBatch spriteBatch , bool flip)
    {

        switch (playerState)
        {
            case 1:         // Стоит 
            {
                spriteBatch.Draw(stayAnime.texture,new Rectangle((int)position.X, (int)position.Y, 80, 90), new Rectangle(activeFrame *48 , 0, 48, 48), Color.White);
            }
            break;
            case 2:         // Идет вправо
            {
                spriteBatch.Draw(walkRightAnime.texture,new Rectangle((int)position.X, (int)position.Y,  80, 90), new Rectangle(activeFrame *48 , 0, 48, 48), Color.White);
            }
            break;
            case 3:         // Идет влево
            {
                spriteBatch.Draw(walkLeftAnime.texture,new Rectangle((int)position.X, (int)position.Y,  80, 90), new Rectangle(activeFrame *48 , 0, 48, 48), Color.White);
            }
            break;
        };
        skills.Draw(spriteBatch, position);
    }  
    public override void Update(GameTime gameTime )
    {
        float changeX = 0;
        float changeY = 6;

        Vector2 leftthumbstick = GamePad.GetState(PlayerIndex.One).ThumbSticks.Left;
        playerState = 1;
        if(GamePad.GetState(PlayerIndex.One).IsConnected)   changeX = leftthumbstick.X * speed;;

        if (GamePad.GetState(PlayerIndex.One).DPad.Right  == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.D))
        {
            changeX += 2 * speed;
            playerState = 2;
        }
        if (GamePad.GetState(PlayerIndex.One).DPad.Left == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.A))
        {
            changeX -= 2 * speed;
            playerState = 3;
  
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
                //changeY -= 50;
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

        if(changeX > 0)
        {
            this.flip = SpriteEffects.None;
        }
        else
        {
            this.flip = SpriteEffects.FlipHorizontally; 
        }
        this.CheckDown();
        stayAnime.Update();
        base.Update(gameTime);

        if(++counter > 7){          // Костыль для анимации 
            counter = 0;
            activeFrame++;
            if(activeFrame >= 6){
                activeFrame = 0;
            }
        }

        skills.Update();
    }

    private void CheckDown()
    {
        if(position.Y > 700) position.Y = 0;
    }

    public void LoadContent()
    {
        skills = new Skills();
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
    bool KeyDownL = false;
    bool KeyDownK = false;
    private void SkillsInput()
    {
        if(GamePad.GetState(PlayerIndex.One).Buttons.X == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.H)) 
        {
            KeyDownH = true;
        }
        if((Keyboard.GetState().IsKeyUp(Keys.H)) && KeyDownH)
        {
            Console.WriteLine("UnPress H");
            fire.posithion.X = 55;
            fire.posithion.Y =  0;
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
            fire.posithion.X = 55;
            fire.posithion.Y =  0;
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
            water.posithion.X = - 75;
            water.posithion.Y =  0;
            skills.Press(earth);
            KeyDownK = false;
        }
        if(GamePad.GetState(PlayerIndex.One).Buttons.X == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.L))  
        {
            KeyDownL = true;
        }
        if(Keyboard.GetState().IsKeyUp(Keys.L) && KeyDownL)
        {
            Console.WriteLine("Unpress L");
            air.posithion.X = - 75;
            air.posithion.Y =  -75;
            skills.Press(air);
            KeyDownL = false;
        }        
        if(Keyboard.GetState().IsKeyDown(Keys.E))
        {
            skills.SendSkill();
        }
    }
}











//spriteBatch.Draw(stayAnime.texture,new Rectangle( (int)position.X , (int)position.Y, 48, 48), stayAnime.getFrame(), Color.White);
// spriteBatch.Draw(texture, position, null, Color.White ,0.0f, Vector2.Zero, 1, this.flip , 0 );  // Версия без анимации 
//spriteBatch.Draw(stayAnime.texture,new Rectangle( (int)position.X , (int)position.Y, 48, 48), new Rectangle( (int)position.X*48 , (int)position.Y, 48, 48), Color.White);
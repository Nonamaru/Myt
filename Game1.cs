using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Mime;
using System.Runtime.ExceptionServices;
using System.Runtime.Serialization.Formatters;
using System.Threading.Tasks.Dataflow;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Sprites;

namespace MyGame;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    Player player;
    List<Sprite> sprites;
    
    Texture2D back;
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        _graphics.PreferredBackBufferHeight = 768;
        _graphics.PreferredBackBufferWidth  = 1024;

        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        

        sprites = new();
        Texture2D ground_texture = Content.Load<Texture2D>("ground"); 

        for(int i = 0 ; i <= 1000 ; i += 40)
        {
            sprites.Add(new Sprite(ground_texture , new Vector2(i , 718) ));
        }
        
        for(int i = 200 ; i <= 280 ; i += 40)
        {
            sprites.Add(new Sprite(ground_texture , new Vector2(i , 550)));
        }

        //player = new Player(Content.Load<Texture2D>("Stay") ,Vector2.Zero , sprites);
        player = new Player(Content.Load<Texture2D>("Player") ,Vector2.Zero , sprites);     // Передавать sprites сюда не правильно
        player.LoadContent(Content.Load<Texture2D>("fireBall1"), Content.Load<Texture2D>("eathTower"),Content.Load<Texture2D>("fullFire") ,Content.Load<Texture2D>("waterBall") );
        player.setStayAnime(Content.Load<Texture2D>("PlayerOnt"));
        player.setWalkAnime(Content.Load<Texture2D>("walkRight"), Content.Load<Texture2D>("walkLeft"));
        player.EarthInit(Content.Load<Texture2D>("earth"));
        player.AirInit(Content.Load<Texture2D>("air"));
        player.WaterInit(Content.Load<Texture2D>("water"));
        player.FireInit(Content.Load<Texture2D>("fire"));

        back = Content.Load<Texture2D>("back");

        

        //animate.Update();
        //sprites.Add(player);

    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        player.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime) 
    { 
        GraphicsDevice.Clear(Color.White); 
 
        _spriteBatch.Begin(); 
        _spriteBatch.Draw(back , new Rectangle(0 , 0 ,1090 , 728), Color.White);
        foreach(var sprite in sprites)
        {   
            if(sprite != player)    sprite.Draw(_spriteBatch);
        }
        player.Draw(_spriteBatch , true); 


        
        player.Draw(_spriteBatch , true); 

        _spriteBatch.End(); 
 
        base.Draw(gameTime); 
    }
}

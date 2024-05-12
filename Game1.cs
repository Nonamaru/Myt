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

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    Player player;
    List<Sprite> sprites;

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

        for(int i = 0 ; i <= 800 ; i += 40)
        {
            sprites.Add(new Sprite(ground_texture , new Vector2(i , 400) ));
        }
        
        for(int i = 200 ; i <= 280 ; i += 40)
        {
            sprites.Add(new Sprite(ground_texture , new Vector2(i , 200)));
        }

        player = new Player(Content.Load<Texture2D>("Stay") ,Vector2.Zero , sprites);
        player.LoadContent((Content.Load<Texture2D>("fireBall1")));
        player.setStayAnime(Content.Load<Texture2D>("PlayerOnt"));
        player.setWalkAnime(Content.Load<Texture2D>("walkRight") , Content.Load<Texture2D>("walkLeft"));
        player.EarthInit(Content.Load<Texture2D>("earth"));
        player.AirInit(Content.Load<Texture2D>("air"));
        player.WaterInit(Content.Load<Texture2D>("water"));
        player.FireInit(Content.Load<Texture2D>("fire"));
        

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
        GraphicsDevice.Clear(Color.CornflowerBlue); 
 
        _spriteBatch.Begin(); 
        foreach(var sprite in sprites)
        {   
            if(sprite != player)    sprite.Draw(_spriteBatch);
        }
        player.Draw(_spriteBatch , true); 

        _spriteBatch.End(); 
 
        base.Draw(gameTime); 
    }
}

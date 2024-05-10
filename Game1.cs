using System.Collections.Generic;
using System.Diagnostics;
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
    SpriteEffects flip = SpriteEffects.FlipHorizontally;
    Player player;
    List<Sprite> sprites;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
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

        for(int i = 0 ; i <= 400 ; i += 40)
        {
            sprites.Add(new Sprite(ground_texture , new Vector2(i , 400)));
        }
        
        for(int i = 200 ; i <= 280 ; i += 40)
        {
            sprites.Add(new Sprite(ground_texture , new Vector2(i , 300)));
        }

        player = new Player(Content.Load<Texture2D>("Stay") ,Vector2.Zero , sprites);

        sprites.Add(player);

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
        //_spriteBatch.Draw(ground.texture, ground.position, Color.White);
        foreach(var sprite in sprites)
        {   
            if(sprite != player)    sprite.Draw(_spriteBatch);
        }
        player.Draw(_spriteBatch , true); 
        _spriteBatch.End(); 
 
        base.Draw(gameTime); 
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame;

public class Game1 : Game
{
    // Texture2D groundTexture;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    Player player;
    Ground ground;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        
        player = new Player();
        Texture2D playerTexture = Content.Load<Texture2D>("Stay");

        player.PlayerInit(playerTexture, Vector2.Zero);

        ground = new Ground();
        Texture2D ground_texture = Content.Load<Texture2D>("ground"); 
        // ground_texture.Width { get; };
        ground.GroundInit(ground_texture, Vector2.Zero);

        ground.position.X = 100;
        ground.position.Y = 400;
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        player.InputLoop();
        
        if(!player.Rect.Intersects(ground.Rect))
        {
            player.position.Y += 4;
        }

        // if (player.position.Y <= (ground.position.Y - 45) || player.position.X > (ground.position.X + 30) || player.position.X < (ground.position.X - 30))
        // {
        //     player.position.Y += 4;
        // }
        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime) 
    { 
        GraphicsDevice.Clear(Color.CornflowerBlue); 
 
        // TODO: Add your drawing code here 
        _spriteBatch.Begin(); 
        _spriteBatch.Draw(ground.texture, ground.position , Color.White); 
 
        for(int g = 0; g <= 10 ; g++ ) 
        { 
            Vector2 newPosi = new Vector2(); 
            newPosi.X =  ground.position.X + 30 ; 
            _spriteBatch.Draw(ground.texture, newPosi , Color.White); 
 
        }     
        _spriteBatch.Draw(player.texture, player.position, Color.White); 
        _spriteBatch.End(); 
        base.Draw(gameTime); 
    }
}

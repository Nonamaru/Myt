using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System.Security.Cryptography.X509Certificates;


namespace MyGame;

class Player
{
    public Texture2D texture;
    public Vector2 position;
    private static readonly float SCALE = 1f; 
    public Rectangle Rect
    {
        get
        {
            return new Rectangle(
                (int)position.X,
                (int)position.Y,
                texture.Width,
                texture.Height
            );
        }
    }
    public int x = 0, y = 0;

    public void PlayerInit(Texture2D texture, Vector2 position)
    {
        this.position = position;
        this.texture = texture;
    }

    public void InputLoop(){
        if (GamePad.GetState(PlayerIndex.One).Buttons.RightStick == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.D))
            this.position.X += 5;
        
        if (GamePad.GetState(PlayerIndex.One).Buttons.LeftStick == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.A))
            this.position.X -= 5;
        
        if (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Space))
            this.position.Y -= 6;
    }
}

using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace MyGame
{
    class AnimationManager
    {

        private readonly int intScale = 30;
        int numColumns;
        int numFrames;
        Vector2 size; 

        public Texture2D texture;
        private int counter ;
        private int activeFrame;
        private int interval;

        private int rowPos; 
        private int colPos; 

        int OffsetX {get; set; } = 0;
        int OffsetY {get; set; } = 0;
        public AnimationManager(Texture2D texture , int numFrames , int numColumns ,Vector2 size)
        {
            this.numFrames = numFrames;
            this.numColumns = numColumns;
            this.size = size;
            this.texture = texture;

            this.counter = 0;
            this.activeFrame = 0;
            this.interval = intScale;
            
            this.rowPos = 0;
            this.colPos = 0;

        }
        public void Update()
        {
            if(++counter > interval)
            {
                counter = 0;
                this.NextFrame();
            }
        }
        public void NextFrame()
        {
            activeFrame++;
            colPos++;
            if(activeFrame >= numFrames)
            {
                this.ResetAnimation();
            }

            if(colPos >= numColumns)
            {
                colPos = 0;
              //  rowPos++;
            }
        }
        public void ResetAnimation()
        {
            activeFrame = 0;
            colPos = 0;
            //rowPos = 0; 
        }
        public Rectangle getFrame()
        {
            return new Rectangle(
                colPos * (int)size.X + OffsetX,
                rowPos * (int)size.Y + OffsetY,
                (int)size.X,
                (int)size.Y
            );
        }
        
        public void setRow(int row)
        {
            rowPos = row;

        }
    }

}
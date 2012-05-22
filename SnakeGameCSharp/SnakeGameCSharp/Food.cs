using System;
using System.Collections;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SnakeGameCSharp
{
    public class Food
    {
        private Vector2 _location= Vector2.Zero;
        
        public Food()
        {

        }

        //public float X()
        //{
        //    return _location.X;
        //}
        //public float Y()
        //{
        //    return _location.Y;
        //}
        public float X
        {
            get
            {
                return _location.X;
            }
            set
            {
                _location.X = value;
            }
        }
        public float Y
        {
            get
            {
                return _location.Y;
            }
            set
            {
                _location.Y = value;
            }
        }

        private void CheckFoodSnakeCollision(BoundingBox snakehead)
        {
            BoundingBox BBFood = new BoundingBox(new Vector3(_location.X + 1, _location.Y + 1, 0),
                                                 new Vector3(_location.X + 9, _location.Y + 9, 0));

            
            
            if (snakehead.Intersects(BBFood))
            {
                SnakeLength += 1;
                Snake[SnakeLength].position = Snake[SnakeLength - 1].position;
                Snake[SnakeLength].facing = Snake[SnakeLength - 1].facing;
                RandomVector(ref Food);
            }

            ///TODO: check for wall collions against the snake

        }
    }
}

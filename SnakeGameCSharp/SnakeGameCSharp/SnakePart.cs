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
    class SnakePart
    {
        private Vector2 _location;
        private int _facing;
        //private BoundingBox _bbox;

        
        public SnakePart(Vector2 Location,int Facing)
        {
            _location = Location;
            _facing = Facing;

        }
        public Vector2 Position
        {
            get
            {
                return _location;
            }
            set
            {
                _location = value;
            }
        }
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
        public int Facing
        {
            get
            {
                return _facing;
            }
            set
            {
                _facing = value;
            }
        }

    }
}

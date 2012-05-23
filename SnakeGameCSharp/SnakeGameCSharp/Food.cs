﻿using System;
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
        private Random random = new Random();
        public Food()
        {
            MakeFood(ref _location);
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
        public BoundingBox BBFood
        {
            get
            {
                BoundingBox BBFood = new BoundingBox(new Vector3(_location.X + 1, _location.Y + 1, 0),
                                                      new Vector3(_location.X + 9, _location.Y + 9, 0));
                return BBFood;
            }

        }
        private Vector2 MakeFood(ref Vector2 Vector)
        {
            do
            {
                Single RndNum = random.Next(10, 590);
                Vector.X = (int)RndNum;
            } while (Vector.X % 10 != 0);
            do
            {
                Single RndNum = random.Next(10, 590);
                Vector.Y = (int)(int)RndNum;
            } while (Vector.Y % 10 != 0);
            return Vector;
        }

    }
}

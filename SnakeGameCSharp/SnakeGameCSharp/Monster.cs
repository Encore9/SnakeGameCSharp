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
    public class Monster
    {
        public Vector2 MonsterXSpeed = new Vector2(10, 0);
        public Vector2 MonsterYSpeed = new Vector2(0, 10);
        Random random = new Random();
        public Vector2 position;
         public int facing;
     }
}

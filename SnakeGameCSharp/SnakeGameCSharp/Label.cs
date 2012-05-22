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
    public class Label
    {
        internal Vector2 _vector;
        internal bool _visible;
        internal string _text;
        internal Color _color;
        internal SpriteFont _font;
        public Label(Vector2 Vector, string Text,SpriteFont Font, bool Visible, Color Color)
        {
            _vector = Vector;
            _text = Text;
            _font = Font;
            _visible = Visible;
            _color = Color;
        }
        public Label(Vector2 Vector, string Text, SpriteFont Font, bool Visible)
        {
            _vector = Vector;
            _text = Text;
            _font = Font;
            _visible = Visible;
            _color = Color.Black;
        }
        public Label(Vector2 Vector, string Text, SpriteFont Font)
        {
            _vector = Vector;
            _text = Text;
            _font = Font;
            _visible = true;
            _color = Color.Black;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (_visible == true)
            {
                spriteBatch.DrawString(_font,_text, _vector, _color);
            }
        }
        public Vector2 Position
        {
            get
            {
                return _vector;
            }
            set
            {
                _vector = value;
            }
        }
        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                
                _text= value;
            }
        }
        public bool Visible
        {
            get
            {
                return _visible;
            }
            set
            {
                _visible = value;
            }
        }
        public Color Color
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
            }
        }

    }
}

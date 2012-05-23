using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SnakeGameCSharp
{

    class Wall
    {

        Texture2D _texture;
        public Vector2 _position;

        public BoundingBox BoundingBox
        {
            get
            {

                return new BoundingBox(new Vector3(_position.X+1,_position.Y+1,0),new Vector3(_texture.Width-1,_texture.Height-1,0));
            }

        }
        public Wall(Texture2D Texture,Vector2 Position)
        {

            _texture = Texture;
            _position = Position;

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, Color.White);
        }
    }
}

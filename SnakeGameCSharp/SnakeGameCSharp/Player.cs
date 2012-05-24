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
using System.Collections.Generic;
namespace SnakeGameCSharp
{
    public class Player
    {
        
        private KeyboardState _oldState;

        private Texture2D _SnakeBodyTexture;
        private Texture2D _SnakeHeadTexture;
        private int _StartLength = 5;
        private bool _Playerone = true;
        private int _Score;
        List<SnakePart> _Snake = new List<SnakePart>();
        //snakePart[] Snake = new snakePart[99];
        private Vector2 _SnakeXSpeed = new Vector2(10, 0);
        private Vector2 _SnakeYSpeed = new Vector2(0, 10);
        //private struct snakePart
        //{
        //    public Vector2 position;
        //    public int facing;
        //};
        List<BoundingBox> _Walls = new List<BoundingBox>();
        //BoundingBox BBLeftWall = new BoundingBox(new Vector3(1, 1, 0), new Vector3(9, 599, 0));
        //BoundingBox BBTopWall = new BoundingBox(new Vector3(1, 1, 0), new Vector3(599, 9, 0));
        //BoundingBox BBRightWall = new BoundingBox(new Vector3(591, 1, 0), new Vector3(599, 599, 0));
        //BoundingBox BBBottomWall = new BoundingBox(new Vector3(1, 591, 0), new Vector3(599, 599, 0));
        
        private bool _dead = false;
        private bool _paused = false;
        private bool pauseKeyDown = false;
        private bool pausedForGuide = false;


        public Player(Texture2D SnakeHeadTexture, Texture2D SnakeBodyTexture, int InitialLength)
        {

            _SnakeHeadTexture = SnakeHeadTexture;
            _SnakeBodyTexture = SnakeBodyTexture;
            _StartLength = InitialLength;
            StartUp();
            

        }
        public Player(Texture2D SnakeHeadTexture, Texture2D SnakeBodyTexture, int InitialLength,bool PlayerOne)
        {

            _SnakeHeadTexture = SnakeHeadTexture;
            _SnakeBodyTexture = SnakeBodyTexture;
            _StartLength = InitialLength;
            _Playerone = PlayerOne;
            StartUp();


        }
        public int SnakeLength { get; set; }
        public int StartLength { get{return _StartLength;}}
        public BoundingBox SnakeHead
        {
            get
            {
                BoundingBox BBSnake = new BoundingBox(new Vector3(_Snake[0].X + 1, _Snake[0].Y + 1, 0),
                                                      new Vector3(_Snake[0].X + 9, _Snake[0].Y + 9, 0));
                return BBSnake;
            }

        }
        public void AddEdges(BoundingBox Left, BoundingBox Top, BoundingBox Right, BoundingBox Bottom)
        {
            List<BoundingBox> walls= new List<BoundingBox>();
            walls.Add(Left);
            walls.Add(Top);
            walls.Add(Right);
            walls.Add(Bottom);            
            _Walls.AddRange(walls);
        }
        public bool CheckSnakeFoodCollision(BoundingBox Food)
        {
            ///TODO: check for food collisions against the snake
            if (SnakeHead.Intersects(Food))
            {
                GrowSnake();
                return true;
            }
            return false;
        }
        public bool Paused
        {
            get
            {
                return _paused;
            }
        }
        public bool Dead
        {
            get
            {
                return _dead;
            }
        }

        private void StartUp()
        {
            _oldState = Keyboard.GetState();
            _dead = false;
            
            EndPause();
            this.SnakeLength = this.StartLength;

            for (int i = 0; i <= this.SnakeLength - 1; i++)
            {
                SnakePart part = new SnakePart(new Vector2(120 - (10 * i), 120), 3);
                 _Snake.Add(part);
                //_Snake[i].Position = new Vector2(120 - (10 * i), 120);
                //_Snake[i].Facing = 3;
            }


            _paused = false;
            //pauseKeyDown = false;
            pausedForGuide = false;

        }

        public void Update(GameTime gameTime)
        {
          if (!_dead)
            {
                CheckPause();
                CheckSnakeWallCollision();
                //CheckSnakeFoodCollision();
                if (!_paused)
                {


                    UpdateInput();
                    MoveSnake();


                }

            }
        } 
        private void UpdateInput()
        {
            KeyboardState newState = Keyboard.GetState();
            // Is the UP key down?
            if (newState.IsKeyDown(Keys.Left))
            {
                // If not down last update, key has just been pressed.
                if (!_oldState.IsKeyDown(Keys.Left))
                {
                    _Snake[0].Facing = 1;//Left
                }
            } if (newState.IsKeyDown(Keys.Up))
            {
                // If not down last update, key has just been pressed.
                if (!_oldState.IsKeyDown(Keys.Up))
                {
                    _Snake[0].Facing = 2;//Up
                }
            }
            if (newState.IsKeyDown(Keys.Right))
            {
                // If not down last update, key has just been pressed.
                if (!_oldState.IsKeyDown(Keys.Right))
                {
                    _Snake[0].Facing = 3;//Right
                }
            }
            if (newState.IsKeyDown(Keys.Down))
            {
                // If not down last update, key has just been pressed.
                if (!_oldState.IsKeyDown(Keys.Down))
                {
                    _Snake[0].Facing = 4;//Down
                }
            }
            _oldState = newState;
        }
        
        private void CheckPause()
        {
            KeyboardState newState = Keyboard.GetState();            
            bool pauseKeyDownThisFrame = (newState.IsKeyDown(Keys.P));
            // If key was not down before, but is down now, we toggle the
            // pause setting
            if (!pauseKeyDown && pauseKeyDownThisFrame)
            {
                if (!_paused)
                    BeginPause(true);
                else
                    EndPause();                
            }
            pauseKeyDown = pauseKeyDownThisFrame;

            //// Pause if the Guide is up
            //if (!_paused && Guide.IsVisible)
            //    BeginPause(false);
            //// If we paused for the guide, unpause if the guide
            //// went away
            //else if (_paused && pausedForGuide && !Guide.IsVisible)
            //    EndPause();
                       
        }
        
        private void BeginPause(bool UserInitiated)
        {
            _paused = true;
            pausedForGuide = !UserInitiated;
            //label[6].visible = true;

            //TODO: Pause audio playback
            //TODO: Pause controller vibration
        }
        private void EndPause()
        {
            //TODO: Resume audio
            //TODO: Resume controller vibration
            pausedForGuide = false;
            _paused = false;
            //label[6].visible = false;
        }
        private void MoveSnake()
        {        
            for (int i = this.SnakeLength - 1; i >= 1; i--)
            {
                
                _Snake[i].Position = _Snake[i - 1].Position;
                _Snake[i].Facing = _Snake[i - 1].Facing;

            }
            if (_Snake[0].Facing == 1)
            {
                _Snake[0].Position -= _SnakeXSpeed;

            }
            else if (_Snake[0].Facing == 2)
            {
                _Snake[0].Position -= _SnakeYSpeed;

            }
            else if (_Snake[0].Facing == 3)
            {
                _Snake[0].Position += _SnakeXSpeed;

            }
            else if (_Snake[0].Facing == 4)
            {
                _Snake[0].Position += _SnakeYSpeed;


            }

            

        }
        private void GrowSnake()
        {
            
            SnakePart part = new SnakePart(_Snake[this.SnakeLength - 1].Position, _Snake[this.SnakeLength - 1].Facing);
            _Snake.Add(part);
            this.SnakeLength += 1;
            //_Snake[this.SnakeLength].Position = _Snake[this.SnakeLength - 1].Position;
            //_Snake[this.SnakeLength].Facing = _Snake[this.SnakeLength - 1].Facing;
        }
       

        private void CheckSnakeWallCollision()
        {
            ///TODO: check for wall collions against the snake
            ///
            //BoundingBox BBSnake = new BoundingBox(new Vector3(_Snake[0].X + 1, _Snake[0].Y + 1, 0),
            //                                      new Vector3(_Snake[0].X + 9, _Snake[0].Y + 9, 0));
            _Walls.ForEach(delegate(BoundingBox CurrentBBOX)
            {
                if (SnakeHead.Intersects(CurrentBBOX))
                {
                    Vector3 max=CurrentBBOX.Max;
                    Vector3 min = CurrentBBOX.Min;
                    _paused = true;
                    _dead = true;

                }
             });
            //switch (_Snake[0].Facing)
            //{
            //    case 1:

            //        if (SnakeHead.Intersects(BBLeftWall))
            //        {

            //            paused = true;
            //            gameover = true;

            //        }
            //        break;
            //    case 2:

            //        if (SnakeHead.Intersects(BBTopWall))
            //        {

            //            paused = true;
            //            gameover = true;
            //        }
            //        break;
            //    case 3:


            //        if (SnakeHead.Intersects(BBRightWall))
            //        {

            //            paused = true;
            //            gameover = true;
            //        }
            //        break;
            //    case 4:

            //        if (SnakeHead.Intersects(BBBottomWall))
            //        {

            //            paused = true;
            //            gameover = true;
            //        }
            //        break;

            //}

        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
      

            for (int i = 0; i <= this.SnakeLength - 1; i++)
            {
                if (i == 0)
                    spriteBatch.Draw(_SnakeHeadTexture, _Snake[i].Position, null, Color.Blue, 0, Vector2.Zero, 1, SpriteEffects.None, .1f);
                //spriteBatch.Draw(_SnakeHeadTexture, _Snake[i].Position,Color.White);
                else
                    spriteBatch.Draw(_SnakeBodyTexture, _Snake[i].Position, null, Color.Red, 0, Vector2.Zero, 1, SpriteEffects.None, .3f);
                //spriteBatch.Draw(_SnakeBodyTexture, _Snake[i].Position, Color.White);
            }
        }
    }
}

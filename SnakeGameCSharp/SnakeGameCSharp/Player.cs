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
    public class Player
    {
        internal KeyboardState oldState;

        internal Texture2D SnakeTexture;
        internal Texture2D SnakeHead;
        //internal int SnakeLength = 5;
        internal int Score;
        snakePart[] Snake = new snakePart[99];
        internal Vector2 SnakeXSpeed = new Vector2(10, 0);
        internal Vector2 SnakeYSpeed = new Vector2(0, 10);
        internal struct snakePart
        {
            public Vector2 position;
            public int facing;
        };
        BoundingBox BBLeftWall = new BoundingBox(new Vector3(1, 1, 0),new Vector3(9, 599, 0));
        BoundingBox BBTopWall = new BoundingBox(new Vector3(1, 1, 0),new Vector3(599, 9, 0));
        BoundingBox BBRightWall = new BoundingBox(new Vector3(591, 1, 0),new Vector3(599, 599, 0));
        BoundingBox BBBottomWall = new BoundingBox(new Vector3(1, 591, 0),new Vector3(599, 599, 0));
        internal bool first = true;
        internal bool gameover = false;
        internal bool paused = false;
      //internal bool pauseKeyDown = false;
        internal bool pausedForGuide = false;


        public Player()
        {
            StartUp();
            
        }
        public int SnakeLength { get; set; }
        public BoundingBox SnakeHead
        {
            get
            {
            BoundingBox BBSnake = new BoundingBox(new Vector3(Snake[0].position.X + 1, Snake[0].position.Y + 1, 0),
                                                  new Vector3(Snake[0].position.X + 9, Snake[0].position.Y + 9, 0));
            return BBSnake;
            }

        }
       private void StartUp()
        {
            oldState = Keyboard.GetState();
            gameover = false;
            first = true;
            EndPause();
            SnakeLength = 5;
            for (int i = 0; i <= SnakeLength - 1; i++)
            {
                Snake[i].position = new Vector2(120 - (10 * i), 120);
                Snake[i].facing = 3;
            }

           
            paused = false;
            //pauseKeyDown = false;
            pausedForGuide = false;
            
        }

        internal void Update(GameTime gameTime)
        {
            CheckRestart();
            if (!gameover)
            {
            CheckPause();
            CheckSnakeWallCollision();
            //CheckSnakeFoodCollision();
                if (!paused)
                {
            
                
                UpdateInput();

                }

        }
        private void CheckPause()
        {
            KeyboardState newState = Keyboard.GetState();
            if (newState.IsKeyDown(Keys.P))
            {
                // If not down last update, key has just been pressed.
                if (!oldState.IsKeyDown(Keys.P))
                {
                    if (!paused)
                        BeginPause(true);
                    else
                        EndPause();
                }
            }
        }
        private void CheckRestart()
        {

            KeyboardState newState = Keyboard.GetState();
            if (newState.IsKeyDown(Keys.Escape))
            {

                //Exit();

            }
            if (newState.IsKeyDown(Keys.R))
            {
                // If not down last update, key has just been pressed.
                if (!oldState.IsKeyDown(Keys.R))
                {
                   
                    StartUp();
                }
            }
        }
        private void BeginPause(bool UserInitiated)
        {
            paused = true;
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
            paused = false;
            //label[6].visible = false;
        }
        internal void MoveSnake()
        {
            if (Snake[0].facing == 1)
            {
                Snake[0].position -= SnakeXSpeed;
                
            }
            else if (Snake[0].facing == 2)
            {
                Snake[0].position -= SnakeYSpeed;
                
            }
            else if (Snake[0].facing == 3)
            {
                Snake[0].position += SnakeXSpeed;
                
            }
            else if (Snake[0].facing == 4)
            {
                Snake[0].position += SnakeYSpeed;
                

            }
            
            for (int i = SnakeLength - 1; i >= 1; i--)
            {
                Snake[i].position = Snake[i - 1].position;
                Snake[i].facing = Snake[i - 1].facing;
                
            }
        }

        private void UpdateInput()
        {
            KeyboardState newState = Keyboard.GetState();
            // Is the UP key down?
            if (newState.IsKeyDown(Keys.Left))
            {
                // If not down last update, key has just been pressed.
                if (!oldState.IsKeyDown(Keys.Left))
                {
                    Snake[0].facing = 1;//Left
                }
            } if (newState.IsKeyDown(Keys.Up))
            {
                // If not down last update, key has just been pressed.
                if (!oldState.IsKeyDown(Keys.Up))
                {
                    Snake[0].facing = 2;//Up
                }
            }
            if (newState.IsKeyDown(Keys.Right))
            {
                // If not down last update, key has just been pressed.
                if (!oldState.IsKeyDown(Keys.Right))
                {
                    Snake[0].facing = 3;//Right
                }
            }
            if (newState.IsKeyDown(Keys.Down))
            {
                // If not down last update, key has just been pressed.
                if (!oldState.IsKeyDown(Keys.Down))
                {
                    Snake[0].facing = 4;//Down
                }
            } 
            oldState = newState;
        }

        private void CheckSnakeWallCollision()
        {
            ///TODO: check for wall collions against the snake
            ///
            BoundingBox BBSnake = new BoundingBox(new Vector3(Snake[0].position.X+1,Snake[0].position.Y+1,0),
                                                  new Vector3(Snake[0].position.X+9,Snake[0].position.Y+9,0));
            switch (Snake[0].facing)
            {
                case 1:
                    
                    if (BBSnake.Intersects(BBLeftWall))
                    {
                        
                        paused = true;
                        gameover= true;
                        
                    }
                    break;
                case 2:
                    
                    if (BBSnake.Intersects(BBTopWall))
                    {
                        
                        paused = true;
                        gameover = true;
                    }
                    break;
                case 3:
                    
                   
                    if (BBSnake.Intersects(BBRightWall))
                    {
                        
                        paused = true;
                        gameover = true;
                    }
                    break;
                case 4:
                    
                    if (BBSnake.Intersects(BBBottomWall))
                    {
                       
                        paused = true;
                        gameover = true;
                    }
                    break;

            }
        
    }
}

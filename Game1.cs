#region all the code
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
namespace pvpShooter
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        #region variables
        enum GameState
        {
            chooseHealth, chooseMap, chooseMode, mapOne, mapTwo, mapThree, OnePlayer
        }

        GameState state = GameState.chooseHealth;

        bool goRight = false;
        bool goLeft = false;
        int shotDelay;
        bool mode = true;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        KeyboardState keys, oldkeys;
        Rectangle ground = new Rectangle(0, 650, 1200, 50);
        Rectangle PlatformOne = new Rectangle(200, 420, 800, 50);
        Rectangle PlatformTwo = new Rectangle(0, 200, 200, 50);
        Rectangle PlatformThree = new Rectangle(1000, 200, 200, 50);
        Rectangle PlatformFour = new Rectangle(1000, 200, 200, 50);
        bool blueWin = false;
        bool redWin = false;
        int timeToHealthSpawn = 300;
        bool healthSpawn = false;
        Rectangle healthRect = new Rectangle(500, 100, 100, 100);
        int OriginalHealth = 250;
        int mapSelection = 1;
        bool platformRight = true;

        Rectangle PlayerOneRect = new Rectangle(0, 400, 150, 150);
        bool PlayerOneRight = true;
        bool PlayerOneFalling = true;
        int PlayerOneJumpTimer = 0;
        bool PlayerOneTouchingGround = false;
        Rectangle PlayerOneBullet;
        bool PlayerOneBulletRight;
        bool PlayerOneBulletFired;
        int PlayerOneHealth = 500;
        int PlayerOneDashCD = 0;

        Rectangle PlayerTwoRect = new Rectangle(1100, 400, 150, 150);
        bool PlayerTwoRight = false;
        bool PlayerTwoFalling = true;
        int PlayerTwoJumpTimer = 0;
        bool PlayerTwoTouchingGround = false;
        Rectangle PlayerTwoBullet;
        bool PlayerTwoBulletRight;
        bool PlayerTwoBulletFired = false;
        int PlayerTwoHealth = 500;
        int PlayerTwoDashCD = 0;

        Rectangle PlayerOneGrenade;
        Rectangle PlayerTwoGrenade;
        bool PlayerOneGrenadeDrawing = false;
        bool PlayerTwoGrenadeDrawing = false;
        int GrenadeOneTimer = 0;
        int GrenadeTwoTimer = 0;
        int PlayerOneGrenadeCount = 5;
        int PlayerTwoGrenadeCount = 5;

        #endregion
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1200;
            graphics.PreferredBackBufferHeight = 700;
        }
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            keys = Keyboard.GetState();

            switch (state)
            #region gameState
            {
                case GameState.chooseHealth:
                    UpdateChooseHealth();
                    break;
                case GameState.chooseMap:
                    UpdateChooseMap();
                    break;
                case GameState.chooseMode:
                    UpdateChooseMode();
                    break;
                case GameState.mapOne:
                    UpdateMapOne();
                    break;
                case GameState.mapTwo:
                    UpdateMapTwo();
                    break;
                case GameState.mapThree:
                    UpdateMapThree();
                    break;
                case GameState.OnePlayer:
                    UpdateOnePlayer();
                    break;
            }
            #endregion
            oldkeys = keys;

            base.Update(gameTime);
        }
        private void UpdateChooseHealth()
        {
            GraphicsDevice.Clear(Color.DarkGray);

            if (keys.IsKeyDown(Keys.Space) && oldkeys.IsKeyUp(Keys.Space))
            {
                #region changing health
                if (OriginalHealth == 100)
                {
                    OriginalHealth = 150;
                }
                else if (OriginalHealth == 150)
                {
                    OriginalHealth = 200;
                }
                else if (OriginalHealth == 200)
                {
                    OriginalHealth = 250;
                }
                else if (OriginalHealth == 250)
                {
                    OriginalHealth = 300;
                }
                else if (OriginalHealth == 300)
                {
                    OriginalHealth = 350;
                }
                else if (OriginalHealth == 350)
                {
                    OriginalHealth = 400;
                }
                else if (OriginalHealth == 400)
                {
                    OriginalHealth = 450;
                }
                else if (OriginalHealth == 450)
                {
                    OriginalHealth = 500;
                }
                else if (OriginalHealth == 500)
                {
                    OriginalHealth = 100;
                }
                #endregion
            }

            if (keys.IsKeyDown(Keys.Enter) && oldkeys.IsKeyUp(Keys.Enter))
            {
                state = GameState.chooseMode;
                PlayerOneHealth = OriginalHealth;
                PlayerTwoHealth = OriginalHealth;
            }

            spriteBatch.Begin();

            spriteBatch.DrawString(Content.Load<SpriteFont>("font"), "PRESS SPACE TO SWITCH AND ENTER TO CHOOSE", new Vector2(0, 0), Color.Black);
            spriteBatch.DrawString(Content.Load<SpriteFont>("bigfont"), OriginalHealth.ToString(), new Vector2(450, 200), Color.Black);
            spriteBatch.DrawString(Content.Load<SpriteFont>("font"), "Remember that each bullet does 25 damage", new Vector2(0, 400), Color.Black);
            spriteBatch.DrawString(Content.Load<SpriteFont>("font"), "and health packs give back 50 health", new Vector2(0, 450), Color.Black);
            
            spriteBatch.End();
        }
        private void UpdateChooseMap()
        {
            if (keys.IsKeyDown(Keys.Space) && oldkeys.IsKeyUp(Keys.Space))
            {
                if (mapSelection == 1)
                {
                    mapSelection = 2;
                }
                else if (mapSelection == 2)
                {
                    mapSelection = 3;
                }
                else if (mapSelection == 3)
                {
                    mapSelection = 1;
                }
            }

            spriteBatch.Begin();

            GraphicsDevice.Clear(Color.DarkGray);

            spriteBatch.DrawString(Content.Load<SpriteFont>("font"), "PRESS SPACE TO SWITCH AND ENTER TO CHOOSE", new Vector2(0, 0), Color.Black);
            
            if (mapSelection == 1)
            {
                spriteBatch.Draw(Content.Load<Texture2D>("mapONe"), new Rectangle(300, 100, 600, 500), Color.White);
                spriteBatch.DrawString(Content.Load<SpriteFont>("font"), "Warehouse", new Vector2(475, 600), Color.Black);
            }

            if (mapSelection == 2)
            {
                spriteBatch.Draw(Content.Load<Texture2D>("mapTWo"), new Rectangle(300, 100, 600, 500), Color.White);
                spriteBatch.DrawString(Content.Load<SpriteFont>("font"), "Farmhouse", new Vector2(475, 600), Color.Black);
            }

            if (mapSelection == 3)
            {
                spriteBatch.Draw(Content.Load<Texture2D>("mapTHree"), new Rectangle(300, 100, 600, 500), Color.White);
                spriteBatch.DrawString(Content.Load<SpriteFont>("font"), "Rooftops", new Vector2(475, 600), Color.Black);
            }

            if (keys.IsKeyDown(Keys.Enter) && oldkeys.IsKeyUp(Keys.Enter))
            {
                if (mapSelection == 1)
                {
                    state = GameState.mapOne;
                    Content.Load<SoundEffect>("eran game music").Play();
                }
                if (mapSelection == 2)
                {
                    state = GameState.mapTwo;
                    PlatformOne = new Rectangle(450, 420, 300, 50);
                    Content.Load<SoundEffect>("eran game music").Play();
                }
                if (mapSelection == 3)
                {
                    state = GameState.mapThree;
                    ground = new Rectangle(0, 650, 300, 50);
                    PlatformOne = new Rectangle(900, 650, 300, 50);
                    PlatformTwo = new Rectangle(400, 420, 400, 50);
                    PlatformThree = new Rectangle(0, 200, 200, 50);
                    Content.Load<SoundEffect>("eran game music").Play();
                }
            }
            spriteBatch.End();
        }
        private void UpdateChooseMode()
        {
            if (keys.IsKeyDown(Keys.Space) && oldkeys.IsKeyUp(Keys.Space))
            {
                if (mode)
                {
                    mode = false;
                }
                else
                {
                    mode = true;
                }
            }
            if (keys.IsKeyDown(Keys.Enter) && oldkeys.IsKeyUp(Keys.Enter))
            {
                if (mode)
                {
                    state = GameState.chooseMap;
                }
                else
                {
                    state = GameState.OnePlayer;
                }
            }
            spriteBatch.Begin();

            GraphicsDevice.Clear(Color.LightGray);

            if (mode)
            {
                spriteBatch.DrawString(Content.Load<SpriteFont>("bigFont"), "2 Players", new Vector2(300, 250), Color.Black);
            }
            if (!mode)
            {
                spriteBatch.DrawString(Content.Load<SpriteFont>("bigFont"), "1 Player", new Vector2(300, 250), Color.Black);
            }
            spriteBatch.End();
        }
        private void UpdateMapOne()
        {
            if (!blueWin && !redWin)
            {
                #region movement
                if (keys.IsKeyDown(Keys.Right) && PlayerOneRect.X < 1100)
                {
                    PlayerOneRect.X += 7;
                    PlayerOneRight = true;
                }
                if (keys.IsKeyDown(Keys.Left) && PlayerOneRect.X > 0)
                {
                    PlayerOneRect.X -= 7;
                    PlayerOneRight = false;
                }

                if (keys.IsKeyDown(Keys.D) && PlayerTwoRect.X < 1100)
                {
                    PlayerTwoRect.X += 7;
                    PlayerTwoRight = true;
                }
                if (keys.IsKeyDown(Keys.A) && PlayerTwoRect.X > 0)
                {
                    PlayerTwoRect.X -= 7;
                    PlayerTwoRight = false;
                }
                if (keys.IsKeyDown(Keys.Down) && oldkeys.IsKeyUp(Keys.Down) && PlayerOneTouchingGround && !PlayerOneRect.Intersects(ground))
                {
                    PlayerOneRect.Y += 5;
                }
                if (keys.IsKeyDown(Keys.S) && oldkeys.IsKeyUp(Keys.S) && PlayerTwoTouchingGround && !PlayerTwoRect.Intersects(ground))
                {
                    PlayerTwoRect.Y += 5;
                }

                if (keys.IsKeyDown(Keys.M) && oldkeys.IsKeyUp(Keys.M) && PlayerOneDashCD == 0)
                {
                    if (PlayerOneRight && PlayerOneRect.X < 1000)
                    {
                        PlayerOneRect.X += 250;
                        PlayerOneDashCD = 120;
                    }
                    else if (PlayerOneRect.X > 200 && !PlayerOneRight)
                    {
                        PlayerOneRect.X -= 250;
                        PlayerOneDashCD = 120;
                    }
                }
                if (PlayerOneDashCD > 0)
                {
                    PlayerOneDashCD--;
                }

                if (keys.IsKeyDown(Keys.T) && oldkeys.IsKeyUp(Keys.T) && PlayerTwoDashCD == 0)
                {
                    if (PlayerTwoRight && PlayerTwoRect.X < 1000)
                    {
                        PlayerTwoRect.X += 250;
                        PlayerTwoDashCD = 120;
                    }
                    else if (PlayerTwoRect.X > 200 && !PlayerTwoRight)
                    {
                        PlayerTwoRect.X -= 250;
                        PlayerTwoDashCD = 120;
                    }
                }
                if (PlayerTwoDashCD > 0)
                {
                    PlayerTwoDashCD--;
                }
                #endregion
                #region falling
                if (PlayerOneFalling)
                {
                    PlayerOneRect.Y += 5;
                }
                if (ground.Intersects(PlayerOneRect) && PlayerOneRect.Y <= ground.Y)
                {
                    PlayerOneFalling = false;
                    PlayerOneTouchingGround = true;
                }
                else if (PlatformOne.Intersects(PlayerOneRect) && PlayerOneRect.Y + 145 <= PlatformOne.Y)
                {
                    PlayerOneFalling = false;
                    PlayerOneTouchingGround = true;
                }
                else if (PlatformTwo.Intersects(PlayerOneRect) && PlayerOneRect.Y + 145 <= PlatformTwo.Y)
                {
                    PlayerOneFalling = false;
                    PlayerOneTouchingGround = true;
                }
                else if (PlatformThree.Intersects(PlayerOneRect) && PlayerOneRect.Y + 145 <= PlatformThree.Y)
                {
                    PlayerOneFalling = false;
                    PlayerOneTouchingGround = true;
                }
                else
                {
                    PlayerOneTouchingGround = false;
                }
                if (!PlayerOneTouchingGround && PlayerOneJumpTimer == 0)
                {
                    PlayerOneFalling = true;
                }

                if (PlayerTwoFalling)
                {
                    PlayerTwoRect.Y += 5;
                }
                if (ground.Intersects(PlayerTwoRect) && PlayerTwoRect.Y <= ground.Y)
                {
                    PlayerTwoFalling = false;
                    PlayerTwoTouchingGround = true;
                }
                else if (PlatformOne.Intersects(PlayerTwoRect) && PlayerTwoRect.Y + 145 <= PlatformOne.Y)
                {
                    PlayerTwoFalling = false;
                    PlayerTwoTouchingGround = true;
                }
                else if (PlatformTwo.Intersects(PlayerTwoRect) && PlayerTwoRect.Y + 145 <= PlatformTwo.Y)
                {
                    PlayerTwoFalling = false;
                    PlayerTwoTouchingGround = true;
                }
                else if (PlatformThree.Intersects(PlayerTwoRect) && PlayerTwoRect.Y + 145 <= PlatformThree.Y)
                {
                    PlayerTwoFalling = false;
                    PlayerTwoTouchingGround = true;
                }
                else
                {
                    PlayerTwoTouchingGround = false;
                }
                if (!PlayerTwoTouchingGround && PlayerTwoJumpTimer == 0)
                {
                    PlayerTwoFalling = true;
                }
                #endregion
                #region jumping
                if (keys.IsKeyDown(Keys.Up) && oldkeys.IsKeyUp(Keys.Up) && !PlayerOneFalling && PlayerOneTouchingGround)
                {
                    PlayerOneJumpTimer = 50;
                }
                if (PlayerOneJumpTimer > 0)
                {
                    PlayerOneRect.Y -= 5;
                    PlayerOneJumpTimer--;
                    if (PlayerOneJumpTimer == 0)
                    {
                        PlayerOneFalling = true;
                    }
                }

                if (keys.IsKeyDown(Keys.W) && oldkeys.IsKeyUp(Keys.W) && !PlayerTwoFalling && PlayerTwoTouchingGround)
                {
                    PlayerTwoJumpTimer = 50;
                }
                if (PlayerTwoJumpTimer > 0)
                {
                    PlayerTwoRect.Y -= 5;
                    PlayerTwoJumpTimer--;
                    if (PlayerTwoJumpTimer == 0)
                    {
                        PlayerTwoFalling = true;
                    }
                }
                #endregion
                #region shooting
                if (keys.IsKeyDown(Keys.Space) && oldkeys.IsKeyUp(Keys.Space) && !PlayerOneBulletFired)
                {
                    PlayerOneBulletFired = true;
                    Content.Load<SoundEffect>("gunshot").Play();
                    if (PlayerOneRight)
                    {
                        PlayerOneBulletRight = true;
                        PlayerOneBullet = new Rectangle(PlayerOneRect.X + 150, PlayerOneRect.Y + 85, 50, 5);
                    }
                    else
                    {
                        PlayerOneBulletRight = false;
                        PlayerOneBullet = new Rectangle(PlayerOneRect.X - 50, PlayerOneRect.Y + 85, 50, 5);
                    }
                }
                if (PlayerOneBulletFired)
                {
                    if (PlayerOneBulletRight)
                    {
                        PlayerOneBullet.X += 25;
                    }
                    else
                    {
                        PlayerOneBullet.X -= 25;
                    }

                    if (PlayerOneBullet.Intersects(PlayerTwoRect))
                    {
                        PlayerOneBulletFired = false;
                        PlayerTwoHealth -= 25;
                        if (PlayerTwoHealth == 0)
                        {
                            blueWin = true;
                        }
                    }
                    if (PlayerOneBullet.X >= 1200 || PlayerOneBullet.X <= -50)
                    {
                        PlayerOneBulletFired = false;
                    }
                }

                if (keys.IsKeyDown(Keys.R) && oldkeys.IsKeyUp(Keys.R) && !PlayerTwoBulletFired)
                {
                    PlayerTwoBulletFired = true;
                    Content.Load<SoundEffect>("gunshot").Play();
                    if (PlayerTwoRight)
                    {
                        PlayerTwoBulletRight = true;
                        PlayerTwoBullet = new Rectangle(PlayerTwoRect.X + 150, PlayerTwoRect.Y + 100, 50, 5);
                    }
                    else
                    {
                        PlayerTwoBulletRight = false;
                        PlayerTwoBullet = new Rectangle(PlayerTwoRect.X - 50, PlayerTwoRect.Y + 100, 50, 5);
                    }
                }
                if (PlayerTwoBulletFired)
                {
                    if (PlayerTwoBulletRight)
                    {
                        PlayerTwoBullet.X += 25;
                    }
                    else
                    {
                        PlayerTwoBullet.X -= 25;
                    }

                    if (PlayerTwoBullet.Intersects(PlayerOneRect))
                    {
                        PlayerTwoBulletFired = false;
                        PlayerOneHealth -= 25;
                        if (PlayerOneHealth == 0)
                        {
                            redWin = true;
                        }
                    }
                    if (PlayerTwoBullet.X >= 1200 || PlayerTwoBullet.X <= -50)
                    {
                        PlayerTwoBulletFired = false;
                    }
                }
                #endregion
                #region health
                if (timeToHealthSpawn > 0 && !healthSpawn)
                {
                    timeToHealthSpawn--;
                }
                if (timeToHealthSpawn == 0)
                {
                    healthSpawn = true;
                }
                if (healthSpawn)
                {
                    if (PlayerOneRect.Intersects(healthRect) && PlayerOneHealth <= OriginalHealth - 50)
                    {
                        PlayerOneHealth += 50;
                        timeToHealthSpawn = 300;
                        healthSpawn = false;
                        PlayerOneGrenadeCount += 3;
                    }
                    else if (PlayerOneRect.Intersects(healthRect))
                    {
                        healthSpawn = false;
                        timeToHealthSpawn = 300;
                        PlayerOneGrenadeCount += 3;
                    }
                    if (PlayerTwoRect.Intersects(healthRect) && PlayerTwoHealth <= OriginalHealth - 50)
                    {
                        PlayerTwoHealth += 50;
                        timeToHealthSpawn = 300;
                        healthSpawn = false;
                        PlayerTwoGrenadeCount += 3;
                    }
                    else if (PlayerTwoRect.Intersects(healthRect))
                    {
                        healthSpawn = false;
                        timeToHealthSpawn = 300;
                        PlayerTwoGrenadeCount += 3;
                    }
                }
                #endregion
                #region grenades
                if (GrenadeOneTimer > 0)
                {
                    PlayerOneGrenadeDrawing = true;
                    GrenadeOneTimer--;
                }
                if (GrenadeOneTimer == 10)
                {
                    PlayerOneGrenade.X -= 75;
                    PlayerOneGrenade.Y -= 75;
                }
                else if (GrenadeOneTimer < 1)
                {
                    PlayerOneGrenadeDrawing = false;
                }
                if (keys.IsKeyDown(Keys.N) && oldkeys.IsKeyUp(Keys.N) && PlayerOneGrenadeDrawing == false && PlayerOneGrenadeCount > 0)
                {
                    GrenadeOneTimer = 130;
                    PlayerOneGrenade = new Rectangle(PlayerOneRect.X, PlayerOneRect.Y, 25, 25);
                    PlayerOneGrenadeDrawing = true;
                }

                if (GrenadeTwoTimer > 0)
                {
                    PlayerTwoGrenadeDrawing = true;
                    GrenadeTwoTimer--;
                }
                if (GrenadeTwoTimer == 10)
                {
                    PlayerTwoGrenade.X -= 75;
                    PlayerTwoGrenade.Y -= 75;
                }
                else if (GrenadeTwoTimer < 1)
                {
                    PlayerTwoGrenadeDrawing = false;
                }
                if (keys.IsKeyDown(Keys.Y) && oldkeys.IsKeyUp(Keys.Y) && PlayerTwoGrenadeDrawing == false && PlayerTwoGrenadeCount > 0)
                {
                    GrenadeTwoTimer = 130;
                    PlayerTwoGrenade = new Rectangle(PlayerTwoRect.X, PlayerTwoRect.Y, 25, 25);
                    PlayerTwoGrenadeDrawing = true;
                }

                if (PlayerOneRect.Intersects(PlayerTwoGrenade) && GrenadeTwoTimer > 0 && GrenadeTwoTimer < 11)
                {
                    PlayerOneHealth -= 75;
                    GrenadeTwoTimer = 0;
                }
                if (PlayerTwoRect.Intersects(PlayerOneGrenade) && GrenadeOneTimer > 0 && GrenadeOneTimer < 11)
                {
                    PlayerTwoHealth -= 75;
                    GrenadeOneTimer = 0;
                }
                #endregion
            }
            if (blueWin || redWin)
            {
                #region restart
                if (keys.IsKeyDown(Keys.Enter))
                {
                    blueWin = false;
                    redWin = false;
                    PlayerOneHealth = OriginalHealth;
                    PlayerTwoHealth = OriginalHealth;

                    PlayerOneRect = new Rectangle(0, 400, 150, 150);
                    PlayerOneRight = true;
                    PlayerOneFalling = true;
                    PlayerOneJumpTimer = 0;
                    PlayerOneTouchingGround = false;

                    PlayerTwoRect = new Rectangle(1100, 400, 150, 150);
                    PlayerTwoRight = false;
                    PlayerTwoFalling = true;
                    PlayerTwoJumpTimer = 0;
                    PlayerTwoTouchingGround = false;

                    PlayerOneGrenadeCount = 5;
                    PlayerTwoGrenadeCount = 5;
                }
                #endregion
            }
            
            #region draw
            spriteBatch.Begin();

            spriteBatch.Draw(Content.Load<Texture2D>("background"), new Rectangle(0, 0, 1200, 700), Color.White);

            spriteBatch.DrawString(Content.Load <SpriteFont>("font"), PlayerOneGrenadeCount.ToString(), new Vector2(0, 100), Color.Black);
            spriteBatch.DrawString(Content.Load<SpriteFont>("font"), PlayerTwoGrenadeCount.ToString(), new Vector2(1150, 100), Color.Black);

            if (PlayerOneHealth < 0)
            {
                redWin = true;
            }
            if (PlayerTwoHealth < 0)
            {
                blueWin = true;
            }

            if (PlayerOneRight)
            {
                spriteBatch.Draw(Content.Load<Texture2D>("pigFacingLeft"), PlayerOneRect, Color.Khaki);
            }
            else
            {
                spriteBatch.Draw(Content.Load<Texture2D>("pigFacingRight"), PlayerOneRect, Color.Khaki);
            }

            if (PlayerTwoRight)
            {
                spriteBatch.Draw(Content.Load<Texture2D>("pigFacingLeft"), PlayerTwoRect, Color.DarkSalmon);
            }
            else
            {
                spriteBatch.Draw(Content.Load<Texture2D>("pigFacingRight"), PlayerTwoRect, Color.DarkSalmon);
            }

            spriteBatch.Draw(Content.Load<Texture2D>("stuff"), ground, Color.White);
            spriteBatch.Draw(Content.Load<Texture2D>("stuff"), PlatformOne, Color.White);
            spriteBatch.Draw(Content.Load<Texture2D>("stuff"), PlatformTwo, Color.White);
            spriteBatch.Draw(Content.Load<Texture2D>("stuff"), PlatformThree, Color.White);

            if (PlayerOneBulletFired)
            {
                spriteBatch.Draw(Content.Load<Texture2D>("bullet"), PlayerOneBullet, Color.White);
            }

            if (PlayerTwoBulletFired)
            {
                spriteBatch.Draw(Content.Load<Texture2D>("bullet"), PlayerTwoBullet, Color.White);
            }

            spriteBatch.Draw(Content.Load<Texture2D>("blue"), new Rectangle(0, 0, PlayerOneHealth, 50), Color.White);
            spriteBatch.Draw(Content.Load<Texture2D>("red"), new Rectangle(700, 0, PlayerTwoHealth, 50), Color.White);

            if (blueWin)
            {
                spriteBatch.DrawString(Content.Load<SpriteFont>("font"), "BLUE WINS", new Vector2(500, 300), Color.Black);
            }
            if (redWin)
            {
                spriteBatch.DrawString(Content.Load<SpriteFont>("font"), "RED WINS", new Vector2(500, 300), Color.Black);
            }

            if (healthSpawn)
            {
                spriteBatch.Draw(Content.Load<Texture2D>("pack"), healthRect, Color.White);
            }

                if (GrenadeOneTimer > 10)
                {
                    PlayerOneGrenade = new Rectangle(PlayerOneGrenade.X, PlayerOneGrenade.Y, 50, 50);
                    spriteBatch.Draw(Content.Load<Texture2D>("grenade"), PlayerOneGrenade, Color.White);
                }
                else if (GrenadeOneTimer < 1)
                {
                    PlayerOneGrenade.X = 2000;
                }
                else if (GrenadeOneTimer < 11)
                {
                    PlayerOneGrenade = new Rectangle(PlayerOneGrenade.X, PlayerOneGrenade.Y, 200, 200);
                    spriteBatch.Draw(Content.Load<Texture2D>("explosionSprite"), PlayerOneGrenade, Color.White);
                }

                if (GrenadeTwoTimer > 10)
                {
                    PlayerTwoGrenade = new Rectangle(PlayerTwoGrenade.X, PlayerTwoGrenade.Y, 50, 50);
                    spriteBatch.Draw(Content.Load<Texture2D>("grenade"), PlayerTwoGrenade, Color.White);
                }
                else if (GrenadeTwoTimer < 1)
                {
                    PlayerTwoGrenade.X = 2000;
                }
                else if (GrenadeTwoTimer < 11)
                {
                    PlayerTwoGrenade = new Rectangle(PlayerTwoGrenade.X, PlayerTwoGrenade.Y, 200, 200);
                    spriteBatch.Draw(Content.Load<Texture2D>("explosionSprite"), PlayerTwoGrenade, Color.White);
                }

            spriteBatch.End();

            #endregion
        }
        private void UpdateMapTwo()
        {
            if (!blueWin && !redWin)
            {
                #region movement
                if (keys.IsKeyDown(Keys.Right) && PlayerOneRect.X < 1100)
                {
                    PlayerOneRect.X += 7;
                    PlayerOneRight = true;
                }
                if (keys.IsKeyDown(Keys.Left) && PlayerOneRect.X > 0)
                {
                    PlayerOneRect.X -= 7;
                    PlayerOneRight = false;
                }

                if (keys.IsKeyDown(Keys.D) && PlayerTwoRect.X < 1100)
                {
                    PlayerTwoRect.X += 7;
                    PlayerTwoRight = true;
                }
                if (keys.IsKeyDown(Keys.A) && PlayerTwoRect.X > 0)
                {
                    PlayerTwoRect.X -= 7;
                    PlayerTwoRight = false;
                }
                if (keys.IsKeyDown(Keys.Down) && oldkeys.IsKeyUp(Keys.Down) && PlayerOneTouchingGround && !PlayerOneRect.Intersects(ground))
                {
                    PlayerOneRect.Y += 5;
                }
                if (keys.IsKeyDown(Keys.S) && oldkeys.IsKeyUp(Keys.S) && PlayerTwoTouchingGround && !PlayerTwoRect.Intersects(ground))
                {
                    PlayerTwoRect.Y += 5;
                }

                if (keys.IsKeyDown(Keys.M) && oldkeys.IsKeyUp(Keys.M) && PlayerOneDashCD == 0)
                {
                    if (PlayerOneRight && PlayerOneRect.X < 1000)
                    {
                        PlayerOneRect.X += 250;
                        PlayerOneDashCD = 120;
                    }
                    else if (PlayerOneRect.X > 200 && !PlayerOneRight)
                    {
                        PlayerOneRect.X -= 250;
                        PlayerOneDashCD = 120;
                    }
                }
                if (PlayerOneDashCD > 0)
                {
                    PlayerOneDashCD--;
                }

                if (keys.IsKeyDown(Keys.T) && oldkeys.IsKeyUp(Keys.T) && PlayerTwoDashCD == 0)
                {
                    if (PlayerTwoRight && PlayerTwoRect.X < 1000)
                    {
                        PlayerTwoRect.X += 250;
                        PlayerTwoDashCD = 120;
                    }
                    else if (PlayerTwoRect.X > 200 && !PlayerTwoRight)
                    {
                        PlayerTwoRect.X -= 250;
                        PlayerTwoDashCD = 120;
                    }
                }
                if (PlayerTwoDashCD > 0)
                {
                    PlayerTwoDashCD--;
                }
                #endregion
                #region falling
                if (PlayerOneFalling)
                {
                    PlayerOneRect.Y += 5;
                }
                if (ground.Intersects(PlayerOneRect) && PlayerOneRect.Y <= ground.Y)
                {
                    PlayerOneFalling = false;
                    PlayerOneTouchingGround = true;
                }
                else if (PlatformOne.Intersects(PlayerOneRect) && PlayerOneRect.Y + 145 <= PlatformOne.Y)
                {
                    PlayerOneFalling = false;
                    PlayerOneTouchingGround = true;
                    if (platformRight && !keys.IsKeyDown(Keys.Right) && !keys.IsKeyDown(Keys.Left))
                    {
                        PlayerOneRect.X += 5;
                    }
                    if (!platformRight && !keys.IsKeyDown(Keys.Right) && !keys.IsKeyDown(Keys.Left))
                    {
                        PlayerOneRect.X -= 5;
                    }
                }
                else if (PlatformTwo.Intersects(PlayerOneRect) && PlayerOneRect.Y + 145 <= PlatformTwo.Y)
                {
                    PlayerOneFalling = false;
                    PlayerOneTouchingGround = true;
                }
                else if (PlatformThree.Intersects(PlayerOneRect) && PlayerOneRect.Y + 145 <= PlatformThree.Y)
                {
                    PlayerOneFalling = false;
                    PlayerOneTouchingGround = true;
                }
                else
                {
                    PlayerOneTouchingGround = false;
                }
                if (!PlayerOneTouchingGround && PlayerOneJumpTimer == 0)
                {
                    PlayerOneFalling = true;
                }

                if (PlayerTwoFalling)
                {
                    PlayerTwoRect.Y += 5;
                }
                if (ground.Intersects(PlayerTwoRect) && PlayerTwoRect.Y <= ground.Y)
                {
                    PlayerTwoFalling = false;
                    PlayerTwoTouchingGround = true;
                }
                else if (PlatformOne.Intersects(PlayerTwoRect) && PlayerTwoRect.Y + 145 <= PlatformOne.Y)
                {
                    PlayerTwoFalling = false;
                    PlayerTwoTouchingGround = true;
                    if (platformRight && !keys.IsKeyDown(Keys.A) && !keys.IsKeyDown(Keys.D))
                    {
                        PlayerTwoRect.X += 5;
                    }
                    if (!platformRight && !keys.IsKeyDown(Keys.A) && !keys.IsKeyDown(Keys.D))
                    {
                        PlayerTwoRect.X -= 5;
                    }
                }
                else if (PlatformTwo.Intersects(PlayerTwoRect) && PlayerTwoRect.Y + 145 <= PlatformTwo.Y)
                {
                    PlayerTwoFalling = false;
                    PlayerTwoTouchingGround = true;
                }
                else if (PlatformThree.Intersects(PlayerTwoRect) && PlayerTwoRect.Y + 145 <= PlatformThree.Y)
                {
                    PlayerTwoFalling = false;
                    PlayerTwoTouchingGround = true;
                }
                else
                {
                    PlayerTwoTouchingGround = false;
                }
                if (!PlayerTwoTouchingGround && PlayerTwoJumpTimer == 0)
                {
                    PlayerTwoFalling = true;
                }
                #endregion
                #region jumping
                if (keys.IsKeyDown(Keys.Up) && oldkeys.IsKeyUp(Keys.Up) && !PlayerOneFalling && PlayerOneTouchingGround)
                {
                    PlayerOneJumpTimer = 50;
                }
                if (PlayerOneJumpTimer > 0)
                {
                    PlayerOneRect.Y -= 5;
                    PlayerOneJumpTimer--;
                    if (PlayerOneJumpTimer == 0)
                    {
                        PlayerOneFalling = true;
                    }
                }

                if (keys.IsKeyDown(Keys.W) && oldkeys.IsKeyUp(Keys.W) && !PlayerTwoFalling && PlayerTwoTouchingGround)
                {
                    PlayerTwoJumpTimer = 50;
                }
                if (PlayerTwoJumpTimer > 0)
                {
                    PlayerTwoRect.Y -= 5;
                    PlayerTwoJumpTimer--;
                    if (PlayerTwoJumpTimer == 0)
                    {
                        PlayerTwoFalling = true;
                    }
                }
                #endregion
                #region shooting
                if (keys.IsKeyDown(Keys.Space) && oldkeys.IsKeyUp(Keys.Space) && !PlayerOneBulletFired)
                {
                    PlayerOneBulletFired = true;
                    Content.Load<SoundEffect>("gunshot").Play();
                    if (PlayerOneRight)
                    {
                        PlayerOneBulletRight = true;
                        PlayerOneBullet = new Rectangle(PlayerOneRect.X + 150, PlayerOneRect.Y + 85, 50, 5);
                    }
                    else
                    {
                        PlayerOneBulletRight = false;
                        PlayerOneBullet = new Rectangle(PlayerOneRect.X - 50, PlayerOneRect.Y + 85, 50, 5);
                    }
                }
                if (PlayerOneBulletFired)
                {
                    if (PlayerOneBulletRight)
                    {
                        PlayerOneBullet.X += 25;
                    }
                    else
                    {
                        PlayerOneBullet.X -= 25;
                    }

                    if (PlayerOneBullet.Intersects(PlayerTwoRect))
                    {
                        PlayerOneBulletFired = false;
                        PlayerTwoHealth -= 25;
                        if (PlayerTwoHealth == 0)
                        {
                            blueWin = true;
                        }
                    }
                    if (PlayerOneBullet.X >= 1200 || PlayerOneBullet.X <= -50)
                    {
                        PlayerOneBulletFired = false;
                    }
                }

                if (keys.IsKeyDown(Keys.R) && oldkeys.IsKeyUp(Keys.R) && !PlayerTwoBulletFired)
                {
                    PlayerTwoBulletFired = true;
                    Content.Load<SoundEffect>("gunshot").Play();
                    if (PlayerTwoRight)
                    {
                        PlayerTwoBulletRight = true;
                        PlayerTwoBullet = new Rectangle(PlayerTwoRect.X + 150, PlayerTwoRect.Y + 100, 50, 5);
                    }
                    else
                    {
                        PlayerTwoBulletRight = false;
                        PlayerTwoBullet = new Rectangle(PlayerTwoRect.X - 50, PlayerTwoRect.Y + 100, 50, 5);
                    }
                }
                if (PlayerTwoBulletFired)
                {
                    if (PlayerTwoBulletRight)
                    {
                        PlayerTwoBullet.X += 25;
                    }
                    else
                    {
                        PlayerTwoBullet.X -= 25;
                    }

                    if (PlayerTwoBullet.Intersects(PlayerOneRect))
                    {
                        PlayerTwoBulletFired = false;
                        PlayerOneHealth -= 25;
                        if (PlayerOneHealth == 0)
                        {
                            redWin = true;
                        }
                    }
                    if (PlayerTwoBullet.X >= 1200 || PlayerTwoBullet.X <= -50)
                    {
                        PlayerTwoBulletFired = false;
                    }
                }
                #endregion
                #region health
                if (timeToHealthSpawn > 0 && !healthSpawn)
                {
                    timeToHealthSpawn--;
                }
                if (timeToHealthSpawn == 0)
                {
                    healthSpawn = true;
                }
                if (healthSpawn)
                {
                    if (PlayerOneRect.Intersects(healthRect) && PlayerOneHealth <= OriginalHealth - 50)
                    {
                        PlayerOneHealth += 50;
                        timeToHealthSpawn = 300;
                        healthSpawn = false;
                        PlayerOneGrenadeCount += 3;
                    }
                    else if (PlayerOneRect.Intersects(healthRect))
                    {
                        healthSpawn = false;
                        timeToHealthSpawn = 300;
                        PlayerOneGrenadeCount += 3;
                    }
                    if (PlayerTwoRect.Intersects(healthRect) && PlayerTwoHealth <= OriginalHealth - 50)
                    {
                        PlayerTwoHealth += 50;
                        timeToHealthSpawn = 300;
                        healthSpawn = false;
                        PlayerTwoGrenadeCount += 3;
                    }
                    else if (PlayerTwoRect.Intersects(healthRect))
                    {
                        healthSpawn = false;
                        timeToHealthSpawn = 300;
                        PlayerTwoGrenadeCount += 3;
                    }
                }
                #endregion
                #region moving platform
                if (platformRight)
                {
                    PlatformOne.X += 5;
                }
                else if (!platformRight)
                {
                    PlatformOne.X -= 5;
                }
                if (PlatformOne.X == 900)
                {
                    platformRight = false;
                }
                if (PlatformOne.X == 0)
                {
                    platformRight = true;
                }
                #endregion
                #region grenades
                if (GrenadeOneTimer > 0)
                {
                    PlayerOneGrenadeDrawing = true;
                    GrenadeOneTimer--;
                }
                if (GrenadeOneTimer == 10)
                {
                    PlayerOneGrenade.X -= 75;
                    PlayerOneGrenade.Y -= 75;
                }
                else if (GrenadeOneTimer < 1)
                {
                    PlayerOneGrenadeDrawing = false;
                }
                if (keys.IsKeyDown(Keys.N) && oldkeys.IsKeyUp(Keys.N) && PlayerOneGrenadeDrawing == false && PlayerOneGrenadeCount > 0)
                {
                    GrenadeOneTimer = 130;
                    PlayerOneGrenade = new Rectangle(PlayerOneRect.X, PlayerOneRect.Y, 25, 25);
                    PlayerOneGrenadeDrawing = true;
                }

                if (GrenadeTwoTimer > 0)
                {
                    PlayerTwoGrenadeDrawing = true;
                    GrenadeTwoTimer--;
                }
                if (GrenadeTwoTimer == 10)
                {
                    PlayerTwoGrenade.X -= 75;
                    PlayerTwoGrenade.Y -= 75;
                }
                else if (GrenadeTwoTimer < 1)
                {
                    PlayerTwoGrenadeDrawing = false;
                }
                if (keys.IsKeyDown(Keys.Y) && oldkeys.IsKeyUp(Keys.Y) && PlayerTwoGrenadeDrawing == false && PlayerTwoGrenadeCount > 0)
                {
                    GrenadeTwoTimer = 130;
                    PlayerTwoGrenade = new Rectangle(PlayerTwoRect.X, PlayerTwoRect.Y, 25, 25);
                    PlayerTwoGrenadeDrawing = true;
                }

                if (PlayerOneRect.Intersects(PlayerTwoGrenade) && GrenadeTwoTimer > 0 && GrenadeTwoTimer < 11)
                {
                    PlayerOneHealth -= 75;
                    GrenadeTwoTimer = 0;
                }
                if (PlayerTwoRect.Intersects(PlayerOneGrenade) && GrenadeOneTimer > 0 && GrenadeOneTimer < 11)
                {
                    PlayerTwoHealth -= 75;
                    GrenadeOneTimer = 0;
                }
                #endregion
            }
            if (blueWin || redWin)
            {
                #region restart
                if (keys.IsKeyDown(Keys.Enter))
                {
                    blueWin = false;
                    redWin = false;
                    PlayerOneHealth = OriginalHealth;
                    PlayerTwoHealth = OriginalHealth;

                    PlayerOneRect = new Rectangle(0, 400, 150, 150);
                    PlayerOneRight = true;
                    PlayerOneFalling = true;
                    PlayerOneJumpTimer = 0;
                    PlayerOneTouchingGround = false;

                    PlayerTwoRect = new Rectangle(1100, 400, 150, 150);
                    PlayerTwoRight = false;
                    PlayerTwoFalling = true;
                    PlayerTwoJumpTimer = 0;
                    PlayerTwoTouchingGround = false;

                    PlayerOneGrenadeCount = 5;
                    PlayerTwoGrenadeCount = 5;
                }
                #endregion
            }

            #region draw
            spriteBatch.Begin();

            spriteBatch.Draw(Content.Load<Texture2D>("farmhouse"), new Rectangle(0, 0, 1200, 700), Color.White);

            spriteBatch.DrawString(Content.Load<SpriteFont>("font"), PlayerOneGrenadeCount.ToString(), new Vector2(0, 100), Color.Black);
            spriteBatch.DrawString(Content.Load<SpriteFont>("font"), PlayerTwoGrenadeCount.ToString(), new Vector2(1150, 100), Color.Black);

            if (PlayerOneHealth < 0)
            {
                redWin = true;
            }
            if (PlayerTwoHealth < 0)
            {
                blueWin = true;
            }

            if (PlayerOneRight)
            {
                spriteBatch.Draw(Content.Load<Texture2D>("pigFacingLeft"), PlayerOneRect, Color.Khaki);
            }
            else
            {
                spriteBatch.Draw(Content.Load<Texture2D>("pigFacingRight"), PlayerOneRect, Color.Khaki);
            }

            if (PlayerTwoRight)
            {
                spriteBatch.Draw(Content.Load<Texture2D>("pigFacingLeft"), PlayerTwoRect, Color.DarkSalmon);
            }
            else
            {
                spriteBatch.Draw(Content.Load<Texture2D>("pigFacingRight"), PlayerTwoRect, Color.DarkSalmon);
            }

            spriteBatch.Draw(Content.Load<Texture2D>("stuff"), ground, Color.White);
            spriteBatch.Draw(Content.Load<Texture2D>("stuff"), PlatformOne, Color.White);
            spriteBatch.Draw(Content.Load<Texture2D>("stuff"), PlatformTwo, Color.White);
            spriteBatch.Draw(Content.Load<Texture2D>("stuff"), PlatformThree, Color.White);

            if (PlayerOneBulletFired)
            {
                spriteBatch.Draw(Content.Load<Texture2D>("bullet"), PlayerOneBullet, Color.White);
            }

            if (PlayerTwoBulletFired)
            {
                spriteBatch.Draw(Content.Load<Texture2D>("bullet"), PlayerTwoBullet, Color.White);
            }

            spriteBatch.Draw(Content.Load<Texture2D>("blue"), new Rectangle(0, 0, PlayerOneHealth, 50), Color.White);
            spriteBatch.Draw(Content.Load<Texture2D>("red"), new Rectangle(700, 0, PlayerTwoHealth, 50), Color.White);

            if (blueWin)
            {
                spriteBatch.DrawString(Content.Load<SpriteFont>("font"), "BLUE WINS", new Vector2(500, 300), Color.Black);
            }
            if (redWin)
            {
                spriteBatch.DrawString(Content.Load<SpriteFont>("font"), "RED WINS", new Vector2(500, 300), Color.Black);
            }

            if (healthSpawn)
            {
                spriteBatch.Draw(Content.Load<Texture2D>("pack"), healthRect, Color.White);
            }

            if (GrenadeOneTimer > 10)
            {
                PlayerOneGrenade = new Rectangle(PlayerOneGrenade.X, PlayerOneGrenade.Y, 50, 50);
                spriteBatch.Draw(Content.Load<Texture2D>("grenade"), PlayerOneGrenade, Color.White);
            }
            else if (GrenadeOneTimer < 1)
            {
                PlayerOneGrenade.X = 2000;
            }
            else if (GrenadeOneTimer < 11)
            {
                PlayerOneGrenade = new Rectangle(PlayerOneGrenade.X, PlayerOneGrenade.Y, 200, 200);
                spriteBatch.Draw(Content.Load<Texture2D>("explosionSprite"), PlayerOneGrenade, Color.White);
            }

            if (GrenadeTwoTimer > 10)
            {
                PlayerTwoGrenade = new Rectangle(PlayerTwoGrenade.X, PlayerTwoGrenade.Y, 50, 50);
                spriteBatch.Draw(Content.Load<Texture2D>("grenade"), PlayerTwoGrenade, Color.White);
            }
            else if (GrenadeTwoTimer < 1)
            {
                PlayerTwoGrenade.X = 2000;
            }
            else if (GrenadeTwoTimer < 11)
            {
                PlayerTwoGrenade = new Rectangle(PlayerTwoGrenade.X, PlayerTwoGrenade.Y, 200, 200);
                spriteBatch.Draw(Content.Load<Texture2D>("explosionSprite"), PlayerTwoGrenade, Color.White);
            }

            spriteBatch.End();

            #endregion
        }
        private void UpdateMapThree()
        {
            if (!blueWin && !redWin)
            {
                #region movement
                if (keys.IsKeyDown(Keys.Right) && PlayerOneRect.X < 1100)
                {
                    PlayerOneRect.X += 7;
                    PlayerOneRight = true;
                }
                if (keys.IsKeyDown(Keys.Left) && PlayerOneRect.X > 0)
                {
                    PlayerOneRect.X -= 7;
                    PlayerOneRight = false;
                }

                if (keys.IsKeyDown(Keys.D) && PlayerTwoRect.X < 1100)
                {
                    PlayerTwoRect.X += 7;
                    PlayerTwoRight = true;
                }
                if (keys.IsKeyDown(Keys.A) && PlayerTwoRect.X > 0)
                {
                    PlayerTwoRect.X -= 7;
                    PlayerTwoRight = false;
                }
                if (keys.IsKeyDown(Keys.Down) && oldkeys.IsKeyUp(Keys.Down) && PlayerOneTouchingGround && !PlayerOneRect.Intersects(ground) && !PlayerOneRect.Intersects(PlatformOne))
                {
                    PlayerOneRect.Y += 5;
                }
                if (keys.IsKeyDown(Keys.S) && oldkeys.IsKeyUp(Keys.S) && PlayerTwoTouchingGround && !PlayerTwoRect.Intersects(ground) && !PlayerOneRect.Intersects(PlatformOne))
                {
                    PlayerTwoRect.Y += 5;
                }

                if (keys.IsKeyDown(Keys.M) && oldkeys.IsKeyUp(Keys.M) && PlayerOneDashCD == 0)
                {
                    if (PlayerOneRight && PlayerOneRect.X < 1000)
                    {
                        PlayerOneRect.X += 250;
                        PlayerOneDashCD = 120;
                    }
                    else if (PlayerOneRect.X > 200 && !PlayerOneRight)
                    {
                        PlayerOneRect.X -= 250;
                        PlayerOneDashCD = 120;
                    }
                }
                if (PlayerOneDashCD > 0)
                {
                    PlayerOneDashCD--;
                }

                if (keys.IsKeyDown(Keys.T) && oldkeys.IsKeyUp(Keys.T) && PlayerTwoDashCD == 0)
                {
                    if (PlayerTwoRight && PlayerTwoRect.X < 1000)
                    {
                        PlayerTwoRect.X += 250;
                        PlayerTwoDashCD = 120;
                    }
                    else if (PlayerTwoRect.X > 200 && !PlayerTwoRight)
                    {
                        PlayerTwoRect.X -= 250;
                        PlayerTwoDashCD = 120;
                    }
                }
                if (PlayerTwoDashCD > 0)
                {
                    PlayerTwoDashCD--;
                }
                #endregion
                #region falling
                if (PlayerOneFalling)
                {
                    PlayerOneRect.Y += 5;
                }
                if (ground.Intersects(PlayerOneRect) && PlayerOneRect.Y <= ground.Y)
                {
                    PlayerOneFalling = false;
                    PlayerOneTouchingGround = true;
                }
                else if (PlatformOne.Intersects(PlayerOneRect) && PlayerOneRect.Y + 145 <= PlatformOne.Y)
                {
                    PlayerOneFalling = false;
                    PlayerOneTouchingGround = true;
                }
                else if (PlatformTwo.Intersects(PlayerOneRect) && PlayerOneRect.Y + 145 <= PlatformTwo.Y)
                {
                    PlayerOneFalling = false;
                    PlayerOneTouchingGround = true;
                }
                else if (PlatformThree.Intersects(PlayerOneRect) && PlayerOneRect.Y + 145 <= PlatformThree.Y)
                {
                    PlayerOneFalling = false;
                    PlayerOneTouchingGround = true;
                }
                else if (PlatformFour.Intersects(PlayerOneRect) && PlayerOneRect.Y + 145 <= PlatformFour.Y)
                {
                    PlayerOneFalling = false;
                    PlayerOneTouchingGround = true;
                }
                else
                {
                    PlayerOneTouchingGround = false;
                }
                if (!PlayerOneTouchingGround && PlayerOneJumpTimer == 0)
                {
                    PlayerOneFalling = true;
                }

                if (PlayerTwoFalling)
                {
                    PlayerTwoRect.Y += 5;
                }
                if (ground.Intersects(PlayerTwoRect) && PlayerTwoRect.Y <= ground.Y)
                {
                    PlayerTwoFalling = false;
                    PlayerTwoTouchingGround = true;
                }
                else if (PlatformOne.Intersects(PlayerTwoRect) && PlayerTwoRect.Y + 145 <= PlatformOne.Y)
                {
                    PlayerTwoFalling = false;
                    PlayerTwoTouchingGround = true;
                }
                else if (PlatformTwo.Intersects(PlayerTwoRect) && PlayerTwoRect.Y + 145 <= PlatformTwo.Y)
                {
                    PlayerTwoFalling = false;
                    PlayerTwoTouchingGround = true;
                }
                else if (PlatformThree.Intersects(PlayerTwoRect) && PlayerTwoRect.Y + 145 <= PlatformThree.Y)
                {
                    PlayerTwoFalling = false;
                    PlayerTwoTouchingGround = true;
                }
                else
                {
                    PlayerTwoTouchingGround = false;
                }
                if (!PlayerTwoTouchingGround && PlayerTwoJumpTimer == 0)
                {
                    PlayerTwoFalling = true;
                }

                if (PlayerOneRect.Y >= 700)
                {
                    redWin = true;
                }

                if (PlayerTwoRect.Y >= 700)
                {
                    blueWin = true;
                }
                #endregion
                #region jumping
                if (keys.IsKeyDown(Keys.Up) && oldkeys.IsKeyUp(Keys.Up) && !PlayerOneFalling && PlayerOneTouchingGround)
                {
                    PlayerOneJumpTimer = 50;
                }
                if (PlayerOneJumpTimer > 0)
                {
                    PlayerOneRect.Y -= 5;
                    PlayerOneJumpTimer--;
                    if (PlayerOneJumpTimer == 0)
                    {
                        PlayerOneFalling = true;
                    }
                }

                if (keys.IsKeyDown(Keys.W) && oldkeys.IsKeyUp(Keys.W) && !PlayerTwoFalling && PlayerTwoTouchingGround)
                {
                    PlayerTwoJumpTimer = 50;
                }
                if (PlayerTwoJumpTimer > 0)
                {
                    PlayerTwoRect.Y -= 5;
                    PlayerTwoJumpTimer--;
                    if (PlayerTwoJumpTimer == 0)
                    {
                        PlayerTwoFalling = true;
                    }
                }
                #endregion
                #region shooting
                if (keys.IsKeyDown(Keys.Space) && oldkeys.IsKeyUp(Keys.Space) && !PlayerOneBulletFired)
                {
                    PlayerOneBulletFired = true;
                    Content.Load<SoundEffect>("gunshot").Play();
                    if (PlayerOneRight)
                    {
                        PlayerOneBulletRight = true;
                        PlayerOneBullet = new Rectangle(PlayerOneRect.X + 150, PlayerOneRect.Y + 85, 50, 5);
                    }
                    else
                    {
                        PlayerOneBulletRight = false;
                        PlayerOneBullet = new Rectangle(PlayerOneRect.X - 50, PlayerOneRect.Y + 85, 50, 5);
                    }
                }
                if (PlayerOneBulletFired)
                {
                    if (PlayerOneBulletRight)
                    {
                        PlayerOneBullet.X += 25;
                    }
                    else
                    {
                        PlayerOneBullet.X -= 25;
                    }

                    if (PlayerOneBullet.Intersects(PlayerTwoRect))
                    {
                        PlayerOneBulletFired = false;
                        PlayerTwoHealth -= 25;
                        if (PlayerTwoHealth == 0)
                        {
                            blueWin = true;
                        }
                    }
                    if (PlayerOneBullet.X >= 1200 || PlayerOneBullet.X <= -50)
                    {
                        PlayerOneBulletFired = false;
                    }
                }

                if (keys.IsKeyDown(Keys.R) && oldkeys.IsKeyUp(Keys.R) && !PlayerTwoBulletFired)
                {
                    PlayerTwoBulletFired = true;
                    Content.Load<SoundEffect>("gunshot").Play();
                    if (PlayerTwoRight)
                    {
                        PlayerTwoBulletRight = true;
                        PlayerTwoBullet = new Rectangle(PlayerTwoRect.X + 150, PlayerTwoRect.Y + 100, 50, 5);
                    }
                    else
                    {
                        PlayerTwoBulletRight = false;
                        PlayerTwoBullet = new Rectangle(PlayerTwoRect.X - 50, PlayerTwoRect.Y + 100, 50, 5);
                    }
                }
                if (PlayerTwoBulletFired)
                {
                    if (PlayerTwoBulletRight)
                    {
                        PlayerTwoBullet.X += 25;
                    }
                    else
                    {
                        PlayerTwoBullet.X -= 25;
                    }

                    if (PlayerTwoBullet.Intersects(PlayerOneRect))
                    {
                        PlayerTwoBulletFired = false;
                        PlayerOneHealth -= 25;
                        if (PlayerOneHealth == 0)
                        {
                            redWin = true;
                        }
                    }
                    if (PlayerTwoBullet.X >= 1200 || PlayerTwoBullet.X <= -50)
                    {
                        PlayerTwoBulletFired = false;
                    }
                }
                #endregion
                #region health
                if (timeToHealthSpawn > 0 && !healthSpawn)
                {
                    timeToHealthSpawn--;
                }
                if (timeToHealthSpawn == 0)
                {
                    healthSpawn = true;
                }
                if (healthSpawn)
                {
                    if (PlayerOneRect.Intersects(healthRect) && PlayerOneHealth <= OriginalHealth - 50)
                    {
                        PlayerOneHealth += 50;
                        timeToHealthSpawn = 300;
                        healthSpawn = false;
                        PlayerOneGrenadeCount += 3;
                    }
                    else if (PlayerOneRect.Intersects(healthRect))
                    {
                        healthSpawn = false;
                        timeToHealthSpawn = 300;
                        PlayerOneGrenadeCount += 3;
                    }
                    if (PlayerTwoRect.Intersects(healthRect) && PlayerTwoHealth <= OriginalHealth - 50)
                    {
                        PlayerTwoHealth += 50;
                        timeToHealthSpawn = 300;
                        healthSpawn = false;
                        PlayerTwoGrenadeCount += 3;
                    }
                    else if (PlayerTwoRect.Intersects(healthRect))
                    {
                        healthSpawn = false;
                        timeToHealthSpawn = 300;
                        PlayerTwoGrenadeCount += 3;
                    }
                }
                #endregion
                #region grenades
                if (GrenadeOneTimer > 0)
                {
                    PlayerOneGrenadeDrawing = true;
                    GrenadeOneTimer--;
                }
                if (GrenadeOneTimer == 10)
                {
                    PlayerOneGrenade.X -= 75;
                    PlayerOneGrenade.Y -= 75;
                }
                else if (GrenadeOneTimer < 1)
                {
                    PlayerOneGrenadeDrawing = false;
                }
                if (keys.IsKeyDown(Keys.N) && oldkeys.IsKeyUp(Keys.N) && PlayerOneGrenadeDrawing == false && PlayerOneGrenadeCount > 0)
                {
                    GrenadeOneTimer = 130;
                    PlayerOneGrenade = new Rectangle(PlayerOneRect.X, PlayerOneRect.Y, 25, 25);
                    PlayerOneGrenadeDrawing = true;
                }

                if (GrenadeTwoTimer > 0)
                {
                    PlayerTwoGrenadeDrawing = true;
                    GrenadeTwoTimer--;
                }
                if (GrenadeTwoTimer == 10)
                {
                    PlayerTwoGrenade.X -= 75;
                    PlayerTwoGrenade.Y -= 75;
                }
                else if (GrenadeTwoTimer < 1)
                {
                    PlayerTwoGrenadeDrawing = false;
                }
                if (keys.IsKeyDown(Keys.Y) && oldkeys.IsKeyUp(Keys.Y) && PlayerTwoGrenadeDrawing == false && PlayerTwoGrenadeCount > 0)
                {
                    GrenadeTwoTimer = 130;
                    PlayerTwoGrenade = new Rectangle(PlayerTwoRect.X, PlayerTwoRect.Y, 25, 25);
                    PlayerTwoGrenadeDrawing = true;
                }

                if (PlayerOneRect.Intersects(PlayerTwoGrenade) && GrenadeTwoTimer > 0 && GrenadeTwoTimer < 11)
                {
                    PlayerOneHealth -= 75;
                    GrenadeTwoTimer = 0;
                }
                if (PlayerTwoRect.Intersects(PlayerOneGrenade) && GrenadeOneTimer > 0 && GrenadeOneTimer < 11)
                {
                    PlayerTwoHealth -= 75;
                    GrenadeOneTimer = 0;
                }
                #endregion
            }
            if (blueWin || redWin)
            {
                #region restart
                if (keys.IsKeyDown(Keys.Enter))
                {
                    blueWin = false;
                    redWin = false;
                    PlayerOneHealth = OriginalHealth;
                    PlayerTwoHealth = OriginalHealth;

                    PlayerOneRect = new Rectangle(0, 400, 150, 150);
                    PlayerOneRight = true;
                    PlayerOneFalling = true;
                    PlayerOneJumpTimer = 0;
                    PlayerOneTouchingGround = false;

                    PlayerTwoRect = new Rectangle(1100, 400, 150, 150);
                    PlayerTwoRight = false;
                    PlayerTwoFalling = true;
                    PlayerTwoJumpTimer = 0;
                    PlayerTwoTouchingGround = false;

                    PlayerOneGrenadeCount = 5;
                    PlayerTwoGrenadeCount = 5;
                }
                #endregion
            }

            #region draw
            spriteBatch.Begin();

            spriteBatch.Draw(Content.Load<Texture2D>("sky"), new Rectangle(0, 0, 1200, 700), Color.White);

            spriteBatch.DrawString(Content.Load<SpriteFont>("font"), PlayerOneGrenadeCount.ToString(), new Vector2(0, 100), Color.Black);
            spriteBatch.DrawString(Content.Load<SpriteFont>("font"), PlayerTwoGrenadeCount.ToString(), new Vector2(1150, 100), Color.Black);

            if (PlayerOneHealth < 0)
            {
                redWin = true;
            }
            if (PlayerTwoHealth < 0)
            {
                blueWin = true;
            }
            
            if (PlayerOneRight)
            {
                spriteBatch.Draw(Content.Load<Texture2D>("pigFacingLeft"), PlayerOneRect, Color.Khaki);
            }
            else
            {
                spriteBatch.Draw(Content.Load<Texture2D>("pigFacingRight"), PlayerOneRect, Color.Khaki);
            }

            if (PlayerTwoRight)
            {
                spriteBatch.Draw(Content.Load<Texture2D>("pigFacingLeft"), PlayerTwoRect, Color.DarkSalmon);
            }
            else
            {
                spriteBatch.Draw(Content.Load<Texture2D>("pigFacingRight"), PlayerTwoRect, Color.DarkSalmon);
            }

            spriteBatch.Draw(Content.Load<Texture2D>("rooftop"), ground, Color.White);
            spriteBatch.Draw(Content.Load<Texture2D>("rooftop"), PlatformOne, Color.White);
            spriteBatch.Draw(Content.Load<Texture2D>("steel beam"), PlatformTwo, Color.White);
            spriteBatch.Draw(Content.Load<Texture2D>("steel beam"), PlatformThree, Color.White);
            spriteBatch.Draw(Content.Load<Texture2D>("steel beam"), PlatformFour, Color.White);

            if (PlayerOneBulletFired)
            {
                spriteBatch.Draw(Content.Load<Texture2D>("bullet"), PlayerOneBullet, Color.White);
            }

            if (PlayerTwoBulletFired)
            {
                spriteBatch.Draw(Content.Load<Texture2D>("bullet"), PlayerTwoBullet, Color.White);
            }

            spriteBatch.Draw(Content.Load<Texture2D>("blue"), new Rectangle(0, 0, PlayerOneHealth, 50), Color.White);
            spriteBatch.Draw(Content.Load<Texture2D>("red"), new Rectangle(700, 0, PlayerTwoHealth, 50), Color.White);

            if (blueWin)
            {
                spriteBatch.DrawString(Content.Load<SpriteFont>("font"), "BLUE WINS", new Vector2(500, 300), Color.Black);
            }
            if (redWin)
            {
                spriteBatch.DrawString(Content.Load<SpriteFont>("font"), "RED WINS", new Vector2(500, 300), Color.Black);
            }

            if (healthSpawn)
            {
                spriteBatch.Draw(Content.Load<Texture2D>("pack"), healthRect, Color.White);
            }

            if (GrenadeOneTimer > 10)
            {
                PlayerOneGrenade = new Rectangle(PlayerOneGrenade.X, PlayerOneGrenade.Y, 50, 50);
                spriteBatch.Draw(Content.Load<Texture2D>("grenade"), PlayerOneGrenade, Color.White);
            }
            else if (GrenadeOneTimer < 1)
            {
                PlayerOneGrenade.X = 2000;
            }
            else if (GrenadeOneTimer < 11)
            {
                PlayerOneGrenade = new Rectangle(PlayerOneGrenade.X, PlayerOneGrenade.Y, 200, 200);
                spriteBatch.Draw(Content.Load<Texture2D>("explosionSprite"), PlayerOneGrenade, Color.White);
            }

            if (GrenadeTwoTimer > 10)
            {
                PlayerTwoGrenade = new Rectangle(PlayerTwoGrenade.X, PlayerTwoGrenade.Y, 50, 50);
                spriteBatch.Draw(Content.Load<Texture2D>("grenade"), PlayerTwoGrenade, Color.White);
            }
            else if (GrenadeTwoTimer < 1)
            {
                PlayerTwoGrenade.X = 2000;
            }
            else if (GrenadeTwoTimer < 11)
            {
                PlayerTwoGrenade = new Rectangle(PlayerTwoGrenade.X, PlayerTwoGrenade.Y, 200, 200);
                spriteBatch.Draw(Content.Load<Texture2D>("explosionSprite"), PlayerTwoGrenade, Color.White);
            }

            spriteBatch.End();

            #endregion
        }
        private void UpdateOnePlayer()
        {
            if (!blueWin && !redWin)
            {
                #region left-right-logic
                if (PlayerOneRect.X - 300 > PlayerTwoRect.X)
                {
                    goRight = true;
                    goLeft = false;
                }
                if (PlayerTwoRect.X - 300 > PlayerOneRect.X)
                {
                    goLeft = true;
                    goRight = false;
                }
                if (PlayerOneRect.X == PlayerTwoRect.X)
                {
                    goLeft = false;
                    goRight = false;
                }
                #endregion
                #region movement
                if (keys.IsKeyDown(Keys.Right) && PlayerOneRect.X < 1100)
                {
                    PlayerOneRect.X += 7;
                    PlayerOneRight = true;
                }
                if (keys.IsKeyDown(Keys.Left) && PlayerOneRect.X > 0)
                {
                    PlayerOneRect.X -= 7;
                    PlayerOneRight = false;
                }

                if (PlayerOneRect.X > PlayerTwoRect.X)
                {
                    PlayerTwoRight = true;
                }
                else
                {
                    PlayerTwoRight = false;
                }

                if (goRight && PlayerTwoRect.X < 1100)
                {
                    PlayerTwoRect.X += 7;
                }
                if (goLeft && PlayerTwoRect.X > 0)
                {
                    PlayerTwoRect.X -= 7;
                }
                if (keys.IsKeyDown(Keys.Down) && oldkeys.IsKeyUp(Keys.Down) && PlayerOneTouchingGround && !PlayerOneRect.Intersects(ground))
                {
                    PlayerOneRect.Y += 5;
                }
                if (PlayerOneRect.Y > PlayerTwoRect.Y && PlayerTwoTouchingGround && !PlayerTwoRect.Intersects(ground))
                {
                    PlayerTwoRect.Y += 5;
                }

                if (keys.IsKeyDown(Keys.M) && oldkeys.IsKeyUp(Keys.M) && PlayerOneDashCD == 0)
                {
                    if (PlayerOneRight && PlayerOneRect.X < 1000)
                    {
                        PlayerOneRect.X += 250;
                        PlayerOneDashCD = 120;
                    }
                    else if (PlayerOneRect.X > 200 && !PlayerOneRight)
                    {
                        PlayerOneRect.X -= 250;
                        PlayerOneDashCD = 120;
                    }
                }
                if (PlayerOneDashCD > 0)
                {
                    PlayerOneDashCD--;
                }
                #endregion
                #region falling
                if (PlayerOneFalling)
                {
                    PlayerOneRect.Y += 5;
                }
                if (ground.Intersects(PlayerOneRect) && PlayerOneRect.Y <= ground.Y)
                {
                    PlayerOneFalling = false;
                    PlayerOneTouchingGround = true;
                }
                else if (PlatformOne.Intersects(PlayerOneRect) && PlayerOneRect.Y + 145 <= PlatformOne.Y)
                {
                    PlayerOneFalling = false;
                    PlayerOneTouchingGround = true;
                }
                else if (PlatformTwo.Intersects(PlayerOneRect) && PlayerOneRect.Y + 145 <= PlatformTwo.Y)
                {
                    PlayerOneFalling = false;
                    PlayerOneTouchingGround = true;
                }
                else if (PlatformThree.Intersects(PlayerOneRect) && PlayerOneRect.Y + 145 <= PlatformThree.Y)
                {
                    PlayerOneFalling = false;
                    PlayerOneTouchingGround = true;
                }
                else
                {
                    PlayerOneTouchingGround = false;
                }
                if (!PlayerOneTouchingGround && PlayerOneJumpTimer == 0)
                {
                    PlayerOneFalling = true;
                }

                if (PlayerTwoFalling)
                {
                    PlayerTwoRect.Y += 5;
                }
                if (ground.Intersects(PlayerTwoRect) && PlayerTwoRect.Y <= ground.Y)
                {
                    PlayerTwoFalling = false;
                    PlayerTwoTouchingGround = true;
                }
                else if (PlatformOne.Intersects(PlayerTwoRect) && PlayerTwoRect.Y + 145 <= PlatformOne.Y)
                {
                    PlayerTwoFalling = false;
                    PlayerTwoTouchingGround = true;
                }
                else if (PlatformTwo.Intersects(PlayerTwoRect) && PlayerTwoRect.Y + 145 <= PlatformTwo.Y)
                {
                    PlayerTwoFalling = false;
                    PlayerTwoTouchingGround = true;
                }
                else if (PlatformThree.Intersects(PlayerTwoRect) && PlayerTwoRect.Y + 145 <= PlatformThree.Y)
                {
                    PlayerTwoFalling = false;
                    PlayerTwoTouchingGround = true;
                }
                else
                {
                    PlayerTwoTouchingGround = false;
                }
                if (!PlayerTwoTouchingGround && PlayerTwoJumpTimer == 0)
                {
                    PlayerTwoFalling = true;
                }
                #endregion
                #region jumping
                if (keys.IsKeyDown(Keys.Up) && oldkeys.IsKeyUp(Keys.Up) && !PlayerOneFalling && PlayerOneTouchingGround)
                {
                    PlayerOneJumpTimer = 50;
                }
                if (PlayerOneJumpTimer > 0)
                {
                    PlayerOneRect.Y -= 5;
                    PlayerOneJumpTimer--;
                    if (PlayerOneJumpTimer == 0)
                    {
                        PlayerOneFalling = true;
                    }
                }

                if (PlayerOneRect.Y < PlayerTwoRect.Y && !PlayerTwoFalling && PlayerTwoTouchingGround)
                {
                    PlayerTwoJumpTimer = 50;
                }
                if (PlayerTwoJumpTimer > 0)
                {
                    PlayerTwoRect.Y -= 5;
                    PlayerTwoJumpTimer--;
                    if (PlayerTwoJumpTimer == 0)
                    {
                        PlayerTwoFalling = true;
                    }
                }
                #endregion
                #region shooting
                if (keys.IsKeyDown(Keys.Space) && oldkeys.IsKeyUp(Keys.Space) && !PlayerOneBulletFired)
                {
                    PlayerOneBulletFired = true;
                    Content.Load<SoundEffect>("gunshot").Play();
                    if (PlayerOneRight)
                    {
                        PlayerOneBulletRight = true;
                        PlayerOneBullet = new Rectangle(PlayerOneRect.X + 150, PlayerOneRect.Y + 85, 50, 5);
                    }
                    else
                    {
                        PlayerOneBulletRight = false;
                        PlayerOneBullet = new Rectangle(PlayerOneRect.X - 50, PlayerOneRect.Y + 85, 50, 5);
                    }
                }
                if (PlayerOneBulletFired)
                {
                    if (PlayerOneBulletRight)
                    {
                        PlayerOneBullet.X += 25;
                    }
                    else
                    {
                        PlayerOneBullet.X -= 25;
                    }

                    if (PlayerOneBullet.Intersects(PlayerTwoRect))
                    {
                        PlayerOneBulletFired = false;
                        PlayerTwoHealth -= 25;
                        if (PlayerTwoHealth == 0)
                        {
                            blueWin = true;
                        }
                    }
                    if (PlayerOneBullet.X >= 1200 || PlayerOneBullet.X <= -50)
                    {
                        PlayerOneBulletFired = false;
                    }
                }

                shotDelay--;

                if (PlayerOneRect.Y == PlayerTwoRect.Y && !PlayerTwoBulletFired && shotDelay < 1)
                {
                    shotDelay = 10;
                    PlayerTwoBulletFired = true;
                    Content.Load<SoundEffect>("gunshot").Play();
                    if (PlayerTwoRight)
                    {
                        PlayerTwoBulletRight = true;
                        PlayerTwoBullet = new Rectangle(PlayerTwoRect.X + 150, PlayerTwoRect.Y + 100, 50, 5);
                    }
                    else
                    {
                        PlayerTwoBulletRight = false;
                        PlayerTwoBullet = new Rectangle(PlayerTwoRect.X - 50, PlayerTwoRect.Y + 100, 50, 5);
                    }
                }
                if (PlayerTwoBulletFired)
                {
                    if (PlayerTwoBulletRight)
                    {
                        PlayerTwoBullet.X += 25;
                    }
                    else
                    {
                        PlayerTwoBullet.X -= 25;
                    }

                    if (PlayerTwoBullet.Intersects(PlayerOneRect))
                    {
                        PlayerTwoBulletFired = false;
                        PlayerOneHealth -= 25;
                        if (PlayerOneHealth == 0)
                        {
                            redWin = true;
                        }
                    }
                    if (PlayerTwoBullet.X >= 1200 || PlayerTwoBullet.X <= -50)
                    {
                        PlayerTwoBulletFired = false;
                    }
                }
                #endregion
                #region health
                if (timeToHealthSpawn > 0 && !healthSpawn)
                {
                    timeToHealthSpawn--;
                }
                if (timeToHealthSpawn == 0)
                {
                    healthSpawn = true;
                }
                if (healthSpawn)
                {
                    if (PlayerOneRect.Intersects(healthRect) && PlayerOneHealth <= OriginalHealth - 50)
                    {
                        PlayerOneHealth += 50;
                        timeToHealthSpawn = 300;
                        healthSpawn = false;
                        PlayerOneGrenadeCount += 3;
                    }
                    else if (PlayerOneRect.Intersects(healthRect))
                    {
                        healthSpawn = false;
                        timeToHealthSpawn = 300;
                        PlayerOneGrenadeCount += 3;
                    }
                    if (PlayerTwoRect.Intersects(healthRect) && PlayerTwoHealth <= OriginalHealth - 50)
                    {
                        PlayerTwoHealth += 50;
                        timeToHealthSpawn = 300;
                        healthSpawn = false;
                        PlayerTwoGrenadeCount += 3;
                    }
                    else if (PlayerTwoRect.Intersects(healthRect))
                    {
                        healthSpawn = false;
                        timeToHealthSpawn = 300;
                        PlayerTwoGrenadeCount += 3;
                    }
                }
                #endregion
                #region grenades
                if (GrenadeOneTimer > 0)
                {
                    PlayerOneGrenadeDrawing = true;
                    GrenadeOneTimer--;
                }
                if (GrenadeOneTimer == 10)
                {
                    PlayerOneGrenade.X -= 75;
                    PlayerOneGrenade.Y -= 75;
                }
                else if (GrenadeOneTimer < 1)
                {
                    PlayerOneGrenadeDrawing = false;
                }
                if (keys.IsKeyDown(Keys.N) && oldkeys.IsKeyUp(Keys.N) && PlayerOneGrenadeDrawing == false && PlayerOneGrenadeCount > 0)
                {
                    GrenadeOneTimer = 130;
                    PlayerOneGrenade = new Rectangle(PlayerOneRect.X, PlayerOneRect.Y, 25, 25);
                    PlayerOneGrenadeDrawing = true;
                }

                if (GrenadeTwoTimer > 0)
                {
                    PlayerTwoGrenadeDrawing = true;
                    GrenadeTwoTimer--;
                }
                if (GrenadeTwoTimer == 10)
                {
                    PlayerTwoGrenade.X -= 75;
                    PlayerTwoGrenade.Y -= 75;
                }
                else if (GrenadeTwoTimer < 1)
                {
                    PlayerTwoGrenadeDrawing = false;
                }
                if (keys.IsKeyDown(Keys.Y) && oldkeys.IsKeyUp(Keys.Y) && PlayerTwoGrenadeDrawing == false && PlayerTwoGrenadeCount > 0)
                {
                    GrenadeTwoTimer = 130;
                    PlayerTwoGrenade = new Rectangle(PlayerTwoRect.X, PlayerTwoRect.Y, 25, 25);
                    PlayerTwoGrenadeDrawing = true;
                }

                if (PlayerOneRect.Intersects(PlayerTwoGrenade) && GrenadeTwoTimer > 0 && GrenadeTwoTimer < 11)
                {
                    PlayerOneHealth -= 75;
                    GrenadeTwoTimer = 0;
                }
                if (PlayerTwoRect.Intersects(PlayerOneGrenade) && GrenadeOneTimer > 0 && GrenadeOneTimer < 11)
                {
                    PlayerTwoHealth -= 75;
                    GrenadeOneTimer = 0;
                }
                #endregion
            }
            if (blueWin || redWin)
            {
                #region restart
                if (keys.IsKeyDown(Keys.Enter))
                {
                    blueWin = false;
                    redWin = false;
                    PlayerOneHealth = OriginalHealth;
                    PlayerTwoHealth = OriginalHealth;

                    PlayerOneRect = new Rectangle(0, 400, 150, 150);
                    PlayerOneRight = true;
                    PlayerOneFalling = true;
                    PlayerOneJumpTimer = 0;
                    PlayerOneTouchingGround = false;

                    PlayerTwoRect = new Rectangle(1100, 400, 150, 150);
                    PlayerTwoRight = false;
                    PlayerTwoFalling = true;
                    PlayerTwoJumpTimer = 0;
                    PlayerTwoTouchingGround = false;

                    PlayerOneGrenadeCount = 5;
                    PlayerTwoGrenadeCount = 5;
                }
                #endregion
            }
            
            #region draw
            spriteBatch.Begin();

            spriteBatch.Draw(Content.Load<Texture2D>("background"), new Rectangle(0, 0, 1200, 700), Color.White);

            spriteBatch.DrawString(Content.Load <SpriteFont>("font"), PlayerOneGrenadeCount.ToString(), new Vector2(0, 100), Color.Black);
            spriteBatch.DrawString(Content.Load<SpriteFont>("font"), PlayerTwoGrenadeCount.ToString(), new Vector2(1150, 100), Color.Black);

            if (PlayerOneHealth < 0)
            {
                redWin = true;
            }
            if (PlayerTwoHealth < 0)
            {
                blueWin = true;
            }

            if (PlayerOneRight)
            {
                spriteBatch.Draw(Content.Load<Texture2D>("pigFacingLeft"), PlayerOneRect, Color.Khaki);
            }
            else
            {
                spriteBatch.Draw(Content.Load<Texture2D>("pigFacingRight"), PlayerOneRect, Color.Khaki);
            }

            if (PlayerTwoRight)
            {
                spriteBatch.Draw(Content.Load<Texture2D>("pigFacingLeft"), PlayerTwoRect, Color.DarkSalmon);
            }
            else
            {
                spriteBatch.Draw(Content.Load<Texture2D>("pigFacingRight"), PlayerTwoRect, Color.DarkSalmon);
            }

            spriteBatch.Draw(Content.Load<Texture2D>("stuff"), ground, Color.White);
            spriteBatch.Draw(Content.Load<Texture2D>("stuff"), PlatformOne, Color.White);
            spriteBatch.Draw(Content.Load<Texture2D>("stuff"), PlatformTwo, Color.White);
            spriteBatch.Draw(Content.Load<Texture2D>("stuff"), PlatformThree, Color.White);

            if (PlayerOneBulletFired)
            {
                spriteBatch.Draw(Content.Load<Texture2D>("bullet"), PlayerOneBullet, Color.White);
            }

            if (PlayerTwoBulletFired)
            {
                spriteBatch.Draw(Content.Load<Texture2D>("bullet"), PlayerTwoBullet, Color.White);
            }

            spriteBatch.Draw(Content.Load<Texture2D>("blue"), new Rectangle(0, 0, PlayerOneHealth, 50), Color.White);
            spriteBatch.Draw(Content.Load<Texture2D>("red"), new Rectangle(700, 0, PlayerTwoHealth, 50), Color.White);

            if (blueWin)
            {
                spriteBatch.DrawString(Content.Load<SpriteFont>("font"), "BLUE WINS", new Vector2(500, 300), Color.Black);
            }
            if (redWin)
            {
                spriteBatch.DrawString(Content.Load<SpriteFont>("font"), "RED WINS", new Vector2(500, 300), Color.Black);
            }

            if (healthSpawn)
            {
                spriteBatch.Draw(Content.Load<Texture2D>("pack"), healthRect, Color.White);
            }

                if (GrenadeOneTimer > 10)
                {
                    PlayerOneGrenade = new Rectangle(PlayerOneGrenade.X, PlayerOneGrenade.Y, 50, 50);
                    spriteBatch.Draw(Content.Load<Texture2D>("grenade"), PlayerOneGrenade, Color.White);
                }
                else if (GrenadeOneTimer < 1)
                {
                    PlayerOneGrenade.X = 2000;
                }
                else if (GrenadeOneTimer < 11)
                {
                    PlayerOneGrenade = new Rectangle(PlayerOneGrenade.X, PlayerOneGrenade.Y, 200, 200);
                    spriteBatch.Draw(Content.Load<Texture2D>("explosionSprite"), PlayerOneGrenade, Color.White);
                }

                if (GrenadeTwoTimer > 10)
                {
                    PlayerTwoGrenade = new Rectangle(PlayerTwoGrenade.X, PlayerTwoGrenade.Y, 50, 50);
                    spriteBatch.Draw(Content.Load<Texture2D>("grenade"), PlayerTwoGrenade, Color.White);
                }
                else if (GrenadeTwoTimer < 1)
                {
                    PlayerTwoGrenade.X = 2000;
                }
                else if (GrenadeTwoTimer < 11)
                {
                    PlayerTwoGrenade = new Rectangle(PlayerTwoGrenade.X, PlayerTwoGrenade.Y, 200, 200);
                    spriteBatch.Draw(Content.Load<Texture2D>("explosionSprite"), PlayerTwoGrenade, Color.White);
                }
                if (goRight && !goLeft)
                {
                    spriteBatch.DrawString(Content.Load<SpriteFont>("font"), "go right", new Vector2(0, 200), Color.Black);
                }
                if (goLeft && !goRight)
                {
                    spriteBatch.DrawString(Content.Load<SpriteFont>("font"), "go left", new Vector2(0, 200), Color.Black);
                }
                if (goRight && goLeft)
                {
                    spriteBatch.DrawString(Content.Load<SpriteFont>("font"), "both", new Vector2(0, 200), Color.Black);
                }
                if (!goRight && !goLeft)
                {
                    spriteBatch.DrawString(Content.Load<SpriteFont>("font"), "none", new Vector2(0, 200), Color.Black);
                }
            spriteBatch.End();

            #endregion
        }
        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
#endregion
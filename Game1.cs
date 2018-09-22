using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Input;
//using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlanetParade
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

		// game objects
        List<Planet> initialPlanets = new List<Planet>();
        List<Planet> resultSequence = new List<Planet>();
        List<Planet> secretPlanetSequence = new List<Planet>();
        Planet[,] userPlanetSequence = new Planet[9, 4];
        List<PlanetType> planetTypeList = new List<PlanetType>();
        List<MenuButton> gameButtons = new List<MenuButton>();
		List<MenuButton> optionsButtons = new List<MenuButton>();
        List<Planet> winSuns = new List<Planet>();
        MenuButton checkButton;
		MenuButton yesButton;
		//MenuButton closeButton;
		List<Star> stars = new List<Star>();

        // planet, button, place sprites
        static Texture2D earthSprite;
        static Texture2D jupiterSprite;
        static Texture2D marsSprite;
        static Texture2D mercurySprite;
        static Texture2D moonSprite;
        static Texture2D neptuneSprite;
        static Texture2D plutoSprite;
        static Texture2D saturnSprite;
        static Texture2D sunSprite;
        static Texture2D uranusSprite;
        static Texture2D venusSprite;
        static Texture2D checkButtonSprite;
        Texture2D yesButtonSprite;
        Texture2D playAgainSprite;
        Texture2D yesNoYesButtonSprite;
        Texture2D yesNoNoButtonSprite;
        Texture2D giveUpButtonSprite;
		Texture2D rulesButtonSprite;
		Texture2D optionsButtonSprite;
		Texture2D starSprite1;
        Texture2D starSprite2;
        Texture2D starSprite3;
        //Texture2D closeSprite;

        TouchCollection touches;
        bool isTouched = false;
        bool buttonTouchStarted = false;

        // resize support
        static int currentWidth;
        int currentHeight;
        const int NominalWidth = 1440;
        const int NominalHeight = 2560;

        // Spacing
        int leftOffset;
		int rulesHorizontalOffset;
        int rulesVerticalOffset;
        int rulesWidth;
        //int rightOffset;
        int horizontalPlanetsOffset;
        //int middleOffset;
        int verticalInitialPlanetsOffset1;
        int verticalInitialPlanetsOffset2;
        int horizontalInitialPlanetsLeftOffset;
        int horizontalInitialPlanetsOffset;
        int verticalFirstAttemptOffset;
        int verticalAttemptOffset;
        int horizontalShowingResultsOffset;
        int verticalPlanetNamesOffset;
		int horizontalRulesButtonOffset;
		int horizontalOptionsButtonOffset;
        int horizontalGiveUpButtonOffset;
        int verticalMenuButtonsOffset;
        int verticalShowingResultsOffset;

        // messages
        SpriteFont messageFont;
        SpriteFont rulesFont;
        List<GameMessage> planetNames = new List<GameMessage>();
        List<GameMessage> attemptNumbers = new List<GameMessage>();
        List<GameMessage> gameMessages = new List<GameMessage>();
		List<GameMessage> optionsMessages = new List<GameMessage>();
        Color messageColor = new Color(169, 169, 169);
        Color usedAttemptsColor = new Color(100, 100, 100);
        GameMessage rules;
		GameMessage winMessage;
		List<GameMessage> looseMessages = new List<GameMessage>();
		//GameMessage askMessage;

        Random rand = new Random();

        // brightness of backgrounud from 0 to 100, +10 for moon, + 25 for sun
        int brightness = 0;

        int offset = 0;

        // game state tracking
        static GameState currentState = GameState.StartingNewGame;
        int currentAttempt = 0;

		// options support
		static bool plutoIsPlanet = true;
		static bool allowDuplicates = true;
        List<PlanetType> usedPlanets = new List<PlanetType>();


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = true;
			graphics.SupportedOrientations = DisplayOrientation.Portrait;

			// Setting resolution according to current screen size         
			currentWidth = (int)UIKit.UIScreen.MainScreen.Bounds.Width *2;
			currentHeight = (int)UIKit.UIScreen.MainScreen.Bounds.Height *2;

			// calculate spacing
            rulesHorizontalOffset = currentWidth / 9;
            rulesVerticalOffset = currentHeight / 20;
            rulesWidth = currentWidth * 7 / 9;
            leftOffset = currentWidth / 27;
            //rightOffset = currentWidth * 2 / 27;
            horizontalPlanetsOffset = currentWidth / 9;
            //middleOffset = currentWidth * 4 / 27;
            verticalInitialPlanetsOffset1 = currentHeight * 3 / 4;
            verticalInitialPlanetsOffset2 = currentHeight * 3 / 20;
            horizontalInitialPlanetsLeftOffset = currentWidth * 4 / 27;
            horizontalInitialPlanetsOffset = currentWidth * 5 / 27;
            verticalFirstAttemptOffset = currentHeight * 3 / 20;
            verticalAttemptOffset = currentHeight / 16;
            horizontalShowingResultsOffset = currentWidth * 16 / 27;
            verticalPlanetNamesOffset = currentHeight / 16;
            horizontalRulesButtonOffset = currentWidth * 1 / 6;
			horizontalOptionsButtonOffset = currentWidth * 3 / 6;
			horizontalGiveUpButtonOffset = currentWidth * 5 / 6;
            verticalMenuButtonsOffset = currentHeight / 20;
            verticalShowingResultsOffset = currentHeight / 5;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

			// load planet sprites
            earthSprite = Content.Load<Texture2D>(@"graphics\earth200");
            jupiterSprite = Content.Load<Texture2D>(@"graphics\jupiter200");
            marsSprite = Content.Load<Texture2D>(@"graphics\mars200");
            mercurySprite = Content.Load<Texture2D>(@"graphics\mercury200");
            moonSprite = Content.Load<Texture2D>(@"graphics\moon200");
            neptuneSprite = Content.Load<Texture2D>(@"graphics\neptun200");
            plutoSprite = Content.Load<Texture2D>(@"graphics\pluto200");
            saturnSprite = Content.Load<Texture2D>(@"graphics\saturn200plus");
            sunSprite = Content.Load<Texture2D>(@"graphics\sun200plus1");
            uranusSprite = Content.Load<Texture2D>(@"graphics\uranus205");
            venusSprite = Content.Load<Texture2D>(@"graphics\venus200");
            // load buttons sprites
            checkButtonSprite = Content.Load<Texture2D>(@"graphics\checkbutton2");
            yesButtonSprite = Content.Load<Texture2D>(@"graphics\yesbutton1");
            playAgainSprite = Content.Load<Texture2D>(@"graphics\playAgain");
            yesNoYesButtonSprite = Content.Load<Texture2D>(@"graphics\yesNoYes");
            yesNoNoButtonSprite = Content.Load<Texture2D>(@"graphics\yesNoNo");
			//closeSprite = Content.Load<Texture2D>(@"graphics\close");
            giveUpButtonSprite = Content.Load<Texture2D>(@"graphics\giveupbutton1");
			rulesButtonSprite = Content.Load<Texture2D>(@"graphics\rulesbutton1");
			optionsButtonSprite = Content.Load<Texture2D>(@"graphics\optionsbutton");

            //loading star sprite
			starSprite1 = Content.Load<Texture2D>(@"graphics\star18x18");
            starSprite2 = Content.Load<Texture2D>(@"graphics\star14x14");
            starSprite3 = Content.Load<Texture2D>(@"graphics\star9x9");
            // load sprite font
            messageFont = Content.Load<SpriteFont>(@"fonts\Arial");
            rulesFont = Content.Load<SpriteFont>(@"fonts\ArialRules");



            // load menu buttons
            gameButtons.Add(new MenuButton(giveUpButtonSprite, new Vector2(
                horizontalGiveUpButtonOffset,
                verticalMenuButtonsOffset),
                GameState.ShowingResults));
			gameButtons.Add(new MenuButton(rulesButtonSprite, new Vector2(
                horizontalRulesButtonOffset,
                verticalMenuButtonsOffset),
                GameState.ShowingRules));
			gameButtons.Add(new MenuButton(optionsButtonSprite, new Vector2(
                horizontalOptionsButtonOffset,
                verticalMenuButtonsOffset),
                GameState.ShowingOptions));
            checkButton = new MenuButton(checkButtonSprite,
               new Vector2(horizontalInitialPlanetsLeftOffset +
                    4 * horizontalInitialPlanetsOffset,
               verticalInitialPlanetsOffset1 + verticalInitialPlanetsOffset2),
               GameState.CheckingUserPlanets);
			yesButton = new MenuButton(playAgainSprite, new Vector2(
                    horizontalShowingResultsOffset +
                    horizontalPlanetsOffset * 3 / 2,
                    verticalShowingResultsOffset + 4 * verticalAttemptOffset),
                    GameState.StartingNewGame);
			//closeButton = new MenuButton(closeSprite, new Vector2(
                //currentWidth / 2,
                    //currentHeight * 5 / 6),
                    //GameState.StartingNewGame);

            // loading option buttons
            optionsButtons.Add(new MenuButton(yesNoYesButtonSprite, yesNoNoButtonSprite,
                                              new Vector2(currentWidth / 2, currentHeight * 2 / 6),
                                              OptionsList.PlutoIsPlanet));
            optionsButtons.Add(new MenuButton(yesNoYesButtonSprite, yesNoNoButtonSprite,
                                                new Vector2(currentWidth / 2, currentHeight * 4 / 6),
                                              OptionsList.AllowDuplicates));

            //loading rules
            rules = new GameMessage("      The world lies in darkness. To make it shine " +
			                        "bright you must crack the secret sequence of " +
			                        "the Planet Parade. The Sun and the Moon will " +
			                        "guide you. \n      You need to guess the sequence " +
                                    "in both planets and order. Planets in the secret sequence can " +
                                    "be duplicated (changable in options). There are 9 turns given. " +
			                        "\n      Each guess is made by choosing 4 planets " +
			                        "and touching Check button. You can delete " +
			                        "chosen planet by touching it before you touch " +
			                        "Check button. \n      You will get sun for each " +
			                        "correct planet that stands in correct " +
			                        "position. And you will get moon for each " +
			                        "correct planet that stands in wrong position. " +
                                    "\n      Good luck!", rulesFont,
                                    new Vector2(currentWidth / 2,
                                                currentHeight / 2), messageColor);
            rules.Position = new Vector2(rulesHorizontalOffset, rulesVerticalOffset);
			//load messages
			//askMessage = new GameMessage("Play again?",
                    //messageFont, new Vector2(
                    //horizontalShowingResultsOffset +
                    //horizontalPlanetsOffset * 3 / 2,
                    //verticalShowingResultsOffset+ 3 * verticalAttemptOffset),
                    //messageColor);
			winMessage = new GameMessage("You won!!!",
                    messageFont, new Vector2(horizontalShowingResultsOffset +
                    horizontalPlanetsOffset * 3 / 2,
                    verticalShowingResultsOffset),
                    messageColor);
			looseMessages.Add(new GameMessage("You lost!",
                       messageFont, new Vector2(horizontalShowingResultsOffset +
                   horizontalPlanetsOffset * 3 / 2,
                   verticalShowingResultsOffset),
                       messageColor));
            looseMessages.Add(new GameMessage("The secret sequence is:",
                messageFont, new Vector2(horizontalShowingResultsOffset +
            horizontalPlanetsOffset * 3 / 2,
            verticalShowingResultsOffset + verticalAttemptOffset),
                messageColor));

			// load options messages
			optionsMessages.Add(new GameMessage("Is Pluto a planet?",
												messageFont, new Vector2(currentWidth / 2,
                                                                          currentHeight * 1 / 6), messageColor));
			optionsMessages.Add(new GameMessage("Allow duplicated planets?",
                                                messageFont, new Vector2(currentWidth / 2,
                                                                          currentHeight * 3 / 6), messageColor));
			// loading win suns
			for (int i = 0; i < 4; i++)
            {
                winSuns.Add(new Planet(PlanetType.Sun, sunSprite,
                    new Vector2(horizontalShowingResultsOffset +
                        horizontalPlanetsOffset * i,
                        verticalShowingResultsOffset + 2 * verticalAttemptOffset)));
            }

			//loading stars
			for (int i = 0; i < 70; i++)
			{
				stars.Add(new Star(starSprite1, starSprite2, starSprite3, currentWidth, currentHeight));

			}
            // add attempt numbers
            for (int i = 0; i < 9; i++)
            {
                attemptNumbers.Add(new GameMessage((i + 1).ToString(),
                    messageFont, new Vector2(leftOffset,
                    verticalFirstAttemptOffset + i * verticalAttemptOffset),
                    messageColor));
            }
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            touches = TouchPanel.GetState();

            //update stars
			foreach (Star star in stars)
			{
				star.Update(gameTime, currentWidth, currentHeight);
			}

            // highlighting the attempt number
            attemptNumbers[currentAttempt].Color = Color.White;
            for (int i = 0; i < currentAttempt; i++)
            {
                attemptNumbers[i].Color = usedAttemptsColor;
            }
            for (int i = currentAttempt +1; i < attemptNumbers.Count; i++)
            {
                attemptNumbers[i].Color = messageColor;
            }

           

            //}
            // state-specific processing

            // update buttons
            if (currentState != GameState.ShowingRules && currentState != GameState.ShowingOptions)
            {
                foreach (MenuButton button in gameButtons)
                {
                    button.Update(touches);
                }

            }
            if (currentState == GameState.ShowingResults)
            {
                yesButton.Update(touches);
            }

			if (currentState == GameState.ShowingRules)
			{
				if (touches.Count == 1 && !isTouched)

                {
                    isTouched = true;
                    buttonTouchStarted = true;
                }
                if (touches.Count == 0)
                {
                    isTouched = false;
                }
				if (buttonTouchStarted && isTouched == false)
                {
                    currentState = GameState.StartingNewGame;
                    buttonTouchStarted = false;
                }
            }
            if (currentState == GameState.ShowingOptions)
            {
                if (touches.Count == 1 && !isTouched && 
                    !optionsButtons[0].CollisionRectangle.Contains(touches[0].Position)
                    && !optionsButtons[1].CollisionRectangle.Contains(touches[0].Position))

                {
                    isTouched = true;
                    buttonTouchStarted = true;
                }
                if (touches.Count == 0)
                {
                    isTouched = false;
                }
                if (buttonTouchStarted && isTouched == false)
                {
                    currentState = GameState.StartingNewGame;
                    buttonTouchStarted = false;
                }
            }



            if (currentState == GameState.StartingNewGame)
            {
                // deleting previous game data
                usedPlanets.Clear();
                initialPlanets.Clear();
                planetNames.Clear();
                secretPlanetSequence.Clear();
                gameMessages.Clear();
                resultSequence.Clear();
                currentAttempt = 0;
                brightness = 0;
				
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        if (userPlanetSequence[j, i] != null)
                        {
                            userPlanetSequence[j, i] = null;
                        }
                    }
                }
                // add initial initialPlanets, planet names 
                planetTypeList = Enum.GetValues(typeof(PlanetType)).Cast<PlanetType>().ToList();
                for (int i = 0; i < 5; i++)
                {
                    initialPlanets.Add(new Planet(planetTypeList[i],
                        GetPlanetSprite(planetTypeList[i]),
                        new Vector2(horizontalInitialPlanetsLeftOffset +
                        i * horizontalInitialPlanetsOffset,
                        verticalInitialPlanetsOffset1)));
                    planetNames.Add(new GameMessage(initialPlanets[i].Type.ToString(),
                        messageFont,
                        new Vector2(horizontalInitialPlanetsLeftOffset +
                        i * horizontalInitialPlanetsOffset,
                        verticalInitialPlanetsOffset1 + verticalPlanetNamesOffset), messageColor));
                }
                if (plutoIsPlanet)
                {
                    for (int i = 5; i < 9; i++)
                    {
                        initialPlanets.Add(new Planet(planetTypeList[i],
                            GetPlanetSprite(planetTypeList[i]),
                            new Vector2(horizontalInitialPlanetsLeftOffset +
                            (i - 5) * horizontalInitialPlanetsOffset,
                            verticalInitialPlanetsOffset1 + verticalInitialPlanetsOffset2)));
                        planetNames.Add(new GameMessage(initialPlanets[i].Type.ToString(),
                            messageFont,
                            new Vector2(horizontalInitialPlanetsLeftOffset +
                            (i - 5) * horizontalInitialPlanetsOffset,
                            verticalInitialPlanetsOffset1 + verticalInitialPlanetsOffset2 +
                            verticalPlanetNamesOffset), messageColor));
                    }

                }
                else
                {
                    for (int i = 5; i < 8; i++)
                    {
                        initialPlanets.Add(new Planet(planetTypeList[i],
                            GetPlanetSprite(planetTypeList[i]),
                            new Vector2(horizontalInitialPlanetsLeftOffset +
                            (i - 5) * horizontalInitialPlanetsOffset,
                            verticalInitialPlanetsOffset1 + verticalInitialPlanetsOffset2)));
                        planetNames.Add(new GameMessage(initialPlanets[i].Type.ToString(),
                            messageFont,
                            new Vector2(horizontalInitialPlanetsLeftOffset +
                            (i - 5) * horizontalInitialPlanetsOffset,
                            verticalInitialPlanetsOffset1 + verticalInitialPlanetsOffset2 +
                            verticalPlanetNamesOffset), messageColor));
                    }
                }


                // making a secret planet sequence
                usedPlanets.Add(PlanetType.Sun);
                usedPlanets.Add(PlanetType.Moon);
                if (!plutoIsPlanet)
                {
                    usedPlanets.Add(PlanetType.Pluto);
                }
                for (int i = 0; i < 4; i++)
                {
                    secretPlanetSequence.Add(new Planet(rand,
                        new Vector2((horizontalShowingResultsOffset +
                        horizontalPlanetsOffset * i),
                                    (verticalShowingResultsOffset + 2 * verticalAttemptOffset)), usedPlanets));
                    if (!allowDuplicates)
                    {
                        usedPlanets.Add(secretPlanetSequence[i].Type);
                    }
                }
                currentState = GameState.AddingUserPlanets;
            }

            if (currentState == GameState.AddingUserPlanets)
            {


                // add planet of selected type to the list on click
                foreach (Planet planet in initialPlanets)
				{
					
                    if (touches.Count == 1 && !planet.PlanetTouched && planet.CollisionRectangle.Contains(touches[0].Position))
					{
						planet.PlanetSelected = true;
						planet.PlanetTouched = true;
                    }
					else if (touches.Count == 1 && !planet.CollisionRectangle.Contains(touches[0].Position))
                    {
                        planet.PlanetSelected = false;
						planet.PlanetTouched = true;
                    }
					else if (touches.Count == 0)
                    {
						planet.PlanetTouched = false;
                    }
					if (planet.PlanetSelected && !planet.PlanetTouched)
					{
						for (int j = 0; j < 4; j ++)
						{
							if (userPlanetSequence[currentAttempt,j] == null)
							{
								userPlanetSequence[currentAttempt, j] = new Planet(
                            planet.Type,
                            GetPlanetSprite(planet.Type),
                            new Vector2(horizontalPlanetsOffset + horizontalPlanetsOffset * j,
                            verticalFirstAttemptOffset + currentAttempt * verticalAttemptOffset)
                        );
                                planet.PlanetSelected = false;
								break;
							}
						}


                      
					}
				}
    	
                // deleting user planets on touch

                for (int i = 3; i >= 0; i--)
                {
                    if (userPlanetSequence[currentAttempt, i] != null &&
					    touches.Count == 1 && !userPlanetSequence[currentAttempt,i].PlanetTouched &&
                        userPlanetSequence[currentAttempt, i].CollisionRectangle.Contains(touches[0].Position))
                    {
						userPlanetSequence[currentAttempt, i].PlanetTouched = true;
						userPlanetSequence[currentAttempt, i].PlanetSelected = true;
                    }
					else if (userPlanetSequence[currentAttempt, i] != null &&
                        touches.Count == 1 && 
                        !userPlanetSequence[currentAttempt, i].CollisionRectangle.Contains(touches[0].Position))
					{
						userPlanetSequence[currentAttempt, i].PlanetTouched = true;
                        userPlanetSequence[currentAttempt, i].PlanetSelected = false;
					}
					else if (userPlanetSequence[currentAttempt, i] != null && touches.Count == 0)
                    {
						userPlanetSequence[currentAttempt, i].PlanetTouched = false;
                    }
					if (userPlanetSequence[currentAttempt, i] != null && userPlanetSequence[currentAttempt, i].PlanetSelected && !userPlanetSequence[currentAttempt, i].PlanetTouched)
                    {
                        userPlanetSequence[currentAttempt, i] = null;
                    }

                }
                // when all 4 planets selected, show check button
                if (userPlanetSequence[currentAttempt, 0] != null &&
                        userPlanetSequence[currentAttempt, 1] != null &&
                        userPlanetSequence[currentAttempt, 2] != null &&
                        userPlanetSequence[currentAttempt, 3] != null)
                {
                    currentState = GameState.UpdatingCheckButton;				
                }
                
				// unchecking all secret planets
                foreach (Planet planet in secretPlanetSequence)
                {
                    planet.PlanetChecked = false;
                }
            }

            if (currentState == GameState.UpdatingCheckButton)
            {
                checkButton.Update(touches);
                // deleting user planets on touch
				for (int i = 3; i >= 0; i--)
                {
                    if (userPlanetSequence[currentAttempt, i] != null &&
                        touches.Count == 1 && !userPlanetSequence[currentAttempt, i].PlanetTouched &&
                        userPlanetSequence[currentAttempt, i].CollisionRectangle.Contains(touches[0].Position))
                    {
                        userPlanetSequence[currentAttempt, i].PlanetTouched = true;
                        userPlanetSequence[currentAttempt, i].PlanetSelected = true;
                    }
                    else if (userPlanetSequence[currentAttempt, i] != null &&
                        touches.Count == 1 &&
                        !userPlanetSequence[currentAttempt, i].CollisionRectangle.Contains(touches[0].Position))
                    {
                        userPlanetSequence[currentAttempt, i].PlanetTouched = true;
                        userPlanetSequence[currentAttempt, i].PlanetSelected = false;
                    }
                    else if (userPlanetSequence[currentAttempt, i] != null && touches.Count == 0)
                    {
                        userPlanetSequence[currentAttempt, i].PlanetTouched = false;
                    }
                    if (userPlanetSequence[currentAttempt, i] != null && userPlanetSequence[currentAttempt, i].PlanetSelected && !userPlanetSequence[currentAttempt, i].PlanetTouched)
                    {
                        userPlanetSequence[currentAttempt, i] = null;
						currentState = GameState.AddingUserPlanets;
                    }

                }
            }

            if (currentState == GameState.CheckingUserPlanets)
            {                
                brightness = 0;
                // checking suns
                offset = 0;
                for (int i = 0; i < 4; i++)
                {
                    if (!userPlanetSequence[currentAttempt, i].PlanetChecked &&
                        userPlanetSequence[currentAttempt, i].Type == secretPlanetSequence[i].Type)
                    {
                        userPlanetSequence[currentAttempt, i].PlanetChecked = true;
                        secretPlanetSequence[i].PlanetChecked = true;
                        resultSequence.Add(new Planet(PlanetType.Sun, sunSprite,
                            new Vector2(horizontalShowingResultsOffset +
                            offset * horizontalPlanetsOffset,
                            verticalFirstAttemptOffset + currentAttempt * verticalAttemptOffset)));
                        brightness += 25;
                        offset += 1;
                    }
                }
                // checking moons
                for (int i = 0; i < 4; i++)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        if (!userPlanetSequence[currentAttempt, k].PlanetChecked &&
                            !secretPlanetSequence[i].PlanetChecked &&
                            secretPlanetSequence[i].Type == userPlanetSequence[currentAttempt, k].Type)
                        {
                            userPlanetSequence[currentAttempt, k].PlanetChecked = true;
                            secretPlanetSequence[i].PlanetChecked = true;
                            resultSequence.Add(new Planet(PlanetType.Moon, moonSprite,
                                new Vector2(horizontalShowingResultsOffset +
                                offset * horizontalPlanetsOffset,
                                verticalFirstAttemptOffset + currentAttempt * verticalAttemptOffset)));
                            brightness += 10;
                            offset += 1;
                        }
                    }
                }


                if (brightness == 100 ||
                    currentAttempt == GameConstants.MaxAttempts)
                {
                    currentState = GameState.ShowingResults;
                }
                else
                {
                    currentState = GameState.AddingUserPlanets;
                    currentAttempt += 1;
                }
            }


            // Showing results and prompting for play again
            if (currentState == GameState.ShowingResults)
            {
                resultSequence.Clear();
				if (brightness != 100)
				{
					brightness = 0;
				} 
            }

			if (currentState == GameState.ShowingOptions)
			{
				foreach (MenuButton button in optionsButtons)
				{
					button.Update(touches);
				}
				//closeButton.Update(touches);
			}
             

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

			GraphicsDevice.Clear(GetBackGroundColor(brightness));

            spriteBatch.Begin();
			// draw stars
            foreach (Star star in stars)
            { star.Draw(spriteBatch); }

            // draw initial initialPlanets
            if (currentState == GameState.ShowingResults)
            {
                foreach (Planet planet in initialPlanets)
                { planet.Draw(spriteBatch, Color.Gray, true); }
            }
			else if (currentState != GameState.ShowingRules && currentState != GameState.ShowingOptions)
            {
                foreach (Planet planet in initialPlanets)
                { planet.Draw(spriteBatch, Color.White, true); }
            }
            // draw check button if all 4 planets are selected
            if (currentState == GameState.UpdatingCheckButton)
            {
                checkButton.Draw(spriteBatch);
            }
            

            
			if (currentState != GameState.ShowingRules && currentState != GameState.ShowingOptions)
			{
				// draw menu buttons
                foreach (MenuButton button in gameButtons)
                { button.Draw(spriteBatch); }
				// draw planet names
				foreach (GameMessage planetName in planetNames)
                { planetName.Draw(spriteBatch); }
                // draw attempt numbers
                foreach (GameMessage attemptNumber in attemptNumbers)
                { attemptNumber.Draw(spriteBatch); }
                // draw game messages
                foreach (GameMessage message in gameMessages)
                { message.Draw(spriteBatch); }
			}
            
            // draw user planets and 4 suns if user wins
            if (currentState == GameState.ShowingResults && brightness < 100)
            {
                foreach (Planet planet in userPlanetSequence)
                {
                    if (planet != null)
                    {
                        planet.Draw(spriteBatch, Color.Gray, false);
                    }
                }

            }
            else if (currentState == GameState.ShowingResults && brightness == 100)
            {
                for (int j = 0; j < currentAttempt; j++)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (userPlanetSequence[j, i] != null)
                        {
                            userPlanetSequence[j, i].Draw(spriteBatch, Color.Gray, false);
                        }
                    }
                }
                for (int i = 0; i < 4; i++)
                {
                    if (userPlanetSequence[currentAttempt, i] != null)
                    {
                        userPlanetSequence[currentAttempt, i].Draw(spriteBatch, Color.White, false);
                    }
                }
				foreach (Planet planet in winSuns)
                { planet.Draw(spriteBatch, Color.White, false); }
				winMessage.Draw(spriteBatch);
			}
			else if (currentState != GameState.ShowingRules && currentState != GameState.ShowingOptions)
            {
                foreach (Planet planet in userPlanetSequence)
                {
                    if (planet != null)
                    {
						planet.Draw(spriteBatch, Color.White, false);
                    }
                }
            }
            

            // draw checking
			if (currentState != GameState.ShowingRules && currentState != GameState.ShowingOptions)
			{
				foreach (Planet planet in resultSequence)
                { planet.Draw(spriteBatch, Color.White, false); }
			}
            
            // draw secret sequence
            if (currentState == GameState.ShowingResults && brightness < 100)
            {
                foreach (Planet planet in secretPlanetSequence)
                { planet.Draw(spriteBatch, Color.White, false); }
				foreach (GameMessage message in looseMessages)
				{ message.Draw(spriteBatch); }
            }
			if (currentState == GameState.ShowingResults)
			{
				yesButton.Draw(spriteBatch);
				//askMessage.Draw(spriteBatch);
			}
			if (currentState == GameState.ShowingRules)
			{
                rules.DrawMultiline(spriteBatch, rulesWidth);
            }


            // draw options messages and buttons
			if (currentState == GameState.ShowingOptions)
			{
				foreach (GameMessage message in optionsMessages)
				{
					message.Draw(spriteBatch);
				}
				foreach (MenuButton button in optionsButtons)
				{
					button.Draw(spriteBatch);
				}
				//closeButton.Draw(spriteBatch);
			}
            spriteBatch.End();


            base.Draw(gameTime);
        }
		#region Public methods

        /// <summary>
        /// Gets the planet sprite for the given planet type
        /// </summary>
        /// <param name="type">the planet type</param>
        /// <returns>the planet sprite for the type</returns>
        public static Texture2D GetPlanetSprite(PlanetType type)
        {
            // return the appropriate sprite based on the provided planet type
            if (type == PlanetType.Earth)
            {
                return earthSprite;
            }
            else if (type == PlanetType.Jupiter)
            {
                return jupiterSprite;
            }
            else if (type == PlanetType.Mars)
            {
                return marsSprite;
            }
            else if (type == PlanetType.Mercury)
            {
                return mercurySprite;
            }
            else if (type == PlanetType.Neptune)
            {
                return neptuneSprite;
            }
            else if (type == PlanetType.Pluto)
            {
                return plutoSprite;
            }
            else if (type == PlanetType.Saturn)
            {
                return saturnSprite;
            }
            else if (type == PlanetType.Uranus)
            {
                return uranusSprite;
            }
            else
            {
                return venusSprite;
            }
        }


        /// <summary>
        /// Gets background color according to current brightness  0 10 20 25 30 35 40 45 50 55 60 70 75 80 100
        /// </summary>
        /// <param name="brightness">brightness</param>
        /// <returns>the color of background</returns>
        public Color GetBackGroundColor(int brightness)
        {
            return new Color(3 + brightness * (52 - 3) / 100,
                10 + brightness * (76 - 10) / 100,
                50 + brightness * (97 - 50) / 100);

        }

        /// <summary>
        /// Changes the state of the game
        /// </summary>
        /// <param name="newState">the new game state</param>
        public static void ChangeState(GameState newState)
        {
            currentState = newState;
        }


        /// <summary>
        /// Changes the option
        /// </summary>
        /// <param name="option">State of option</param>
        public static void ChangeOption(OptionsList typeOfOption, bool option)
        {
            if (typeOfOption == OptionsList.AllowDuplicates)
            {
                allowDuplicates = option;
            }
            else if (typeOfOption == OptionsList.PlutoIsPlanet)
            {
                plutoIsPlanet = option;
            }
        }



        /// <summary>
        /// calculates the resizing coefficient
        /// </summary>
        /// <returns>resizing coefficient (float)</returns>
        public static float Resizing()
        {
            return (float)currentWidth / NominalWidth;
        }
        #endregion
    }

}


using System;
using Foundation;
using UIKit;
using AVFoundation;

namespace PlanetParade
{
    [Register("AppDelegate")]
    class Program : UIApplicationDelegate
    {
        private static Game1 game;

        internal static void RunGame()
        {
            game = new Game1();
            game.Run();
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            UIApplication.Main(args, null, "AppDelegate");
        }

        public override void FinishedLaunching(UIApplication app)
        {
            AVAudioSession audioSession = AVAudioSession.SharedInstance();

            NSError audioSessionError = new NSError();

            audioSession.SetCategory(new NSString("AVAudioSessionCategoryAmbient"), out audioSessionError);
            RunGame();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using System;

namespace GooglePlayGames
{
    public class InitGoogleServices : MonoBehaviour
    {
        public static event Action E_GoogleServices_AutchComplete;

        public static bool ready = false;

        private void Awake()
        {
            Init();
            Auth();
        }

        private void Init()
        {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
        // enables saving game progress.
        .EnableSavedGames()
        // requests the email address of the player be available.
        // Will bring up a prompt for consent.
        .RequestEmail()
        // requests a server auth code be generated so it can be passed to an
        //  associated back end server application and exchanged for an OAuth token.
        .RequestServerAuthCode(false)
        // requests an ID token be generated.  This OAuth token can be used to
        //  identify the player to other services such as Firebase.
        .RequestIdToken()
        .Build();

            PlayGamesPlatform.InitializeInstance(config);
            PlayGamesPlatform.DebugLogEnabled = true;
            PlayGamesPlatform.Activate();
        }

        private void Auth()
        {
            PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptOnce, (result) => {
                ready = true;
                E_GoogleServices_AutchComplete?.Invoke();
            });
        }
    }
}
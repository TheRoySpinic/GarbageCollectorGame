using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Base;
using UnityEngine.Audio;

namespace Music
{
    public class MusicManager : SingletonGen<MusicManager>
    {
        [SerializeField]
        private AudioMixerSnapshot menuSnapshot = null;
        [SerializeField]
        private AudioMixerSnapshot gameSnapshot = null;

        public override void Init()
        {
            base.Init();
        }

        public override void Destroy()
        {
            base.Destroy();
        }


        public void SetupMenu()
        {
            menuSnapshot.TransitionTo(0.5f);
        }

        public void SetupGame()
        {
            gameSnapshot.TransitionTo(1f);
        }
    }
}
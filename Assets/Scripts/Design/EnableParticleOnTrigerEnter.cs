using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Design
{
    public class EnableParticleOnTrigerEnter : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem[] particles = null;

        [SerializeField]
        private bool singleActive = true;

        private bool active = true;

        private void Awake()
        {
            foreach (ParticleSystem particle in particles)
            {
                particle.gameObject.SetActive(false);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (active && other.gameObject.tag.Equals("Player"))
            {
                foreach (ParticleSystem particle in particles)
                {
                    particle.gameObject.SetActive(true);
                    particle.Play();
                }

                if (singleActive)
                    active = false;
            }
        }
    }
}
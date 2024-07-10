﻿using UnityEngine;

namespace BlazeAIDemo
{
    public class PlayHitSound : MonoBehaviour
    {
        public AudioSource hitSound;
        public FlagAttack flagAttack;

        private void OnTriggerEnter(Collider col)
        {
            if (col.CompareTag("Player") && flagAttack.attacking)
                if (!hitSound.isPlaying)
                    hitSound.Play();
        }
    }
}
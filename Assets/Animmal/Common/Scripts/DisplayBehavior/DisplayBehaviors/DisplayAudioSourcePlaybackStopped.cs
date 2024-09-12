using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Animmal
{
    [AddComponentMenu("Animmal/DisplayBehavior/Hide on AudioSource Playback Stopped")]
    public class DisplayAudioSourcePlaybackStopped : DisplayBehaviorBase
    {
        public AudioSource AudioSource;
        IEnumerator AudioPlaybackTrackingCoroutine;

        private WaitForEndOfFrame _WaitForEndOfFrame;
        public WaitForEndOfFrame WaitForEndOfFrame { get { if (_WaitForEndOfFrame == null) _WaitForEndOfFrame = new WaitForEndOfFrame(); return _WaitForEndOfFrame; } }


        protected override void ShowItem(bool _Instant)
        {
            base.ShowItem(_Instant);
            if (AudioSource == null)
            {
                AudioSource = GetComponent<AudioSource>();
                if (AudioSource == null)
                {
                    Debug.LogError("Ultimate Notification System - DisplayAudioSourcePlaybackStopped - Audio Source Missing on gameobject: " + gameObject.name);
                    return;
                }
            }

            AudioPlaybackTrackingCoroutine = TrackAudioPlayback();
            StartCoroutine(AudioPlaybackTrackingCoroutine);
        }

        protected override void HideItem(bool _Instant)
        {
            if (AudioPlaybackTrackingCoroutine != null)
                StopCoroutine(AudioPlaybackTrackingCoroutine);
            base.HideItem(_Instant);      
        }

        IEnumerator TrackAudioPlayback()
        {
            while (AudioSource.isPlaying)
            {
                yield return WaitForEndOfFrame;
            }
            StartCoroutine(HidingFinishedDelayed());
        }

    }
}
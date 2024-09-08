using System.Collections;
using System.Collections.Generic;
using Common;
using Presentation.GUI;
using Presentation.Helper;
using Runtime;
using UnityEngine;

namespace Presentation.Sound
{
    public class SoundManager : MonoBehaviorInstance<SoundManager>
    {
        [Header("Audio Source")]
        [SerializeField] private AudioSource SFXSource;
        [SerializeField] private AudioSource musicSource;

        [Header("Audio Clips")] 
        [SerializeField] private List<AudioClip> musicList;
        [SerializeField] private List<AudioClip> soundEffects;
        
        private MusicEnum currentBackgroundMusic;
        private float fadeTime = 2f;
        public bool winBoss = false;

        private void Start()
        {
            SFXSource = gameObject.AddComponent<AudioSource>();
            musicSource = gameObject.AddComponent<AudioSource>();
            
            // if current scene is not main menu, hide main menu
            if (SceneManager.Instance.IsCurrentScene(SceneTypeEnum.MAINMENU))
            {
                PlayMusic(MusicEnum.MenuMusic);
            } else if (SceneManager.Instance.IsCurrentScene(SceneTypeEnum.LOBBY))
            {
                Debug.Log("Play lobby music");
                PlayMusic(GlobalVariables.Get<bool>("isWin") ? MusicEnum.WinMusic : MusicEnum.LobbyMusic);
            }
            else
            {
                if (currentBackgroundMusic != MusicEnum.TroopMusic)
                {
                    PlayMusic(MusicEnum.TroopMusic);
                }
                else
                {
                    GlobalVariables.Set("isWin", false);
                    // PlayMusic(MusicEnum);
                }
            }
        }

        public void PlaySound(SoundEnum soundEnum)
        {
            SFXSource.PlayOneShot(soundEffects[(int)soundEnum]);
        }
        
        public void PlayMusic(MusicEnum musicEnum, bool immediate = false)
        {
            StartCoroutine(PlayMusicCaroutine(musicEnum, immediate));
        }

        private IEnumerator PlayMusicCaroutine(MusicEnum musicEnum, bool immediate = false)
        {
            MusicEnum oldMusic = currentBackgroundMusic;
            currentBackgroundMusic = musicEnum;
            if (immediate)
            {
                Debug.Log("Play music immediately");
                musicSource.clip = musicList[(int)musicEnum];
                musicSource.loop = true;
                musicSource.Play();
            }
            else
            {
                if (oldMusic != musicEnum)
                {
                    Debug.Log("Fade out music");
                    StartCoroutine(FadeOutMusic(fadeTime));
                    yield return new WaitForSeconds(fadeTime);
                }

                musicSource.clip = musicList[(int)musicEnum];
                musicSource.loop = true;
                StartCoroutine(FadeInMusic(fadeTime));
                yield return new WaitForSeconds(fadeTime);
            }
        }
        
        private IEnumerator FadeOutMusic(float seconds)
        {
            float startVolume = musicSource.volume;

            while (musicSource.volume > 0)
            {
                musicSource.volume -= startVolume * Time.deltaTime / seconds;
                yield return null;
            }

            musicSource.Stop();
            musicSource.volume = startVolume;
        }
        
        private IEnumerator FadeInMusic(float seconds)
        {
            float startVolume = musicSource.volume;
            musicSource.volume = 0;
            musicSource.Play();

            while (musicSource.volume < startVolume)
            {
                musicSource.volume += startVolume * Time.deltaTime / seconds;
                yield return null;
            }

            musicSource.volume = startVolume;
        }

        public void StopMusic()
        {
            musicSource.Stop();
        }
    }
}

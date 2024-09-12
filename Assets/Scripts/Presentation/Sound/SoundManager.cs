using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using Presentation.GUI;
using Presentation.Helper;
using Runtime;
using UnityEngine;
using Random = UnityEngine.Random;

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

        [Header("InGame Music")] 
        [SerializeField] private List<AudioClip> amadeusMusic;
        [SerializeField] private List<AudioClip> ludwigMusic;
        [SerializeField] private List<AudioClip> maestroTroopMusic;
        [SerializeField] private List<AudioClip> maestroMusic;
        
        private MusicEnum currentBackgroundMusic;
        private float fadeTime = 2f;
        public bool winBoss = false;

        private void Start()
        {
            SFXSource = gameObject.AddComponent<AudioSource>();
            musicSource = gameObject.AddComponent<AudioSource>();
            
            if (!PlayerPrefs.HasKey("sfxVolume"))
            {
                PlayerPrefs.SetFloat("sfxVolume", 1f);
            }
            
            if (!PlayerPrefs.HasKey("musicVolume"))
            {
                PlayerPrefs.SetFloat("musicVolume", 1f);
            }
            
            SFXSource.volume = PlayerPrefs.GetFloat("sfxVolume");
            musicSource.volume = PlayerPrefs.GetFloat("musicVolume");
            
            GlobalVariables.Set("sfxVolume", SFXSource.volume);
            GlobalVariables.Set("musicVolume", musicSource.volume);
            
            // if current scene is not main menu, hide main menu
            if (SceneManager.Instance.IsCurrentScene(SceneTypeEnum.MAINMENU))
            {
                PlayMusic(MusicEnum.MenuMusic);
            } else if (SceneManager.Instance.IsCurrentScene(SceneTypeEnum.LOBBY))
            {
                // Debug.Log("Play lobby music");
                PlayMusic(GlobalVariables.Get<bool>("isWin") ? MusicEnum.WinMusic : MusicEnum.LobbyMusic);
            }
            else
            {
                if (currentBackgroundMusic != MusicEnum.TroopMusic)
                {
                    if (SceneManager.Instance.IsCurrentScene(SceneTypeEnum.AMADEUS_BOSS))
                    {
                        PlayMusic(MusicEnum.AmadeusBossMusic, index: 0);
                    } else if (SceneManager.Instance.IsCurrentScene(SceneTypeEnum.LUDWIG_BOSS))
                    {
                        PlayMusic(MusicEnum.LudwigBossMusic, index: 0);
                    } else if (SceneManager.Instance.IsCurrentScene(SceneTypeEnum.MAESTRO_BOSS))
                    {
                        PlayMusic(MusicEnum.MaestroBossMusic, index: 0);
                    } else if (SceneManager.Instance.IsCurrentScene(SceneTypeEnum.MAESTRO_TROOP))
                    {
                        PlayMusic(MusicEnum.MaestroTroopMusic, index: 0);
                    } else {
                        PlayMusic(MusicEnum.TroopMusic);
                    }
                }
                else
                {
                    GlobalVariables.Set("isWin", false);
                }
            }
        }

        private void Update()
        {
            // Check if musicSource is playing
            if (!musicSource.isPlaying)
            {
                if (SceneManager.Instance.IsCurrentScene(SceneTypeEnum.AMADEUS_BOSS))
                {
                    PlayMusic(MusicEnum.AmadeusBossMusic, true, Random.Range(0, amadeusMusic.Count));
                } else if (SceneManager.Instance.IsCurrentScene(SceneTypeEnum.LUDWIG_BOSS))
                {
                    PlayMusic(MusicEnum.LudwigBossMusic, true, Random.Range(0, ludwigMusic.Count));
                } else if (SceneManager.Instance.IsCurrentScene(SceneTypeEnum.MAESTRO_BOSS))
                {
                    PlayMusic(MusicEnum.MaestroBossMusic, true, Random.Range(0, maestroMusic.Count));
                } else if (SceneManager.Instance.IsCurrentScene(SceneTypeEnum.MAESTRO_TROOP))
                {
                    PlayMusic(MusicEnum.MaestroTroopMusic, true, Random.Range(0, maestroTroopMusic.Count));
                }
                else if (!SceneManager.Instance.IsCurrentScene(SceneTypeEnum.MAINMENU) && !SceneManager.Instance.IsCurrentScene(SceneTypeEnum.LOBBY))
                {
                    PlayMusic(MusicEnum.TroopMusic);
                }
            }
        }
        
        public void SetSFXVolume(float volume)
        {
            GlobalVariables.Set("sfxVolume", volume);
            PlayerPrefs.SetFloat("sfxVolume", volume);
            SFXSource.volume = volume;
        }
        
        public void SetMusicVolume(float volume)
        {
            GlobalVariables.Set("musicVolume", volume);
            PlayerPrefs.SetFloat("musicVolume", volume);
            musicSource.volume = volume;
        }

        public void PlaySound(SoundEnum soundEnum)
        {
            SFXSource.PlayOneShot(soundEffects[(int)soundEnum]);
        }
        
        public void PlayMusic(MusicEnum musicEnum, bool immediate = false, int index = -1)
        {
            StartCoroutine(PlayMusicCaroutine(musicEnum, immediate, index));
        }

        private IEnumerator PlayMusicCaroutine(MusicEnum musicEnum, bool immediate, int index)
        {
            MusicEnum oldMusic = currentBackgroundMusic;
            currentBackgroundMusic = musicEnum;
            if (immediate)
            {
                Debug.Log("Play music immediately");
                if (musicEnum is MusicEnum.AmadeusBossMusic or MusicEnum.LudwigBossMusic or MusicEnum.MaestroBossMusic
                    or MusicEnum.MaestroTroopMusic)
                {
                    if (index == -1)
                    {
                        Debug.LogError("Index is not set");
                        yield break;
                    }

                    musicSource.clip = musicEnum switch
                    {
                        MusicEnum.AmadeusBossMusic => amadeusMusic[index],
                        MusicEnum.LudwigBossMusic => ludwigMusic[index],
                        MusicEnum.MaestroTroopMusic => maestroTroopMusic[index],
                        MusicEnum.MaestroBossMusic => maestroMusic[index],
                        _ => musicSource.clip
                    };
                    musicSource.loop = false;
                } else {
                    musicSource.clip = musicList[(int)musicEnum];
                    musicSource.loop = true;
                }
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

        public float GetMusicVolume()
        {
            return musicSource.volume;
        }
        
        public float GetSFXVolume()
        {
            return SFXSource.volume;
        }
    }
}

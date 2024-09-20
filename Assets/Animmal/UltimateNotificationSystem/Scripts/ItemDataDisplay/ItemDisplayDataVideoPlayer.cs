using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using System;

namespace Animmal.NotificationSystem
{
    [System.Serializable]
    public class NotificationVideoPlayerItem
    {
        public VideoPlayer VideoPlayer;
        public RawImage RawImage;
        public RenderTexture RenderTextureTemplate;
        RenderTexture CachedTexture;

        public bool ErrorCheck(string _GameObjectName)
        {
            if (VideoPlayer == null)
            {
                Debug.LogError("Ultimate Notification System - ItemDisplayDataVideoPlayer - Video Player Missing on gameobject: " + _GameObjectName);
                return false;
            }
            if (RawImage == null)
            {
                Debug.LogError("Ultimate Notification System - ItemDisplayDataVideoPlayer - RawImage Missing on gameobject: " + _GameObjectName);
                return false;
            }
            if (RenderTextureTemplate == null)
            {
                Debug.LogError("Ultimate Notification System - ItemDisplayDataVideoPlayer - RenderTextureTemplate Missing on gameobject: " + _GameObjectName);
                return false;
            }

            return true;
        }

        public void Load(VideoClip _Clip)
        {
            CachedTexture = MonoBehaviour.Instantiate(RenderTextureTemplate);
            VideoPlayer.targetTexture = CachedTexture;
            VideoPlayer.clip = _Clip;
            RawImage.texture = CachedTexture;
            VideoPlayer.Play();
        }

        public void Unload()
        {
            VideoPlayer.Stop();
            VideoPlayer.clip = null;
            CachedTexture = null;
            VideoPlayer.targetTexture = null;
            RawImage.texture = null;
        }
    }
    [AddComponentMenu("Animmal/DisplayBehavior/Display Video Player")]
    public class ItemDisplayDataVideoPlayer : ItemDisplayDataBase
    {
        public List<NotificationVideoPlayerItem> VideoPlayerItems = new List<NotificationVideoPlayerItem>();


        protected override void Init()
        {
            base.Init();
            NotificationItem.OnHidingFinished.AddListener(Unload);
        }

        private void Unload(NotificationItem _Item)
        {
            for (int i = 0; i < VideoPlayerItems.Count; i++)
            {
                if (VideoPlayerItems[i].ErrorCheck(gameObject.name))
                    VideoPlayerItems[i].Unload();
            }
        }

        protected override void DataAssigned(NotificationData _Data)
        {
            for (int i = 0; i < _Data.VideoClips.Count; i++)
            {
                if (i < VideoPlayerItems.Count)
                {
                    if (VideoPlayerItems[i].ErrorCheck(gameObject.name))
                        VideoPlayerItems[i].Load(_Data.VideoClips[i]);
                }
            }

            base.DataAssigned(_Data);
        }
    }
}
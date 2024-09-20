using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

namespace Animmal.NotificationSystem
{
    [System.Serializable]
    public partial class NotificationData
    {
        public int StyleVariationID = 0;
        public List<string> Texts = new List<string>();
        public List<Sprite> Sprites = new List<Sprite>();
        public List<Texture> Textures = new List<Texture>();
        public List<VideoClip> VideoClips = new List<VideoClip>();
        public List<AudioClip> AudioClips = new List<AudioClip>();
    }
}
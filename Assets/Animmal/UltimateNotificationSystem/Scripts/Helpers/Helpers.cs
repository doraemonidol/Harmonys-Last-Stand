using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Animmal.NotificationSystem
{
    public static partial class Helpers 
    {
        public static NotificationData GetACopyOfNotificationData(NotificationData _Data)
        {
            NotificationData _NewData = new NotificationData();
            _NewData.StyleVariationID = _Data.StyleVariationID;
            _NewData.Texts.AddRange(_Data.Texts);
            _NewData.Sprites.AddRange(_Data.Sprites);
            _NewData.Textures.AddRange(_Data.Textures);
            _NewData.VideoClips.AddRange(_Data.VideoClips);
            _NewData.AudioClips.AddRange(_Data.AudioClips);
            return _NewData;
        }
    }
}
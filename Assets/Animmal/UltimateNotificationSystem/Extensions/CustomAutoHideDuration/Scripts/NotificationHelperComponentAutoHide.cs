using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Animmal.NotificationSystem
{
    [AddComponentMenu("Animmal/NotificationSystem/Extensions/Helpers/Notification Helper Component")]
    public class NotificationHelperComponentAutoHide : NotificationHelperComponent
    {
        public override void ShowNotification()
        {
            NotificationManager.Instance.ShowNotification(StyleID, GetACopyOfNotificationData(NotificationData));
        }


        public virtual NotificationData GetACopyOfNotificationData(NotificationData _Data)
        {
            NotificationData _NewData = new NotificationData();
            _NewData.StyleVariationID = _Data.StyleVariationID;
            _NewData.Texts.AddRange(_Data.Texts);
            _NewData.Sprites.AddRange(_Data.Sprites);
            _NewData.AutoHideDuration = _Data.AutoHideDuration;
            return _NewData;
        }
    }
}
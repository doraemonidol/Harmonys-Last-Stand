using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Animmal.NotificationSystem
{
    [AddComponentMenu("Animmal/NotificationSystem/Extensions/DisplayBehavior/Notification Display Lerp Custom Autohide")]
    public class ItemDisplayCanvasGroupLerpCustoAutoHide : DisplayCanvasGroupLerp
    {
        public NotificationItem _NotificationItem;
        public NotificationItem NotificationItem { get { if (_NotificationItem == null) GetComponent<NotificationItem>(); return _NotificationItem; } }

        protected override void Init()
        {
            base.Init();
            if (NotificationItem != null)
                NotificationItem.OnDataAssign.AddListener(DataAssigned);
        }

        private void DataAssigned(NotificationData _NotificationData)
        {
            if (_NotificationData.AutoHideDuration < 0)
                return;
            AutoHideTriggerDelay = _NotificationData.AutoHideDuration;
        }
    }
}
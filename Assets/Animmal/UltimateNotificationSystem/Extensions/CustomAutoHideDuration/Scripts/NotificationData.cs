using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Animmal.NotificationSystem
{
    public partial class NotificationData
    {
        [Tooltip("If this is not -1, and item has 'Notification Display Lerp Custom Autohide' component this value will override Auto Hide Duration of Notification Item ")]
        public float AutoHideDuration = -1;
    }
}
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Notifications.Android;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Playables;

public class NotificationManager : MonoBehaviour
{
    public static NotificationManager Instance;

    // Start is called before the first frame update
    void Awake ()
    {
        if ( !Permission.HasUserAuthorizedPermission ( "android.permission.POST_NOTIFICATIONS" ) )
        {
            Permission.RequestUserPermission ( "android.permission.POST_NOTIFICATIONS" );
        }

        //Creating new channel to show notifications
        var channel = new AndroidNotificationChannel ()
        {
            Id = "InnerCalm",
            Name = "Inner Calm Channel",
            Importance = Importance.Default,
            Description = "Inner Calm Notification",
        };

        AndroidNotificationCenter.RegisterNotificationChannel ( channel );
    }
}

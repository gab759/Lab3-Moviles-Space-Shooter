using UnityEngine;
#if UNITY_ANDROID
using Unity.Notifications.Android;
using UnityEngine.Android;
#endif

public class NotificationInitializer : MonoBehaviour
{
    private const string idCanal = "default_channel";

    private void Awake()
    {
#if UNITY_ANDROID
        RequestAuthorization();
        RegisterNotificationChannel();
#endif
    }

#if UNITY_ANDROID
    private void RequestAuthorization()
    {
        if (!Permission.HasUserAuthorizedPermission("android.permission.POST_NOTIFICATIONS"))
        {
            Permission.RequestUserPermission("android.permission.POST_NOTIFICATIONS");
        }
    }

    private void RegisterNotificationChannel()
    {
        AndroidNotificationChannel channel = new AndroidNotificationChannel
        {
            Id = idCanal,
            Name = "Canal Puntajes",
            Importance = Importance.Default,
            Description = "Notificaciones de puntajes del videojuego"
        };

        AndroidNotificationCenter.RegisterNotificationChannel(channel);
    }
#endif
}
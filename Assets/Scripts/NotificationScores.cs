using System;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_ANDROID
using Unity.Notifications.Android;
#endif

public class NotificationScores : MonoBehaviour
{
    private const string channelId = "default_channel";

    [Header("Score Scriptable Object")]
    public ScoreRecordSO scoreRecordSO;
    public float currentScore = 0f;

    public void SetCurrentScore(float newScore)
    {
        currentScore = newScore;
    }

#if UNITY_ANDROID
    private void OnDisable()
    {
        SendResultsNotification();
    }
#endif

#if UNITY_ANDROID
    private void SendResultsNotification()
    {
        bool isNewRecord = false;
        if (currentScore > scoreRecordSO.maxScore)
        {
            isNewRecord = true;
            scoreRecordSO.maxScore = currentScore;
        }

        string title = "Ronda Terminada";
        string text = "Tu puntaje: " + ((int)currentScore).ToString();
        string icon = "icon_0";

        if (isNewRecord)
        {
            title = "Nuevo Puntaje Máximo";
            icon = "icon_1";
        }

        AndroidNotification notification = new AndroidNotification();
        notification.Title = title;
        notification.Text = text;
        notification.FireTime = DateTime.Now.AddSeconds(0);
        notification.SmallIcon = icon;
        notification.LargeIcon = icon;

        AndroidNotificationCenter.SendNotification(notification, channelId);
    }
#endif
}
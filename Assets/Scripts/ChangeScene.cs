using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public GyroMovement player;

    private bool hasChangedScene = false;

    void Update()
    {
        if (!hasChangedScene && player.stats.maxHealth <= 0f)
        {
            hasChangedScene = true;
            SceneManager.LoadScene("Results");
        }
    }
}
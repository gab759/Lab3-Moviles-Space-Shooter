using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class MenuSeleccion : MonoBehaviour
{
    public StatsPlayers[] navesStats;
    public TextMeshProUGUI[] statsTexts;
    public Button[] botonesNaves;
    int index;
    void Start()
    {

        for (int i = 0; i < 3; i++)
        {
             index = i;

            statsTexts[i].text =
                "Life: " + navesStats[i].maxHealth +
                "\nSpeed: " + navesStats[i].speedY +
                "\nScore: " + navesStats[i].scoreSpeed;

            botonesNaves[i].onClick.AddListener(() => SeleccionarNave(index));
        }
    }

    void SeleccionarNave(int indexNave)
    {
        StatsPlayers selectedStats = navesStats[indexNave];
        StatsPlayers.naveSeleccionada = selectedStats;

        // Actualizar GameManager ANTES de cambiar de escena
        if (GameManager.Instance != null)
        {
            GameManager.Instance.playerStats = selectedStats; // ¡Actualiza los stats!
        }

        SceneManager.LoadScene("MainGame");
    }
}
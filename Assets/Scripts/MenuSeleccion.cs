using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuSeleccion : MonoBehaviour
{
    public StatsPlayers[] navesStats;
    public TextMeshProUGUI[] statsTexts;
    public Button[] botonesNaves;

    void Start()
    {
        for (int i = 0; i < navesStats.Length; i++)
        {
            int index = i;

            statsTexts[i].text =
                "Vida: " + navesStats[i].maxHealth +
                "\nManejo: " + navesStats[i].speedY +
                "\nVelocidad Puntaje: " + navesStats[i].scoreSpeed;

            Image buttonImage = botonesNaves[i].GetComponent<Image>();
            if (buttonImage != null)
            {
                buttonImage.color = navesStats[i].shipColor;
            }

            botonesNaves[i].onClick.AddListener(() => SeleccionarNave(index));
        }
    }

    void SeleccionarNave(int indexNave)
    {
        StatsPlayers.naveSeleccionada = navesStats[indexNave];

        SceneManager.LoadScene("MainGame");
    }
}

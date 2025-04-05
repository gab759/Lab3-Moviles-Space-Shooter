using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class MenuSeleccion : MonoBehaviour
{
    public StatsPlayers[] navesStats; 
    public TextMeshProUGUI[] statsTexts;
    public Button botonConfirmar; 

    private int naveSeleccionadaIndex = 0;

    void Start()
    {
        for (int i = 0; i < navesStats.Length; i++)
        {
            ActualizarStatsTexto(i);
        }

        botonConfirmar.onClick.AddListener(() =>
        {
            PlayerPrefs.SetInt("NaveSeleccionadaIndex", naveSeleccionadaIndex);
            SceneManager.LoadScene("MainGame");
        });
    }

    void ActualizarStatsTexto(int indexNave)
    {
        StatsPlayers stats = navesStats[indexNave];
        statsTexts[indexNave].text =
            $"Life: {stats.maxHealth}\nSpeed: {stats.speedY}\nScore Speed: {stats.scoreSpeed}";
    }

    public void SeleccionarNave(int index)
    {
        naveSeleccionadaIndex = index;
    }
}
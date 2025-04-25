using UnityEngine;

public class Panel : MonoBehaviour
{
    [SerializeField] private GameObject panelTarget;

    public void ShowPanel()
    {
        if (panelTarget != null)
            panelTarget.SetActive(true);
    }

    public void ExitPanel()
    {
        if (panelTarget != null)
            panelTarget.SetActive(false);
    }
}
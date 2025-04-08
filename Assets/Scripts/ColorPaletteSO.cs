using UnityEngine;

[CreateAssetMenu(fileName = "ColorPaletteSO", menuName = "Scriptable Objects/ColorPaletteSO")]
public class ColorPaletteSO : ScriptableObject
{
    [Header("UI Colors")]
    public Color primaryColor = Color.white;
    public Color secondaryColor = Color.gray;
    public Color textColor = Color.black;
    public Color buttonHoverColor = Color.blue;
    
    [Header("Game Elements")]
    public Color playerColor = Color.cyan;
    public Color enemyColor = Color.red;
    public Color projectileColor = Color.yellow;
}

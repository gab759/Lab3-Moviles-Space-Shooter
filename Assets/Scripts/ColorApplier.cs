using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ColorApplier : MonoBehaviour
{
    [SerializeField] private ColorPaletteSO colorPalette;
    
    [Header("UI Elements")]
    [SerializeField] private Image[] primaryUIElements;
    [SerializeField] private Image[] secondaryUIElements;
    [SerializeField] private TMP_Text[] textElements;
    [SerializeField] private Button[] interactiveButtons;

    [Header("Game Elements")]
    [SerializeField] private SpriteRenderer playerSprite;
    [SerializeField] private SpriteRenderer[] enemySprites;

    private void Start() 
    {
        ApplyColors();
    }

    private void ApplyColors() 
    {
        ApplyToImages(primaryUIElements, colorPalette.primaryColor);
        ApplyToImages(secondaryUIElements, colorPalette.secondaryColor);
        ApplyToTMPTexts(textElements, colorPalette.textColor);
        ApplyToButtons(interactiveButtons);
        ApplyToSprites(playerSprite, colorPalette.playerColor);
        ApplyToSprites(enemySprites, colorPalette.enemyColor);
    }

    private void ApplyToImages(Image[] elements, Color color) 
    {
        if (elements == null) return;
        for (int i = 0; i < elements.Length; i++) 
        {
            if (elements[i] != null) elements[i].color = color;
        }
    }

    private void ApplyToTMPTexts(TMP_Text[] elements, Color color) 
    {
        if (elements == null) return;
        for (int i = 0; i < elements.Length; i++) 
        {
            if (elements[i] != null) elements[i].color = color;
        }
    }

    private void ApplyToButtons(Button[] buttons) 
    {
        if (buttons == null) return;
        for (int i = 0; i < buttons.Length; i++) 
        {
            if (buttons[i] == null) continue;
            ColorBlock cb = buttons[i].colors;
            cb.normalColor = colorPalette.secondaryColor;
            cb.highlightedColor = colorPalette.buttonHoverColor;
            buttons[i].colors = cb;
        }
    }

    private void ApplyToSprites(SpriteRenderer sprite, Color color) 
    {
        if (sprite != null) sprite.color = color;
    }

    private void ApplyToSprites(SpriteRenderer[] sprites, Color color) 
    {
        if (sprites == null) return;
        for (int i = 0; i < sprites.Length; i++) 
        {
            if (sprites[i] != null) sprites[i].color = color;
        }
    }
}

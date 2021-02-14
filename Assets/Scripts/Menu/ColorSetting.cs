using UnityEngine;
using System;

public class ColorSetting : MonoBehaviour
{
    [SerializeField] private Color[] colors;
    [SerializeField] private GameObject colorPanel = null;
    private Color color;

    public static event Action<Color> HandleChangeColor;
    public void OpenColorPanel()
    {
        colorPanel.SetActive(true);
    }

    public void CloseColorPanel()
    {
        colorPanel.SetActive(false);
    }

    public void SetColor(int colorIndex)
    {
        this.color = colors[colorIndex];
        HandleChangeColor?.Invoke(color);
        CloseColorPanel();
    }
}

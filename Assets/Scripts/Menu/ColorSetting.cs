using UnityEngine;
using System;

public class ColorSetting : MonoBehaviour
{
    [SerializeField] private Color[] colors;
    [SerializeField] private GameObject colorPanel = null;
    public static Color color { get; private set; }

    public static event Action<Color> HandleChangeColor;

    private void Awake()
    {
        color = colors[0];
    }

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
        color = colors[colorIndex];
        HandleChangeColor?.Invoke(color);
        CloseColorPanel();
    }
}

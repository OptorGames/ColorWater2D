using System;
using UnityEngine;
using UnityEngine.UI;

public class ThemeButton : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Button button;

    private int _theme;

    public event Action<int> OnButtonClicked;

    public void SetInteractable(bool value) => button.interactable = value;

    public void Init(Sprite sprite, int theme, Action<int> action)
    {
        image.sprite = sprite;
        _theme = theme;
        OnButtonClicked += action;
    }

    public void ClickButton() => OnButtonClicked?.Invoke(_theme);
}

using UIPages;
using UnityEngine;
using UnityEngine.UI;

public class ToggleUpdater : MonoBehaviour
{
    public void Configure(UIPage _page)
    {
        var tg = GetComponent<Toggle>();
        _page.PageActivatedEvent += () =>
        {
            tg.toggleTransition = Toggle.ToggleTransition.None;
            tg.isOn= false;
            tg.toggleTransition = Toggle.ToggleTransition.Fade;
        };
    }
}
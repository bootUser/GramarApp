using System.Collections;
using System.Collections.Generic;
using UIPages;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollViewUpdater : MonoBehaviour
{
    [SerializeField] private UIPage _page;
    void Start()
    {
        var sr = GetComponent<ScrollRect>();
        _page.PageActivatedEvent += () => { sr.verticalScrollbar.value = 1; };
    }
}

using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class InfoBox : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI headerText;
    [SerializeField] private TextMeshProUGUI messageText;

    private Queue<(string header, string message)> _queue = new Queue<(string header, string message)>();
    private bool isShowing;

    public void Show(string header, string message)
    {
        if(!isShowing)
            isShowing = true;
        else
        {
            _queue.Enqueue((header, message));
            return;
        }
        headerText.text = header;
        messageText.text = message;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        isShowing = false;
        gameObject.SetActive(false);
        if(_queue.Count > 0)
        {
            var data = _queue.Dequeue();
            Show(data.header, data.message);
        }
    }
}

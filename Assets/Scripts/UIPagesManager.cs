using System.Collections;
using UIPages;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIPagesManager : MonoBehaviour
{
    [SerializeField] private UIPage _startPage;
    private UIPage _currentScreen;

    private bool _waiting;

    private void Awake()
    {
        foreach(var screen in GameObject.FindGameObjectsWithTag("Screen"))
            screen.SetActive(false);
    }
    private void Start()
    {
        _currentScreen = _startPage;
        _startPage.gameObject.SetActive(true);
        _startPage.ActivatePage();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
            StartCoroutine(BackAndWait(0.2f));
    }

    private IEnumerator BackAndWait(float seconds)
    {
        if(_waiting)
            yield break;
        _waiting = true;
        if(_currentScreen.previousPage != null)
            SetCurrentScreen(_currentScreen.previousPage);
        else if(_currentScreen.previousScene != null && _currentScreen.previousScene != "")
            LoadScene(_currentScreen.previousScene);
        yield return new WaitForSeconds(seconds);
        _waiting = false;
    }

    public void SetCurrentScreen(UIPage screen)
    {
        if (_currentScreen.name == screen.name)
            return;
        _currentScreen.DisablePage();
        _currentScreen.gameObject.SetActive(false);
        _currentScreen = screen;
        screen.gameObject.SetActive(true);
        screen.ActivatePage();
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name, LoadSceneMode.Single);
    }
}

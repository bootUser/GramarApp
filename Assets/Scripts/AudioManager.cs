using UIPages;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private AudioClip rightAnswerSound;
    [SerializeField] private AudioClip wrongAnswerSound;
    [SerializeField] private AudioClip moneyEarnedSound;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayRightAnswerSound()
    {
        if (!AppSettings.RightAnswerSoundEnabled)
            return;
        _audioSource.clip = rightAnswerSound;
        _audioSource.Play();
    }
    public void PlayWrongAnswerSound()
    {
        if (!AppSettings.WrongAnswerSoundEnabled)
            return;
        _audioSource.clip = wrongAnswerSound;
        _audioSource.Play();
    }
    public void PlayMoneyEarnedSound()
    {
        if (!AppSettings.MoneyEarnedSoundEnabled)
            return;
        _audioSource.clip = moneyEarnedSound;
        _audioSource.Play();
    }
    
}
using UnityEngine;
using UnityEngine.UI;

public class SoundPlayer : MonoBehaviour
{
    private Button _button;
    private AudioSource _audio;
    private void Awake()
    {
        _button = GetComponent<Button>();
        _audio = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _button.onClick.AddListener(() => PlaySound());        
    }

    private void PlaySound() 
    {
        _audio.PlayOneShot(_audio.clip);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(() => PlaySound());
    }
}

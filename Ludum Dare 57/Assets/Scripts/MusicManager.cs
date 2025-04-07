using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] GameObject breathingSound;
    [SerializeField] GameObject leviathanMusic;

    void OnEnable()
    {
        GameEvents.OnPlayerEnterSubmarine += SwitchMusic;
    }
    void OnDisable()
    {
        GameEvents.OnPlayerEnterSubmarine -= SwitchMusic;
    }

    void SwitchMusic()
    {
        breathingSound.SetActive(false);
        leviathanMusic.SetActive(true);
    }
}

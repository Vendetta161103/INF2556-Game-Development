using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // überlebe Scene Reload
    }   
        else
        {
            Destroy(gameObject);  // doppelten MusicManager löschen
        }
    }
}
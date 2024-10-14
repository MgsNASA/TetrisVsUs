using UnityEngine;

public class MusicService : MonoBehaviour
{
    public static MusicService Instance
    {
        get; private set;
    } // Синглтон для доступа к сервису музыки

    public AudioSource audioSource; // Компонент AudioSource для проигрывания музыки
    public AudioClip [ ] musicTracks; // Массив треков музыки
    public int currentTrackIndex = 0; // Индекс текущего трека

    public float musicVolume = 1f; // Громкость музыки



    private void Start( )
    {
        audioSource.volume = musicVolume;
        PlayMusic (); // Воспроизводим первый трек при запуске
    }

    public void PlayMusic( )
    {
        if ( musicTracks.Length == 0 )
            return;

        audioSource.clip = musicTracks [ currentTrackIndex ];
        audioSource.loop = true; // Зацикливаем музыку
        audioSource.Play ();
    }

    public void StopMusic( )
    {
        audioSource.Stop ();
    }

    public void PauseMusic( )
    {
        audioSource.Pause ();
    }

    public void ResumeMusic( )
    {
        audioSource.UnPause ();
    }

    public void NextTrack( )
    {
        currentTrackIndex = ( currentTrackIndex + 1 ) % musicTracks.Length;
        PlayMusic ();
    }

    public void PreviousTrack( )
    {
        currentTrackIndex--;
        if ( currentTrackIndex < 0 )
        {
            currentTrackIndex = musicTracks.Length - 1;
        }
        PlayMusic ();
    }

    public void SetVolume( float volume )
    {
        musicVolume = volume;
        audioSource.volume = musicVolume;
    }
}

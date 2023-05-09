using UnityEngine;

[CreateAssetMenu(menuName = "Audio Manager")]
public class AudioManager : ScriptableObject
{
    public AudioChannel masterChannel;
    public AudioChannel musicChannel;
    public AudioChannel effectsChannel;

    public void SetMasterVolume(float volume)
    {
        masterChannel.volume = volume;
        // Actualizar los canales secundarios cuando se ajuste el volumen maestro
        musicChannel.volume = volume;
        effectsChannel.volume = volume;
    }

    public void SetMusicVolume(float volume)
    {
        musicChannel.volume = volume;
    }

    public void SetEffectsVolume(float volume)
    {
        effectsChannel.volume = volume;
    }
}

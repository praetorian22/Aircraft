using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    public float volumeMusic = 1f;
    public typeTrack typeTrack;

    public AudioClip traffic_traffic;
    public AudioClip vertical_speed;
    public AudioClip climb_climb;
    public AudioClip descent_descent;
    public AudioClip crossing_climb;
    public AudioClip crossing_descent;

    private void MakeSound(AudioClip original, Vector3 position)
    {
        AudioSource.PlayClipAtPoint(original, position, volumeMusic);
    }
    private void MakeSoundCicle(AudioClip original)
    {
        audioSource.volume = volumeMusic;
        audioSource.clip = original;
        audioSource.Play();
    }
    public void StopMusic()
    {
        audioSource.Stop();
    }

    public void SoundTraffic_Traffic()
    {
        if (typeTrack != typeTrack.traffic_traffic) MakeSoundCicle(traffic_traffic);
    }
    public void SoundVertical_Speed()
    {
        if (typeTrack != typeTrack.vertical_speed) MakeSoundCicle(vertical_speed);
    }
    public void SoundClimb_Climb()
    {
        if (typeTrack != typeTrack.climb_climb) MakeSoundCicle(climb_climb);
    }
    public void SoundDescent_Descent()
    {
        if (typeTrack != typeTrack.descent_descent) MakeSoundCicle(descent_descent);
    }
    public void SoundCrossing_Climb()
    {
        if (typeTrack != typeTrack.crossing_climb) MakeSoundCicle(crossing_climb);
    }
    public void SoundCrossing_Descent()
    {
        if (typeTrack != typeTrack.crossing_descent) MakeSoundCicle(crossing_descent);
    }
}

public enum typeTrack
{
    traffic_traffic,
    vertical_speed,
    climb_climb,
    descent_descent,
    crossing_descent,
    crossing_climb,
}
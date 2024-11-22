using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private typeTrack typeTrack;
    private float volumeSound = 0.8f;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip traffic_traffic;
    [SerializeField] private AudioClip vertical_speed;
    [SerializeField] private AudioClip climb_climb;
    [SerializeField] private AudioClip descent_descent;
    [SerializeField] private AudioClip crossing_climb;
    [SerializeField] private AudioClip crossing_descent;
    [SerializeField] private AudioClip increase_climb;
    [SerializeField] private AudioClip increase_descent;
    [SerializeField] private AudioClip climb_climp_now;
    [SerializeField] private AudioClip descent_descent_now;
    [SerializeField] private AudioClip clear_of_conflict;

    private bool taSPSV;

    public void NewSimulation()
    {
        typeTrack = typeTrack.none;        
    }

    private void MakeSound(AudioClip original, Vector3 position)
    {
        AudioSource.PlayClipAtPoint(original, position, volumeSound);
    }
    private void MakeSoundCicle(AudioClip original)
    {
        if (taSPSV && original != traffic_traffic) return;
        audioSource.volume = volumeSound;
        audioSource.clip = original;
        audioSource.Play();
    }
    public void StopSound()
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
    public void SoundIncrease_Climp()
    {
        if (typeTrack != typeTrack.increase_climb) MakeSoundCicle(increase_climb);
    }
    public void SoundIncrease_Descent()
    {
        if (typeTrack != typeTrack.increase_descent) MakeSoundCicle(increase_descent);
    }
    public void SoundClimb_Climp_Now()
    {
        if (typeTrack != typeTrack.climb_climp_now) MakeSoundCicle(climb_climp_now);
    }
    public void SoundDescent_Descent_Now()
    {
        if (typeTrack != typeTrack.descent_descent_now) MakeSoundCicle(descent_descent_now);
    }
    public void SoundClear_Of_Conflict()
    {
        if (typeTrack != typeTrack.clear_of_conflict) MakeSoundCicle(clear_of_conflict);
    }

    public void SoundOff()
    {
        volumeSound = 0f;
        StopSound();
    }

    public void SoundOn()
    {
        volumeSound = 0.8f;
        StopSound();
    }

    public void TaSPSVOn()
    {
        taSPSV = true;
    }
    public void TaSPSVOff()
    {
        taSPSV = false;
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
    increase_climb,
    increase_descent,
    climb_climp_now,
    descent_descent_now,
    clear_of_conflict,
    none,
}
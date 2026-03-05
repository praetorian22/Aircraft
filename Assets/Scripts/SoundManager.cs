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
    [SerializeField] private AudioClip maintain_vertical_speed;
    [SerializeField] private AudioClip slink_rate;
    [SerializeField] private AudioClip pull_up;
    [SerializeField] private AudioClip terrain_terrain;
    [SerializeField] private AudioClip too_low_terrain;
    [SerializeField] private AudioClip too_low_gear;
    [SerializeField] private AudioClip caution_terrain;
    [SerializeField] private AudioClip terrain_terrain_pull_up;
    [SerializeField] private AudioClip clide_slope;

    private bool taSPSV;
    private bool soundOff;

    public void NewSimulation()
    {
        typeTrack = typeTrack.none;        
    }
    public void SetAudioSource(AudioSource audioSource)
    {
        this.audioSource = audioSource;
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
    public void SoundMaintain_Vertical_Speed()
    {
        if (typeTrack != typeTrack.maintain_vertical_speed) MakeSoundCicle(maintain_vertical_speed);
    }
    public void SoundSlinkRate()
    {
        if (typeTrack != typeTrack.slink_rate) MakeSoundCicle(slink_rate);
    }
    public void SoundPullUp()
    {
        if (typeTrack != typeTrack.pull_up) MakeSoundCicle(pull_up);
    }
    public void Terrain_Terrain()
    {
        if (typeTrack != typeTrack.terrain_terrain) MakeSoundCicle(terrain_terrain);
    }
    public void Too_Low_Terrain()
    {
        if (typeTrack != typeTrack.too_low_terrain) MakeSoundCicle(too_low_terrain);
    }
    public void Too_Low_Gear()
    {
        if (typeTrack != typeTrack.too_low_gear) MakeSoundCicle(too_low_gear);
    }
    public void CautionTerrain()
    {
        if (typeTrack != typeTrack.caution_terrain) MakeSoundCicle(caution_terrain);
    }
    public void TerrainTerrainPullUp()
    {
        if (typeTrack != typeTrack.terrain_terrain_pull_up) MakeSoundCicle(terrain_terrain_pull_up);
    }
    public void ClideSlope()
    {
        if (typeTrack != typeTrack.clide_slope) MakeSoundCicle(clide_slope);
    }

    public void SoundOff()
    {
        soundOff = true;
        volumeSound = 0f;
        StopSound();
    }

    public void SoundOn()
    {
        soundOff = false;
        volumeSound = 0.3f;
        StopSound();
    }
    public void SetVolume(float value)
    {
        if (!soundOff) volumeSound = value;
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
    maintain_vertical_speed,
    none,
    slink_rate,
    pull_up,
    terrain_terrain,
    too_low_terrain,
    too_low_gear,
    caution_terrain,
    terrain_terrain_pull_up,
    clide_slope,
}
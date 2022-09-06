using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private List<AudioSource> songs = new List<AudioSource>();
    [SerializeField] private List<float> songOriginalVolume = new List<float>();

    [Space(5)]

    [SerializeField] private AudioSource coin;
    [SerializeField] private AudioSource buttonClickLight;
    [SerializeField] private AudioSource buttonClickStrong;
    [SerializeField] private AudioSource footstep;

    [Space(5)]

    [SerializeField] private AudioSource[] swordShort;
    [SerializeField] private AudioSource[] swordLong;

    [Space(5)]
    [SerializeField] private AudioSource[] swordHit;
    [SerializeField] private AudioSource[] damageHit;

    private AudioSource actualSong;
    private float footstepInitialVolume;
    private float footstepInitialPitch;

    private void Start()
    {
        if (songs.Count > 0)
        {
            actualSong = songs[0];
            for (int i = 0; i < songs.Count; i++)
                songOriginalVolume.Add(songs[i].volume);
        }
        if (footstep)
        {
            footstepInitialVolume = footstep.volume;
            footstepInitialPitch = footstep.pitch;
        }
        UnpauseSong();
    }

    public void Coin()
    {
        coin.Play();
    }

    public void SwordShort()
    {
        int random = Random.Range(0, swordShort.Length);
        swordShort[random].Play();
    }
    public void SwordLong()
    {
        int random = Random.Range(0, swordLong.Length);
        swordLong[random].Play();
    }
    public void SwordHit()
    {
        int random = Random.Range(0, swordHit.Length);
        swordHit[random].Play();
    }
    public void DamageHit()
    {
        int random = Random.Range(0, damageHit.Length);
        damageHit[random].Play();
    }

    public void ButtonClickStrong()
    {
        buttonClickStrong.Play();
    }
    public void ButtonClickLight()
    {
        buttonClickLight.Play();
    }

    public void Footstep(bool enable, float volumeScale)
    {
        if (enable && !footstep.isPlaying)
            footstep.Play();
        else if(!enable && footstep.isPlaying)
            footstep.Stop();

        float volumeScaleClamp = Mathf.Clamp01(volumeScale);

        footstep.pitch = footstepInitialPitch * volumeScaleClamp;
        footstep.volume = footstepInitialVolume * volumeScaleClamp;
    }

    public void ChangeSong(int number)
    {
        if (actualSong == songs[number])
            return;
        StartCoroutine(CrossfadeAudio(actualSong, songs[number], 2f));
    }
    public void ChangeSongQuickly(int number)
    {
        if (actualSong == songs[number])
            return;
        StartCoroutine(CrossfadeAudio(actualSong, songs[number], 0.5f));
    }

    public void SongPitchDown()
    {
        actualSong.pitch *= 0.6f;
    }

    public void PauseSong()
    {
        audioMixer.SetFloat("MusicVolume", -80);
    }
    public void UnpauseSong()
    {
        audioMixer.SetFloat("MusicVolume", 0);
    }

    private IEnumerator CrossfadeAudio(AudioSource clipOut, AudioSource clipIn, float duration)
    {        
        float time = 0;
        float clipOutVolume = clipOut.volume;
        float clipInVolume = songOriginalVolume[songs.IndexOf(clipIn)];
        clipIn.volume = 0;
        clipIn.Play();

        while (time < duration)
        {
            clipOut.volume = Mathf.Lerp(clipOutVolume, 0, time / duration);
            clipIn.volume = Mathf.Lerp(0, clipInVolume, time / duration);
            time += Time.unscaledDeltaTime;
            yield return null;
        }
        clipOut.volume = 0;
        clipIn.volume = clipInVolume;
        clipOut.Stop();
        actualSong = clipIn;
    }
}

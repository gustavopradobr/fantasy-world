using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
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

    private void Start()
    {
        actualSong = songs[0];
        for(int i=0; i<songs.Count; i++)
            songOriginalVolume.Add(songs[i].volume);
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

    public void Footstep(bool enable)
    {
        if (enable && !footstep.isPlaying)
            footstep.Play();
        else if(!enable && footstep.isPlaying)
            footstep.Stop();
    }

    public void ChangeSong(int number)
    {
        if (actualSong == songs[number])
            return;
        StartCoroutine(CrossfadeAudio(actualSong, songs[number], 2f));
    }

    public void SongPitchDown()
    {
        actualSong.pitch *= 0.6f;
    }

    public void PauseSong()
    {
        actualSong.Pause();
    }
    public void UnpauseSong()
    {
        actualSong.UnPause();
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

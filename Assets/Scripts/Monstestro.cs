using UnityEngine;

public class Monstestro : MonoBehaviour
{
    public float bpm;
    public float firstOffset;
    public AudioSource music;
    public static Monstestro Instance;

    public float secPer => 60f / bpm;
    public float songTime => music.time - firstOffset;
    public float beat => songTime / secPer;
    public bool isPlaying => music.isPlaying;

    public void Awake()
    {
        Instance = this;
    }
    
    public void startSong()
    {
        music.time = 0f;
        music.Play();
    }

    public void stopSong()
    {
        music.Stop();
    }

    public float beatTime(float beat)
    {
        return beat * secPer;
    }
    void Start()
    {
        startSong();
    }

    void Update()
    {
        Debug.Log(beat);
    }
}

/*

7. TEST IT (temporary)
- Press Play. In the Console the number should climb steadily: about 2 per second at 120 BPM, about 1.67 at 100. If it stays 0, the song isn't playing.
- Remove the Debug.Log afterwards; it floods the Console. Leave the StartSong call in Start until step 2.3 is done. Later the WaveManager calls it.
*/
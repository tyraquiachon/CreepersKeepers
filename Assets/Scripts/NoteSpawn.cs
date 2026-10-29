using UnityEngine;
using System.Collections.Generic;

public class NoteSpawn : MonoBehaviour
{
    public WaveSettings testWave;
    public float hitWindow = 0.15f;
    public List<Note>notes; 

    void Start()
    {
        generate(testWave);    
    }
    
    public void generate(WaveSettings w)
    {
        notes = new List<Note>();
        float[] laneFreeAt = new float[6];
        int lastLane = -1;

        float beat = w.movingBeats + 2;
        
        while(beat <= w.beatLength)
        {            
            float hit = Monstestro.Instance.beatTime(beat);
            float spawnT = hit - Monstestro.Instance.beatTime(w.movingBeats);

            List<int> free = new List<int>();

            for (int i = 0; i < 6; i++)
            {
                if(laneFreeAt[i] <= spawnT && i != lastLane)
                {
                free.Add(i);
                }
            }
            
            if(free.Count > 0)
            {
                int lane = free[Random.Range(0, free.Count)];
                bool soul = beat >= w.earlySouls && Random.value < w.soulSpawnRate;

                Note n = new Note();
                n.lane = lane;
                n.hitTime = hit;
                n.spawnTime = spawnT;
                n.isSoul = soul;

                laneFreeAt[lane] = hit + hitWindow;
                lastLane = lane;

                notes.Add(n);
            }

            beat += w.beatsInBetween;

        }
        
        
    }
}

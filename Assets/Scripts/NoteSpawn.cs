using UnityEngine;
using System.Collections.Generic;

public class NoteSpawn : MonoBehaviour
{
    public WaveSettings testWave;
    public float hitWindow = 0.15f;
    public List<Note>notes; 

    public Board board;
    public GameObject monsterPrefab;
    public GameObject soulPrefab;
    List<Note> active = new List<Note>();
    float travelTime;

    void Start()
    {
        generate(testWave);    
    }
    
    
    public void generate(WaveSettings w)
    {
        travelTime = Monstestro.Instance.beatTime(w.movingBeats);

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

    void Update()
    {
        float now = Monstestro.Instance.songTime;

        foreach(Note n in notes)
        {
            if (!n.spawn && n.spawnTime <= now)
            {
                Vector3 startPos = board.spawns[n.lane].position;

                if (n.isSoul)
                {
                    n.view = Instantiate(soulPrefab, startPos, Quaternion.identity);
                }else{
                    n.view = Instantiate(monsterPrefab, startPos, Quaternion.identity);
                }

                n.spawn = true;
                active.Add(n);
                
            }
        }

       for (int k = active.Count - 1; k >= 0; k--)
        {
            Note n = active[k];

            float t = 1 - (n.hitTime - now) / travelTime;
            Vector3 spawnPos = board.spawns[n.lane].position;
            Vector3 pointPos = board.points[n.lane].position;

            if (t <= 1)
            {
                n.view.transform.position = Vector3.Lerp(spawnPos, pointPos, t);
            }
            else
            {
                 n.view.transform.position = Vector3.Lerp(pointPos, board.mansion.position, (t - 1) * 2);
            }

            if (t >= 1.5f)
            {
                Destroy(n.view); active.RemoveAt(k);
            }
        }

    }
}
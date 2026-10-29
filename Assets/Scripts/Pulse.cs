using UnityEngine;

public class Pulse : MonoBehaviour
{
    int lastBeat = -1;
    Vector3 normScale;
    public float pulseSize = 1.15f;

    void Start()
    {
        normScale = transform.localScale;
    }

    void Update()
    {
       int round = Mathf.FloorToInt(Monstestro.Instance.beat);
        if(round > lastBeat)
        {
            lastBeat = round;
            transform.localScale = normScale * pulseSize;
        }

        transform.localScale = Vector3.Lerp(transform.localScale, normScale, Time.deltaTime * 15f);
    }
}
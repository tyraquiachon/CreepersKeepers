using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Board board;
public int currLane;
public bool canAct = true;
public SpriteRenderer sr;

public NoteSpawn spawner;

    public void moveTo(int lane)
{
    currLane = lane;
    transform.position = board.points[lane].position;
    sr.flipX = board.points[lane].position.x < board.mansion.position.x;
}
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        moveTo(0);
    }

    void Update()
    {
        if (!canAct)
        {
            return;
        }

        for(int i = 0; i < 6; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i) || Input.GetKeyDown(KeyCode.Keypad1 + i))
            {
                moveTo(i);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            spawner.toExplode(currLane);
        }
    }
}

using UnityEngine;

using UnityEngine.UI;

public class Hearts : MonoBehaviour
{
    public NoteSpawn spawner;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    void Start()
    {
        
    }

    void Update()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < spawner.lives)
            {
                hearts[i].sprite = fullHeart;
            } else {
                 hearts[i].sprite = emptyHeart;

            }
        }
    }
}

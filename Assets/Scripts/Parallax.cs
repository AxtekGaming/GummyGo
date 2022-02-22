using UnityEngine;

public class Parallax : MonoBehaviour
{
    [Range(0f, 20f)] [SerializeField] float scrollSpeed = 1f;
    [SerializeField] float scrollOffset;

    Vector2 startPos;
    float newPos;

    void Start()
    {
        DontDestroyOnLoad(this);
        startPos = transform.position;
    }

    void Update()
    {
        newPos = Mathf.Repeat(Time.time * -scrollSpeed, scrollOffset);
        transform.position = startPos + Vector2.up * newPos;
    }

}
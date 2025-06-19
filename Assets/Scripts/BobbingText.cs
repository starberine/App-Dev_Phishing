using UnityEngine;

public class BobbingText : MonoBehaviour
{
    public float amplitude = 0.5f;   // Height of bobbing
    public float frequency = 1f;     // Speed of bobbing

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }
}
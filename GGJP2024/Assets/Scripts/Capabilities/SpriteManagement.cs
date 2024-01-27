using UnityEngine;

public class SpriteManagement : MonoBehaviour
{
    private SpriteRenderer renderer;

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        renderer.flipX = true;
    }

    public void ShouldFlip(bool condition)
    {
        if (!condition)
        {
            renderer.flipX = false;
        }
        else
        {
            renderer.flipX = true;
        }
    }
}

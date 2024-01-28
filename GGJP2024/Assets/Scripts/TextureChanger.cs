using UnityEngine;

public class SpriteSheetAnimator : MonoBehaviour
{
    public Material targetMaterial;
    public Texture spritesheet;
    public int rows = 1;
    public int columns = 1;
    public float framesPerSecond = 10f;

    private int currentFrame = 0;
    private int totalFrames;

    void Start()
    {
        if (targetMaterial == null || spritesheet == null || rows <= 0 || columns <= 0)
        {
            Debug.LogError("Target material, spritesheet, rows, or columns not set correctly!");
            enabled = false;
            return;
        }

        targetMaterial.mainTexture = spritesheet;
        
        totalFrames = rows * columns;
        SetSprite();
    }

    void Update()
    {
        float timePerFrame = 1f / framesPerSecond;

        if (Time.deltaTime > timePerFrame)
        {
            currentFrame = (currentFrame + 1) % totalFrames;
            SetSprite();
        }
    }

    void SetSprite()
    {
        float xSize = 1f / columns;
        float ySize = 1f / rows;

        int column = currentFrame % columns;
        int row = currentFrame / columns;

        Vector2 offset = new Vector2(xSize * column, 1f - ySize * (row + 1));

        targetMaterial.SetTextureOffset("_MainTex", offset);
        targetMaterial.SetTextureScale("_MainTex", new Vector2(xSize, ySize));
    }
}
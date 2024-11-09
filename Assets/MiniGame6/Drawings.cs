using UnityEngine;

public class Drawings : MonoBehaviour
{
    public Camera mainCamera;
    public Texture2D maskTexture;
    public SpriteRenderer topSprite;
    private RenderTexture renderTexture;
    private Material eraserMaterial;
    private Vector2 lastMousePos;

    void Start()
    {
        // Set up RenderTexture
        renderTexture = new RenderTexture(Screen.width, Screen.height, 0);
        eraserMaterial = topSprite.material;
        eraserMaterial.SetTexture("_MaskTex", maskTexture);
        eraserMaterial.SetTexture("_RenderTex", renderTexture);
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            if (Vector2.Distance(mousePos, lastMousePos) > 0.1f)
            {
                // Draw on maskTexture to reveal the lower layer
                DrawAtPosition(mousePos);
                lastMousePos = mousePos;
            }
        }
    }

    private void DrawAtPosition(Vector2 position)
    {
        RenderTexture.active = renderTexture;

        // Set up drawing context
        GL.PushMatrix();
        GL.LoadPixelMatrix(0, renderTexture.width, renderTexture.height, 0);

        // Draw a circle or shape to simulate erasing
        Graphics.DrawTexture(new Rect(position.x - 10, position.y - 10, 20, 20), maskTexture);

        GL.PopMatrix();
        RenderTexture.active = null;
    }
}
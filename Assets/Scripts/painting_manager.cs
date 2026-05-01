using UnityEngine;

public class painting_manager : MonoBehaviour
{
    public Transform brush_tip;
    public float     brush_size  = 5f;
    public Color     brush_color = Color.red;

    Texture2D canvas_texture;

    void Start()
    {
        canvas_texture = new Texture2D(512, 512, TextureFormat.RGBA32, false);

        Color[] fill = canvas_texture.GetPixels();
        for (int i = 0; i < fill.Length; i++)
            fill[i] = Color.white;
        canvas_texture.SetPixels(fill);
        canvas_texture.Apply();

        GetComponent<Renderer>().material.mainTexture = canvas_texture;
    }

    void Update()
    {
        if (brush_tip == null) return;

        RaycastHit hit;
        if (Physics.Raycast(brush_tip.position, brush_tip.forward, out hit, 0.05f))
        {
            if (hit.collider.gameObject != this.gameObject) return;

            Vector2 uv = hit.textureCoord;
            int px = (int)(uv.x * canvas_texture.width);
            int py = (int)(uv.y * canvas_texture.height);
            paint_at(px, py);
        }
    }

    void paint_at(int x, int y)
    {
        int r = (int)brush_size;
        for (int dx = -r; dx <= r; dx++)
        {
            for (int dy = -r; dy <= r; dy++)
            {
                if (dx * dx + dy * dy <= r * r)
                    canvas_texture.SetPixel(x + dx, y + dy, brush_color);
            }
        }
        canvas_texture.Apply();
     // haptics
    var controllers = FindObjectsOfType<UnityEngine.XR.Interaction.Toolkit.XRBaseController>();
    foreach (var c in controllers)
        c.SendHapticImpulse(0.3f, 0.05f);
}

    public void set_red()    { brush_color = Color.red;    }
    public void set_black()  { brush_color = Color.black;  }
    public void set_yellow() { brush_color = Color.yellow; }
    public void set_blue()   { brush_color = Color.blue;   }
    public void set_white()  { brush_color = Color.white;  }
}
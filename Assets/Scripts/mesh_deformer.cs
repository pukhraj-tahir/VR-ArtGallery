using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class mesh_deformer : MonoBehaviour
{
    public Transform        tool_tip;
    public float            deform_radius   = 0.15f;
    public float            deform_strength = 0.3f;
    public bool             push_mode       = true;

    Mesh      mesh;
    Vector3[] original_verts;
    Vector3[] displaced_verts;
    bool      is_deforming = false;

    void Start()
    {
        mesh            = GetComponent<MeshFilter>().mesh;
        original_verts  = mesh.vertices;
        displaced_verts = (Vector3[])mesh.vertices.Clone();
    }

    void Update()
    {
        if (tool_tip == null) return;

        Vector3 local_point = transform.InverseTransformPoint(tool_tip.position);
        bool    did_deform  = false;

        for (int i = 0; i < displaced_verts.Length; i++)
        {
            float dist = Vector3.Distance(displaced_verts[i], local_point);
            if (dist >= deform_radius) continue;

            float   falloff = 1f - (dist / deform_radius);
            Vector3 dir     = push_mode
                ? (displaced_verts[i] - local_point).normalized
                : (local_point - displaced_verts[i]).normalized;

            displaced_verts[i] += dir * deform_strength * falloff * Time.deltaTime;
            did_deform = true;
        }

        if (did_deform)
        {
            mesh.vertices = displaced_verts;
            mesh.RecalculateNormals();

            var controllers = FindObjectsOfType<XRBaseController>();
            foreach (var c in controllers)
                c.SendHapticImpulse(0.4f, 0.05f);
        }
    }

    public void reset_mesh()
    {
        displaced_verts = (Vector3[])original_verts.Clone();
        mesh.vertices   = displaced_verts;
        mesh.RecalculateNormals();
    }

    public void set_push_mode(bool val) { push_mode = val; }
}
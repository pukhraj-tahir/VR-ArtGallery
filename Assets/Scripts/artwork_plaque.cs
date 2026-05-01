using UnityEngine;

public class artwork_plaque : MonoBehaviour
{
    public string      artwork_title       = "Artwork Title";
    public string      artwork_description = "Description here.";
    public GameObject  plaque_object;

    void Start()
    {
        if (plaque_object != null)
            plaque_object.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (plaque_object != null)
            plaque_object.SetActive(true);
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (plaque_object != null)
            plaque_object.SetActive(false);
    }
}
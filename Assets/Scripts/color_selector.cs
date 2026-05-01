using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class colour_selector : MonoBehaviour
{
    public painting_manager canvas;
    public string           color_name;

    XRGrabInteractable grab;

    void Start()
    {
        grab = GetComponent<XRGrabInteractable>();
        grab.selectEntered.AddListener(on_grab);
    }

    void on_grab(SelectEnterEventArgs args)
    {
        if (canvas == null) return;

        switch (color_name.ToLower())
        {
            case "red":    canvas.set_red();    break;
            case "black":  canvas.set_black();  break;
            case "yellow": canvas.set_yellow(); break;
            case "blue":   canvas.set_blue();   break;
            case "white":  canvas.set_white();  break;
        }
    }
}


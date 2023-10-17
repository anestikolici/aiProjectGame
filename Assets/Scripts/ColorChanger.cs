using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public Material lightsOff;
    public Material lightsOn;

    private Renderer rend;
    public bool isRed = false;

    private void Start()
    {
        rend = GetComponent<Renderer>();

        
    }

    private void OnTriggerEnter(Collider other)
    {

        ToggleColor();
        
    }
    private void ToggleColor()
    {
        isRed = !isRed;
        SetTargetColor();
    }
    private void SetTargetColor()
    {
        rend.material = isRed ? lightsOn : lightsOff;
    }

    
}


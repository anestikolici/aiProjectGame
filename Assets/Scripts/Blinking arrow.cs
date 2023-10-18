using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Blinkingarrow : MonoBehaviour
{
    public MaskableGraphic arrow;

    public float interval = 1f;
	public float startDelay = 0.5f;
	public bool currentState = true;
	public bool defaultState = true;
	bool isBlinking = false;

    // Start is called before the first frame update
    void Start()
    {
       StartBlink();
    }

    public void StartBlink()
	{
		// do not invoke the blink twice - needed if you need to start the blink from an external object
		if (isBlinking)
			return;

		if (arrow !=null)
		{
			isBlinking = true;
			InvokeRepeating("ToggleState", startDelay, interval);
		}
	}

	public void ToggleState()
	{
		arrow.enabled = !arrow.enabled;
    }

}

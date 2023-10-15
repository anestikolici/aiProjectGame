using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorRotation : MonoBehaviour 
{
    // Pause script reference
    [Tooltip("Pause script reference")]
    [SerializeField]
    private Pause pause;

    // Timer script reference
    [Tooltip("Timer script reference")]
    [SerializeField]
    private Timer timer;

    private bool isAnswered = false;

    public int Speed;
	private bool isOpen;
	
    void Start()
    {
        Time.timeScale = 1;
    }
	// Update is called once per frame
	void Update ()
	
	{
        transform.Rotate(0,Speed*Time.deltaTime,0);
		isOpen = true;
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player" && isOpen && isAnswered)
        {
            pause.DisablePause();
            timer.ResetTimer();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    /// <param name="isSolved">Controls whether the puzzle has been solved or not</param>
    public void SetIsAnswered(bool isAnswered)
    {
        this.isAnswered = isAnswered;
    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class TabToSelect : MonoBehaviour
{
    // Input fields
    [Tooltip("Input fields")]
    [SerializeField]
    private List<TMP_InputField> inputFields;

    // Player input actions
    private PlayerInputs controls;

    // Player controls
    private PlayerInputs.PlayerActions player;

    // Tab key
    private InputAction tab;

    // Current selected input field
    private int currentField = 0;

    /// <summary>
    /// Called when the script instance is loaded
    /// </summary>
    private void Awake()
    {
        controls = new PlayerInputs();
        player = controls.Player;
        tab = player.Tab;
    }

    /// <summary>
    /// Called when the Player object becomes active
    /// </summary>
    public void OnEnable()
    {
        controls.Enable();
        tab.performed += ctx => MoveToNextField();
    }

    /// <summary>
    /// Moves the cursor to the next available Text Input Field
    /// </summary>
    public void MoveToNextField()
    {
        currentField++;
        if (currentField == inputFields.Count)
            currentField = 0;
        inputFields[currentField].Select();
    }
}

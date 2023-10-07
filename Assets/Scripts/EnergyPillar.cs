using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPillar : MonoBehaviour
{
    // List of the 4 energy bars for this pillar
    [Tooltip("List of the 4 energy bars for this pillar")]
    [SerializeField]
    private List<EnergyBar> energyBarList;

    // List of the other 2 pillars
    [Tooltip("List of the other 2 pillars")]
    [SerializeField]
    private List<EnergyPillar> energyPillarList;

    // EnergyPillarLogic script
    [Tooltip("EnergyPillarLogic script")]
    [SerializeField]
    private EnergyPillarLogic logic;

    // Index of the energy bar that has to be moved
    private int indexToMove;

    // Controls if this pilalr has an energy bar that has been selected to be moved
    private bool toMove = false;

    // The previous energy pillar that has been shot
    private EnergyPillar previouslyHitPillar;

    // Times the pillars were reset
    private static int totalResets = 0;

    // Controls if the pillars are currently reset
    private static bool areReset = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("laser"))
        {
            bool selectedToMove = true;

            foreach (EnergyPillar energyPillar in energyPillarList)
            {
                if (energyPillar != previouslyHitPillar)
                {
                    if (energyPillar.GetToMove())
                    {
                        int indexToMove = energyPillar.GetIndexToMove();
                        energyPillar.energyBarList[indexToMove].gameObject.transform.localScale -= new Vector3(0.005f, 0.005f, 0f);
                        energyPillar.energyBarList[indexToMove].gameObject.SetActive(false);
                        energyPillar.ResetToMove();
                        selectedToMove = false;
                        areReset = false;
                        energyBarList[indexToMove].gameObject.SetActive(true);
                        logic.CheckPuzzle();
                        break;
                    }
                }
                else
                {
                    if (energyPillar.GetToMove())
                    {
                        selectedToMove = false;
                        energyPillar.ResetToMove();
                        energyBarList[indexToMove].gameObject.transform.localScale -= new Vector3(0.005f, 0.005f, 0f);
                    }
                }
            }

            // Select the first active bar of the pillar
            if (selectedToMove && energyBarList.Any(go => go.isActiveAndEnabled))
            {
                toMove = true;
                foreach (EnergyBar energyBar in energyBarList)
                {
                    if (energyBar.isActiveAndEnabled)
                    {
                        energyBar.gameObject.transform.localScale += new Vector3(0.005f, 0.005f, 0f);
                        indexToMove = energyBarList.IndexOf(energyBar);
                        break;
                    }
                }
            }

            // Set previouslyHitPillar to the current EnergyPillar
            previouslyHitPillar = energyPillarList.FirstOrDefault(ep => ep.name == gameObject.name);
        }
    }

    /// <summary>
    /// Sets the toMove parameter to false
    /// </summary>
    public void ResetToMove()
    {
        toMove = false;
    }

    /// <summary>
    /// Returns the toMove parameter
    /// </summary>
    /// <returns>Parameter that controls if the pillar has been selected to move an energy bar</returns>
    public bool GetToMove()
    {
        return toMove;
    }

    /// <summary>
    /// Return the indexToMove parameter
    /// </summary>
    /// <returns>Index of the energy bar that is selected to be moved</returns>
    public int GetIndexToMove()
    {
        return indexToMove;
    }

    /// <summary>
    /// Resets the pillars to their initial state
    /// </summary>
    public void ResetPillars()
    {
        areReset = true;
        totalResets++;
        foreach (EnergyPillar energyPillar in energyPillarList)
        {
            if (energyPillar.name == "Pillar1")
                energyPillar.ResetPillarBars(true);
            else
                energyPillar.ResetPillarBars(false);
        }
    }

    /// <summary>
    /// Changes the status of all energy bars in a pillar
    /// </summary>
    /// <param name="status">New status of all energy bars</param>
    private void ResetPillarBars(bool status)
    {
        foreach (EnergyBar energyBar in energyBarList)
            energyBar.gameObject.SetActive(status);      
    }

    /// <summary>
    /// Gets the list of energy bars for a pillar
    /// </summary>
    /// <returns>List of energy bars</returns>
    public List<EnergyBar> GetEnergyBars()
    {
        return energyBarList;
    }

    /// <summary>
    /// Returns the number of total resets
    /// </summary>
    /// <returns>Number of total resets</returns>
    public int GetTotalResets()
    {
        return totalResets;
    }

    /// <summary>
    /// Returns the areReset parameter
    /// </summary>
    /// <returns>If the energy pillars are currently reset to their initial sate</returns>
    public bool GetAreReset()
    {
        return areReset;
    }
}

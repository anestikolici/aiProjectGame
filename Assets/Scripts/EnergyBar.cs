using System.Linq;
using UnityEngine;

public class EnergyBar : MonoBehaviour
{
    // Energy pillar that this energy bar belongs to
    [Tooltip("Energy pillar that this energy bar belongs to")]
    [SerializeField]
    private EnergyPillar parentPillar;

    // Controls if the energy bar is selected to be moved
    private bool toMove = false;

    /// <summary>
    /// Sets the toMove parameter
    /// </summary>
    /// <param name="move">Controls if the energy bar is selected to be moved</param>
    public void ToMove(bool move)
    {
        toMove = move;
    }

    /// <summary>
    /// Returns the toMove parameter
    /// </summary>
    /// <returns>Parameter that controls if the energy bar is selected to be moved</returns>
    public bool GetToMove()
    {
        return toMove;
    }

    private void OnEnable()
    {
        if (!parentPillar.GetAreReset())
        {
            switch (name)
            {
                case "2nd Bar":
                    if (parentPillar.GetEnergyBars()[0].isActiveAndEnabled)
                        parentPillar.ResetPillars();
                    break;
                case "3rd Bar":
                    if (parentPillar.GetEnergyBars()[0].isActiveAndEnabled || parentPillar.GetEnergyBars()[1].isActiveAndEnabled)
                        parentPillar.ResetPillars();
                    break;
                case "4th Bar":
                    foreach (EnergyBar energybar in parentPillar.GetEnergyBars())
                    {
                        if (energybar.isActiveAndEnabled && energybar.name != "4th Bar")
                        {
                            parentPillar.ResetPillars();
                            break;
                        }
                    }
                    break;
            }
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    // List of tiles adjacent to this one
    [Tooltip("List of tiles adjacent to this one")]
    [SerializeField]
    private List<MeshRenderer> adjacentTiles;

    // Material for when the tile is off
    [Tooltip("Material for when the tile is off")]
    [SerializeField]
    private Material lightOff;

    // Material for when the tile is on
    [Tooltip("Material for when the tile is on")]
    [SerializeField]
    private Material lightOn;

    // TilePuzzleLogic script
    [Tooltip("TilePuzzleLogic script")]
    [SerializeField]
    private TilePuzzleLogic tilePuzzleLogic;

    // MeshRenderer of this tile
    private MeshRenderer thisRenderer;

    // MaterialPropertyBlock of a tile
    private MaterialPropertyBlock materialPropertyBlock;

    // Total number of times the reset tile was stepepd on
    private int totalResets;

    // Controls whether the tile is on or off
    private bool isOn;

    // Controls whether the puzzle has been solved
    private static bool isSolved = false;

    /// <summary>
    /// Called on the first frame
    /// </summary>
    private void Start()
    {
        thisRenderer = GetComponent<MeshRenderer>();
        materialPropertyBlock = new MaterialPropertyBlock();

        if (adjacentTiles.Count > 0)
            isOn = false;
        else 
            isOn = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isSolved)
        {
            Color currentColor;
            if (adjacentTiles.Count > 0)
            {
                foreach (MeshRenderer r in adjacentTiles)
                {
                    r.GetPropertyBlock(materialPropertyBlock);
                    currentColor = materialPropertyBlock.GetColor("_Color");
                    if (currentColor == lightOff.color)
                        materialPropertyBlock.SetColor("_Color", lightOn.color);
                    else
                        materialPropertyBlock.SetColor("_Color", lightOff.color);
                    
                    Tile adjacentTile = r.GetComponentInParent<Tile>();
                    adjacentTile.ChangeTileStatus(!adjacentTile.GetIsOn());
                    r.SetPropertyBlock(materialPropertyBlock);
                }                

                thisRenderer.GetPropertyBlock(materialPropertyBlock);
                currentColor = materialPropertyBlock.GetColor("_Color");
                if (currentColor == lightOff.color)
                    materialPropertyBlock.SetColor("_Color", lightOn.color);              
                else
                    materialPropertyBlock.SetColor("_Color", lightOff.color);

                ChangeTileStatus(!isOn);
                thisRenderer.SetPropertyBlock(materialPropertyBlock);

                isSolved = tilePuzzleLogic.CheckPuzzle();
            }
            else
            {
                GameObject[] allTiles = GameObject.FindGameObjectsWithTag("Tile");
                bool areReset = true;
                foreach (GameObject tile in allTiles)
                {
                    if (tile.name != "Tile0")
                    {
                        var renderer = tile.GetComponent<MeshRenderer>();
                        renderer.GetPropertyBlock(materialPropertyBlock);
                        currentColor = materialPropertyBlock.GetColor("_Color");
                        if (currentColor == lightOn.color)
                            areReset = false;
                        Debug.Log(areReset);
                        materialPropertyBlock.SetColor("_Color", lightOff.color);
                        tile.GetComponent<Tile>().ChangeTileStatus(false);
                        renderer.SetPropertyBlock(materialPropertyBlock);
                    }
                }

                if (!areReset)
                    totalResets++;

                thisRenderer.GetPropertyBlock(materialPropertyBlock);
                materialPropertyBlock.SetColor("_Color", lightOn.color);
                thisRenderer.SetPropertyBlock(materialPropertyBlock);
                ChangeTileStatus(true);
            }
        }
    }

    /// <summary>
    /// Returns the total number of times the reset tile was stepped on
    /// </summary>
    /// <returns>Total number of times the reset tile was stepped on</returns>
    public int GetTotalResets()
    {
        return totalResets;
    }

    /// <summary>
    /// Changes the status of the tile from on/off to off/onn
    /// </summary>
    /// <param name="status">New status</param>
    private void ChangeTileStatus(bool status)
    {
        isOn = status;
    }

    /// <summary>
    /// Returns the current status of the tile
    /// </summary>
    /// <returns>Current status of the tile</returns>
    public bool GetIsOn()
    {
        return isOn;
    }
}

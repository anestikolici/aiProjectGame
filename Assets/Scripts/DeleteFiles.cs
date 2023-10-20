using System.IO;
using UnityEngine;

public class DeleteFiles : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (File.Exists("player_pregame.csv"))
        {
            File.Delete("player_pregame.csv");
        }

        if (File.Exists("player_data.csv"))
        {
            File.Delete("player_data.csv");
        }
    }
}

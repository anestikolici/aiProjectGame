using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMovement: MonoBehaviour
{
    public float velocita = 100.0f; // Velocità di movimento del cubo
    public float distanza = 1.0f; // Distanza massima percorsa da sinistra a destra

    private Vector3 posIniziale; // Posizione iniziale del cubo

    void Start()
    {
        posIniziale = transform.position; // Salva la posizione iniziale del cubo
    }

    void Update()
    {
        // Calcola la nuova posizione del cubo
        float spostamento = Mathf.PingPong(Time.time * velocita, distanza);
        Vector3 nuovaPosizione = posIniziale + Vector3.right * spostamento;

        // Aggiorna la posizione del cubo
        transform.position = nuovaPosizione;
    }

}



using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance { get; private set; }
    public GameObject fallingObjPrefab;
    public float spawnTimer = 4f;
    public Vector2[] direcciones;
    public int switchNumber = 0;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        //Se establecen la posicion de las dos diferentes direcciones que puede tomar el falling object
        direcciones = new Vector2[]
        {
            new Vector2 (2.5f, 10f),
            new Vector2 (-2.5f, 10f)
        };
    }

    //Metodo para comenzar el spawn de falling objects
    public void StartSpawn()
    {
        InvokeRepeating("SpawnFallingObj", 0f, spawnTimer);
    }

    void SpawnFallingObj()
    {
        //Switch utilizado para cambiar la variable que se utilizara para indicar
        //en que posicion se generara el falling object
        switch (switchNumber)
        {
            case 0:
                switchNumber = 1;
                break;
            case 1:
                switchNumber = 0;
                break;
        }
        GameObject fallingObj = Instantiate(fallingObjPrefab, direcciones[switchNumber], Quaternion.identity);
    }

    public void StopSpawn()
    {
        CancelInvoke("SpawnFallingObj");
    }

}

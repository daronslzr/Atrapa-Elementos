using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
    public static FallingObject Instance { get; private set; }

    public float speed = 3f;
    public System.Random random = new System.Random();

    void Start()
    {
        //Tomamos un valor aleatorio del directorio de elementos y lo asignamos al nombre
        int index = random.Next(WordsManager.Instance.numDict);
        string simboloElemento = WordsManager.Instance.dictElementos.Values.ElementAt(index);
        this.GetComponentInChildren<TextMeshPro>().text = simboloElemento;
        //Si la seleccion del modo fue dificil cambia la velocidad
        if (GameManager.Instance.difficulty == "EscenaDificil")
        {
            speed = 2f;
        }
    }

    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
        if (transform.position.y < -10 || GameManager.Instance.gameState == GameState.Ended) 
        { 
            Destroy(gameObject);
        };
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //Destruimos el falling object al entrar en contacto con el player
        Destroy(this.gameObject);
        
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    public float dirX;
    public float velocidadMovimiento = 7f;
    public GameObject player;
    //public Rigidbody2D rb;
    //Con esto arreglo haremos el cambio de Sprite cuando el jugador
    //obtenga una respuesta correcta
    public Sprite[] spritesDisponibles;
    public AnimationClip[] animacionesVerdeDisponibles;
    //Hacemos un array con las animaciones del matraz Rojo
    public AnimationClip[] animacionesRojasDisponibles;
    public SpriteRenderer spriteRenderer;
    string nombreElemento;
    //public int counter = 0;
    public Animator animator;

    public System.Random random = new System.Random();

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        ChangeNameAndAnimation();
    }

    void Update()
    {   
        MovePlayer();
    }

    void MovePlayer()
    {
        if(GameManager.Instance.gameState == GameState.Playing)
        {
            //Obtiene la magnitud de movimiento en el eje Y
            dirX = Input.GetAxisRaw("Horizontal");
            //Movemos el player en el eje Y
            //Mathf.Clamp restringe el movimiento
            transform.position = new Vector2(Mathf.Clamp(dirX * velocidadMovimiento, -2.5f, 2.5f),transform.position.y);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        
        //Checa si el value del key (nombreElemento) es igual al nombre del falling object
        if (WordsManager.Instance.dictElementos[nombreElemento] == collider.GetComponentInChildren<TextMeshPro>().text)
        {
            print("El simbolo es correcto");
            GameManager.Instance.counterWin++;
            AudioManager.Instance.PlaySound("Point");
            ChangeNameAndAnimation();
        }
        else
        {
            //Disminuimos el counter para que el jugador pierda
            GameManager.Instance.counterLose--;
            AudioManager.Instance.PlaySound("Wrong");
            GameManager.Instance.livesText.text = "Vidas: "+GameManager.Instance.counterLose;
        }
    }

    void ChangeNameAndAnimation()
    {
        if(GameManager.Instance.gameState != GameState.Ended)
        {
            //Tomamos un valor aleatorio del directorio de elementos y lo asignamos al nombre
            int index = random.Next(WordsManager.Instance.numDict);
            nombreElemento = WordsManager.Instance.dictElementos.Keys.ElementAt(index);
            this.GetComponentInChildren<TextMeshPro>().text = nombreElemento;
            //Hacemos la llamada al metodo para cambiar la animacion dependiendo del counter
            //SetAnimation(animacionesVerdeDisponibles[GameManager.Instance.counterWin].name);
            SetAnimation(GameManager.Instance.counterWin);
        }
        
    }

    //Utilizado para cambiar la animacion
    public void SetAnimation(int index)
    {
        string name;
        //Dependiendo de la dificultad del juego se cambian las animaciones del player
        if(GameManager.Instance.difficulty == "EscenaFacil")
        {
            name = animacionesVerdeDisponibles[index].name;
        }
        else
        {
            name = animacionesRojasDisponibles[index].name;
        }
        animator.Play(name);
    }

}

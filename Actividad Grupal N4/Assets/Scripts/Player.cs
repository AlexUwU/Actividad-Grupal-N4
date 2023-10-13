using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Linq;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    KeywordRecognizer movimientos; // creo mi reconocedor de comandode voz

    Dictionary<string, Action> movimiento = new Dictionary<string, Action>(); // creo las comando

    public int level = 1;

    public int speedDerecha = 0;

    public int speedIzquierda = 0;

    public int speedArriba = 0;

    public int speedAbajo = 0;

    public bool horizontal;

    public bool vertical;

    public bool agarroMoneda = false;

    void Start()
    {
        //creo las acciones segun el comandod e voz
        movimiento.Add("izquierda", MoverIzquierda);
        movimiento.Add("derecha", MoverDerecha);
        movimiento.Add("arriba", MoverArriba);
        movimiento.Add("abajo", MoverAbajo);
        movimiento.Add("detener", Detener);
        //

        //seteo el ronocedor de voz y lo inicio
        movimientos = new KeywordRecognizer(movimiento.Keys.ToArray());
        movimientos.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        movimientos.Start();

    }

    private void Detener()
    {
        speedArriba = 0;
        speedAbajo = 0;
        speedIzquierda = 0;
        speedDerecha = 0;
        Debug.Log("Detener");
        horizontal = false;
        vertical = false;
    }

    private void MoverAbajo()
    {
        speedArriba = 0;
        speedIzquierda = 0;
        speedDerecha = 0;
        Debug.Log("Mover Abajo");
        horizontal = false;
        vertical = true;
        speedAbajo = -4;
    }

    private void MoverArriba()
    {
        speedAbajo = 0;
        speedIzquierda = 0;
        speedDerecha = 0;
        Debug.Log("Mover Arriva");
        horizontal = false;
        vertical = true;
        speedArriba = 4;
    }

    private void MoverDerecha()
    {
        speedAbajo = 0;
        speedIzquierda = 0;
        speedArriba = 0;
        Debug.Log("Mover Derecha");
        horizontal = true;
        vertical = false;
        speedDerecha = 4;
    }

    private void MoverIzquierda()
    {
        speedAbajo = 0;
        speedArriba = 0;
        speedDerecha = 0;
        Debug.Log("Mover Izquierda");
        horizontal = true;
        vertical = false;
        speedIzquierda = -4;
    }

    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs command)
    {
        movimiento[command.text].Invoke();
    }

    private void Update()
    {
        if (horizontal)
        {
            if(speedDerecha == 4)
            {
                transform.Translate(speedDerecha * Time.deltaTime, 0, 0);
            }

            if(speedIzquierda == -4)
            {
                transform.Translate(speedIzquierda * Time.deltaTime, 0, 0);
            }
        }

        if (vertical)
        {
            if (speedArriba == 4)
            {
                transform.Translate(0, speedArriba * Time.deltaTime, 0);
            }

            if (speedAbajo == -4)
            {
                transform.Translate(0, speedAbajo * Time.deltaTime, 0);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("ParedDerecha"))
        {
            Debug.Log("Pared Derecha");
            speedDerecha = 0;
        }

        if (other.collider.CompareTag("ParedIzquierda"))
        {
            Debug.Log("Pared Izquierda");
            speedIzquierda = 0;

        }

        if (other.collider.CompareTag("ParedArriba"))
        {
            Debug.Log("Pared Arriba");
            speedArriba = 0;

        }

        if (other.collider.CompareTag("ParedAbajo"))
        {
            Debug.Log("Pared Abajo");
            speedAbajo = 0;
        }

        if (other.collider.CompareTag("Meta"))
        {
            if (agarroMoneda)
            {
                speedIzquierda = 0;
                speedDerecha = 0;
                speedArriba = 0;
                speedAbajo = 0;
                horizontal = false;
                vertical = false;
                agarroMoneda = false;
                Debug.Log("Meta");
                level++;
                if (level == 2)
                {
                    SceneManager.LoadScene(level);
                    Debug.Log("Level 2");
                    transform.position = new Vector2(-11, 0);
                }

                if (level == 3)
                {
                    SceneManager.LoadScene(level);
                    Debug.Log("Level 3");
                    transform.position = new Vector2(-4, 7);
                }
            }
        }

        if (other.collider.CompareTag("Enemy"))
        {
            Debug.Log("Enemigo");
            speedIzquierda = 0;
            speedDerecha = 0;
            speedArriba = 0;
            speedAbajo = 0;
            horizontal = false;
            vertical = false;
            agarroMoneda = false;
            if (level == 1)
            {
                transform.position = new Vector2(-13, 0);
            }
            if (level == 2)
            {
                SceneManager.LoadScene(level);
                Debug.Log("Level 2");
                transform.position = new Vector2(-11, 0);
            }

            if (level == 3)
            {
                SceneManager.LoadScene(level);
                Debug.Log("Level 3");
                transform.position = new Vector2(-4, 7);
            }
        }

        if (other.collider.CompareTag("Coin"))
        {
            agarroMoneda = true;
            other.gameObject.SetActive(false);
        }
    }
}


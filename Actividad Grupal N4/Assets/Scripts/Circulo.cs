using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circulo : MonoBehaviour
{
    [SerializeField]
    private float distanciaMovimiento;
    [SerializeField]
    private float velocidad;
    [SerializeField]
    private bool moverHorizontalmente;
    [SerializeField]
    private bool invertirDireccion;
    [SerializeField]
    private float radioCirculo;
    [SerializeField]
    private bool moviendoAdelante = true;
    [SerializeField]
    private Vector2 posicionInicial;
    [SerializeField]
    private Vector2 posicionFinal;
    private Vector2 posicionInicialGizmos;
    private Vector2 posicionFinalGizmos;

    private void Start()
    {
        calcularDistanciaFinal();
    }

    private void Update()
    {
        mover();
    }

    public void mover()
    {
        float distancia = Vector2.Distance(transform.position, posicionInicial);
        if (distancia < distanciaMovimiento)
        {
            transform.position = Vector2.MoveTowards(transform.position, posicionFinal, velocidad * Time.deltaTime);
        }
        else
        {
            moviendoAdelante = !moviendoAdelante;
            calcularDistanciaFinal();
        }
    }
    public void calcularDistanciaFinal()
    {
        posicionInicial = transform.position;
        if (moverHorizontalmente)
        {
            if (invertirDireccion)
            {
                posicionFinal = moviendoAdelante ? posicionInicial + Vector2.left * distanciaMovimiento : posicionInicial - Vector2.left * distanciaMovimiento;
            }
            else
            {
                posicionFinal = moviendoAdelante ? posicionInicial + Vector2.right * distanciaMovimiento : posicionInicial - Vector2.right * distanciaMovimiento;
            }
        }
        else
        {
            if (invertirDireccion)
            {
                posicionFinal = moviendoAdelante ? posicionInicial + Vector2.down * distanciaMovimiento : posicionInicial - Vector2.down * distanciaMovimiento;
            }
            else
            {
                posicionFinal = moviendoAdelante ? posicionInicial + Vector2.up * distanciaMovimiento : posicionInicial - Vector2.up * distanciaMovimiento;
            }
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        posicionInicialGizmos = Application.isPlaying ? posicionInicial : (Vector2)transform.position;
        if (invertirDireccion)
        {
            posicionFinalGizmos = Application.isPlaying ? posicionFinal : (moverHorizontalmente ? posicionInicialGizmos + Vector2.left * distanciaMovimiento : posicionInicialGizmos + Vector2.down * distanciaMovimiento);
        }
        else
        {
            posicionFinalGizmos = Application.isPlaying ? posicionFinal : (moverHorizontalmente ? posicionInicialGizmos + Vector2.right * distanciaMovimiento : posicionInicialGizmos + Vector2.up * distanciaMovimiento);
        }

        Gizmos.DrawLine(posicionInicialGizmos, posicionFinalGizmos);
        Gizmos.DrawWireSphere(posicionInicialGizmos, radioCirculo);
        Gizmos.DrawWireSphere(posicionFinalGizmos, radioCirculo);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Colision");
        }
    }
}

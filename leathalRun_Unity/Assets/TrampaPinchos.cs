using UnityEngine;

public class TrampaPinchos : MonoBehaviour, IActivable
{
    public float tiempoActivacion = 0.5f;
    public float alturaMaxima = 1f;
    public float tiempoArriba = 1f;
    private Vector3 posicionInicial;
    private bool activada = false;

    void Start()
    {
        posicionInicial = transform.position;
        Debug.Log("TrampaPinchos inicializada. Posici�n inicial: " + posicionInicial);
    }

    public void Activar()
    {
        Debug.Log("M�todo Activar() llamado en TrampaPinchos");
        if (!activada)
        {
            activada = true;
            Invoke("SubirPinchos", tiempoActivacion);
        }
        else
        {
            Debug.Log("La trampa ya est� activada");
        }
    }

    void SubirPinchos()
    {
        Debug.Log("Subiendo pinchos");
        transform.position = posicionInicial + Vector3.up * alturaMaxima;
        Debug.Log("Nueva posici�n de los pinchos: " + transform.position);
        Invoke("BajarPinchos", tiempoArriba);
    }

    void BajarPinchos()
    {
        Debug.Log("Bajando pinchos");
        transform.position = posicionInicial;
        Debug.Log("Pinchos vuelven a la posici�n inicial: " + transform.position);
        activada = false;
    }
}

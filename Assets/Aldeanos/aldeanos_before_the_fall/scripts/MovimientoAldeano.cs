// MovimientoAldeano.cs
// Hace que el aldeano camine al azar y actualiza la animacion.
// Requiere: Rigidbody2D (Body Type = Dynamic, Gravity Scale = 0) y un Animator.
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovimientoAldeano : MonoBehaviour
{
    public float velocidad = 1.2f;
    public float radioDeambular = 2.5f;   // que tan lejos se aleja de su punto inicial
    public Vector2 tiempoEntrePausas = new Vector2(1.5f, 4f);

    Rigidbody2D rb;
    Animator anim;
    Vector2 origen, destino;
    float esperarHasta;
    bool caminando;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        origen = rb.position;
        ElegirDestino();
    }

    void ElegirDestino()
    {
        destino = origen + Random.insideUnitCircle * radioDeambular;
        caminando = true;
    }

    void FixedUpdate()
    {
        if (caminando)
        {
            Vector2 dir = (destino - rb.position);
            if (dir.magnitude < 0.08f)
            {
                caminando = false;
                rb.linearVelocity = Vector2.zero;
                esperarHasta = Time.time + Random.Range(tiempoEntrePausas.x, tiempoEntrePausas.y);
                ActualizarAnim(Vector2.zero);
            }
            else
            {
                Vector2 v = dir.normalized * velocidad;
                rb.linearVelocity = v;
                ActualizarAnim(v);
            }
        }
        else if (Time.time >= esperarHasta)
        {
            ElegirDestino();
        }
    }

    void ActualizarAnim(Vector2 v)
    {
        if (anim == null) return;
        anim.SetFloat("MoveX", v.x);
        anim.SetFloat("MoveY", v.y);
        anim.SetBool("Caminando", v.sqrMagnitude > 0.001f);
    }
}

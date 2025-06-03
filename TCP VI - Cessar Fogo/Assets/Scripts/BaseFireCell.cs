using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FireState
{
    None, Ignition, Growing, FullBurn, Smoldering, Suppressed, Extinguished
}

public class BaseFireCell : MonoBehaviour
{
    public FireState State { get; protected set; } = FireState.None;

    [Header("Fases do Fogo")]
    [SerializeField] protected float ignitionTime = 3f;
    [SerializeField] protected float growTime = 5f;
    [SerializeField] protected float fullBurnTime = 8f;
    [SerializeField] protected float smolderTime = 4f;

    protected float timer = 0f;
    protected bool hasPropagated = false;

    [Header("Propagação")]
    [SerializeField] protected float propagationRadius = 5f;
    [SerializeField] protected float propagationChance = 0.6f;
    [SerializeField] protected float propagationDelay = 2f;

    [Header("Visual")]
    [SerializeField] private GameObject visualObject;
    [SerializeField] private ParticleSystem smokeParticles;

    [Header("Combustível")]
    [SerializeField] protected float combustivelMaximo = 100f;
    protected float combustivelAtual;

    protected float mult = 1;




    protected ParticleSystem fireParticles;
    protected Renderer rend;

    protected virtual void Start()
    {
        combustivelAtual = combustivelMaximo;
        fireParticles = GetComponentInChildren<ParticleSystem>();
        if (visualObject != null)
        {
            rend = visualObject.GetComponent<Renderer>();
        }
        UpdateVisuals();
        if (smokeParticles != null)
            smokeParticles.Stop();

    }

    protected virtual void Update()
    {
        if (State == FireState.None || State == FireState.Extinguished) return;
        if (Input.GetKeyDown(KeyCode.N))
        {
            if (mult <= 1)
            {
                mult = 1;
            }
            else
            {
                mult = mult - 0.5f;
            }
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            mult = mult + 0.5f;
        }

        timer += (mult * Time.deltaTime);
        Debug.Log("multiplicador atual: " +mult);
        //Debug.Log("Combustivel atual: "+combustivelAtual);

        switch (State)
        {
            case FireState.Ignition:
                combustivelAtual -= Time.deltaTime * 2f; // consumo de combustivel
                if (combustivelAtual <= 0f)
                    ChangeState(FireState.Extinguished);
                else if (timer >= ignitionTime)
                    ChangeState(FireState.Growing);
                break;


            case FireState.Growing:
                combustivelAtual -= Time.deltaTime * 3f;
                if (combustivelAtual <= 0f)
                    ChangeState(FireState.Extinguished);
                else if (timer >= growTime)
                    ChangeState(FireState.FullBurn);
                break;

            case FireState.FullBurn:
                combustivelAtual -= Time.deltaTime * 5f;
                if (combustivelAtual <= 0f)
                    ChangeState(FireState.Extinguished);
                else if (!hasPropagated && timer >= propagationDelay)
                {
                    TryPropagate();
                    hasPropagated = true;
                }
                else if (timer >= fullBurnTime)
                    ChangeState(FireState.Smoldering);
                break;

            case FireState.Smoldering:
                combustivelAtual -= Time.deltaTime * 2f;
                if (combustivelAtual <= 0f)
                    ChangeState(FireState.Extinguished);
                else if (timer >= smolderTime)
                    ChangeState(FireState.Extinguished);
                combustivelAtual = 0f;
                break;
        }

        if (combustivelAtual <= 0f && State != FireState.Extinguished)
        {
            ChangeState(FireState.Extinguished);
        }
    }

    public virtual void Ignite()
    {
        if ((State == FireState.None || State == FireState.Suppressed) && combustivelAtual > 0f)
        {
            ChangeState(FireState.Ignition);
        }
    }

    public virtual void Extinguish()
    {
        if (State != FireState.Extinguished && State != FireState.None && combustivelAtual >0f)
        {
            ChangeState(FireState.Suppressed);
        }
    }

    protected virtual void ChangeState(FireState newState)
    {
        State = newState;
        timer = 0f;
        UpdateVisuals();
    }

    protected virtual void TryPropagate()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, propagationRadius);
        foreach (Collider hit in hits)
        {
            BaseFireCell neighbor = hit.GetComponent<BaseFireCell>();
            if (neighbor != null && neighbor != this && neighbor.PodeSerReacendido())
            {
                if (Random.value < propagationChance)
                {
                    neighbor.Ignite();
                    //Debug.Log($"{name} propagou para {neighbor.name}");
                }
            }
        }
    }

    protected virtual void UpdateVisuals()
    {
        if (fireParticles != null)
        {
            if (State == FireState.None || State == FireState.Extinguished)
                fireParticles.Stop();
            else
                fireParticles.Play();
        }

        if (smokeParticles != null)
        {
            var main = smokeParticles.main;

            switch (State)
            {
                case FireState.Ignition:
                    main.startColor = new Color(1f, 1f, 1f, 0.6f);
                    smokeParticles.Play();
                    break;

                case FireState.Growing:
                    main.startColor = new Color(1f, 1f, 1f, 1f);
                    smokeParticles.Play();
                    break;

                case FireState.FullBurn:
                    main.startColor = new Color(0.5f, 0.5f, 0.5f, 1f); 
                    smokeParticles.Play();
                    break;

                case FireState.Smoldering:
                    main.startColor = new Color(0.5f, 0.5f, 0.5f, 1f); 
                    smokeParticles.Play();
                    break;

                case FireState.Suppressed:
                    if (fireParticles != null) fireParticles.Stop();
                    if (smokeParticles != null) smokeParticles.Stop();
                    rend.material.color = new Color(0.2f, 0.2f, 0.2f);
                    break;

                case FireState.Extinguished:
                    smokeParticles.Stop();
                    break;

                default:
                    smokeParticles.Stop();
                    break;
            }
        }

        if (rend != null)
        {
            switch (State)
            {
                case FireState.Ignition:
                    rend.material.color = Color.yellow;
                    break;
                case FireState.Growing:
                    rend.material.color = new Color(1f, 0.5f, 0f);
                    break;
                case FireState.FullBurn:
                    rend.material.color = Color.red;
                    break;
                case FireState.Smoldering:
                    rend.material.color = Color.gray;
                    break;
                case FireState.Extinguished:
                    rend.material.color = Color.black;
                    break;
                default:
                    rend.material.color = Color.green;
                    break;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (State != FireState.None && State != FireState.Extinguished)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, propagationRadius);
        }
    }

    public bool PodeSerReacendido()
    {
        return (State == FireState.None || State == FireState.Suppressed) && combustivelAtual > 0f;
    }

    public virtual bool PodeReceberPropagacao()
    {
        return (State == FireState.None || State == FireState.Suppressed) && combustivelAtual > 0f;
    }

    public virtual void Resfriar(float quantidade)
    {
        if (State == FireState.Extinguished || combustivelAtual <= 0f)
            return;

        combustivelAtual -= quantidade;

        if (combustivelAtual <= 0f)
        {
            ChangeState(FireState.Extinguished); // fogo acaba naturalmente
            return;
        }

        // Se está pegando fogo, pode ser apagado antes do fim
        if (State != FireState.None && State != FireState.Suppressed)
        {
            ChangeState(FireState.None); // volta ao estado original, mas ainda com combustível restante
            hasPropagated = false; // permite que propague novamente no futuro
        }
    }

}



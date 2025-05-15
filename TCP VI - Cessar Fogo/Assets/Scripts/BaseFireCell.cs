using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FireState
{
    None, Ignition, Growing, FullBurn, Smoldering, Extinguished
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


    protected ParticleSystem fireParticles;
    protected Renderer rend;

    protected virtual void Start()
    {
        fireParticles = GetComponentInChildren<ParticleSystem>();
        if (visualObject != null)
        {
            rend = visualObject.GetComponent<Renderer>();
        }
        UpdateVisuals();
    }

    protected virtual void Update()
    {
        if (State == FireState.None || State == FireState.Extinguished) return;

        timer += Time.deltaTime;

        switch (State)
        {
            case FireState.Ignition:
                if (timer >= ignitionTime)
                    ChangeState(FireState.Growing);
                break;

            case FireState.Growing:
                if (timer >= growTime)
                    ChangeState(FireState.FullBurn);
                break;

            case FireState.FullBurn:
                if (timer >= fullBurnTime)
                    ChangeState(FireState.Smoldering);

                if (!hasPropagated && timer >= propagationDelay)
                {
                    TryPropagate();
                    hasPropagated = true;
                }
                break;

            case FireState.Smoldering:
                if (timer >= smolderTime)
                    ChangeState(FireState.Extinguished);
                break;
        }
    }

    public virtual void Ignite()
    {
        if (State == FireState.None)
        {
            ChangeState(FireState.Ignition);
        }
    }

    public virtual void Extinguish()
    {
        if (State != FireState.Extinguished)
        {
            ChangeState(FireState.Extinguished);
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
            if (neighbor != null && neighbor != this && neighbor.State == FireState.None)
            {
                if (Random.value < propagationChance)
                {
                    neighbor.Ignite();
                    Debug.Log($"{name} propagou para {neighbor.name}");
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
}



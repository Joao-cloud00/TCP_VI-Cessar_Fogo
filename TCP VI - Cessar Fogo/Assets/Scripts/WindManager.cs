using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindManager : MonoBehaviour
{
    [Header("Configuração do Vento")]
    [Tooltip("Direção do vento no plano XZ (Ex: (1, 0) = Leste, (0, 1) = Norte)")]
    public Vector2 direcaoDoVento = Vector2.right;

    [Tooltip("Intensidade do vento (quanto ele influencia a propagação do fogo).")]
    [Range(0f, 1f)]
    public float intensidade = 0.5f;

    [Tooltip("Futuro uso para HUD/efeitos visuais.")]
    public bool ventoAtivo = true;

    public static WindManager instancia;

    private void Awake()
    {
        if (instancia == null)
            instancia = this;
        else
            Destroy(gameObject);
    }

    /// <summary>
    /// Retorna a direção do vento como Vector3, aplicada ao plano XZ.
    /// </summary>
    public Vector3 ObterDirecaoComIntensidade()
    {
        Vector2 direcaoXZ = direcaoDoVento.normalized * intensidade;
        return new Vector3(direcaoXZ.x, 0f, direcaoXZ.y);
    }

    private void OnDrawGizmosSelected()
    {
        if (!ventoAtivo) return;

        Gizmos.color = Color.cyan;

        Vector3 origem = transform.position;
        Vector3 direcao = ObterDirecaoComIntensidade();

        Gizmos.DrawLine(origem, origem + direcao * 5f);
        Gizmos.DrawSphere(origem + direcao * 5f, 0.2f);

        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.white;
        #if UNITY_EDITOR
        UnityEditor.Handles.Label(origem + direcao * 5.5f, "Vento", style);
        #endif
    }

    private void Update()
    {
        //para uso em HUD
        //Vector3 direcao = WindManager.instancia.direcaoDoVento.normalized;

    }
}


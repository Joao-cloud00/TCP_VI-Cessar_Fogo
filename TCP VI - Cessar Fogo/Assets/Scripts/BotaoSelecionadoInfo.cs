using UnityEngine;
using UnityEngine.EventSystems;

public class BotaoSelecionadoInfo : MonoBehaviour
{
    public TMPro.TextMeshProUGUI descricaoTexto;

    private GameObject ultimoSelecionado;

    void Update()
    {
        GameObject selecionado = EventSystem.current.currentSelectedGameObject;

        if (selecionado != ultimoSelecionado)
        {
            ultimoSelecionado = selecionado;
            AtualizarDescricao(selecionado);
        }
    }

    void AtualizarDescricao(GameObject botao)
    {
        if (botao == null)
        {
            descricaoTexto.text = "";
            return;
        }

        // Tenta obter um script que contenha a descrição
        BotaoDescricao desc = botao.GetComponent<BotaoDescricao>();

        if (desc != null)
            descricaoTexto.text = desc.descricao;
        else
            descricaoTexto.text = "";
    }
}

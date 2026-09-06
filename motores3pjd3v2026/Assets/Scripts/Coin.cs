using UnityEngine;
using TMPro;

public class CoinUi : MonoBehaviour
{
    [Header("Configuração")]
    [Tooltip("Defina se este texto é do Jogador 1 ou Jogador 2")]
    public int PlayerID = 1;

    private TextMeshProUGUI _textoMoedas;

    private void Awake()
    {
        _textoMoedas = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        // Inscreve no Observer
        PlayerObserverManager.OnCoinCollected += AtualizarTextoMoedas;
    }

    private void OnDisable()
    {
        // Desinscreve do Observer
        PlayerObserverManager.OnCoinCollected -= AtualizarTextoMoedas;
    }

    private void AtualizarTextoMoedas(int idJogador, int totalMoedas)
    {
        // Atualiza apenas se a notificação for para este jogador
        if (idJogador == PlayerID && _textoMoedas != null)
        {
            _textoMoedas.text = $"P{PlayerID} Moedas: {totalMoedas}";
        }
    }
}
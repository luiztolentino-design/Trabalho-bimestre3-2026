using UnityEngine;
using TMPro;

public class CoinUIController : MonoBehaviour
{
    [Header("Textos dos Jogadores")]
    [SerializeField] private TextMeshProUGUI textoMoedasP1;
    [SerializeField] private TextMeshProUGUI textoMoedasP2;

    [Header("Painel de Vitória")]
    [SerializeField] private GameObject painelVencedor;
    [SerializeField] private TextMeshProUGUI textoVencedor;

    private void Start()
    {
        if (painelVencedor != null)
        {
            painelVencedor.SetActive(false);
        }

        // Inicializa ambos os contadores no início da partida
        AtualizarTextoP1(0);
        AtualizarTextoP2(0);
    }

    private void OnEnable()
    {
        PlayerOM.OnCoinCountChanged += OnCoinCollected;
        PlayerOM.OnGameOver += ExibirVencedor;
    }

    private void OnDisable()
    {
        PlayerOM.OnCoinCountChanged -= OnCoinCollected;
        PlayerOM.OnGameOver -= ExibirVencedor;
    }

    private void OnCoinCollected(int playerID, int totalAtual)
    {
        if (playerID == 1)
        {
            AtualizarTextoP1(totalAtual);
        }
        else if (playerID == 2)
        {
            AtualizarTextoP2(totalAtual);
        }
    }

    private void AtualizarTextoP1(int total)
    {
        int meta = (GameManager.Instance != null) ? GameManager.Instance.MoedasParaVencer : 6;
        if (textoMoedasP1 != null)
        {
            textoMoedasP1.text = $"P1 Moedas: {total}/{meta}";
        }
    }

    private void AtualizarTextoP2(int total)
    {
        int meta = (GameManager.Instance != null) ? GameManager.Instance.MoedasParaVencer : 6;
        if (textoMoedasP2 != null)
        {
            textoMoedasP2.text = $"P2 Moedas: {total}/{meta}";
        }
    }

    private void ExibirVencedor(string mensagem)
    {
        if (painelVencedor != null && textoVencedor != null)
        {
            textoVencedor.text = mensagem;
            painelVencedor.SetActive(true);
        }
    }
}
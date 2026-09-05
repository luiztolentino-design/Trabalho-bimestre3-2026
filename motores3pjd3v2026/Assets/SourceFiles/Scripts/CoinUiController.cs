using UnityEngine;
using TMPro;

public class CoinUIController : MonoBehaviour
{
    [Header("Placar Único")]
    [SerializeField] private TextMeshProUGUI textoPlacar; // Arraste seu texto único aqui

    [Header("Painel de Vitória")]
    [SerializeField] private GameObject painelVencedor;
    [SerializeField] private TextMeshProUGUI textoVencedor;

    private int _moedasP1 = 0;
    private int _moedasP2 = 0;

    private void Start()
    {
        if (painelVencedor != null)
        {
           // painelVencedor.SetActive(false);
        }

        AtualizarPlacar();
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
        if (playerID == 1) _moedasP1 = totalAtual;
        else if (playerID == 2) _moedasP2 = totalAtual;

        AtualizarPlacar();
    }

    private void AtualizarPlacar()
    {
        int meta = (GameManager.Instance != null) ? GameManager.Instance.MoedasParaVencer : 6;

        if (textoPlacar != null)
        {
            textoPlacar.text = $"P1: {_moedasP1}/{meta}  |  P2: {_moedasP2}/{meta}";
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
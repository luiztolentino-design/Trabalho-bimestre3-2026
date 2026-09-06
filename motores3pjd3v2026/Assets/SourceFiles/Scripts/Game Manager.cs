using UnityEngine;
using UnityEngine.SceneManagement;
using StarterAssets;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Configurações de Cena")]
    public string GameplaySceneName = "Gameplay";
    public string GUISceneName = "GUI";

    [Header("Regras do Jogo")]
    public int TotalMoedasNaFase = 10;
    public int MoedasParaVencer = 6;

    private int _moedasP1 = 0;
    private int _moedasP2 = 0;
    private bool _jogoFinalizado = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        CarregarJogo();
    }

    public void CarregarJogo()
    {
        _jogoFinalizado = false;
        _moedasP1 = 0;
        _moedasP2 = 0;
        Time.timeScale = 1f;

        SceneManager.LoadScene(GameplaySceneName, LoadSceneMode.Single);
        SceneManager.LoadScene(GUISceneName, LoadSceneMode.Additive);
    }

    public void RegistrarMoedaColetada(int playerID, int totalMoedasJogador)
    {
        if (_jogoFinalizado) return;

        if (playerID == 1) _moedasP1 = totalMoedasJogador;
        else if (playerID == 2) _moedasP2 = totalMoedasJogador;

        if (_moedasP1 >= MoedasParaVencer)
        {
            FinalizarPartida("Jogador 1 Venceu!");
        }
        else if (_moedasP2 >= MoedasParaVencer)
        {
            FinalizarPartida("Jogador 2 Venceu!");
        }
        else if (_moedasP1 + _moedasP2 >= TotalMoedasNaFase)
        {
            if (_moedasP1 > _moedasP2) FinalizarPartida("Jogador 1 Venceu!");
            else if (_moedasP2 > _moedasP1) FinalizarPartida("Jogador 2 Venceu!");
            else FinalizarPartida("Empate!");
        }
    }

    private void FinalizarPartida(string mensagem)
    {
        _jogoFinalizado = true;

        // Desativa a movimentação dos robôs sem congelar o motor do jogo
        ThirdPersonController[] players = FindObjectsOfType<ThirdPersonController>();
        foreach (ThirdPersonController player in players)
        {
            player.enabled = false;
        }

        PlayerOM.OnGameOver?.Invoke(mensagem);
    }
}
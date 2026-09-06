using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Configurações de Cena")]
    public string GameplaySceneName = "Gameplay";
    public string GUISceneName = "GUI";

    [Header("Regras do Jogo")]
    [Tooltip("Total de moedas na cena para encerrar a partida")]
    public int TotalMoedasNaCena = 10;

    private int _p1Moedas = 0;
    private int _p2Moedas = 0;

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

    private void OnEnable()
    {
        PlayerObserverManager.OnCoinCollected += VerificarVitoria;
    }

    private void OnDisable()
    {
        PlayerObserverManager.OnCoinCollected -= VerificarVitoria;
    }

    public void CarregarJogo()
    {
        // Carrega Gameplay primeiro e adiciona GUI em cima de forma assíncrona
        SceneManager.LoadScene(GameplaySceneName, LoadSceneMode.Single);
        SceneManager.LoadSceneAsync(GUISceneName, LoadSceneMode.Additive);
    }

    private void VerificarVitoria(int playerId, int count)
    {
        if (playerId == 1) _p1Moedas = count;
        else if (playerId == 2) _p2Moedas = count;

        if (_p1Moedas + _p2Moedas >= TotalMoedasNaCena)
        {
            int vencedor = 0; // 0 = Empate
            if (_p1Moedas > _p2Moedas) vencedor = 1;
            else if (_p2Moedas > _p1Moedas) vencedor = 2;

            PlayerObserverManager.NotifyGameOver(vencedor);
        }
    }
}
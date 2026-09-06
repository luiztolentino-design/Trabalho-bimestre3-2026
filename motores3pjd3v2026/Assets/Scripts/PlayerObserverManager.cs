using System;

public static class PlayerObserverManager
{
    // Notifica quando um jogador coleta moedas: (PlayerID, TotalMoedas)
    public static Action<int, int> OnCoinCollected;

    // Notifica o vencedor da partida: (WinnerPlayerID -> 1, 2 ou 0 para Empate)
    public static Action<int> OnGameOver;

    public static void NotifyCoinCollected(int playerId, int totalCoins)
    {
        OnCoinCollected?.Invoke(playerId, totalCoins);
    }

    public static void NotifyGameOver(int winnerPlayerId)
    {
        OnGameOver?.Invoke(winnerPlayerId);
    }
}
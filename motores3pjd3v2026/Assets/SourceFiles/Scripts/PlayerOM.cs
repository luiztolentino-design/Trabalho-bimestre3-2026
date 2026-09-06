using System;

public static class PlayerOM
{
    // Transmite: (int playerID, int totalMoedasDoJogador)
    public static Action<int, int> OnCoinCountChanged;

    // Transmite: (string mensagemVencedor)
    public static Action<string> OnGameOver;
}
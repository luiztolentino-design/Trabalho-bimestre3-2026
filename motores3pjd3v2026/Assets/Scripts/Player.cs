using UnityEngine;

public class Player : MonoBehaviour
{
    private int totalCoins = 0;

    public void CollectCoin()
    {
        totalCoins++;

        PlayerObserverManager.NotifyCoinCollected(totalCoins);
    }
}
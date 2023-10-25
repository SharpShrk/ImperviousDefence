using UnityEngine;

public class RewardCollector : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private Score _score;

    public void TakeReward(int money, int score)
    {
        _wallet.AddMoney(money);
        _score.AddScore(score);
    }
}

namespace UIReward
{
    public interface IReward
    {
        event System.Action<int> ValueChanged;
    }
}
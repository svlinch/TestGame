using UniRx;

public class PlayerInitializedEvent
{
    public ReactiveProperty<uint> Score;

    public PlayerInitializedEvent(ReactiveProperty<uint> score)
    {
        Score = score;
    }
}

public class PlayerInitializedEvent
{
    public CustomProperty<uint> Score;

    public PlayerInitializedEvent(CustomProperty<uint> score)
    {
        Score = score;
    }
}

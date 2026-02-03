public class ScoreChanged : IGameEvent
{
    public int Score { get; private set; }
    
    public ScoreChanged(int score)
    {
        Score = score;
    }
}
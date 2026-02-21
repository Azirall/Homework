public class ScoreChanged : IGameEvent
{
    public string Score { get; private set; }
    
    public ScoreChanged(string score)
    {
        Score = score;
    }
}
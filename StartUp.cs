namespace BettingManager.UI
{
    using BettingManager.Logic.Engine;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            BettingManagerEngine.Instance.Start();
        }
    }
}

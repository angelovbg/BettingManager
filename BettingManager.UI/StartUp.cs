using Ninject;

namespace BettingManager.UI
{
    using Logic.Engine;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            IKernel kernel = new StandardKernel(new BettingManagerModule());
            var engine = kernel.Get<BettingManagerEngine>();
            engine.Start();
        }
    }
}
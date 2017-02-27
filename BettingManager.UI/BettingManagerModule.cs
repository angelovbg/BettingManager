using Ninject.Extensions.Conventions;
using BettingManager.Logic.Contracts.Factories;
using BettingManager.Logic.Engine;
using Ninject.Modules;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using BettingManager.Logic.Commands;
using Ninject;
using Ninject.Extensions.Factory;
using BettingManager.Logic.Contracts;
using BettingManager.Logic.Provider;
using BettingManager.Logic.Models;
using BettingManager.Logic.Contracts.Models;

namespace BettingManager.UI
{
    public class BettingManagerModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind(x =>
            {
                //x.FromThisAssembly()
                x.FromAssembliesInPath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                .SelectAllClasses()
                .Where(type => type != typeof(BettingManagerEngine))
                .BindDefaultInterface();
            });
            
            Kernel.Bind<BettingManagerEngine>().ToSelf().InSingletonScope();
            Kernel.Bind<IAccount>().To<BetfairAccount>();
            Kernel.Bind<IResult>().To<FinalResult>();

            //named Binding may be instanced in Factory with Get...SameNameLikeThisOne - GetRegularBet
            Kernel.Bind<IBet>().To<RegularBet>().Named("RegularBet");
            Kernel.Bind<IBet>().To<DoubleSignBet>().Named("DoubleSignBet");
            Kernel.Bind<IBet>().To<UnderOverBet>().Named("UnderOverBet");

            Kernel.Bind<CreateAccountCommand>().ToSelf().InSingletonScope();
            Kernel.Bind<ShowAccountCommand>().ToSelf().InSingletonScope();
            Kernel.Bind<CreateLineCommand>().ToSelf().InSingletonScope();
            Kernel.Bind<CreateMatchCommand>().ToSelf().InSingletonScope();
            Kernel.Bind<CreateFinalResultCommand>().ToSelf().InSingletonScope();
            Kernel.Bind<CreateBetCommand>().ToSelf().InSingletonScope();
            Kernel.Bind<CreateTipsterCommand>().ToSelf().InSingletonScope();

            Kernel.Bind<IWriter>().To<ConsoleWriterProvider>().InSingletonScope();
            Kernel.Bind<IReader>().To<ConsoleReaderProvider>().InSingletonScope();
            Kernel.Bind<IParser>().To<CommandParserProvider>().InSingletonScope();

            Kernel.Bind<ICommandFactory>().ToFactory().InSingletonScope();
            Kernel.Bind<IAccountFactory>().ToFactory().InSingletonScope();
            Kernel.Bind<IBetLineFactory>().ToFactory().InSingletonScope();
            Kernel.Bind<IMatchFactory>().ToFactory().InSingletonScope();
            Kernel.Bind<IResultFactory>().ToFactory().InSingletonScope();
            Kernel.Bind<IBetFactory>().ToFactory().InSingletonScope();
            Kernel.Bind<ITipsterFactory>().ToFactory().InSingletonScope();

            Bind(typeof(IAddAccount), typeof(IGetAccountById),
                    typeof(IAddLine),  typeof(IGetLineById), typeof(IGetLineByName),
                    typeof(IAddMatch), typeof(IGetMatchById), typeof(IGetAllMacthesAfterDate), typeof(IGetMatchesWithoutResults),
                    typeof(IAddResult), typeof(IAddBet), typeof(IGetBetById), typeof(IAddTipster), typeof(IGetTipsterById))
                    .To<BettingManagerData>()
                    .InSingletonScope();

            Kernel.Bind<ICommand>().ToMethod(context =>
            {
                Type commandType = (Type)context.Parameters.Single().GetValue(context, null);
                return (ICommand)context.Kernel.Get(commandType);
            }).NamedLikeFactoryMethod((ICommandFactory commandFactory) => commandFactory.GetCommand(null));
        }
    }
}

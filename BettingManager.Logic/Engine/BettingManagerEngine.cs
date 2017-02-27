namespace BettingManager.Logic.Engine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Common.Constants;
    using Contracts;
    using Contracts.Controllers;
    using Contracts.Models;
    using Controllers;
    using Logic.Models;
    using Commands;

    public class BettingManagerEngine
    {
        private const string TerminationCommand = "End";
        private const string UnknownCommandMessage = "Unknown command name";
        private const string NullProvidersExceptionMessage = "cannot be null.";

        private readonly Dictionary<string, IAccount> accounts;
        private readonly Dictionary<string, IBetLine> lines;
        private readonly Dictionary<int, IMatch> matches;

        private readonly IBetController betController;
        private readonly IBetLineController betLineController;
        private readonly IMatchController matchController;
        private readonly IResultController resultController;

        private readonly IReader reader;
        private readonly IWriter writer;
        private readonly IParser parser;

        public BettingManagerEngine(IReader readerProvider, IWriter writerProvider, IParser parserProvider)
        {
            this.accounts = new Dictionary<string, IAccount>();
            this.lines = new Dictionary<string, IBetLine>();
            this.matches = new Dictionary<int, IMatch>();

            this.betController = new BetController();
            this.betLineController = new BetLineController();
            this.matchController = new MatchController();
            this.resultController = new ResultController();

            if (readerProvider == null)
            {
                throw new ArgumentNullException($"Reader {NullProvidersExceptionMessage}");
            }

            if (writerProvider == null)
            {
                throw new ArgumentNullException($"Writer {NullProvidersExceptionMessage}");
            }

            if (parserProvider == null)
            {
                throw new ArgumentNullException($"Parser {NullProvidersExceptionMessage}");
            }

            this.reader = readerProvider;
            this.writer = writerProvider;
            this.parser = parserProvider;
        }

        public void Start()
        {
            //this.ReadCommands();

            while (true)
            {
                try
                {
                    var commandAsString = this.reader.ReadLine();

                    if (commandAsString == TerminationCommand)
                    {
                        break;
                    }

                    this.ProcessCommand(commandAsString);
                }
                catch (Exception ex)
                {
                    this.writer.WriteLine(ex.Message);
                }
            }
        }

        private void ReadCommands()
        {
            while (true)
            {
                var currentLine = Console.ReadLine();
                ICommandParser currentCommand = null;
                try
                {
                    currentCommand = CommandParser.Parse(currentLine);
                    var commandResult = this.ProcessCommands(currentCommand);
                    this.PrintReports(commandResult);
                }
                catch (ArgumentException argumentException)
                {
                    Console.WriteLine("\t" + argumentException.Message);
                }
                catch (NullReferenceException nullReferenceException)
                {
                    Console.WriteLine("\t" + nullReferenceException.Message);
                }
                catch (InvalidOperationException invalidOperationException)
                {
                    Console.WriteLine("\t" + invalidOperationException.Message);
                }
                catch (FormatException formatException)
                {
                    Console.WriteLine("\t" + formatException.Message);
                }
            }
        }

        private void ProcessCommand(string commandAsString)
        {
            if (string.IsNullOrWhiteSpace(commandAsString))
            {
                throw new ArgumentNullException("Command cannot be null or empty.");
            }

            var command = this.parser.ParseCommand(commandAsString);
            var parameters = this.parser.ParseParameters(commandAsString);

            var executionResult = command.Execute(parameters);
            this.writer.WriteLine(executionResult);
        }

        private IList<string> ProcessCommands(ICommandParser command)
        {
            var reports = new List<string>();
            var parameters = command.Parameters;
            string commandResult = string.Empty;

            switch (command.Name)
            {
                //case "CreateAccount":
                //var commandName = new CreateAccountCommand(this.accounts);
                //commandResult = commandName.Execute(parameters);
                //break;
                //case "CreateAccount":
                //commandResult = this.CreateAccount(parameters);
                //break;
                //case "ShowAccount":
                //    commandResult = this.ShowAccount(parameters);
                //    break;
                //case "DepositAccount":
                //    commandResult = this.DepositAccount(parameters);
                //    break;
                //case "WithdrawAccount":
                //    commandResult = this.WithdrawAccount(parameters);
                //    break;
                //case "HelpAccount":
                //    commandResult = this.HelpAccount();
                //    break;
                //case "ChangeAccountRake":
                //    commandResult = this.ChangeAccountRake(parameters);
                //    break;
                // case "AddLine":
                //    commandResult = this.AddLine(parameters);
                //    break;
                //case "ShowLine":
                //    commandResult = this.ShowLine(parameters);
                //    break;
                //case "ChangeLineStep":
                //    commandResult = this.ChangeLineStep(parameters);
                //    break;
                //case "CheckNextBet":
                //    commandResult = this.CheckNextBet(parameters);
                //    break;
                //case "AddMatch":
                //    commandResult = this.AddMatch(parameters);
                //    break;
                //case "ShowMatch":
                //    commandResult = this.ShowMatch(parameters);
                //    break;
                //case "ShowIncommingMatches":
                //    this.ShowIncommingMatches(parameters);
                //    break;
                //case "ShowMatchesWithoutResults":
                //    this.ShowMatchesWithoutResults(parameters);
                //    break;
                //case "AddRegularBet":
                //    commandResult = this.AddRegularBet(parameters);
                //    break;
                //case "AddGoalsBet":
                //    commandResult = this.AddGoalsBet(parameters);
                //    break;
                //case "AddDoubleBet":
                //    commandResult = this.AddDoubleBet(parameters);
                //    break;
                //TODO case "AddRegularLayBet"
                //case "AddFinalResult":
                //    commandResult = this.AddFinalResult(parameters);
                //    break;
                case "ShowCommands":
                    this.ShowCommands();
                    break;
                case "Exit":
                    Environment.Exit(0);
                    break;
                default:
                    commandResult = UnknownCommandMessage;
                    break;
            }

            reports.Add("\t" + commandResult);
         
            return reports;
        }

        private void PrintReports(IList<string> reports)
        {
            var output = new StringBuilder();

            foreach (var report in reports)
            {
                output.AppendLine(report);
            }

            Console.Write(output.ToString());
        }

        /*
        private string HelpAccount()
        {
            return string.Format("CreateAccount name balance rake currency");
        }
        */
        /*
        private string DepositAccount(IList<string> parameters)
        {
            var accountName = parameters[0];
            var amount = decimal.Parse(parameters[1]);

            var account = this.AccountContainsKey(accountName);
            account.DepositBalance(amount);

            return string.Format("{0} {1} has been deposited to account {2}", amount, account.Currency, account.Name);
        }
        */
        /*
        private string WithdrawAccount(IList<string> parameters)
        {
            var accountName = parameters[0];
            var amount = decimal.Parse(parameters[1]);

            var account = this.AccountContainsKey(accountName);
            account.WithdrawBalance(amount);

            return string.Format("{0} {1} has been withdrawed from account {2}", amount, account.Currency, account.Name);
        }
        */
        //TODO remove - depricated
        /*
        private string CreateAccount(IList<string> parameters)
        {
            var accountName = parameters[0];
            var balance = decimal.Parse(parameters[1]);
            var rake = decimal.Parse(parameters[2]);
            var currency = parameters[3];

            var currencyType = (CurrencyType)Enum.Parse(typeof(CurrencyType), currency);

            TODO remove comment
            var account = new BetfairAccount(accountName, balance, currencyType, rake);

            this.accounts.Add(accountName, account);

            return string.Format("Account {0} has been added.", account.Name);
        }
        */
        //TODO remove depricated
        /*
        /// <exception cref="NullReferenceException">Throw when account is null</exception>
        private string ShowAccount(IList<string> parameters)
        {
            var accountName = parameters[0];

            var account = this.AccountContainsKey(accountName);

            return account.ToString();
        }
        */
        /// <exception cref="NullReferenceException">Throw when account is null</exception>
        //private string ChangeAccountRake(IList<string> parameters)
        //{
        //    var accountName = parameters[0];
        //    var rake = decimal.Parse(parameters[1]);
            
        //    var account = this.AccountContainsKey(accountName);
        //    account.CurrentRake = rake;
        //    return string.Format("Rake for account {0} has been changed to {1}.", account.Name, account.CurrentRake);
        //}

        /*
        TODO remove Depricated   
        /// <exception cref="NullReferenceException">Throw when account is null</exception>
        private string AddLine(IList<string> parameters)
        {
            var accountName = parameters[0];

            var account = this.AccountContainsKey(accountName);

            var line = this.betLineController.AddLine(parameters, account);

            this.lines.Add(line.Name, line);

            return string.Format("Line {0} has been added.", line.Name);
        }
        
        //TODO remove Depricated
        /// <exception cref="NullReferenceException">Throw when line is null</exception>
        private string ShowLine(IList<string> parameters)
        {
            var lineName = parameters[0];

            var line = this.BetLineContainsKey(lineName);

            return this.lines[lineName].ToString();
        }
        */
        /// <exception cref="NullReferenceException">Throw when line is null</exception>
        //private string ChangeLineStep(IList<string> parameters)
        //{
        //    var lineName = parameters[0];
        //    var step = decimal.Parse(parameters[1]);

        //    var line = this.BetLineContainsKey(lineName);
        //    line.StepAmount = step;

        //    return string.Format("Step for Line {0} has been changed to {1}", line.Name, line.StepAmount);
        //}

        /// <exception cref="NullReferenceException">Throw when match is null</exception>
        //private string AddMatch(IList<string> parameters)
        //{
        //    var match = this.matchController.AddMatch(parameters);

        //    if (this.matches.ContainsKey(match.Id))
        //    {
        //        throw new NullReferenceException("This match key is already used!");
        //    }

        //    this.matches.Add(match.Id, match);

        //    return string.Format("Match {0} has been added.", match.Id);
        //}

        /// <exception cref="NullReferenceException">Throw when match is null</exception>
        //private string ShowMatch(IList<string> parameters)
        //{
        //    var matchId = parameters[0];

        //    var match = this.MatchContainsKey(matchId);

        //    return match.ToString();
        //}

        /// <exception cref="NullReferenceException">Throw when match is null</exception>
        //private string AddFinalResult(IList<string> parameters)
        //{
        //    var matchId = parameters[0];

        //    var match = this.MatchContainsKey(matchId);

        //    var result = this.resultController.AddFinalResult(match, parameters);

        //    this.betController.SetupCheckedBets(match.Bets);

        //    return string.Format("Result for match {0} has been added.", match.Id);
        //}

        /// <exception cref="NullReferenceException">Throw when match is null</exception>
        /// <exception cref="NullReferenceException">Throw when line is null</exception>
        //private string AddRegularBet(IList<string> parameters)
        //{
        //    var matchId = parameters[0];
        //    var lineName = parameters[1];
        //    var line = this.BetLineContainsKey(lineName);

        //    var tipster = new Tipster("Angel", TipsterCompany.Myself);

        //    var match = this.MatchContainsKey(matchId);

        //    var bet = this.betController.AddRegularBet(parameters, match, line, tipster);

        //    return string.Format("Bet for match {0} has been added.", bet.Match.Id);
        //}

        //private string AddDoubleBet(IList<string> parameters)
        //{
        //    var matchId = parameters[0];
        //    var lineName = parameters[1];
        //    var line = this.BetLineContainsKey(lineName);

        //    var tipster = new Tipster("Angel", TipsterCompany.Myself);

        //    var match = this.MatchContainsKey(matchId);

        //    var bet = this.betController.AddDoubleBet(parameters, match, line, tipster);

        //    return string.Format("Bet for match {0} has been added.", bet.Match.Id);
        //}

        //private string AddGoalsBet(IList<string> parameters)
        //{
        //    var matchId = parameters[0];
        //    var lineName = parameters[1];
        //    var line = this.BetLineContainsKey(lineName);

        //    var tipster = new Tipster("Angel", TipsterCompany.Myself);

        //    var match = this.MatchContainsKey(matchId);

        //    var bet = this.betController.AddGoalsBet(parameters, match, line, tipster);

        //    return string.Format("Bet for match {0} has been added.", bet.Match.Id);
        //}

        /// <exception cref="NullReferenceException">Throw when line is null</exception>
        //private string CheckNextBet(IList<string> parameters)
        //{
        //    var lineName = parameters[0];
        //    var line = this.BetLineContainsKey(lineName);
        //    var coefficient = decimal.Parse(parameters[1]);
            
        //    var valueToBet = line.ChechValueToBeBet(coefficient);

        //    return string.Format("Bet value at coefficiet {1:F2} must be: {0:F2} - Line: {2}", valueToBet, coefficient, line.Name);
        //}

        private IBetLine BetLineContainsKey(string lineName)
        {
            if (!this.lines.ContainsKey(lineName))
            {
                throw new NullReferenceException("Line name cannot be empty");
            }

            return this.lines[lineName];
        }

        private IMatch MatchContainsKey(string id)
        {
            var matchId = int.Parse(id);
            if (!this.matches.ContainsKey(matchId))
            {
                throw new NullReferenceException("Match id do not exist");
            }

            return this.matches[matchId];
        }

        //private IAccount AccountContainsKey(string name)
        //{
        //    var accountName = name;
        //    if (!this.accounts.ContainsKey(accountName))
        //    {
        //        throw new NullReferenceException(string.Format("No such account!"));
        //    }

        //    return this.accounts[accountName];
        //}

        //private void ShowIncommingMatches(IList<string> parameters)
        //{
        //    var sortedIncommingMatches = from t in this.matches.Values
        //        where t.Date > DateTime.Now
        //        orderby t.Date
        //        select t;
        //    if (sortedIncommingMatches.ToList().Count > 0)
        //    {
        //        foreach (var match in sortedIncommingMatches)
        //        {
        //            Console.WriteLine(match.ToString());
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("There is no one incomming match");
        //    }
        //}

        //private void ShowMatchesWithoutResults(IList<string> parameters)
        //{
        //    var matchesWithoutResults = from t in this.matches.Values
        //        where t.Date < DateTime.Now && t.Results.Count == 0
        //        orderby t.Date
        //        select t;

        //    if (matchesWithoutResults.ToList().Count > 0)
        //    {
        //        foreach (var match in matchesWithoutResults)
        //        {
        //            Console.WriteLine(match.ToString());
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("There are no past matches without result");
        //    }
        //}

        private void ShowCommands()
        {
            foreach (Commands command in Enum.GetValues(typeof(Commands)))
            {
                Console.WriteLine(command.ToString());
            }
        }
    }
}
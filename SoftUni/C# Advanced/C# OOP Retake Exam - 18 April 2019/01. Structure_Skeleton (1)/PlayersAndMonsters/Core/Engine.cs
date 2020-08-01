using PlayersAndMonsters.Core.Contracts;
using PlayersAndMonsters.Models.BattleFields;
using PlayersAndMonsters.Models.BattleFields.Contracts;
using PlayersAndMonsters.Models.Players.Contracts;
using PlayersAndMonsters.Repositories;
using PlayersAndMonsters.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlayersAndMonsters.Core
{
    class Engine : IEngine
    {
        private IManagerController managerController;

        private IPlayerRepository playerRepository = new PlayerRepository();
        private ICardRepository cardRepository = new CardRepository();
        private IBattleField battleField = new BattleField();
        private ICardRepository cards = new CardRepository();
        public void Run()
        {
            managerController = new ManagerController(playerRepository, cardRepository, battleField, cards);
            while (true)
            {
                string command = Console.ReadLine();
                if (command == "Exit")
                {
                    break;
                }

                string[] commandParts = command.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                if (commandParts[0] == "AddPlayer")
                {
                    string playerType = commandParts[1];
                    string playerUsername = commandParts[2];
                    Console.WriteLine(managerController.AddPlayer(playerType, playerUsername));
                }

                else if (commandParts[0] == "AddCard")
                {
                    string cardType = commandParts[1];
                    string cardUsername = commandParts[2];
                    Console.WriteLine(managerController.AddCard(cardType, cardUsername));
                }

                else if (commandParts[0] == "AddPlayerCard")
                {
                    string playerUsername = commandParts[1];
                    string cardUsername = commandParts[2];
                    Console.WriteLine(managerController.AddPlayerCard(playerUsername, cardUsername));
                }

                else if (commandParts[0] == "Fight")
                {
                    string attackPlayer = commandParts[1];
                    string enemyPlayer = commandParts[2];
                    Console.WriteLine(managerController.Fight(attackPlayer, enemyPlayer));
                }

                else if (commandParts[0] == "Report")
                {
                    Console.WriteLine(managerController.Report());
                }
            }
        }
    }
}

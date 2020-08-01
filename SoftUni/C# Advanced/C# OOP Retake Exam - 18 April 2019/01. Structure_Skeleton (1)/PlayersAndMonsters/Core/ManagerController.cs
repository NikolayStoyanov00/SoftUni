namespace PlayersAndMonsters.Core
{
    using System;
    using System.Text;
    using Contracts;
    using PlayersAndMonsters.Core.Factories.Contracts;
    using PlayersAndMonsters.Models.BattleFields;
    using PlayersAndMonsters.Models.BattleFields.Contracts;
    using PlayersAndMonsters.Models.Cards;
    using PlayersAndMonsters.Models.Cards.Contracts;
    using PlayersAndMonsters.Models.Players;
    using PlayersAndMonsters.Models.Players.Contracts;
    using PlayersAndMonsters.Repositories;
    using PlayersAndMonsters.Repositories.Contracts;

    public class ManagerController : IManagerController
    {
        public ManagerController(
            IPlayerRepository playerRepository,
            ICardRepository cardRepository,
            IBattleField battleField,
            ICardRepository cards)
        {
            this.playerRepository = playerRepository;
            this.cardRepository = cardRepository;
            this.battleField = battleField;
            this.cards = cards;
        }

        private IPlayerRepository playerRepository;
        private ICardRepository cardRepository;
        private IBattleField battleField;

        private ICardRepository cards;
        public string AddPlayer(string type, string username)
        {
            switch (type)
            {
                case "Beginner":
                    Beginner beginner = new Beginner(new CardRepository(), username);
                    playerRepository.Add(beginner);
                    break;
                case "Advanced":
                    Advanced advanced = new Advanced(new CardRepository(), username);
                    playerRepository.Add(advanced);
                    break;
                default:
                    break;
            }

            return $"Successfully added player of type {type} with username: {username}";
        }

        public string AddCard(string type, string name)
        {
            switch (type)
            {
                case "Magic":
                    MagicCard magicCard = new MagicCard(name);
                    cards.Add(magicCard);
                    break;

                case "Trap":
                    TrapCard trapCard = new TrapCard(name);
                    cards.Add(trapCard);
                    break;
                default:
                    break;
            }

            return $"Successfully added card of type {type}Card with name: {name}";
        }

        public string AddPlayerCard(string username, string cardName)
        {
            IPlayer player = playerRepository.Find(username);
            ICard card = cards.Find(cardName);

            player.CardRepository.Add(card);
            return $"Successfully added card: {cardName} to user: {username}";
        }

        public string Fight(string attackUser, string enemyUser)
        {
            IPlayer attackPlayer = playerRepository.Find(attackUser);
            IPlayer enemyPlayer = playerRepository.Find(enemyUser);

            while (!attackPlayer.IsDead && !enemyPlayer.IsDead)
            {
                battleField.Fight(attackPlayer, enemyPlayer);
            }

            return $"Attack user health {attackPlayer.Health} - Enemy user health {enemyPlayer.Health}";
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var player in playerRepository.Players)
            {
                sb.AppendLine($"Username: {player.Username} - Health: {player.Health} – Cards {player.CardRepository.Cards.Count}");

                foreach (var card in player.CardRepository.Cards)
                {
                    sb.AppendLine($"Card: {card.Name} - Damage: {card.DamagePoints}");
                }
                sb.AppendLine("###");
            }

            return sb.ToString().TrimEnd();
        }
    }
}

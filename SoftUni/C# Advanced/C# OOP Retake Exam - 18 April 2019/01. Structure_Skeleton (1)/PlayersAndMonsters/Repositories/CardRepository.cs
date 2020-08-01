using PlayersAndMonsters.Models.Cards.Contracts;
using PlayersAndMonsters.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlayersAndMonsters.Repositories
{
    public class CardRepository : ICardRepository
    {
        public CardRepository()
        {
            this.cards = new Dictionary<string, ICard>();
        }
        public int Count => throw new NotImplementedException();

        private Dictionary<string, ICard> cards;

        public IReadOnlyCollection<ICard> Cards => cards.Values;

        public void Add(ICard card)
        {
            if (card == null)
            {
                throw new ArgumentException("Card cannot be null!");
            }

            if (cards.ContainsKey(card.Name))
            {
                throw new ArgumentException($"Card {card.Name} already exists!");
            }

            cards.Add(card.Name, card);
        }

        public ICard Find(string name)
        {
            return cards[name];
        }

        public bool Remove(ICard card)
        {
            if (card == null)
            {
                throw new ArgumentException("Card cannot be null!");
            }

            if (cards.Remove(card.Name))
            {
                return true;
            }

            return false;
        }
    }
}

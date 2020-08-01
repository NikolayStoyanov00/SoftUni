using PlayersAndMonsters.Models.BattleFields.Contracts;
using PlayersAndMonsters.Models.Players;
using PlayersAndMonsters.Models.Players.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlayersAndMonsters.Models.BattleFields
{
    public class BattleField : IBattleField
    {
        public void Fight(IPlayer attackPlayer, IPlayer enemyPlayer)
        {
            if (attackPlayer.IsDead || enemyPlayer.IsDead)
            {
                throw new ArgumentException("Player is dead!");
            }

            if (attackPlayer is Beginner)
            {
                attackPlayer.Health += 40;

                foreach (var card in attackPlayer.CardRepository.Cards)
                {
                    card.DamagePoints += 30;
                }
            }

            if (enemyPlayer is Beginner)
            {
                enemyPlayer.Health += 40;

                foreach (var card in enemyPlayer.CardRepository.Cards)
                {
                    card.DamagePoints += 30;
                }
            }

            attackPlayer.Health += attackPlayer.CardRepository.Cards.Sum(x => x.HealthPoints);
            enemyPlayer.Health += enemyPlayer.CardRepository.Cards.Sum(x => x.HealthPoints);

            int attackPlayerDamage = attackPlayer.CardRepository.Cards.Sum(x => x.DamagePoints);
            enemyPlayer.TakeDamage(attackPlayerDamage);

            if (enemyPlayer.Health <= 0)
            {
                return;
            }

            int enemyPlayerDamage = enemyPlayer.CardRepository.Cards.Sum(x => x.DamagePoints);
            attackPlayer.TakeDamage(enemyPlayerDamage);

            if (attackPlayer.Health <= 0)
            {
                return;
            }
        }
    }
}

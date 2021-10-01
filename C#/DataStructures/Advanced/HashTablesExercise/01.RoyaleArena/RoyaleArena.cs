namespace _01.RoyaleArena
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class RoyaleArena : IArena
    {
        private Dictionary<int, BattleCard> deck = new Dictionary<int, BattleCard>();
        private Dictionary<CardType, Table<BattleCard>> cardTypeSortedByDamage = 
                        new Dictionary<CardType, Table<BattleCard>>();
        private Dictionary<string, Table<BattleCard>> nameSortedBySwag =
                        new Dictionary<string, Table<BattleCard>>();
        private Table<BattleCard> sortedBySwag = new Table<BattleCard>(new SwagIndex());

        public void Add(BattleCard card)
        {
            deck[card.Id] = card;
            AddToSearchableCollection<DamageIndex>(cardTypeSortedByDamage, card, (c) => c.Type);
            AddToSearchableCollection<SwagIndex>(nameSortedBySwag, card, (c) => c.Name);
            sortedBySwag.Add(card);
        }

        public bool Contains(BattleCard card)
        {
            return deck.ContainsKey(card.Id);
        }

        public int Count => this.deck.Count;

        public void ChangeCardType(int id, CardType type)
        {
            if (!this.deck.ContainsKey(id))
            {
                throw new InvalidOperationException($"There is no card with this id: {id}");
            }

            this.RemoveFromSearchableCollection(cardTypeSortedByDamage, deck[id], c => c.Type);
            this.RemoveFromSearchableCollection(nameSortedBySwag, deck[id], c => c.Name);
            sortedBySwag.Remove(deck[id]);
            this.deck[id].Type = type;
            AddToSearchableCollection<DamageIndex>(cardTypeSortedByDamage, deck[id], (c) => c.Type);
            AddToSearchableCollection<DamageIndex>(nameSortedBySwag, deck[id], (c) => c.Name);
            sortedBySwag.Add(deck[id]);

        }

        public BattleCard GetById(int id)
        {
            if (!this.deck.ContainsKey(id))
            {
                throw new InvalidOperationException($"The key does noe exist: {id}");
            }

            return this.deck[id];
        }

        public void RemoveById(int id)
        {
            if (!deck.ContainsKey(id))
            {
                throw new InvalidOperationException($"The key does noe exist: {id}");
            }

            this.RemoveFromSearchableCollection(cardTypeSortedByDamage, deck[id], c => c.Type);
            this.RemoveFromSearchableCollection(nameSortedBySwag, deck[id], c => c.Name);
            sortedBySwag.Remove(deck[id]);
            deck.Remove(id);
        }

        public IEnumerable<BattleCard> GetByCardType(CardType type)
        {
            if(!cardTypeSortedByDamage.ContainsKey(type))
            {
                throw new InvalidOperationException($"The type does noe exist: {type}");
            }

            return cardTypeSortedByDamage[type];
        }

        public IEnumerable<BattleCard> GetByTypeAndDamageRangeOrderedByDamageThenById(CardType type, int lo, int hi)
        {
            if (!cardTypeSortedByDamage.ContainsKey(type))
            {
                throw new InvalidOperationException($"The type does noe exist: {type}");
            }

            return cardTypeSortedByDamage[type]?.GetViewBetween(lo, hi)
                .OrderBy(c => c);
        }

        public IEnumerable<BattleCard> GetByCardTypeAndMaximumDamage(CardType type, double damage)
        {
            if (!cardTypeSortedByDamage.ContainsKey(type))
            {
                throw new InvalidOperationException($"The type does noe exist: {type}");
            }

            return cardTypeSortedByDamage[type]?.GetViewBetween(cardTypeSortedByDamage[type].MinKey, damage)
                        .OrderBy(c => c);
        }

        public IEnumerable<BattleCard> GetByNameOrderedBySwagDescending(string name)
        {
            if (!nameSortedBySwag.ContainsKey(name))
            {
                throw new InvalidOperationException($"The name does not exist: {name}");
            }

            return nameSortedBySwag[name].Reverse();
        }

        public IEnumerable<BattleCard> GetByNameAndSwagRange(string name, double lo, double hi)
        {
            if (!nameSortedBySwag.ContainsKey(name))
            {
                throw new InvalidOperationException($"The name does not exist: {name}");
            }

            return nameSortedBySwag[name].GetViewBetween(lo, hi);
        }

        public IEnumerable<BattleCard> FindFirstLeastSwag(int n)
        {
            if (n > this.deck.Count)
            {
                throw new InvalidOperationException("There is not enough cards");
            }
            return sortedBySwag.GetFirstN(n, c => c.Id);
        }

        public IEnumerable<BattleCard> GetAllInSwagRange(double lo, double hi)
        {
            return sortedBySwag.GetViewBetween(lo, hi);
        }


        public IEnumerator<BattleCard> GetEnumerator()
        {
            return deck.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private void AddToSearchableCollection<T>(IDictionary dict, BattleCard card, Func<BattleCard, object> getKey)
                where T : Index<double>, new()
        {
            var key = getKey(card);

            if (dict[key] == null)
            {
                dict[key] = new Table<BattleCard>(new T());
            }

            (dict[key] as Table<BattleCard>).Add(card);
        }

        private void RemoveFromSearchableCollection(IDictionary dict, BattleCard card, Func<BattleCard, object> getKey)
        {
            var key = getKey(card);

            if (dict[key] != null)
            {
                var items = dict[key] as Table<BattleCard>;
                items.Remove(card);

                if (items.Count == 0)
                {
                    dict.Remove(key);
                }
            }
        }
    }
}
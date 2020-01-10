using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TotallyFairRace.Model
{
    public class RankCollection : IEnumerable<Account>
    {
        private IList<Account> _rank;

        public RankCollection()
        {
            _rank = new List<Account>();
        }

        public RankCollection(List<Account> collection)
        {
            _rank = collection;
        }

        public bool Add(Account item)
        {
            if (_rank.Any(e => e.Nickname == item.Nickname))
                return false;
            _rank.Add(item);
            _rank = _rank.OrderByDescending(a => a.Money).ToList();
            return true;
        }

        public bool Remove(string nickname)
        {
            if (_rank.All(e => e.Nickname != nickname)) return false;
            _rank.Remove(_rank.First(e => e.Nickname == nickname));
            return true;
        }

        public Account this[int x] => _rank[x];
        public IEnumerable<Account> Where(Func<Account, bool> func) => _rank.Where(func);

        public IEnumerator<Account> GetEnumerator() => _rank.GetEnumerator();

        public ICollection<Account> ToArray() => _rank.ToArray();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Sort(bool Descending) => 
            _rank = Descending ? _rank.OrderByDescending(a => a.Money).ToList() : _rank.OrderBy(a => a.Money).ToList();
    }
}

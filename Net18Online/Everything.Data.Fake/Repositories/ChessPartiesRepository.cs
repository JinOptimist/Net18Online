using Everything.Data.Fake.Models;
using Everything.Data.Interface.Models;
using Everything.Data.Interface.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Everything.Data.Fake.Repositories
{
    public class ChessPartiesRepository: IChessPartiesRepository
    {
        private static List<IPartiesData> parties = new List<IPartiesData>();

        public void Add(IPartiesData party)
        {
            party.Id = 1;
            parties.Add(party);
        }

        public void Delete(IPartiesData party)
        {
            parties.Remove(party);
        }

        List<IPartiesData> IChessPartiesRepository.GetAll()
        {
            return parties;
        }

        IPartiesData? IChessPartiesRepository.Get(int id)
        {
            return parties.FirstOrDefault(x => x.Id == id);
        }
    }
}

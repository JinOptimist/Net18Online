﻿using Everything.Data.Interface.Models;
using Everything.Data.Interface.Repositories;

namespace Everything.Data.Fake.Repositories
{
    public class CakeRepository : BaseRepository<ICakeData>, ICakeRepository
    {
    }
}

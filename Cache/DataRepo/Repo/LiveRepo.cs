using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataRepo.Interface;

namespace DataRepo.Repo
{
    public class LiveRepo<Dto> : ILiveRepo<Dto> where Dto : LibFYP.Dto
    {
        public async Task<IEnumerable<Dto>> GetAll()
        {

        }

        public async Task<IEnumerable<Dto>> FindBy()
        {

        }

        public async Task<Dto> FindById(int Id)
        {

        }
    }
}
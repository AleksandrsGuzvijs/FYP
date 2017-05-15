using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibFYP;

namespace DataRepo.Interfaces
{
    public interface ILiveRepo<T> : IGenericRepo<T> where T : Dto
    {
    }
}

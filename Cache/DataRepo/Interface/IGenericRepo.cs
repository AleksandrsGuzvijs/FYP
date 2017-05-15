using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibFYP;

namespace DataRepo.Interface
{
    public interface IGenericRepo<T> where T : Dto
    {
        Task<IEnumerable<T>> GetAll();

        Task<IEnumerable<T>> FindBy();

        Task<T> FindById(int Id);
    }
}

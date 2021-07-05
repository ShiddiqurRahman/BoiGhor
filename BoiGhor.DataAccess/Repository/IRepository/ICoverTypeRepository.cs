using BoiGhor.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoiGhor.DataAccess.Repository.IRepository
{
    public interface ICoverTypeRepository: IRepository<CoverType>
    {
        void Update(CoverType coverType);
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace BoiGhor.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork:IDisposable
    {
        ICategoryRepository Category { get; }
        ISP_Call SP_Call { get; }

        ICoverTypeRepository CoverType { get; }

        void Save();
    }
}

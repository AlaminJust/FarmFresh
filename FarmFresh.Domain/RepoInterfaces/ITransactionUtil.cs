﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmFresh.Domain.RepoInterfaces
{
    public interface ITransactionUtil : IDisposable
    {
        Task BeginAsync();
        Task CommitAsync();
        Task RollBackAsync();
    }
}

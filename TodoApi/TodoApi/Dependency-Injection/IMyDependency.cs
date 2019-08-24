using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Dependency_Injection
{
    public interface IMyDependency
    {
        Task WriteMessage(string message);
    }
}

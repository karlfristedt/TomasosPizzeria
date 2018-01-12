using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TomasosPizzeria.Models
{
    public interface IMatrattRepository
    {
        IQueryable<Matratt> Matratter { get; }
    }
}

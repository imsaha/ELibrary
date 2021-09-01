using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.Domain.Models
{
    public abstract class BaseModel<T> where T : IEquatable<T>
    {
        public T Id { get; set; }
    }

    public abstract class BaseModel : BaseModel<long>
    {

    }

    public interface IModel<T> where T : IEquatable<T>
    {
        T Id { get; set; }
    }

}

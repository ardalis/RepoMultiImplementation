using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoMultiImplementation.Models
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
    }
    public class Customer : BaseEntity
    {
        public string Name { get; set; }
    }

    public class Product : BaseEntity
    {
        public string Name { get; set; }
    }
}

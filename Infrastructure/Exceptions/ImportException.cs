using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyCsvParser.Mapping;

namespace Infrastructure.Exceptions
{
    public class ImportException : Exception
    {
        public readonly IEnumerable<string> errors;

        public ImportException(IEnumerable<string> errors)
        {
            this.errors = errors;
        }
    }
}

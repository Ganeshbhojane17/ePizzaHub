using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzahub.Applicationn.Exceptions
{
    public class InvalidCredentialsExceptions(string message) : Exception(message)
    {

    }
}

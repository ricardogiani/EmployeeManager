using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManager.Domain.Exceptions
{
    public class BusinessRuleValidationException : Exception
    {
        public IEnumerable<string> Errors { get; } // Lista de mensagens de erro

        // Construtor para uma única mensagem de erro
        public BusinessRuleValidationException(string message)
            : base(message)
        {
            Errors = new List<string> { message };
        }

        // Construtor para múltiplas mensagens de erro (útil para validar vários campos de uma vez)
        public BusinessRuleValidationException(IEnumerable<string> errors)
            : base("Uma ou mais regras de negócio foram violadas.")
        {
            if (errors == null || !errors.Any())
            {
                throw new ArgumentException("A lista de erros não pode ser nula ou vazia.", nameof(errors));
            }
            Errors = errors.ToList();
        }

        // Construtor para serialização (padrão para exceções)
        protected BusinessRuleValidationException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
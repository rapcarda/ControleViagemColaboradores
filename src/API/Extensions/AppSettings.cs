using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Extensions
{
    public class AppSettings
    {
        /* Chave de criptografia do token */
        public string Secret { get; set; }

        /* Por quantas horas o token será válido */
        public int ExpiracaoHoras { get; set; }

        /*Quem emite, no caso, a aplicação */
        public string Emissor { get; set; }

        /* Quais URL´s o token é válido */
        public string ValidoEm { get; set; }

    }
}

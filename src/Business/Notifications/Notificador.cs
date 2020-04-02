using Business.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Business.Notifications
{
    public class Notificador : INotificador
    {
        private List<Notificacao> _notificacao;

        public Notificador()
        {
            _notificacao = new List<Notificacao>();
        }

        public bool TemNotificacao()
        {
            return _notificacao.Any();
        }

        public List<Notificacao> ObterNotificacoes()
        {
            return _notificacao;
        }

        public void Handle(Notificacao notificacao)
        {
            _notificacao.Add(notificacao);
        }
    }
}

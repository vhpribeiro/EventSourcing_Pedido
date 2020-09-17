using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace EventSourcing_Pedido.Dominio._Helper
{
     public class ExcecaoDeDominio : Exception
    {
        private readonly Expression<Func<object, object>> _objeto = x => x;
        protected readonly IList<ViolacaoDeRegra> _erros = new List<ViolacaoDeRegra>();
        public IEnumerable<ViolacaoDeRegra> Erros => _erros;

        internal void AdicionarErroAoModelo(string mensagem)
        {
            _erros.Add(new ViolacaoDeRegra { Propriedade = _objeto, Mensagem = mensagem });
        }

        internal void AdicionarErrosAoModelo(IEnumerable<string> erros)
        {
            foreach (var erro in erros)
                AdicionarErroAoModelo(erro);
        }

        public string MensagensDeErroEmTexto()
        {
            var stringBuilder = new StringBuilder();
            foreach (var erro in Erros)
            {
                stringBuilder.Append(erro.Mensagem);
                stringBuilder.Append("\n");
            }
            return stringBuilder.Length > 0 ? stringBuilder.Remove(stringBuilder.Length - 1, 1).ToString() : stringBuilder.ToString();
        }

        public override string Message => ToString();

        public IEnumerable<string> Mensagens()
        {
            return Erros.Select(erro => erro.Mensagem);
        }

        public bool PossuiErroComAMensagemIgualA(string mensagem)
        {
            return Erros.Any(e => e.Mensagem.Equals(mensagem));
        }

        public override string ToString()
        {
            var texto = new StringBuilder();
            foreach (var erro in _erros)
                texto.AppendLine(erro.Mensagem);

            return texto.ToString();
        }
    }

    public class ExcecaoDeDominio<TModelo> : ExcecaoDeDominio
    {
        internal void AdicionarErroPara<TPropriedade>(Expression<Func<TModelo, TPropriedade>> propriedade, string mensagem)
        {
            _erros.Add(new ViolacaoDeRegra { Propriedade = propriedade, Mensagem = mensagem });
        }

        public bool PossuiErroComAMensagemIgualA(string mensagem)
        {
            return Erros.Any(e => e.Mensagem.Equals(mensagem));
        }
    }

    public static class ExtensoesDeRegrasException
    {
        public static void DispararExcecaoComMensagem(this ExcecaoDeDominio excecaoDeDominio, string mensagem)
        {
            excecaoDeDominio.AdicionarErroAoModelo(mensagem);
            throw excecaoDeDominio;
        }

        public static void DispararExcecaoComMensagens(this ExcecaoDeDominio excecaoDeDominio, IEnumerable<string> mensagens)
        {
            excecaoDeDominio.AdicionarErrosAoModelo(mensagens);
            throw excecaoDeDominio;
        }
    }
}
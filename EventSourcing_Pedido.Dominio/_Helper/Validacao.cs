using System;
using System.Collections.Generic;
using System.Linq;

namespace EventSourcing_Pedido.Dominio._Helper
{
   internal static class Validador
    {
        public static bool EstaInvalido(int valor)
        {
            return valor <= 0;
        }
        
        public static bool EstaInvalido(decimal valor)
        {
            return valor <= 0;
        }

        public static bool EstaInvalido(string valor)
        {
            return string.IsNullOrWhiteSpace(valor);
        }

        public static bool EstaInvalido<T>(T entidade)
        {
            return null == entidade;
        }
    }

    public class Validacao<T> where T : class
    {
        public static void EhObrigatorio(string valor, string mensagem)
        {
            if (string.IsNullOrWhiteSpace(valor))
                DispararComMensagem(mensagem);
        }

        public static void EhObrigatorio(int valor, string mensagem)
        {
            if (valor <= 0)
                DispararComMensagem(mensagem);
        }

        public static void EhObrigatorio(decimal valor, string mensagem)
        {
            if (valor <= 0)
                DispararComMensagem(mensagem);
        }

        public static void EhObrigatorio(DateTime data, string mensagem)
        {
            if (!data.EhDataValida())
                DispararComMensagem(mensagem);
        }

        public static void EhObrigatorio(DateTime? data, string mensagem)
        {
            if (!data.HasValue)
                DispararComMensagem(mensagem);

            EhObrigatorio(data.Value, mensagem);
        }

        public static void EhObrigatorio(object[] colecao, string mensagem)
        {
            if (colecao == null || !colecao.Any())
                DispararComMensagem(mensagem);
        }

        public static void EhObrigatorio<TZ>(TZ entidade, string mensagem)
        {
            if (null == entidade)
                DispararComMensagem(mensagem);
        }

        public static void Quando(bool condicao, string mensagem)
        {
            if (condicao)
                DispararComMensagem(mensagem);
        }

        public static void DispararComMensagem(string mensagem)
        {
            new ExcecaoDeDominio<T>().DispararExcecaoComMensagem(mensagem);
        }
    }

    public class Validacoes<T> where T : class
    {
        private readonly List<string> _erros;

        private Validacoes()
        {
            _erros = new List<string>();
        }

        public static Validacoes<T> Criar()
        {
            return new Validacoes<T>();
        }

        public Validacoes<T> Quando(bool condicao, string mensagem)
        {
            if (condicao)
                _erros.Add(mensagem);

            return this;
        }

        public Validacoes<T> Obrigando(int valor, string mensagem)
        {
            if (Validador.EstaInvalido(valor))
                _erros.Add(mensagem);

            return this;
        }
        
        public Validacoes<T> Obrigando(decimal valor, string mensagem)
        {
            if (Validador.EstaInvalido(valor))
                _erros.Add(mensagem);

            return this;
        }

        public Validacoes<T> Obrigando(string valor, string mensagem)
        {
            if (Validador.EstaInvalido(valor))
                _erros.Add(mensagem);

            return this;
        }

        public Validacoes<T> Obrigando(DateTime? data, string mensagem)
        {
            if (!data.HasValue || !data.Value.EhDataValida())
                _erros.Add(mensagem);

            return this;
        }

        public Validacoes<T> Obrigando<TZ>(TZ entidade, string mensagem)
        {
            if (Validador.EstaInvalido(entidade))
                _erros.Add(mensagem);

            return this;
        }

        public void DispararSeHouverErros()
        {
            if (_erros.Any())
                new ExcecaoDeDominio<T>().DispararExcecaoComMensagens(_erros);
        }
    }
}
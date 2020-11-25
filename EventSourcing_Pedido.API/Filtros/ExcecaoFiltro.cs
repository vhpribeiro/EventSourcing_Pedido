using EventSourcing_Pedido.Dominio._Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EventSourcing_Pedido.API.Filtros
{
    public class ExcecaoFiltro : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var status = context.Exception is ExcecaoDeDominio ? 400 : 500;
            var mensagem = status == 400 ? context.Exception.Message : context.Exception.Message;
            var response = context.HttpContext.Response;

            response.StatusCode = (int)status;
            response.ContentType = "application/json";
            context.Result = new JsonResult(mensagem);
        }
    }
}
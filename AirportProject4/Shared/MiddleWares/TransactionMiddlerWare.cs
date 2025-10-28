using AirportProject4.Project.core;

namespace AirportProject4.Shared.MiddleWares
{
    public class TransactionMiddlerWare : IMiddleware
    {
        private readonly IunitofWork _iunitofWork;

        public TransactionMiddlerWare(IunitofWork iunitofWork)
        {
            _iunitofWork = iunitofWork;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {

          
            if (context.Request.Method == HttpMethods.Get)
            {
               
               await next(context);
                    
            }
            else
            {
               await _iunitofWork.BeginTransactionAsync();
               await next(context);
                await _iunitofWork.CommitTransactionAsync();
            }

            }
            catch (Exception)
            {
               await _iunitofWork.RollbackTransactionAsync();
                throw;
            }
        }
    }
}

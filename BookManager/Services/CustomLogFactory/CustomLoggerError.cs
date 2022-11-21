using BookManager.Data;
using BookManager.Repository;
using BookManager.Services.CustomLogFactory.Interface;

namespace BookManager.Services.CustomLogFactory
{
    public class CustomLoggerError : ICustomLogger
    {
        public void logger(string pOperation, string pTrace)
        {
            //Classe e metodo desacoplado
        }
    }
}
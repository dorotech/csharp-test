
using BookManager.Services.CustomLog;

namespace BookManager.Services.CustomLogFactory
{
    public class CustomLogger
    {
        public void logger(string pOperation, string pTrace, int tipo)
        {
            var log = new Factory().FactoryMethod(tipo);
            log.logger(pOperation, pTrace);
        }
    }
}
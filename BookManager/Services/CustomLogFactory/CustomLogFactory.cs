
using BookManager.Services.CustomLogFactory;
using BookManager.Services.CustomLogFactory.Interface;

namespace BookManager.Services.CustomLog
{
    public class Factory
    {
        public ICustomLogger FactoryMethod(int tipo)
        {

            switch (tipo)
            {
                case 1:
                    return new CustomLoggerError();
                //break;
                case 2:
                    return new CustomLoggerSuccess();
                //break;
                default:
                    return new CustomLoggerError();
                    //break;
            }
        }
    }
}
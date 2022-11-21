using BookManager.Model;
using BookManager.Repository;

namespace BookManager.Services
{
    public static class CustonLogServices
    {

        //private  readonly CostomLogRepository repository = new CostomLogRepository();

        public static void Logger(string pOperation, string pTrace)
        {
            var log = new CustomLog()
            {
                operation = pOperation,
                trace = pTrace,
                createAt = DateTime.Now
            };
            //  await repository.Add(log);
            //await repository.SaveChangesAsync();

        }

    }
}
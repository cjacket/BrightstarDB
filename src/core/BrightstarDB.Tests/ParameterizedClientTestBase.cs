using BrightstarDB.Client;

namespace BrightstarDB.Tests
{
    public class ParameterizedClientTestBase : ClientTestBase
    {
        protected readonly string ConnectionString;
        protected readonly string ServiceDirectoryPath;

        public ParameterizedClientTestBase(string connectionString, string serviceDirectoryPath)
        {
            ConnectionString = connectionString;
            ServiceDirectoryPath = serviceDirectoryPath;
        }

        protected IBrightstarService GetClient()
        {
            return BrightstarService.GetClient(ConnectionString);
        }

        public virtual void SetUp()
        {
            if (ConnectionString.Contains("type=rest"))
            {
                StartService();
            }
        }

        public virtual void TearDown()
        {
            if (ConnectionString.Contains("type=rest"))
            {
                CloseService();
            }
        }
    }
}
namespace UsingDapper.Services
{
    public class SqlConnectionFactory
    {
        private readonly string connecionstring;

        public SqlConnectionFactory(string connecionstring)
        {
            this.connecionstring = connecionstring;
        }

        public SqlConnectionFactory Create()
        {
            return new SqlConnectionFactory(connecionstring);
        }
    }
}

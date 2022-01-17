using System;

namespace MiniORM
{
    internal class ConnectionMenager : IDisposable
    {
        private readonly DatabaseConnection connection;

        public ConnectionMenager(DatabaseConnection connection)
        {
            this.connection = connection;

            this.connection.Open();
        }

        public void Dispose()
        {
            this.connection.Close();
        }
    }
}
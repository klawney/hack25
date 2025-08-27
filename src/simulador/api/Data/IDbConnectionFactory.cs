// Core/Interfaces/IDbConnectionFactory.cs

using System.Data;

namespace Core.Interfaces
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
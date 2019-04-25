using DL.DataAccess.Abstract;
using System;
using System.Collections.Generic;
using DL.Models;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using Dapper;

namespace DL.DataAccess
{
    public class ReceiversRepository : IRepository<Receiver>
    {
        private DbConnection _connection;

        public ReceiversRepository()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["AppConnection"].ConnectionString;

            _connection = new SqlConnection(connectionString);
        }

        public void Add(Receiver item)
        {
            var sqlQuery = "insert into Receivers (Id, CreationDate, DeletedDate, FullName, Address) " +
                            "values(@Id, @CreationDate, @DeletedDate, @FullName, @Address)";

            var result = _connection.Execute(sqlQuery, item);
            if (result < 1) throw new Exception("Запись не вставлена!");

        }

        public void Delete(Guid id)
        {
            var sqlQuery = $"update Receivers set DeletedDate = GETDATE() where id like @Id";

            var result = _connection.Execute(sqlQuery,new { Id = id });
        }

        public void Dispose()
        {
            _connection.Close();
        }

        public ICollection<Receiver> GetAll()
        {
            var sqlQuery = "select * from receivers";
            return _connection.Query<Receiver>(sqlQuery).AsList();
        }

        public void Update(Receiver item)
        {
            var sqlQuery = "update Receiver " +
                "set  CreationDate=@CreationDate FullName = @FullName Address = @Address where Id = @Id";
            var result = _connection.Execute(sqlQuery,item);
        }

        public List<Receiver> GetReceiver(string fullName)
        {
            var sqlQuery = "select * from receivers where FullName like @FullName";
            return _connection.Query<Receiver>(sqlQuery,new { FullName = fullName}).AsList();
        }
    }
}

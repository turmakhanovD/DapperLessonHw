using DL.DataAccess.Abstract;
using DL.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using Dapper;

namespace DL.DataAccess
{
    public class MailRepository : IRepository<Mail>
    {
        private DbConnection _connection;

        public MailRepository()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["AppConnection"].ConnectionString;

            _connection = new SqlConnection(connectionString);
        }

        public void Add(Mail item)
        {
            var sqlQuery = "insert into Mails (Id, CreationDate, DeletedDate, Theme, Text, ReceiverId) " +
                            "values(@Id, @CreationDate, @DeletedDate, @Theme, @Text, @ReceiverId) ";

            var result = _connection.Execute(sqlQuery, item);
            if (result < 1) throw new Exception("Запись не вставлена!");
        }

        public void Delete(Guid id)
        {
            var sqlQuery = $"update Mails set DeletedDate = GETDATE() where id like @Id";

            var result = _connection.Execute(sqlQuery, new { Id = id });
        }

        public void Dispose()
        {
            _connection.Close();
        }

        public ICollection<Mail> GetAll()
        {
            var sqlQuery = "select * from mails";
            return _connection.Query<Mail>(sqlQuery).AsList();
        }

        public void Update(Mail item)
        {
            var sqlQuery = "update Mails " +
               "set  CreationDate=@CreationDate Theme = @Theme Text = @Text where Id = @Id";
            var result = _connection.Execute(sqlQuery, item);
        }


    }
}

using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using NodeServiceAttrTest.Contracts;
using NodeServiceAttrTest.Contracts.Repositories;
using NodeServiceAttrTest.Models;

namespace NodeServiceAttrTest.Data.DpRepositories
{
    public class DpNodeRepository : DpGenericRepository<Node>, INodeRepository
    {
        private string _connectionString;
        private IConfigurationService _configurationService;
        public DpNodeRepository(IConfigurationService configurationService) : base(configurationService.ConnectionString)
        {
            _configurationService = configurationService;
            _connectionString = configurationService.ConnectionString;
        }

        public new IQueryable<Node> GetAll()
        {
            IQueryable<Node> entries;
            var t = typeof(Node);
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                entries = db.Query<Node>("SELECT * FROM UINode").AsQueryable();
            }
            return entries;
        }

        public new Node Create(Node entity)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                    var sqlQuery = "INSERT INTO UINode (Name, ParentId) VALUES(@Name, @ParentId); SELECT CAST(SCOPE_IDENTITY() as int)";
                    int? id = db.Query<int>(sqlQuery, entity).FirstOrDefault();
                    entity.Id = (int)id;
            }

            return entity;
        }

        public new void CreateRange(IEnumerable<Node> entities)
        {
            if (!entities.Any())
                return;
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = "INSERT INTO UINode (Name, ParentId) VALUES(@Name, @ParentId); ";
                foreach (var entity in entities)
                {
                    sqlQuery += "";
                }
                sqlQuery = "INSERT INTO UINode (Name, ParentId) VALUES(@Name, @ParentId); ";
                int? id = db.Query<int>(sqlQuery).FirstOrDefault();
            }
        }

        public new Node Update(Node entity)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var sqlQuery = "UPDATE Users SET Name = @Name, Age = @Age WHERE Id = @Id";
                db.Execute(sqlQuery, entity);
            }

            return entity;
        }

        public new Node Delete(Node entity)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var sqlQuery = "DELETE FROM Users WHERE Id = @id";
                db.Execute(sqlQuery, new { entity.Id });
            }

            return entity;
        }

    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using Dapper;
using NodeServiceAttrTest.Contracts;
using NodeServiceAttrTest.Contracts.Repositories;
using NodeServiceAttrTest.Models;
using NodeServiceAttrTest.Models.ViewModels;
using Attribute = NodeServiceAttrTest.Models.Attribute;

namespace NodeServiceAttrTest.Data.DpRepositories
{
    public class DpServiceRepository : DpGenericRepository<Service>, IDapperServiceRepository
    {
        private string _connectionString;
        public DpServiceRepository(IConfigurationService configurationService) : base(configurationService.ConnectionString)
        {
            _connectionString = configurationService.ConnectionString;
        }

        public IQueryable<Services> GetServicesTop(int top)
        {
            return GetAll(top);
        }

        public IQueryable<Services> GetServices()
        {
            return GetAll(0);
        }

        private IQueryable<Services> GetAll(int top)
        {
            var tops = top > 0 ? $" TOP({top}) " : " ";
            var sql = @"select distinct s.Id as Id, s.Name as Name
                , n.id as NodeId, n.Name as NodeName, n.ParentId as ParentId, a.Id as AttributeId, a.Name as AttributeName
            FROM (select " + tops + @" * from [dbo].[Service]  where Id in (select distinct ServiceId from [dbo].[ServiceNodes] ) 
            and Id in (select distinct ServiceId from [dbo].[Attribute])) as s INNER JOIN[dbo].[ServiceNodes] as sn 
            ON sn.ServiceId = s.Id INNER JOIN[dbo].[UINode] as n ON  sn.NodeId = n.Id
            INNER JOIN[dbo].[Attribute] as a ON a.ServiceId = s.Id";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                Mapping();
                var services = db.Query<Services, Node, Attribute, Services>(sql, map: (s, n, a) =>
                        {
                            s.Attributes = s.Attributes ?? new List<Attribute>();
                            s.Nodes = s.Nodes ?? new List<Node>();
                            s.Attributes.Add(a);
                            s.Nodes.Add(n);
                            return s;
                        },
                        splitOn: "NodeId,AttributeId"
                    ).GroupBy(s => s.Id)
                    .Select(group =>
                    {
                        var combinedService = group.First();
                        combinedService.Nodes = group.Select(service => service.Nodes.FirstOrDefault()).GroupBy(node => node?.Id).Select(ng => ng?.FirstOrDefault()).ToList();
                        combinedService.Attributes = group.Select(service => service.Attributes.FirstOrDefault()).GroupBy(at => at?.Id).Select(ag => ag?.FirstOrDefault()).ToList();
                        return combinedService;
                    }).AsQueryable();
                return services;
            }
        }


        void Mapping()
        {

            Dictionary<string, string> columnMaps = new Dictionary<string, string>
            {
                { "Id", "Id" },
                { "Name", "Name" },
                { "NodeId", "Id" },
                { "NodeName", "Name" },
                { "ParentId", "ParentId" },
                { "AttributeId", "Id" },
                { "AttributeName", "Name" },
            };

            Dictionary<string, string> nodeColumnMaps = new Dictionary<string, string>
            {
                { "NodeId", "Id" },
                { "NodeName", "Name" },
                { "ParentId", "ParentId" },
            };

            var mapper = new Func<Type, string, PropertyInfo>((type, columnName) =>
            {
                if (columnMaps.ContainsKey(columnName))
                    return type.GetProperty(columnMaps[columnName]);
                else
                    return type.GetProperty(columnName);
            });
            var mapper2 = new Func<Type, string, PropertyInfo>((type, columnName) =>
            {
                if (nodeColumnMaps.ContainsKey(columnName))
                    return type.GetProperty(nodeColumnMaps[columnName]);
                else
                    return type.GetProperty(columnName);
            });



            var customMap = new CustomPropertyTypeMap(
                typeof(CustomDataSet),
                (type, columnName) => mapper(type, columnName));

            var serviceMap = new CustomPropertyTypeMap(
                typeof(Service),
                (type, columnName) => mapper(type, columnName));

            var nodeMap = new CustomPropertyTypeMap(
                typeof(Node),
                (type, columnName) => mapper2(type, columnName));
            
            var attrMap = new CustomPropertyTypeMap(
                typeof(Attribute),
                (type, columnName) => mapper(type, columnName));

            SqlMapper.SetTypeMap(typeof(CustomDataSet), customMap);
            SqlMapper.SetTypeMap(typeof(Service), serviceMap);
            SqlMapper.SetTypeMap(typeof(Node), nodeMap);
            SqlMapper.SetTypeMap(typeof(Models.Attribute), attrMap);

        }
    }
}
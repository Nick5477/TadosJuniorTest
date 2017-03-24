using System;
using System.Collections.Generic;
using System.Web.Http;
using Domain.Entities;
using Domain.Queries.Criteria;
using Infrastructure.Db.Commands;
using Infrastructure.Db.Queries;

namespace WebApp.Controllers
{
    public class ClientController:ApiController
    {
        public IQueryBuilder queryBuilder {get; set; }
        public ICommandBuilder commandBuilder { get; set; }
        public ClientController()
        {
            
        }
        [HttpGet]
        public IEnumerable<Client> List()
        {
            throw new NotImplementedException();
        }
        [HttpGet]
        public Client Get(int id)
        {
            return queryBuilder
                .For<Client>()
                .With(new GetClientByIdCriterion()
                {
                    Id = id
                });
        }
    }
}
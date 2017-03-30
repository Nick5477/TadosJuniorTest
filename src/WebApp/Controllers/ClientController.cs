using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Domain.Commands.Contexts;
using Domain.Entities;
using Domain.Queries.Criteria;
using Infrastructure.Db.Commands;
using Infrastructure.Db.Queries;
using Newtonsoft.Json.Linq;
using WebApp.Structures;

namespace WebApp.Controllers
{
    public class ClientController:ApiController
    {
        public IQueryBuilder queryBuilder {get; set; }
        public ICommandBuilder commandBuilder { get; set; }

        [HttpGet]//get
        public HttpResponseMessage List(JObject jsonData)
        {
            HttpResponseMessage response;
            GetClientsCriterion criterion = jsonData.ToObject<GetClientsCriterion>();
            try
            {
                IEnumerable<Client> clients =
                    queryBuilder
                        .For<IEnumerable<Client>>()
                        .With(criterion);
                if (!clients.Any())
                    response = Request.CreateResponse(HttpStatusCode.BadRequest);
                else
                    response = Request.CreateResponse(HttpStatusCode.OK, clients);
            }
            catch (Exception ex)
            {
                ErrorObject err = new ErrorObject(ex.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, err);
            }
            return response;
        }

        [HttpGet]
        public HttpResponseMessage Get(int id)
        {
            HttpResponseMessage response;
            try
            {
                Client client = queryBuilder
                    .For<Client>()
                    .With(new GetClientByIdCriterion()
                    {
                        Id = id
                    });
                if (client == null)
                    response = Request.CreateResponse(HttpStatusCode.BadRequest);
                else
                    response = Request.CreateResponse(HttpStatusCode.OK, client);
            }
            catch (Exception ex)
            {
                ErrorObject err = new ErrorObject(ex.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, err);
            }
            return response;

        }

        [HttpPost]
        public HttpResponseMessage Add(JObject jsonData)
        {
            HttpResponseMessage response=new HttpResponseMessage(HttpStatusCode.OK);
            var context=jsonData.ToObject<AddClientCommandContext>();
            context.DatabasePath = HomeController.DatabasePath;
            try
            {
                commandBuilder.Execute(context);
            }
            catch (Exception ex)
            {
                ErrorObject err = new ErrorObject(ex.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, err);
            }
            return response;
        }

        [HttpPut]
        public HttpResponseMessage ChangeName(int id,JObject jsonData)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            ChangeClientNameCommandContext context =
                jsonData.ToObject<ChangeClientNameCommandContext>();
            context.Id = id;
            context.DatabasePath = HomeController.DatabasePath;
            try
            {
                commandBuilder.Execute(context);
            }
            catch (Exception ex)
            {
                ErrorObject err = new ErrorObject(ex.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, err);
            }
            return response;
        }
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            DeleteClientCommandContext context = new DeleteClientCommandContext()
            {
                Id = id,
                DatabasePath = HomeController.DatabasePath
            };
            try
            {
                commandBuilder
                    .Execute(context);
                if (!context.IsSuccess)
                    response = Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch(Exception ex)
            {
                ErrorObject err = new ErrorObject(ex.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, err);
            }
            return response;
        }

        [HttpGet]//get
        [Route("client/{id}/bills")]
        public HttpResponseMessage Bills(int id,JObject jsonData)
        {
            HttpResponseMessage response;
            
            GetClientBillsCriterion criterion = 
                jsonData
                .ToObject<GetClientBillsCriterion>();
            criterion.Id = id;
            try
            {
                IEnumerable<Bill> bills = queryBuilder
                    .For<IEnumerable<Bill>>()
                    .With(criterion);
                if (!bills.Any())
                    response = Request.CreateResponse(HttpStatusCode.BadRequest);
                else
                    response = Request.CreateResponse(HttpStatusCode.OK, bills);
            }
            catch (Exception ex)
            {
                ErrorObject err = new ErrorObject(ex.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, err);
            }
            return response;
        }
    }
}
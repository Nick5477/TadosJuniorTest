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
    public class BillController:ApiController
    {
        public IQueryBuilder queryBuilder { get; set; }
        public ICommandBuilder commandBuilder { get; set; }
        [HttpPost]
        public HttpResponseMessage Add(JObject jsonData)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            AddBillCommandContext context = jsonData.ToObject<AddBillCommandContext>();
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
        public HttpResponseMessage Pay(int id)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            PayBillCommandContext context = new PayBillCommandContext()
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
            catch (Exception ex)
            {
                ErrorObject err = new ErrorObject(ex.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, err);
            }
            return response;
        }
        [HttpGet]//get
        public HttpResponseMessage List(JObject jsonData)
        {
            HttpResponseMessage response;
            GetBillsCriterion criterion = jsonData.ToObject<GetBillsCriterion>();
            try
            {
                IEnumerable<Bill> bills =
                    queryBuilder
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
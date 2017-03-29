using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Domain.Queries.Criteria;
using Domain.Structures;
using Infrastructure.Db.Queries;
using Newtonsoft.Json.Linq;
using WebApp.Structures;

namespace WebApp.Controllers
{
    public class StatsController:ApiController
    {
        public IQueryBuilder queryBuilder { get; set; }
        [HttpGet]
        public HttpResponseMessage PayedBillsSum(JObject jsonData)
        {
            HttpResponseMessage response;
            GetPayedBillsSumCriterion criterion =
                jsonData
                    .ToObject<GetPayedBillsSumCriterion>();
            try
            {
                IEnumerable<ClientPayedBillsSum> clientBillsSums =
                    queryBuilder
                        .For<IEnumerable<ClientPayedBillsSum>>()
                        .With(criterion);
                if (clientBillsSums == null)
                    response = Request.CreateResponse(HttpStatusCode.BadRequest);
                else
                    response = Request.CreateResponse(HttpStatusCode.OK, clientBillsSums);
            }
            catch (Exception ex)
            {
                ErrorObject err = new ErrorObject(ex.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, err);
            }
            return response;
        }

        [HttpGet]
        public HttpResponseMessage Bills()
        {
            HttpResponseMessage response;
            try
            {
                BillsStat billsStat =
                    queryBuilder
                        .For<BillsStat>()
                        .With(new EmptyCriterion());
                response = Request.CreateResponse(HttpStatusCode.OK, billsStat);
            }
            catch (Exception ex)
            {
                ErrorObject err = new ErrorObject(ex.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, err);
            }
            return response;
        }
        [HttpGet]
        public HttpResponseMessage GetClientBillsStat(int id,JObject jsonData)
        {
            HttpResponseMessage response;
            GetClientBillsStatCriterion criterion =
                jsonData.ToObject<GetClientBillsStatCriterion>();
            try
            {
                BillsStat clientBillsStat =
                    queryBuilder
                        .For<BillsStat>()
                        .With(criterion);
                response = Request.CreateResponse(HttpStatusCode.OK, clientBillsStat);
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
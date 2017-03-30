using System;
using System.Collections.Generic;
using System.Linq;
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

        [HttpGet]//get
        public HttpResponseMessage PayedBillsSum(JObject jsonData)
        {
            HttpResponseMessage response;
            string s = jsonData.ToString().Replace(" ","");
            GetPayedBillsSumCriterion criterion =
                jsonData
                    .ToObject<GetPayedBillsSumCriterion>();
            if (criterion.StartDateTime == null)
                criterion.StartDateTime = "";
            else
                criterion.StartDateTime = s.Substring(s.IndexOf("StartDateTime") + "StartDateTime".Length + 3, 19);
            if (criterion.EndDateTime == null)
                criterion.EndDateTime = "";
            else
                criterion.EndDateTime = s.Substring(s.IndexOf("EndDateTime") + "EndDateTime".Length + 3, 19);
            try
            {
                IEnumerable<ClientPayedBillsSum> clientBillsSums =
                    queryBuilder
                        .For<IEnumerable<ClientPayedBillsSum>>()
                        .With(criterion);
                if (!clientBillsSums.Any())
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

        [HttpGet]//get
        [Route("stats/client/{id}/bills")]
        public HttpResponseMessage GetClientBillsStat(int id,JObject jsonData)
        {
            HttpResponseMessage response;
            string s = jsonData.ToString().Replace(" ","");
            GetClientBillsStatCriterion criterion =
                jsonData.ToObject<GetClientBillsStatCriterion>();
            criterion.ClientId = id;
            if (criterion.StartDateTime == null)
                criterion.StartDateTime = "";
            else
                criterion.StartDateTime = s.Substring(s.IndexOf("StartDateTime") + "StartDateTime".Length + 3, 19);
            if (criterion.EndDateTime == null)
                criterion.EndDateTime = "";
            else
                criterion.EndDateTime = s.Substring(s.IndexOf("EndDateTime") + "EndDateTime".Length + 3, 19);
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
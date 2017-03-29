using System;
using Domain.Queries.Criteria;
using Domain.Services;
using Domain.Structures;
using Infrastructure.Db.Queries;

namespace Infrastructure.Db.Stats.Queries
{
    public class GetClientBillsStatQuery:IQuery<GetClientBillsStatCriterion,BillsStat>
    {
        private readonly IStatsService _statService;

        public GetClientBillsStatQuery(IStatsService statService)
        {
            if (statService==null)
                throw new ArgumentNullException(nameof(statService));
            _statService = statService;
        }
        public BillsStat Ask(GetClientBillsStatCriterion criterion)
        {
            BillsStat clientBillsStat=
                _statService
                .GetClientBillsStat(
                    criterion.ClientId,
                    criterion.StartDateTime,
                    criterion.EndDateTime);
            return clientBillsStat;
        }
    }
}

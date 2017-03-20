using System;
using Domain.Queries.Criteria;
using Domain.Services;
using Domain.Structures;
using Infrastructure.Db.Queries;

namespace Infrastructure.Db.Stat.Queries
{
    class GetClientBillsStatQuery:IQuery<GetClientBillsStatCriterion,ClientBillsStat>
    {
        private readonly IStatService _statService;

        public GetClientBillsStatQuery(IStatService statService)
        {
            if (statService==null)
                throw new ArgumentNullException(nameof(statService));
            _statService = statService;
        }
        public ClientBillsStat Ask(GetClientBillsStatCriterion criterion)
        {
            ClientBillsStat clientBillsStat=
                _statService
                .GetClientBillsStat(
                    criterion.ClientId,
                    criterion.StartDateTime,
                    criterion.EndDateTime);
            return clientBillsStat;
        }
    }
}

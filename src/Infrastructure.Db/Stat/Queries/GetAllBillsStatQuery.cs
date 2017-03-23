using System;
using Domain.Queries.Criteria;
using Domain.Services;
using Domain.Structures;
using Infrastructure.Db.Queries;

namespace Infrastructure.Db.Stat.Queries
{
    public class GetAllBillsStatQuery:IQuery<EmptyCriterion,BillsStat>
    {
        private readonly IStatService _statService;

        public GetAllBillsStatQuery(IStatService statService)
        {
            if (statService==null)
                throw new ArgumentNullException(nameof(statService));
            _statService = statService;
        }
        public BillsStat Ask(EmptyCriterion criterion)
        {
            BillsStat billsStat =
                _statService.GetAllBillsStat();
            return billsStat;
        }
    }
}

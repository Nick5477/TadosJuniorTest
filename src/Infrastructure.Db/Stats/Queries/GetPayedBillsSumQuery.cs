﻿using System;
using System.Collections.Generic;
using Domain.Queries.Criteria;
using Domain.Services;
using Domain.Structures;
using Infrastructure.Db.Queries;

namespace Infrastructure.Db.Stats.Queries
{
    public class GetPayedBillsSumQuery:IQuery<GetPayedBillsSumCriterion,IEnumerable<ClientPayedBillsSum>>
    {
        private readonly IStatsService _statService;

        public GetPayedBillsSumQuery(IStatsService statService)
        {
            if(statService==null)
                throw new ArgumentNullException(nameof(statService));
            _statService = statService;
        }
        public IEnumerable<ClientPayedBillsSum> Ask(GetPayedBillsSumCriterion criterion)
        {
            List<ClientPayedBillsSum> clientPayedBillsSums=
                _statService
                .GetPayedBillsSum
                (criterion.Count,
                criterion.StartDateTime,
                criterion.EndDateTime);
            return clientPayedBillsSums;
        }
    }
}

using System;
using System.Collections.Generic;
using Domain.Queries.Criteria;
using Domain.Services;
using Infrastructure.Db.Queries;

namespace Infrastructure.Db.Client.Queries
{
    public class GetClientsQuery:IQuery<GetClientsCriterion,IEnumerable<Domain.Entities.Client>>
    {
        private readonly IClientService _clientService;

        public GetClientsQuery(IClientService clientService)
        {
            if (clientService==null)
                throw new ArgumentNullException(nameof(clientService));
            _clientService = clientService;
        }

        public IEnumerable<Domain.Entities.Client> Ask(GetClientsCriterion criterion)
        {
            List <Domain.Entities.Client> clients=_clientService.GetClients(criterion.Offset, criterion.Count);
            return clients;
        }
    }
}

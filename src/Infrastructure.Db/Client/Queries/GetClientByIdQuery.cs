using System;
using Domain.Queries.Criteria;
using Domain.Services;
using Infrastructure.Db.Queries;

namespace Infrastructure.Db.Client.Queries
{
    class GetClientByIdQuery:IQuery<GetClientByIdCriterion,Domain.Entities.Client>
    {
        private readonly IClientService _clientService;

        public GetClientByIdQuery(IClientService clientService)
        {
            if (clientService==null)
                throw new ArgumentNullException(nameof(clientService));
            _clientService = clientService;
        }
        public Domain.Entities.Client Ask(GetClientByIdCriterion criterion)
        {
            Domain.Entities.Client client = _clientService.GetClientById(criterion.Id);
            return client;
        }
    }
}

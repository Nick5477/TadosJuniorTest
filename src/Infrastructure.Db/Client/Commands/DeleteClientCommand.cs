﻿using System;
using System.Data.SQLite;
using Domain.Commands.Contexts;
using Domain.Services;
using Infrastructure.Db.Commands;

namespace Infrastructure.Db.Client.Commands
{
    public class DeleteClientCommand:ICommand<DeleteClientCommandContext>
    {
        private readonly IClientService _clientService;

        public DeleteClientCommand(IClientService clientService)
        {
            if (clientService == null)
                throw new ArgumentNullException(nameof(clientService));
            _clientService = clientService;
        }
        public void Execute(DeleteClientCommandContext commandContext)
        {
            bool isSuccess=_clientService.DeleteClient(commandContext.Id);
            commandContext.IsSuccess = isSuccess;

            string databaseName = commandContext.DatabasePath;

            using (SQLiteConnection conn = new SQLiteConnection(string.Format(@"Data Source={0};", databaseName)))
            {
                conn.Open();
                SQLiteCommand command = new SQLiteCommand(string.Format(@"DELETE FROM Clients WHERE Id=@id"), conn);
                command.Parameters.AddWithValue("@id", commandContext.Id);
                command.ExecuteNonQuery();
            }
        }
    }
}

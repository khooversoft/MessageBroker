﻿// Copyright (c) KhooverSoft. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Khooversoft.Sql;
using MessageBroker.Common;
using Xunit;

[assembly: CollectionBehavior(CollectionBehavior.CollectionPerAssembly)]

namespace MessageBroker.QueueDbRepository.Test
{
    public static class TestAssembly
    {
        public static IMessageBrokerConfiguration Configuration { get; } = new MessageBrokerConfiguration
        {
            SqlConfiguration = new SqlConfiguration("Server=(local);Database=QueueDb_Test;Trusted_Connection=True;"),
        };

        public static IMessageBrokerAdministration Administration { get; } = new MessageBrokerAdministrationRepository(Configuration);

        public static IMessageBroker Message { get; } = new MessageBrokerRepository(Configuration);

        public static IMessageBrokerManagement Management { get; } = new MessageBrokerManagementRepository(Configuration);
    }
}

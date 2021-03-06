﻿// Copyright (c) KhooverSoft. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Autofac;
using Khooversoft.Net;
using MessageBroker.Common;
using MessageBroker.Common.RestApi;

namespace MessageBroker.Configuration
{
    public class MessageBrokerClientAutoFacModule : Module
    {
        private readonly MessageBrokerEnvironment _environment;

        public MessageBrokerClientAutoFacModule(MessageBrokerEnvironment environment)
        {
            _environment = environment;
        }

        protected override void Load(ContainerBuilder builder)
        {
            IMessageBrokerClientConfiguration environmentConfiguration = new ClientConfigurationManager().Get(_environment);

            builder.RegisterInstance(environmentConfiguration).As<IMessageBrokerClientConfiguration>();
            builder.RegisterInstance(new RestClientConfigurationBuilder().Build()).As<IRestClientConfiguration>();
            builder.RegisterType<MessageBrokerClient>().As<IMessageBrokerClient>();
        }
    }
}

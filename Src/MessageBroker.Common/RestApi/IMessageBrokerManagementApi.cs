﻿// Copyright (c) KhooverSoft. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Khooversoft.Net;
using Khooversoft.Toolbox;
using System.Net;
using System.Threading.Tasks;

namespace MessageBroker.Common.RestApi
{
    public interface IMessageBrokerManagementApi
    {
        Task<RestResponse<HealthCheckContractV1>> HealthCheck(IWorkContext context);

        Task<RestResponse> SetQueue(IWorkContext context, SetQueueContractV1 contract);

        Task<RestResponse> ClearQueue(IWorkContext context, string queueName, bool copyToHistory = false);

        Task<RestResponse> DeleteQueue(IWorkContext context, string queueName);

        Task<RestResponse> DisableQueue(IWorkContext context, string queueName);

        Task<RestResponse> EnableQueue(IWorkContext context, string queueName);

        Task<RestResponse<QueueDetailContractV1>> GetQueue(IWorkContext context, string queueName, HttpStatusCode[] acceptedCodes = null);

        Task<RestResponse<RestPageResultV1<QueueDetailContractV1>>> GetQueueList(IWorkContext context, bool disable = false);

        Task<RestResponse<RestPageResultV1<QueueStatusContractV1>>> GetQueueStatus(IWorkContext context);
    }
}

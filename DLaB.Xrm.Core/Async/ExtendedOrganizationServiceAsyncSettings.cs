﻿#if DLAB_ASYNC
using DLaB.Xrm;
using Microsoft.Extensions.Logging;

namespace Source.DLaB.Xrm
{
    public class ExtendedOrganizationServiceAsyncSettings : ExtendedOrganizationServiceSettings
    {
        public LogLevel LogLevel { get; set; } = LogLevel.Information;
    }
}
#endif
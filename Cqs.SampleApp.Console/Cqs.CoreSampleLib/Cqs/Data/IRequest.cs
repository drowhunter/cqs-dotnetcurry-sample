using System;

namespace Cqs.CoreSampleLib.Cqs.Data
{
    public interface IRequest
    {
        Guid CorrelationId { get; set; }
    }
}
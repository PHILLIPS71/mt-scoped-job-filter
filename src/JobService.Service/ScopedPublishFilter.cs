namespace JobService.Service;

using System.Threading.Tasks;
using MassTransit;

public class ScopedPublishFilter<TMessage> : IFilter<PublishContext<TMessage>>
    where TMessage : class
{
    readonly ScopedService _scoped;

    public ScopedPublishFilter(ScopedService scoped)
    {
        _scoped = scoped;
    }

    public Task Send(PublishContext<TMessage> context, IPipe<PublishContext<TMessage>> next)
    {
        _scoped.Id = NewId.NextSequentialGuid();
        return next.Send(context);
    }

    public void Probe(ProbeContext context)
    {
    }
}
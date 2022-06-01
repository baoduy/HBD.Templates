using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HBD.EfCore.Abstractions.Entities;
using HBD.EfCore.Abstractions.Events;
using HBD.EfCore.DataAuthorization;

namespace TEMP.Domains.Abstracts;

public abstract class EntityBase<TKey> : AuditEntity<TKey>, IEventEntity, IDataKeyEntity
{
    private readonly ICollection<IEventItem> _events = new Collection<IEventItem>();

    /// <inheritdoc />
    protected EntityBase(TKey id, string createdBy, DateTimeOffset? createdOn = null) : base(id,createdBy,createdOn) => SetCreatedBy(createdBy, createdOn);

    /// <inheritdoc />
    // protected EntityBase(TKey id) : base(id)
    // {
    // }

    /// <inheritdoc />
    protected EntityBase()
    {
    }

    public Guid? DataKey { get; private set; }
        
    public void AddEvent(IEventItem @event) => _events.Add(@event);

    public IEventItem[] GetEventsAndClear()
    {
        var list = _events.ToArray();
        _events.Clear();

        return list;
    }

    public void UpdateDataKey(Guid dataKey) => DataKey = dataKey;
        
    public override string ToString() => $"{GetType().Name} '{Id}'";
}
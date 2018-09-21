using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;

namespace Domain.Exceptions
{
    public class EntityNotFoundException : DomainException
    {
        public EntityNotFoundException(Type entitiyType, int entityId)
        : base($"{entitiyType.Name} not found with id {entityId}")
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace PostMortem.Domain
{
    public interface IAggregateId
    {
        string AsIdString();
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace PostMortem.Domain
{
    public interface IEntityId
    {
        string AsIdString();
    }
}

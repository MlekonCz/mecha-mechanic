using System.Collections.Generic;

namespace Mechs
{
    public interface INextPart
    {
        PartsOfMech? GetNextBodyPart();
    }
}
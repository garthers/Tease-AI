using System;
using System.Collections.Generic;
using System.Text;

namespace Tai.Common
{
    public interface ILog
    {
        void WriteError(params object[] args);
    }
}


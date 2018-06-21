using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Wooting
{
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void DisconnectedCallback();
}

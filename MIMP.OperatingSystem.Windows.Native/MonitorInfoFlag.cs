using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIMP.OperatingSystem.Windows.Native
{
    /// <summary>
    /// Flags that may be defined on the <see cref="MonitorInfoExtended.Flags"/> field.
    /// </summary>
    [Flags]
    public enum MonitorInfoFlag : uint
    {
        /// <summary>
        /// No flags.
        /// </summary>
        /// <remarks></remarks>
        None = 0x0,

        /// <summary>
        /// This is the primary display monitor.
        /// </summary>
        /// <remarks>MONITORINFOF_PRIMARY</remarks>
        Primary = 0x1,
    }
}

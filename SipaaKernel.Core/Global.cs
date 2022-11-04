using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipaaKernel.Core
{
    internal unsafe class Global
    {
        /// <summary>
        /// Call the system to do something (like writing to console)
        /// </summary>
        /// <param name="EAX">The action the system will do. All other params is arguments for the syscall. See more at </param>
        /// <returns></returns>
        public static uint SysCall(ref uint* EAX, ref uint* EBX, ref uint* ECX, ref uint* EDX, ref uint* ESI, ref uint* EDI)
        {
            switch ((uint)EAX)
            {
                case 0x0: // Print something to console
                    Console.WriteLine(new String((char*)EBX));
                    return 0;
            }
            return 11; // Syscall not found
        }
    }
}

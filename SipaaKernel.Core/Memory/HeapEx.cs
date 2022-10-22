using Cosmos.Core;
using Cosmos.Core.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipaaKernel.Core.Memory
{
    /// <summary>
    /// This class gives Heap.Realloc() because i don't have updated my devkit 💀
    /// </summary>
    public unsafe class HeapEx
    {
        public static byte* Realloc(byte* aPtr, uint newSize)
        {
            // TODO: don't move memory position if there is enough space in the current one.

            // Get existing size
            uint Size = (RAT.GetPageType(aPtr) == RAT.PageType.HeapSmall ? ((ushort*)aPtr)[-2] : ((uint*)aPtr)[-4]);

            if (Size == newSize)
            {
                // Return existing pointer as nothing needs to be done.
                return aPtr;
            }
            if (Size > newSize)
            {
                Size -= (newSize - Size);
            }

            // Allocate a new buffer to use
            byte* ToReturn = Heap.Alloc(newSize);

            // Copy the old buffer to the new one
            MemoryOperations.Copy(ToReturn, aPtr, (int)Size);

            // Comented out to help in the future if we use objects with realloc
            // Copy the GC state
            //((ushort*)ToReturn)[-1] = ((ushort*)aPtr)[-1];
            ((ushort*)ToReturn)[-1] = 0;

            // Free the old data and return
            Heap.Free(aPtr);
            return ToReturn;
        }
    }
}

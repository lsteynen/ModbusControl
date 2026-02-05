/*
 * RegisterToOffset
 * Convert modbus register numbers to protocol offsets, and back
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace ModbusControlSpace
{
    public static class RegisterToOffset
    {
        private static int mRegisterNumberingScheme = 40001;        // 40000, 40001, 400000, 400001

        #region Properties

        /// <summary>
        /// Set register numbering scheme
        /// 40001:  5-digit numbering, uses 6 digits when needed, 1-based, is default
        /// 40000:  5-digit numbering, uses 6 digits when needed, 0-based
        /// 400001: 6-digit numbering, 1-based
        /// 400000: 6-digit numbering, 0-based
        /// </summary>
        public static int RegisterNumberingScheme
        {
            get
            {
                return mRegisterNumberingScheme;
            }

            set
            {
                if (value == 40001 || value == 40000 || value == 400001 || value == 400000)
                {
                    mRegisterNumberingScheme = value;
                }
                else
                {
                    throw new ArgumentException("Invalid value!", "RegisterNumberingScheme");
                }
            }
        }

        #endregion Properties

        /// <summary>
        /// Convert holding register number 4xxxx to register offset 0-65535.
        /// if RegisterNumberingScheme is 40001: accept 40001-49999 + 400001-465536
        /// if RegisterNumberingScheme is 40000: accept 40000-49999 + 400000-465535
        /// if RegisterNumberingScheme is 400001: accept 400001-465536
        /// if RegisterNumberingScheme is 400000: accept 400000-465535
        /// </summary>
        public static UInt16 GetHoldingregisterOffset(UInt32 Regnr)
        {
            if (mRegisterNumberingScheme == 40001)
            {
                if (Regnr >= 40001 && Regnr <= 49999)
                {
                    return (UInt16)(Regnr - 40001);
                }
                if (Regnr >= 400001 && Regnr <= 465536)
                {
                    return (UInt16)(Regnr - 400001);
                }
            }

            if (mRegisterNumberingScheme == 40000)
            {
                if (Regnr >= 40000 && Regnr <= 49999)
                {
                    return (UInt16)(Regnr - 40000);
                }
                if (Regnr >= 400000 && Regnr <= 465535)
                {
                    return (UInt16)(Regnr - 400000);
                }
            }

            if (mRegisterNumberingScheme == 400001)
            {
                if (Regnr >= 400001 && Regnr <= 465536)
                {
                    return (UInt16)(Regnr - 400001);
                }
            }

            if (mRegisterNumberingScheme == 400000)
            {
                if (Regnr >= 400000 && Regnr <= 465535)
                {
                    return (UInt16)(Regnr - 400000);
                }
            }

            throw new ArgumentException("Invalid holding register number");
        }

        /// <summary>
        /// Convert holding register offset to register number.
        /// if RegisterNumberingScheme is 40001: produce 40001-49999 + 410000-465536 when needed
        /// if RegisterNumberingScheme is 40000: produce 40000-49999 + 409999-465535 when needed
        /// if RegisterNumberingScheme is 400001: produce 400001-465536
        /// if RegisterNumberingScheme is 400000: produce 400000-465535
        /// </summary>
        public static Int32 ConvertHoldingregisterOffsetToRegisterNumber(UInt16 Offset)
        {
            if (mRegisterNumberingScheme == 40001)
            {
                if (Offset <= 9998)
                {
                    return 40001 + Offset;
                }
                if (Offset <= 65535)
                {
                    return 400001 + Offset;
                }
            }

            if (mRegisterNumberingScheme == 40000)
            {
                if (Offset <= 9999)
                {
                    return 40000 + Offset;
                }
                if (Offset <= 65535)
                {
                    return 400000 + Offset;
                }
            }

            if (mRegisterNumberingScheme == 400001)
            {
                if (Offset <= 65535)
                {
                    return 400001 + Offset;
                }
            }

            if (mRegisterNumberingScheme == 400000)
            {
                if (Offset <= 65535)
                {
                    return 400000 + Offset;
                }
            }

            throw new ArgumentException("Invalid holding register offset");
        }

        /// <summary>
        /// Convert input register number 3xxxx to register offset 0-65535.
        /// if RegisterNumberingScheme is 40001: accept 30001-39999 + 300001-365536
        /// if RegisterNumberingScheme is 40000: accept 30000-39999 + 300000-365535
        /// if RegisterNumberingScheme is 400001: accept 300001-365536
        /// if RegisterNumberingScheme is 400000: accept 300000-365535
        /// </summary>
        public static UInt16 GetInputregisterOffset(UInt32 Regnr)
        {
            if (mRegisterNumberingScheme == 40001)
            {
                if (Regnr >= 30001 && Regnr <= 39999)
                {
                    return (UInt16)(Regnr - 30001);
                }
                if (Regnr >= 300001 && Regnr <= 365536)
                {
                    return (UInt16)(Regnr - 300001);
                }
            }

            if (mRegisterNumberingScheme == 40000)
            {
                if (Regnr >= 30000 && Regnr <= 39999)
                {
                    return (UInt16)(Regnr - 30000);
                }
                if (Regnr >= 300000 && Regnr <= 365535)
                {
                    return (UInt16)(Regnr - 300000);
                }
            }

            if (mRegisterNumberingScheme == 400001)
            {
                if (Regnr >= 300001 && Regnr <= 365536)
                {
                    return (UInt16)(Regnr - 300001);
                }
            }

            if (mRegisterNumberingScheme == 400000)
            {
                if (Regnr >= 300000 && Regnr <= 365535)
                {
                    return (UInt16)(Regnr - 300000);
                }
            }

            throw new ArgumentException("Invalid input register number");
        }

        /// <summary>
        /// Convert input register offset to register number.
        /// if RegisterNumberingScheme is 40001: produce 30001-39999 + 310000-365536 when needed
        /// if RegisterNumberingScheme is 40000: produce 30000-39999 + 309999-365535 when needed
        /// if RegisterNumberingScheme is 400001: produce 300001-365536
        /// if RegisterNumberingScheme is 400000: produce 300000-365535
        /// </summary>
        public static Int32 ConvertInputregisterOffsetToRegisterNumber(UInt16 Offset)
        {
            if (mRegisterNumberingScheme == 40001)
            {
                if (Offset <= 9998)
                {
                    return 30001 + Offset;
                }
                if (Offset <= 65535)
                {
                    return 300001+ Offset;
                }
            }

            if (mRegisterNumberingScheme == 40000)
            {
                if (Offset <= 9999)
                {
                    return 30000 + Offset;
                }
                if (Offset <= 65535)
                {
                    return 300000 + Offset;
                }
            }

            if (mRegisterNumberingScheme == 400001)
            {
                if (Offset <= 65535)
                {
                    return 300001 + Offset; 
                }
            }

            if (mRegisterNumberingScheme == 400000)
            {
                if (Offset <= 65535)
                {
                    return 300000 + Offset;
                }
            }

            throw new ArgumentException("Invalid input register offset");
        }

        /// <summary>
        /// Convert coil number 0xxxx to offset 0-65535.
        /// if RegisterNumberingScheme is 40001: accept 1-9999
        /// if RegisterNumberingScheme is 40000: accept 0-9999
        /// if RegisterNumberingScheme is 400001: accept 1-65536
        /// if RegisterNumberingScheme is 400000: accept 0-65535
        /// </summary>
        public static UInt16 GetCoilOffset(UInt32 Regnr)
        {
            if (mRegisterNumberingScheme == 40001)
            {
                if (Regnr >= 1 && Regnr <= 9999)
                {
                    return (UInt16)(Regnr - 1);
                }
            }

            if (mRegisterNumberingScheme == 40000)
            {
                if (Regnr >= 0 && Regnr <= 9999)
                {
                    return (UInt16)(Regnr - 0);
                }
            }

            if (mRegisterNumberingScheme == 400001)
            {
                if (Regnr >= 1 && Regnr <= 65536)
                {
                    return (UInt16)(Regnr - 1);
                }
            }

            if (mRegisterNumberingScheme == 400000)
            {
                if (Regnr >= 0 && Regnr <= 65535)
                {
                    return (UInt16)(Regnr - 0);
                }
            }

            throw new ArgumentException("Invalid coil number");
        }

        /// <summary>
        /// Convert coil offset to coil number.
        /// if RegisterNumberingScheme is 40001: produce 1-9999
        /// if RegisterNumberingScheme is 40000:produce 0-9999
        /// if RegisterNumberingScheme is 400001: produce 1-65536
        /// if RegisterNumberingScheme is 400000: produce 0-65535
        /// </summary>
        public static Int32 ConvertCoilOffsetToNumber(UInt16 Offset)
        {
            if (mRegisterNumberingScheme == 40001)
            {
                if (Offset <= 9998)
                {
                    return 1 + Offset;
                }
            }

            if (mRegisterNumberingScheme == 40000)
            {
                if (Offset <= 9999)
                {
                    return 0 + Offset;
                }
            }

            if (mRegisterNumberingScheme == 400001)
            {
                if (Offset <= 65535)
                {
                    return 1 + Offset;
                }
            }

            if (mRegisterNumberingScheme == 400000)
            {
                if (Offset <= 65535)
                {
                    return 0 + Offset;
                }
            }

            throw new ArgumentException("Invalid coil offset");
        }

        /// <summary>
        /// Convert discrete input number 1xxxx to offset 0-65535.
        /// if RegisterNumberingScheme is 40001: accept 10001-19999 + 100001-165536
        /// if RegisterNumberingScheme is 40000: accept 10000-19999 + 100000-165535
        /// if RegisterNumberingScheme is 400001: accept 100001-165536
        /// if RegisterNumberingScheme is 400000: accept 100000-165535
        /// </summary>
        public static UInt16 GetDiscreteInputOffset(UInt32 Regnr)
        {
            if (mRegisterNumberingScheme == 40001)
            {
                if (Regnr >= 10001 && Regnr <= 19999)
                {
                    return (UInt16)(Regnr - 10001);
                }
                if (Regnr >= 100001 && Regnr <= 165536)
                {
                    return (UInt16)(Regnr - 100001);
                }
            }

            if (mRegisterNumberingScheme == 40000)
            {
                if (Regnr >= 10000 && Regnr <= 19999)
                {
                    return (UInt16)(Regnr - 10000);
                }
                if (Regnr >= 100000 && Regnr <= 165535)
                {
                    return (UInt16)(Regnr - 100000);
                }
            }

            if (mRegisterNumberingScheme == 400001)
            {
                if (Regnr >= 100001 && Regnr <= 165536)
                {
                    return (UInt16)(Regnr - 100001);
                }
            }

            if (mRegisterNumberingScheme == 400000)
            {
                if (Regnr >= 100000 && Regnr <= 165535)
                {
                    return (UInt16)(Regnr - 100000);
                }
            }

            throw new ArgumentException("Invalid discrete input number");
        }

        /// <summary>
        /// Convert discrete input offset to register number.
        /// if RegisterNumberingScheme is 40001: produce 10001-19999 + 110000-165536 when needed
        /// if RegisterNumberingScheme is 40000: produce 10000-39999 + 109999-165535 when needed
        /// if RegisterNumberingScheme is 400001: produce 100001-165536
        /// if RegisterNumberingScheme is 400000: produce 100000-165535
        /// </summary>
        public static Int32 ConvertDiscreteInputOffsetToNumber(UInt16 Offset)
        {
            if (mRegisterNumberingScheme == 40001)
            {
                if (Offset <= 9998)
                {
                    return 10001 + Offset;
                }
            }

            if (mRegisterNumberingScheme == 40000)
            {
                if (Offset <= 9999)
                {
                    return 10000 + Offset;
                }
            }

            if (mRegisterNumberingScheme == 400001)
            {
                if (Offset <= 65535)
                {
                    return 100001 + Offset;
                }
            }

            if (mRegisterNumberingScheme == 400000)
            {
                if (Offset <= 65535)
                {
                    return 100000 + Offset;
                }
            }

            throw new ArgumentException("Invalid discrete input offset");
        }

    }
}

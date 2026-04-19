using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModbusControl;

namespace UnitTests
{
    [TestClass]
    public class Test_RegisterToOffset_1
    {
        [TestMethod]
        public void Test_RegisterNumberingScheme_1()
        {
            RegisterToOffset registerToOffset = new RegisterToOffset(40001);
            Assert.AreEqual<int>(40001, registerToOffset.RegisterNumberingScheme);

            registerToOffset = new RegisterToOffset(40000);
            Assert.AreEqual<int>(40000, registerToOffset.RegisterNumberingScheme);

            registerToOffset = new RegisterToOffset(400001);
            Assert.AreEqual<int>(400001, registerToOffset.RegisterNumberingScheme);

            registerToOffset = new RegisterToOffset(400000);
            Assert.AreEqual<int>(400000, registerToOffset.RegisterNumberingScheme);

            Assert.ThrowsException<ArgumentException>(() =>
                registerToOffset.RegisterNumberingScheme = 0
                );

            Assert.ThrowsException<ArgumentException>(() =>
                registerToOffset.RegisterNumberingScheme = 30001
                );

            Assert.ThrowsException<ArgumentException>(() =>
                registerToOffset.RegisterNumberingScheme = 300001
                );

            Assert.ThrowsException<ArgumentException>(() =>
                registerToOffset.RegisterNumberingScheme = 100000
                );

            Assert.ThrowsException<ArgumentException>(() =>
                registerToOffset.RegisterNumberingScheme = 400002
                );
        }



        [TestMethod]
        public void Test_GetHoldingregOffset_40001()
        {
            RegisterToOffset registerToOffset = new RegisterToOffset(40001);

            Assert.ThrowsException<ArgumentException>(() =>
                registerToOffset.GetHoldingregisterOffset(40000)
                );

            UInt16 offset = registerToOffset.GetHoldingregisterOffset(40001);
            Assert.AreEqual<UInt16>(0, offset);

            offset = registerToOffset.GetHoldingregisterOffset(40002);
            Assert.AreEqual<UInt16>(1, offset);

            offset = registerToOffset.GetHoldingregisterOffset(49999);
            Assert.AreEqual<UInt16>(9998, offset);

            Assert.ThrowsException<ArgumentException>(() =>
                registerToOffset.GetHoldingregisterOffset(50000)
                );

            Assert.ThrowsException<ArgumentException>(() =>
               registerToOffset.GetHoldingregisterOffset(400000)
               );

            offset = registerToOffset.GetHoldingregisterOffset(400001);
            Assert.AreEqual<UInt16>(0, offset);

            offset = registerToOffset.GetHoldingregisterOffset(465536);
            Assert.AreEqual<UInt16>(65535, offset);

            Assert.ThrowsException<ArgumentException>(() =>
                registerToOffset.GetHoldingregisterOffset(465537)
                );
        }

        [TestMethod]
        public void Test_GetHoldingregOffset_40000()
        {
            RegisterToOffset registerToOffset = new RegisterToOffset(40000);

            UInt16 offset = registerToOffset.GetHoldingregisterOffset(40000);
            Assert.AreEqual<UInt16>(0, offset);

            offset = registerToOffset.GetHoldingregisterOffset(40001);
            Assert.AreEqual<UInt16>(1, offset);

            offset = registerToOffset.GetHoldingregisterOffset(49999);
            Assert.AreEqual<UInt16>(9999, offset);

            Assert.ThrowsException<ArgumentException>(() =>
                registerToOffset.GetHoldingregisterOffset(50000)
                );

            offset = registerToOffset.GetHoldingregisterOffset(400000);
            Assert.AreEqual<UInt16>(0, offset);

            offset = registerToOffset.GetHoldingregisterOffset(400001);
            Assert.AreEqual<UInt16>(1, offset);

            offset = registerToOffset.GetHoldingregisterOffset(465535);
            Assert.AreEqual<UInt16>(65535, offset);

            Assert.ThrowsException<ArgumentException>(() =>
                registerToOffset.GetHoldingregisterOffset(465536)
                );
        }

        [TestMethod]
        public void Test_GetHoldingregOffset_400001()
        {
            RegisterToOffset registerToOffset = new RegisterToOffset(400001);

            Assert.ThrowsException<ArgumentException>(() =>
                registerToOffset.GetHoldingregisterOffset(40001)
                );

            Assert.ThrowsException<ArgumentException>(() =>
                registerToOffset.GetHoldingregisterOffset(400000)
                );

            UInt16 offset = registerToOffset.GetHoldingregisterOffset(400001);
            Assert.AreEqual<UInt16>(0, offset);

            offset = registerToOffset.GetHoldingregisterOffset(400002);
            Assert.AreEqual<UInt16>(1, offset);

            offset = registerToOffset.GetHoldingregisterOffset(410000);
            Assert.AreEqual<UInt16>(9999, offset);

            offset = registerToOffset.GetHoldingregisterOffset(465536);
            Assert.AreEqual<UInt16>(65535, offset);

            Assert.ThrowsException<ArgumentException>(() =>
                registerToOffset.GetHoldingregisterOffset(465537)
                );

            Assert.ThrowsException<ArgumentException>(() =>
                registerToOffset.GetHoldingregisterOffset(300001)
                );
        }

        [TestMethod]
        public void Test_GetHoldingregOffset_400000()
        {
            RegisterToOffset registerToOffset = new RegisterToOffset(400000);

            Assert.ThrowsException<ArgumentException>(() =>
                registerToOffset.GetHoldingregisterOffset(40001)
                );

            UInt16 offset = registerToOffset.GetHoldingregisterOffset(400000);
            Assert.AreEqual<UInt16>(0, offset);

            offset = registerToOffset.GetHoldingregisterOffset(400001);
            Assert.AreEqual<UInt16>(1, offset);

            offset = registerToOffset.GetHoldingregisterOffset(409999);
            Assert.AreEqual<UInt16>(9999, offset);

            offset = registerToOffset.GetHoldingregisterOffset(410000);
            Assert.AreEqual<UInt16>(10000, offset);

            offset = registerToOffset.GetHoldingregisterOffset(465535);
            Assert.AreEqual<UInt16>(65535, offset);

            Assert.ThrowsException<ArgumentException>(() =>
                registerToOffset.GetHoldingregisterOffset(465536)
                );
        }



        [TestMethod]
        public void Test_ConvertHoldingregisterOffsetToRegisterNumber_40001()
        {
            RegisterToOffset registerToOffset = new RegisterToOffset(40001);

            int regnr = registerToOffset.ConvertHoldingregisterOffsetToRegisterNumber(0);
            Assert.AreEqual<int>(40001, regnr);

            regnr = registerToOffset.ConvertHoldingregisterOffsetToRegisterNumber(1);
            Assert.AreEqual<int>(40002, regnr);

            regnr = registerToOffset.ConvertHoldingregisterOffsetToRegisterNumber(9998);
            Assert.AreEqual<int>(49999, regnr);

            regnr = registerToOffset.ConvertHoldingregisterOffsetToRegisterNumber(9999);
            Assert.AreEqual<int>(410000, regnr);

            regnr = registerToOffset.ConvertHoldingregisterOffsetToRegisterNumber(10000);
            Assert.AreEqual<int>(410001, regnr);

            regnr = registerToOffset.ConvertHoldingregisterOffsetToRegisterNumber(65535);
            Assert.AreEqual<int>(465536, regnr);
        }

        [TestMethod]
        public void Test_ConvertHoldingregisterOffsetToRegisterNumber_40000()
        {
            RegisterToOffset registerToOffset = new RegisterToOffset(40000);

            int regnr = registerToOffset.ConvertHoldingregisterOffsetToRegisterNumber(0);
            Assert.AreEqual<int>(40000, regnr);

            regnr = registerToOffset.ConvertHoldingregisterOffsetToRegisterNumber(1);
            Assert.AreEqual<int>(40001, regnr);

            regnr = registerToOffset.ConvertHoldingregisterOffsetToRegisterNumber(9998);
            Assert.AreEqual<int>(49998, regnr);

            regnr = registerToOffset.ConvertHoldingregisterOffsetToRegisterNumber(9999);
            Assert.AreEqual<int>(49999, regnr);

            regnr = registerToOffset.ConvertHoldingregisterOffsetToRegisterNumber(10000);
            Assert.AreEqual<int>(410000, regnr);

            regnr = registerToOffset.ConvertHoldingregisterOffsetToRegisterNumber(65535);
            Assert.AreEqual<int>(465535, regnr);
        }

        [TestMethod]
        public void Test_ConvertHoldingregisterOffsetToRegisterNumber_400001()
        {
            RegisterToOffset registerToOffset = new RegisterToOffset(400001);

            int regnr = registerToOffset.ConvertHoldingregisterOffsetToRegisterNumber(0);
            Assert.AreEqual<int>(400001, regnr);

            regnr = registerToOffset.ConvertHoldingregisterOffsetToRegisterNumber(1);
            Assert.AreEqual<int>(400002, regnr);

            regnr = registerToOffset.ConvertHoldingregisterOffsetToRegisterNumber(9998);
            Assert.AreEqual<int>(409999, regnr);

            regnr = registerToOffset.ConvertHoldingregisterOffsetToRegisterNumber(9999);
            Assert.AreEqual<int>(410000, regnr);

            regnr = registerToOffset.ConvertHoldingregisterOffsetToRegisterNumber(10000);
            Assert.AreEqual<int>(410001, regnr);

            regnr = registerToOffset.ConvertHoldingregisterOffsetToRegisterNumber(65535);
            Assert.AreEqual<int>(465536, regnr);
        }

        [TestMethod]
        public void Test_ConvertHoldingregisterOffsetToRegisterNumber_400000()
        {
            RegisterToOffset registerToOffset = new RegisterToOffset(400000);

            int regnr = registerToOffset.ConvertHoldingregisterOffsetToRegisterNumber(0);
            Assert.AreEqual<int>(400000, regnr);

            regnr = registerToOffset.ConvertHoldingregisterOffsetToRegisterNumber(1);
            Assert.AreEqual<int>(400001, regnr);

            regnr = registerToOffset.ConvertHoldingregisterOffsetToRegisterNumber(9998);
            Assert.AreEqual<int>(409998, regnr);

            regnr = registerToOffset.ConvertHoldingregisterOffsetToRegisterNumber(9999);
            Assert.AreEqual<int>(409999, regnr);

            regnr = registerToOffset.ConvertHoldingregisterOffsetToRegisterNumber(10000);
            Assert.AreEqual<int>(410000, regnr);

            regnr = registerToOffset.ConvertHoldingregisterOffsetToRegisterNumber(65535);
            Assert.AreEqual<int>(465535, regnr);
        }



        [TestMethod]
        public void Test_GetInputregisterOffset_40001()
        {
            RegisterToOffset registerToOffset = new RegisterToOffset(40001);

            Assert.ThrowsException<ArgumentException>(() =>
                registerToOffset.GetInputregisterOffset(30000)
                );

            UInt16 offset = registerToOffset.GetInputregisterOffset(30001);
            Assert.AreEqual<UInt16>(0, offset);

            offset = registerToOffset.GetInputregisterOffset(30002);
            Assert.AreEqual<UInt16>(1, offset);

            offset = registerToOffset.GetInputregisterOffset(39999);
            Assert.AreEqual<UInt16>(9998, offset);

            Assert.ThrowsException<ArgumentException>(() =>
                registerToOffset.GetInputregisterOffset(40000)
                );

            Assert.ThrowsException<ArgumentException>(() =>
                 registerToOffset.GetInputregisterOffset(300000)
                 );

            offset = registerToOffset.GetInputregisterOffset(300001);
            Assert.AreEqual<UInt16>(0, offset);

            offset = registerToOffset.GetInputregisterOffset(310000);
            Assert.AreEqual<UInt16>(9999, offset);

            offset = registerToOffset.GetInputregisterOffset(310001);
            Assert.AreEqual<UInt16>(10000, offset);

            offset = registerToOffset.GetInputregisterOffset(365536);
            Assert.AreEqual<UInt16>(65535, offset);

            Assert.ThrowsException<ArgumentException>(() =>
                registerToOffset.GetInputregisterOffset(365537)
                );
        }

        [TestMethod]
        public void Test_GetInputregisterOffset_40000()
        {
            RegisterToOffset registerToOffset = new RegisterToOffset(40000);

            UInt16 offset = registerToOffset.GetInputregisterOffset(30000);
            Assert.AreEqual<UInt16>(0, offset);

            offset = registerToOffset.GetInputregisterOffset(30001);
            Assert.AreEqual<UInt16>(1, offset);

            offset = registerToOffset.GetInputregisterOffset(39999);
            Assert.AreEqual<UInt16>(9999, offset);

            Assert.ThrowsException<ArgumentException>(() =>
                registerToOffset.GetInputregisterOffset(40000)
                );

            offset = registerToOffset.GetInputregisterOffset(300000);
            Assert.AreEqual<UInt16>(0, offset);

            offset = registerToOffset.GetInputregisterOffset(300001);
            Assert.AreEqual<UInt16>(1, offset);

            offset = registerToOffset.GetInputregisterOffset(309999);
            Assert.AreEqual<UInt16>(9999, offset);

            offset = registerToOffset.GetInputregisterOffset(310000);
            Assert.AreEqual<UInt16>(10000, offset);

            offset = registerToOffset.GetInputregisterOffset(310001);
            Assert.AreEqual<UInt16>(10001, offset);

            offset = registerToOffset.GetInputregisterOffset(365535);
            Assert.AreEqual<UInt16>(65535, offset);

            Assert.ThrowsException<ArgumentException>(() =>
                registerToOffset.GetInputregisterOffset(365536)
                );
        }

        [TestMethod]
        public void Test_GetInputregOffset_400001()
        {
            RegisterToOffset registerToOffset = new RegisterToOffset(400001);

            Assert.ThrowsException<ArgumentException>(() =>
                registerToOffset.GetInputregisterOffset(30001)
                );

            Assert.ThrowsException<ArgumentException>(() =>
                registerToOffset.GetInputregisterOffset(300000)
                );

            UInt16 offset = registerToOffset.GetInputregisterOffset(300001);
            Assert.AreEqual<UInt16>(0, offset);

            offset = registerToOffset.GetInputregisterOffset(300002);
            Assert.AreEqual<UInt16>(1, offset);

            offset = registerToOffset.GetInputregisterOffset(310000);
            Assert.AreEqual<UInt16>(9999, offset);

            offset = registerToOffset.GetInputregisterOffset(365536);
            Assert.AreEqual<UInt16>(65535, offset);

            Assert.ThrowsException<ArgumentException>(() =>
                registerToOffset.GetInputregisterOffset(365537)
                );

            Assert.ThrowsException<ArgumentException>(() =>
                registerToOffset.GetInputregisterOffset(400001)
                );
        }

        [TestMethod]
        public void Test_GetInputregOffset_400000()
        {
            RegisterToOffset registerToOffset = new RegisterToOffset(400000);

            Assert.ThrowsException<ArgumentException>(() =>
                registerToOffset.GetInputregisterOffset(30001)
                );

            UInt16 offset = registerToOffset.GetInputregisterOffset(300000);
            Assert.AreEqual<UInt16>(0, offset);

            offset = registerToOffset.GetInputregisterOffset(300001);
            Assert.AreEqual<UInt16>(1, offset);

            offset = registerToOffset.GetInputregisterOffset(309999);
            Assert.AreEqual<UInt16>(9999, offset);

            offset = registerToOffset.GetInputregisterOffset(310000);
            Assert.AreEqual<UInt16>(10000, offset);

            offset = registerToOffset.GetInputregisterOffset(310001);
            Assert.AreEqual<UInt16>(10001, offset);

            offset = registerToOffset.GetInputregisterOffset(365535);
            Assert.AreEqual<UInt16>(65535, offset);

            Assert.ThrowsException<ArgumentException>(() =>
                registerToOffset.GetInputregisterOffset(365536)
                );
        }


        [TestMethod]
        public void Test_ConvertInputregisterOffsetToRegisterNumber_40001()
        {
            RegisterToOffset registerToOffset = new RegisterToOffset(40001);

            int regnr = registerToOffset.ConvertInputregisterOffsetToRegisterNumber(0);
            Assert.AreEqual<int>(30001, regnr);

            regnr = registerToOffset.ConvertInputregisterOffsetToRegisterNumber(1);
            Assert.AreEqual<int>(30002, regnr);

            regnr = registerToOffset.ConvertInputregisterOffsetToRegisterNumber(9998);
            Assert.AreEqual<int>(39999, regnr);

            regnr = registerToOffset.ConvertInputregisterOffsetToRegisterNumber(9999);
            Assert.AreEqual<int>(310000, regnr);

            regnr = registerToOffset.ConvertInputregisterOffsetToRegisterNumber(10000);
            Assert.AreEqual<int>(310001, regnr);

            regnr = registerToOffset.ConvertInputregisterOffsetToRegisterNumber(65535);
            Assert.AreEqual<int>(365536, regnr);

        }

        [TestMethod]
        public void Test_ConvertInputregisterOffsetToRegisterNumber_40000()
        {
            RegisterToOffset registerToOffset = new RegisterToOffset(40000);

            int regnr = registerToOffset.ConvertInputregisterOffsetToRegisterNumber(0);
            Assert.AreEqual<int>(30000, regnr);

            regnr = registerToOffset.ConvertInputregisterOffsetToRegisterNumber(1);
            Assert.AreEqual<int>(30001, regnr);

            regnr = registerToOffset.ConvertInputregisterOffsetToRegisterNumber(9998);
            Assert.AreEqual<int>(39998, regnr);

            regnr = registerToOffset.ConvertInputregisterOffsetToRegisterNumber(9999);
            Assert.AreEqual<int>(39999, regnr);

            regnr = registerToOffset.ConvertInputregisterOffsetToRegisterNumber(10000);
            Assert.AreEqual<int>(310000, regnr);

            regnr = registerToOffset.ConvertInputregisterOffsetToRegisterNumber(65535);
            Assert.AreEqual<int>(365535, regnr);

        }

        [TestMethod]
        public void Test_ConvertInputregisterOffsetToRegisterNumber_400001()
        {
            RegisterToOffset registerToOffset = new RegisterToOffset(400001);

            int regnr = registerToOffset.ConvertInputregisterOffsetToRegisterNumber(0);
            Assert.AreEqual<int>(300001, regnr);

            regnr = registerToOffset.ConvertInputregisterOffsetToRegisterNumber(1);
            Assert.AreEqual<int>(300002, regnr);

            regnr = registerToOffset.ConvertInputregisterOffsetToRegisterNumber(9998);
            Assert.AreEqual<int>(309999, regnr);

            regnr = registerToOffset.ConvertInputregisterOffsetToRegisterNumber(9999);
            Assert.AreEqual<int>(310000, regnr);

            regnr = registerToOffset.ConvertInputregisterOffsetToRegisterNumber(10000);
            Assert.AreEqual<int>(310001, regnr);

            regnr = registerToOffset.ConvertInputregisterOffsetToRegisterNumber(65535);
            Assert.AreEqual<int>(365536, regnr);

        }

        [TestMethod]
        public void Test_ConvertInputregisterOffsetToRegisterNumber_400000()
        {
            RegisterToOffset registerToOffset = new RegisterToOffset(400000);

            int regnr = registerToOffset.ConvertInputregisterOffsetToRegisterNumber(0);
            Assert.AreEqual<int>(300000, regnr);

            regnr = registerToOffset.ConvertInputregisterOffsetToRegisterNumber(1);
            Assert.AreEqual<int>(300001, regnr);

            regnr = registerToOffset.ConvertInputregisterOffsetToRegisterNumber(9998);
            Assert.AreEqual<int>(309998, regnr);

            regnr = registerToOffset.ConvertInputregisterOffsetToRegisterNumber(9999);
            Assert.AreEqual<int>(309999, regnr);

            regnr = registerToOffset.ConvertInputregisterOffsetToRegisterNumber(10000);
            Assert.AreEqual<int>(310000, regnr);

            regnr = registerToOffset.ConvertInputregisterOffsetToRegisterNumber(65535);
            Assert.AreEqual<int>(365535, regnr);

        }

        
        [TestMethod]
        public void Test_GetCoilOffset_40001()
        {
            RegisterToOffset registerToOffset = new RegisterToOffset(40001);

            Assert.ThrowsException<ArgumentException>(() =>
                registerToOffset.GetCoilOffset(0)
                );

            UInt16 offset = registerToOffset.GetCoilOffset(1);
            Assert.AreEqual<UInt16>(0, offset);

            offset = registerToOffset.GetCoilOffset(2);
            Assert.AreEqual<UInt16>(1, offset);

            offset = registerToOffset.GetCoilOffset(9999);
            Assert.AreEqual<UInt16>(9998, offset);

            Assert.ThrowsException<ArgumentException>(() =>
                registerToOffset.GetCoilOffset(10000)
                );

            Assert.ThrowsException<ArgumentException>(() =>
                 registerToOffset.GetCoilOffset(65535)
                 );
        }

        
        [TestMethod]
        public void Test_GetCoilOffset_40000()
        {
            RegisterToOffset registerToOffset = new RegisterToOffset(40000);

            UInt16 offset = registerToOffset.GetCoilOffset(0);
            Assert.AreEqual<UInt16>(0, offset);

            offset = registerToOffset.GetCoilOffset(1);
            Assert.AreEqual<UInt16>(1, offset);

            offset = registerToOffset.GetCoilOffset(9998);
            Assert.AreEqual<UInt16>(9998, offset);

            offset = registerToOffset.GetCoilOffset(9999);
            Assert.AreEqual<UInt16>(9999, offset);

            Assert.ThrowsException<ArgumentException>(() =>
                registerToOffset.GetCoilOffset(10000)
                );

            Assert.ThrowsException<ArgumentException>(() =>
                 registerToOffset.GetCoilOffset(65535)
                 );
        }

        [TestMethod]
        public void Test_GetCoilOffset_400001()
        {
            RegisterToOffset registerToOffset = new RegisterToOffset(400001);

            Assert.ThrowsException<ArgumentException>(() =>
                registerToOffset.GetCoilOffset(0)
                );

            UInt16 offset = registerToOffset.GetCoilOffset(1);
            Assert.AreEqual<UInt16>(0, offset);

            offset = registerToOffset.GetCoilOffset(2);
            Assert.AreEqual<UInt16>(1, offset);

            offset = registerToOffset.GetCoilOffset(9999);
            Assert.AreEqual<UInt16>(9998, offset);

            offset = registerToOffset.GetCoilOffset(10000);
            Assert.AreEqual<UInt16>(9999, offset);

            offset = registerToOffset.GetCoilOffset(10001);
            Assert.AreEqual<UInt16>(10000, offset);

            offset = registerToOffset.GetCoilOffset(65536);
            Assert.AreEqual<UInt16>(65535, offset);

            Assert.ThrowsException<ArgumentException>(() =>
                registerToOffset.GetCoilOffset(65537)
                );
        }

        [TestMethod]
        public void Test_GetCoilOffset_400000()
        {
            RegisterToOffset registerToOffset = new RegisterToOffset(400000);

            UInt16 offset = registerToOffset.GetCoilOffset(0);
            Assert.AreEqual<UInt16>(0, offset);

            offset = registerToOffset.GetCoilOffset(1);
            Assert.AreEqual<UInt16>(1, offset);

            offset = registerToOffset.GetCoilOffset(9999);
            Assert.AreEqual<UInt16>(9999, offset);

            offset = registerToOffset.GetCoilOffset(10000);
            Assert.AreEqual<UInt16>(10000, offset);

            offset = registerToOffset.GetCoilOffset(10001);
            Assert.AreEqual<UInt16>(10001, offset);

            offset = registerToOffset.GetCoilOffset(65535);
            Assert.AreEqual<UInt16>(65535, offset);

           Assert.ThrowsException<ArgumentException>(() =>
                registerToOffset.GetCoilOffset(65536)
                );

            Assert.ThrowsException<ArgumentException>(() =>
                registerToOffset.GetCoilOffset(65537)
                );
        }



        [TestMethod]
        public void Test_ConvertCoilOffsetToNumber_40001()
        {
            RegisterToOffset registerToOffset = new RegisterToOffset(40001);

            int regnr = registerToOffset.ConvertCoilOffsetToNumber(0);
            Assert.AreEqual<int>(1, regnr);

            regnr = registerToOffset.ConvertCoilOffsetToNumber(1);
            Assert.AreEqual<int>(2, regnr);

            regnr = registerToOffset.ConvertCoilOffsetToNumber(9998);
            Assert.AreEqual<int>(9999, regnr);

            regnr = registerToOffset.ConvertCoilOffsetToNumber(9999);
            Assert.AreEqual<int>(10000, regnr);

            regnr = registerToOffset.ConvertCoilOffsetToNumber(10000);
            Assert.AreEqual<int>(10001, regnr);

            regnr = registerToOffset.ConvertCoilOffsetToNumber(65535);
            Assert.AreEqual<int>(65536, regnr);
        }

        [TestMethod]
        public void Test_ConvertCoilOffsetToNumber_40000()
        {
            RegisterToOffset registerToOffset = new RegisterToOffset(40000);

            int regnr = registerToOffset.ConvertCoilOffsetToNumber(0);
            Assert.AreEqual<int>(0, regnr);

            regnr = registerToOffset.ConvertCoilOffsetToNumber(1);
            Assert.AreEqual<int>(1, regnr);

            regnr = registerToOffset.ConvertCoilOffsetToNumber(9998);
            Assert.AreEqual<int>(9998, regnr);

            regnr = registerToOffset.ConvertCoilOffsetToNumber(9999);
            Assert.AreEqual<int>(9999, regnr);

            regnr = registerToOffset.ConvertCoilOffsetToNumber(10000);
            Assert.AreEqual<int>(10000, regnr);

            regnr = registerToOffset.ConvertCoilOffsetToNumber(65535);
            Assert.AreEqual<int>(65535, regnr);
        }

        [TestMethod]
        public void Test_ConvertCoilOffsetToNumber_400001()
        {
            RegisterToOffset registerToOffset = new RegisterToOffset(400001);

            int regnr = registerToOffset.ConvertCoilOffsetToNumber(0);
            Assert.AreEqual<int>(1, regnr);

            regnr = registerToOffset.ConvertCoilOffsetToNumber(1);
            Assert.AreEqual<int>(2, regnr);

            regnr = registerToOffset.ConvertCoilOffsetToNumber(9998);
            Assert.AreEqual<int>(9999, regnr);

            regnr = registerToOffset.ConvertCoilOffsetToNumber(9999);
            Assert.AreEqual<int>(10000, regnr);

            regnr = registerToOffset.ConvertCoilOffsetToNumber(10000);
            Assert.AreEqual<int>(10001, regnr);

            regnr = registerToOffset.ConvertCoilOffsetToNumber(65535);
            Assert.AreEqual<int>(65536, regnr);

        }

        [TestMethod]
        public void Test_ConvertCoilOffsetToNumber_400000()
        {
            RegisterToOffset registerToOffset = new RegisterToOffset(400000);

            int regnr = registerToOffset.ConvertCoilOffsetToNumber(0);
            Assert.AreEqual<int>(0, regnr);

            regnr = registerToOffset.ConvertCoilOffsetToNumber(1);
            Assert.AreEqual<int>(1, regnr);

            regnr = registerToOffset.ConvertCoilOffsetToNumber(9998);
            Assert.AreEqual<int>(9998, regnr);

            regnr = registerToOffset.ConvertCoilOffsetToNumber(9999);
            Assert.AreEqual<int>(9999, regnr);

            regnr = registerToOffset.ConvertCoilOffsetToNumber(10000);
            Assert.AreEqual<int>(10000, regnr);

            regnr = registerToOffset.ConvertCoilOffsetToNumber(65535);
            Assert.AreEqual<int>(65535, regnr);

        }



        [TestMethod]
        public void Test_GetDiscreteInputOffset_40001()
        {
            RegisterToOffset registerToOffset = new RegisterToOffset(40001);

            Assert.ThrowsException<ArgumentException>(() =>
                registerToOffset.GetDiscreteInputOffset(10000)
                );

            UInt16 offset = registerToOffset.GetDiscreteInputOffset(10001);
            Assert.AreEqual<UInt16>(0, offset);

            offset = registerToOffset.GetDiscreteInputOffset(10002);
            Assert.AreEqual<UInt16>(1, offset);

            offset = registerToOffset.GetDiscreteInputOffset(19999);
            Assert.AreEqual<UInt16>(9998, offset);

            Assert.ThrowsException<ArgumentException>(() =>
                registerToOffset.GetDiscreteInputOffset(20000)
                );

            Assert.ThrowsException<ArgumentException>(() =>
                 registerToOffset.GetDiscreteInputOffset(100000)
                 );

            offset = registerToOffset.GetDiscreteInputOffset(100001);
            Assert.AreEqual<UInt16>(0, offset);

            offset = registerToOffset.GetDiscreteInputOffset(100002);
            Assert.AreEqual<UInt16>(1, offset);

            offset = registerToOffset.GetDiscreteInputOffset(165536);
            Assert.AreEqual<UInt16>(65535, offset);

            Assert.ThrowsException<ArgumentException>(() =>
                registerToOffset.GetDiscreteInputOffset(365537)
                );
        }

        [TestMethod]
        public void Test_GetDiscreteInputOffset_40000()
        {
            RegisterToOffset registerToOffset = new RegisterToOffset(40000);

            UInt16 offset = registerToOffset.GetDiscreteInputOffset(10000);
            Assert.AreEqual<UInt16>(0, offset);

            offset = registerToOffset.GetDiscreteInputOffset(10001);
            Assert.AreEqual<UInt16>(1, offset);

            offset = registerToOffset.GetDiscreteInputOffset(19999);
            Assert.AreEqual<UInt16>(9999, offset);

            Assert.ThrowsException<ArgumentException>(() =>
                registerToOffset.GetDiscreteInputOffset(20000)
                );

            offset = registerToOffset.GetDiscreteInputOffset(100000);
            Assert.AreEqual<UInt16>(0, offset);

            offset = registerToOffset.GetDiscreteInputOffset(10001);
            Assert.AreEqual<UInt16>(1, offset);

            offset = registerToOffset.GetDiscreteInputOffset(109998);
            Assert.AreEqual<UInt16>(9998, offset);

            offset = registerToOffset.GetDiscreteInputOffset(109999);
            Assert.AreEqual<UInt16>(9999, offset);

            offset = registerToOffset.GetDiscreteInputOffset(110000);
            Assert.AreEqual<UInt16>(10000, offset);

            offset = registerToOffset.GetDiscreteInputOffset(165535);
            Assert.AreEqual<UInt16>(65535, offset);

            Assert.ThrowsException<ArgumentException>(() =>
                registerToOffset.GetDiscreteInputOffset(165536)
                );
        }

        [TestMethod]
        public void Test_GetDiscreteInputOffset_400001()
        {
            RegisterToOffset registerToOffset = new RegisterToOffset(400001);

            Assert.ThrowsException<ArgumentException>(() =>
                registerToOffset.GetDiscreteInputOffset(10001)
                );

            Assert.ThrowsException<ArgumentException>(() =>
                registerToOffset.GetDiscreteInputOffset(100000)
                );

            UInt16 offset = registerToOffset.GetDiscreteInputOffset(100001);
            Assert.AreEqual<UInt16>(0, offset);

            offset = registerToOffset.GetDiscreteInputOffset(100002);
            Assert.AreEqual<UInt16>(1, offset);

            offset = registerToOffset.GetDiscreteInputOffset(109999);
            Assert.AreEqual<UInt16>(9998, offset);

            offset = registerToOffset.GetDiscreteInputOffset(110000);
            Assert.AreEqual<UInt16>(9999, offset);

            offset = registerToOffset.GetDiscreteInputOffset(110001);
            Assert.AreEqual<UInt16>(10000, offset);

            offset = registerToOffset.GetDiscreteInputOffset(165535);
            Assert.AreEqual<UInt16>(65534, offset);

            offset = registerToOffset.GetDiscreteInputOffset(165536);
            Assert.AreEqual<UInt16>(65535, offset);

            Assert.ThrowsException<ArgumentException>(() =>
                registerToOffset.GetDiscreteInputOffset(165537)
                );
        }

        [TestMethod]
        public void Test_GetDiscreteInputOffset_400000()
        {
            RegisterToOffset registerToOffset = new RegisterToOffset(400000);

            Assert.ThrowsException<ArgumentException>(() =>
                registerToOffset.GetDiscreteInputOffset(10001)
                );

            UInt16 offset = registerToOffset.GetDiscreteInputOffset(100000);
            Assert.AreEqual<UInt16>(0, offset);

            offset = registerToOffset.GetDiscreteInputOffset(100001);
            Assert.AreEqual<UInt16>(1, offset);

            offset = registerToOffset.GetDiscreteInputOffset(100002);
            Assert.AreEqual<UInt16>(2, offset);

            offset = registerToOffset.GetDiscreteInputOffset(109999);
            Assert.AreEqual<UInt16>(9999, offset);

            offset = registerToOffset.GetDiscreteInputOffset(110000);
            Assert.AreEqual<UInt16>(10000, offset);

            offset = registerToOffset.GetDiscreteInputOffset(165535);
            Assert.AreEqual<UInt16>(65535, offset);

            Assert.ThrowsException<ArgumentException>(() =>
                registerToOffset.GetDiscreteInputOffset(165536)
                );
        }



        [TestMethod]
        public void Test_ConvertDiscreteInputOffsetToNumber_40001()
        {
            RegisterToOffset registerToOffset = new RegisterToOffset(40001);

            int regnr = registerToOffset.ConvertDiscreteInputOffsetToNumber(0);
            Assert.AreEqual<int>(10001, regnr);

            regnr = registerToOffset.ConvertDiscreteInputOffsetToNumber(1);
            Assert.AreEqual<int>(10002, regnr);

            regnr = registerToOffset.ConvertDiscreteInputOffsetToNumber(9998);
            Assert.AreEqual<int>(19999, regnr);

            regnr = registerToOffset.ConvertDiscreteInputOffsetToNumber(9999);
            Assert.AreEqual<int>(110000, regnr);

            regnr = registerToOffset.ConvertDiscreteInputOffsetToNumber(10000);
            Assert.AreEqual<int>(110001, regnr);

            regnr = registerToOffset.ConvertDiscreteInputOffsetToNumber(65535);
            Assert.AreEqual<int>(165536, regnr);
        }

        [TestMethod]
        public void Test_ConvertDiscreteInputOffsetToNumber_40000()
        {
            RegisterToOffset registerToOffset = new RegisterToOffset(40000);

            int regnr = registerToOffset.ConvertDiscreteInputOffsetToNumber(0);
            Assert.AreEqual<int>(10000, regnr);

            regnr = registerToOffset.ConvertDiscreteInputOffsetToNumber(1);
            Assert.AreEqual<int>(10001, regnr);

            regnr = registerToOffset.ConvertDiscreteInputOffsetToNumber(9998);
            Assert.AreEqual<int>(19998, regnr);

            regnr = registerToOffset.ConvertDiscreteInputOffsetToNumber(9999);
            Assert.AreEqual<int>(19999, regnr);

            regnr = registerToOffset.ConvertDiscreteInputOffsetToNumber(10000);
            Assert.AreEqual<int>(110000, regnr);

            regnr = registerToOffset.ConvertDiscreteInputOffsetToNumber(65535);
            Assert.AreEqual<int>(165535, regnr);
        }

        [TestMethod]
        public void Test_ConvertDiscreteInputOffsetToNumber_400001()
        {
            RegisterToOffset registerToOffset = new RegisterToOffset(400001);

            int regnr = registerToOffset.ConvertDiscreteInputOffsetToNumber(0);
            Assert.AreEqual<int>(100001, regnr);

            regnr = registerToOffset.ConvertDiscreteInputOffsetToNumber(1);
            Assert.AreEqual<int>(100002, regnr);

            regnr = registerToOffset.ConvertDiscreteInputOffsetToNumber(9998);
            Assert.AreEqual<int>(109999, regnr);

            regnr = registerToOffset.ConvertDiscreteInputOffsetToNumber(9999);
            Assert.AreEqual<int>(110000, regnr);

            regnr = registerToOffset.ConvertDiscreteInputOffsetToNumber(10000);
            Assert.AreEqual<int>(110001, regnr);

            regnr = registerToOffset.ConvertDiscreteInputOffsetToNumber(65535);
            Assert.AreEqual<int>(165536, regnr);
        }

        [TestMethod]
        public void Test_ConvertDiscreteInputOffsetToNumber_400000()
        {
            RegisterToOffset registerToOffset = new RegisterToOffset(400000);

            int regnr = registerToOffset.ConvertDiscreteInputOffsetToNumber(0);
            Assert.AreEqual<int>(100000, regnr);

            regnr = registerToOffset.ConvertDiscreteInputOffsetToNumber(1);
            Assert.AreEqual<int>(100001, regnr);

            regnr = registerToOffset.ConvertDiscreteInputOffsetToNumber(9998);
            Assert.AreEqual<int>(109998, regnr);

            regnr = registerToOffset.ConvertDiscreteInputOffsetToNumber(9999);
            Assert.AreEqual<int>(109999, regnr);

            regnr = registerToOffset.ConvertDiscreteInputOffsetToNumber(10000);
            Assert.AreEqual<int>(110000, regnr);

            regnr = registerToOffset.ConvertDiscreteInputOffsetToNumber(65535);
            Assert.AreEqual<int>(165535, regnr);
        }

    }

}



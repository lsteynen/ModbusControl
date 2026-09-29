using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LST.Helpers;
using ModbusControl;

using LST.Helpers;

namespace UnitTests
{
    [TestClass]
    public class Test_ModbusDecode
    {
        [TestMethod]
        public void Test0101()
        {
            byte[] Buf = new byte[] { 0x01, 0x01, 0x01, 0x10, 0x00, 0x0A };
            string s = ModbusDecoder.DecodeQuery(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Read coils: addr=273, quant=10",s);
        }

        [TestMethod]
        public void Test0102()
        {
            byte[] Buf = new byte[] { 0x01, 0x02, 0x00, 0x00, 0x00, 50 };
            string s = ModbusDecoder.DecodeQuery(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Read discrete inputs: addr=10001, quant=50",s);
        }


        [TestMethod]
        public void Test0103()
        {
            byte[] Buf = new byte[] { 0x01, 0x03, 0x01, 0x10, 0x00, 0x0A };
            string s = ModbusDecoder.DecodeQuery(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Read holding registers: addr=40273, quant=10",s);
        }

        [TestMethod]
        public void Test_0104()
        {
            byte[] Buf = new byte[] { 0x01, 0x04, 0x01, 0x10, 0x00, 0x0A };
            string s = ModbusDecoder.DecodeQuery(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Read input registers: addr=30273, quant=10",s);
        }

        [TestMethod]
        public void Test0105()
        {
            byte[] Buf = new byte[] { 0x01, 0x05, 0x01, 0x10, 0x00, 0x00 };
            string s = ModbusDecoder.DecodeQuery(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write single coil: addr=273, value=OFF",s);
        }

        [TestMethod]
        public void Test0105_2()
        {
            byte[] Buf = new byte[] { 0x01, 0x05, 0x01, 0x10, 0xFF, 0x00 };
            string s = ModbusDecoder.DecodeQuery(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write single coil: addr=273, value=ON",s);
        }

        [TestMethod]
        public void Test0106()
        {
            byte[] Buf = new byte[] { 0x01, 0x06, 0x02, 0x00, 0x12, 0x34 };
            string s = ModbusDecoder.DecodeQuery(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write single register: addr=40513, value=4660", s);
        }

        [TestMethod]
        public void Test0115()
        {
            byte[] Buf = new byte[] { 0x01, 15, 0x10, 0x20, 0x00, 50, 7, 1,2,3,4,5,6,7 };
            string s = ModbusDecoder.DecodeQuery(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write multiple coils: addr=4129, quant=50", s);
        }

        [TestMethod]
        public void Test0116()
        {
            byte[] Buf = new byte[] { 0x01, 16, 0x02, 0x00, 0x00, 8, 16, 1,2,3,4,5,6,7,8,9,10,11,12,13,14,0x90,0xA0 };
            string s = ModbusDecoder.DecodeQuery(Buf, Buf.Length);
        }

        [TestMethod]
        public void Test0200()
        {
            // too short
            byte[] Buf = new byte[] {};
            string s = ModbusDecoder.DecodeQuery(Buf, Buf.Length);
            Assert.AreEqual("DECODE: ERROR: QUERY TOO SHORT!", s);
        }

        [TestMethod]
        public void Test020100()
        {
            // too short
            byte[] Buf = new byte[] { 0x01 };
            string s = ModbusDecoder.DecodeQuery(Buf, Buf.Length);
            Assert.AreEqual("DECODE: ERROR: QUERY TOO SHORT!", s);
        }

        [TestMethod]
        public void Test020101()
        {
            // FC 01 too short
            byte[] Buf = new byte[] { 0x01, 0x01 };
            string s = ModbusDecoder.DecodeQuery(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Read coils: ERROR: QUERY TOO SHORT!", s);
        }

        [TestMethod]
        public void Test020102()
        {
            // FC 01 too short
            byte[] Buf = new byte[] { 0x01, 0x01, 0x01, 0x10, 0x00 };
            string s = ModbusDecoder.DecodeQuery(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Read coils: ERROR: QUERY TOO SHORT!", s);
        }

        [TestMethod]
        public void Test020103()
        {
            // FC 01 too long
            byte[] Buf = new byte[] { 0x01, 0x01, 0x01, 0x10, 0x00, 0x0A, 0xFF };
            string s = ModbusDecoder.DecodeQuery(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Read coils: addr=273, quant=10 ERROR: QUERY TOO LONG!", s);
        }

        [TestMethod]
        public void Test020201()
        {
            // FC 02 too short
            byte[] Buf = new byte[] { 0x01, 0x02, 0x01, 0x10, 0x00 };
            string s = ModbusDecoder.DecodeQuery(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Read discrete inputs:ERROR: QUERY TOO SHORT!", s);
        }

        [TestMethod]
        public void Test020202()
        {
            // FC 02 too long
            byte[] Buf = new byte[] { 0x01, 0x02, 0x01, 0x10, 0x00, 0x0A, 0xFF };
            string s = ModbusDecoder.DecodeQuery(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Read discrete inputs: addr=10273, quant=10 ERROR: QUERY TOO LONG!", s);
        }

        [TestMethod]
        public void Test020301()
        {
            // FC 03 too short
            byte[] Buf = new byte[] { 0x01, 0x03, 0x01, 0x10, 0x00 };
            string s = ModbusDecoder.DecodeQuery(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Read holding registers: ERROR: QUERY TOO SHORT!", s);
        }

        [TestMethod]
        public void Test020302()
        {
            // FC 03 too long
            byte[] Buf = new byte[] { 0x01, 0x03, 0x01, 0x10, 0x00, 0x0A, 0xFF };
            string s = ModbusDecoder.DecodeQuery(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Read holding registers: addr=40273, quant=10 ERROR: QUERY TOO LONG!", s);
        }

        [TestMethod]
        public void Test020401()
        {
            // FC 04 too short
            byte[] Buf = new byte[] { 0x01, 0x04, 0x01, 0x10, 0x00 };
            string s = ModbusDecoder.DecodeQuery(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Read input registers: ERROR: QUERY TOO SHORT!", s);
        }

        [TestMethod]
        public void Test020402()
        {
            // FC 04 too long
            byte[] Buf = new byte[] { 0x01, 0x04, 0x01, 0x10, 0x00, 0x0A, 0xFF };
            string s = ModbusDecoder.DecodeQuery(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Read input registers: addr=30273, quant=10 ERROR: QUERY TOO LONG!", s);
        }

        [TestMethod]
        public void Test020501()
        {
            // FC 05 too short
            byte[] Buf = new byte[] { 0x01, 0x05, 0x01, 0x10, 0x00 };
            string s = ModbusDecoder.DecodeQuery(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write single coil: ERROR: QUERY TOO SHORT!", s);
        }

        [TestMethod]
        public void Test020502()
        {
            // FC 05 invalid data
            byte[] Buf = new byte[] { 0x01, 0x05, 0x01, 0x10, 0x00, 0x01 };
            string s = ModbusDecoder.DecodeQuery(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write single coil: addr=273, value=ERROR: ILLEGAL VALUE!", s);
        }

        [TestMethod]
        public void Test020503()
        {
           // FC 05 too long
            byte[] Buf = new byte[] { 0x01, 0x05, 0x01, 0x10, 0x00, 0x00, 0xFF };
            string s = ModbusDecoder.DecodeQuery(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write single coil: addr=273, value=OFF ERROR: QUERY TOO LONG!", s);
        }

        [TestMethod]
        public void Test020601()
        {
            // FC 06 too short
            byte[] Buf = new byte[] { 0x01, 0x06, 0x01, 0x10, 0x00 };
            string s = ModbusDecoder.DecodeQuery(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write single register: ERROR: QUERY TOO SHORT!", s);
        }

        [TestMethod]
        public void Test020602()
        {
            // FC 06 too long
            byte[] Buf = new byte[] { 0x01, 0x06, 0x01, 0x10, 0x00, 0x01, 0xFF };
            string s = ModbusDecoder.DecodeQuery(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write single register: addr=40273, value=1 ERROR: QUERY TOO LONG!", s);
        }

        [TestMethod]
        public void Test021501()
        {
            // FC 15 too short
            byte[] Buf = new byte[] { 0x01, 15, 0x01, 0x10, 0x00, 0xDD };
            string s = ModbusDecoder.DecodeQuery(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write multiple coils: ERROR: QUERY TOO SHORT!", s);
        }

        [TestMethod]
        public void Test021502()
        {
            // FC 15 bytecount does not match quant
            byte[] Buf = new byte[] { 0x01, 15, 0x01, 0x10, 0x00, 0x0A, 1,  };
            string s = ModbusDecoder.DecodeQuery(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write multiple coils: addr=273, quant=10 ERROR: BYTECOUNT DOES NOT MATCH QUANTITY! ERROR: QUERY TOO SHORT!", s);
        }

        [TestMethod]
        public void Test021503()
        {
            // FC 15 bytecount does not match quant
            byte[] Buf = new byte[] { 0x01, 15, 0x01, 0x10, 0x00, 0x0A, 3, };
            string s = ModbusDecoder.DecodeQuery(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write multiple coils: addr=273, quant=10 ERROR: BYTECOUNT DOES NOT MATCH QUANTITY! ERROR: QUERY TOO SHORT!", s);
        }

        [TestMethod]
        public void Test021504()
        {
            // FC 15 too short
            byte[] Buf = new byte[] { 0x01, 15, 0x01, 0x10, 0x00, 0x0A, 2, 0x55 };
            string s = ModbusDecoder.DecodeQuery(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write multiple coils: addr=273, quant=10 ERROR: QUERY TOO SHORT!", s);
        }

        [TestMethod]
        public void Test021505()
        {
            // FC 15 too long
            byte[] Buf = new byte[] { 0x01, 15, 0x01, 0x10, 0x00, 0x0A, 2, 0x55, 0x66, 0x77 };
            string s = ModbusDecoder.DecodeQuery(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write multiple coils: addr=273, quant=10 ERROR: QUERY TOO LONG!", s);
        }

        [TestMethod]
        public void Test021601()
        {
            // FC 16 too short
            byte[] Buf = new byte[] { 0x01, 16, 0x01, 0x10, 0x00, 0xDD };
            string s = ModbusDecoder.DecodeQuery(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write multiple registers: ERROR: QUERY TOO SHORT!", s);
        }

        [TestMethod]
        public void Test021602()
        {
            // FC 16 bytecount does not match quant
            byte[] Buf = new byte[] { 0x01, 16, 0x01, 0x10, 0x00, 0x0A, 19, };
            string s = ModbusDecoder.DecodeQuery(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write multiple registers: addr=40273, quant=10 ERROR: BYTECOUNT DOES NOT MATCH QUANTITY! ERROR: QUERY TOO SHORT!", s);
        }

        [TestMethod]
        public void Test021603()
        {
            // FC 16 bytecount does not match quant
            byte[] Buf = new byte[] { 0x01, 16, 0x01, 0x10, 0x00, 0x0A, 21, };
            string s = ModbusDecoder.DecodeQuery(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write multiple registers: addr=40273, quant=10 ERROR: BYTECOUNT DOES NOT MATCH QUANTITY! ERROR: QUERY TOO SHORT!", s);
        }

        [TestMethod]
        public void Test021604()
        {
            // FC 16 too short
            byte[] Buf = new byte[] { 0x01, 16, 0x01, 0x10, 0x00, 0x0A, 20, 1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19 };
            string s = ModbusDecoder.DecodeQuery(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write multiple registers: addr=40273, quant=10 ERROR: QUERY TOO SHORT!", s);
        }

        [TestMethod]
        public void Test021605()
        {
            // FC 16 too long
            byte[] Buf = new byte[] { 0x01, 16, 0x01, 0x10, 0x00, 0x0A, 20, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19,20,21 };
            string s = ModbusDecoder.DecodeQuery(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write multiple registers: addr=40273, quant=10, values=258,772,1286,1800,2314,2828,3342,3856,4370,4884 ERROR: QUERY TOO LONG!", s);
        }

        [TestMethod]
        public void Test029901()
        {
            // unknown function code
            byte[] Buf = new byte[] { 0x01, 0x99, 0x01, 0x02, 0x03 };
            string s = ModbusDecoder.DecodeQuery(Buf, Buf.Length);
            Assert.AreEqual("Slave=1,(not decoded)", s);
        }

        [TestMethod]
        public void Test030101()
        {
            byte[] Buf = new byte[] { 0x01, 0x01, 3, 0x10, 0x11, 0x12 };
            string s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Read coils: bytecount=3", s);
        }

        [TestMethod]
        public void Test030201()
        {
            byte[] Buf = new byte[] { 0x01, 0x02, 1, 0x20 };
            string s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Read discrete inputs: bytecount=1", s);
        }

        [TestMethod]
        public void Test030301()
        {
            byte[] Buf = new byte[] { 0x01, 0x03, 8, 0x61,0xA9,0x61,0xAA,0xE6,0x71,0xE6,0x70 };
            string s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Read holding registers: values=25001,25002,-6543,-6544", s);
        }

        [TestMethod]
        public void Test030401()
        {
            byte[] Buf = new byte[] { 0x01, 0x04, 2, 0x12, 0x34 };
            string s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Read input registers: values=4660", s);
        }

        [TestMethod]
        public void Test030501()
        {
            byte[] Buf = new byte[] { 0x01, 0x05, 0x01, 0x02, 0xFF, 0x00 };
            string s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write single coil: addr=259, value=ON", s);
        }

        [TestMethod]
        public void Test030601()
        {
            byte[] Buf = new byte[] { 0x01, 0x06, 0x01, 0x02, 0x12, 0x34 };
            string s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write single register: addr=40259, value=4660", s);
        }

        [TestMethod]
        public void Test031501()
        {
            byte[] Buf = new byte[] { 0x01, 15, 0x01, 0x02, 0, 10 };
            string s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write multiple coils: addr=259, quant=10", s);
        }

        [TestMethod]
        public void Test031601()
        {
            byte[] Buf = new byte[] { 0x01, 16, 0x01, 0x02, 0, 10 };
            string s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write multiple registers: addr=40259, quant=10", s);
        }

        [TestMethod]
        public void Test0401()
        {
            byte[] Buf = new byte[] { 0x01, 0x80 + 1 };
            string s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("DECODE: ERROR: RESPONSE TOO SHORT!", s);
        }

        [TestMethod]
        public void Test040102()
        {
            byte[] Buf = new byte[] { 0x01, 0x80 + 1, 0x01 };
            string s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Read coils: Exception: EXCEPTION 1 (ILLEGAL FUNCTION)", s);
        }

        [TestMethod]
        public void Test0402()
        {
            byte[] Buf = new byte[] { 0x01, 0x80 + 2, 0x02 };
            string s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Read discrete inputs: Exception: EXCEPTION 2 (ILLEGAL ADDRESS)", s);
        }

        [TestMethod]
        public void Test0403()
        {
            byte[] Buf = new byte[] { 0x01, 0x80 + 3, 0x03 };
            string s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Read holding registers: Exception: EXCEPTION 3 (ILLEGAL DATA VALUE)", s);
        }

        [TestMethod]
        public void Test0404()
        {
            byte[] Buf = new byte[] { 0x01, 0x80 + 4, 0x04 };
            string s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Read input registers: Exception: EXCEPTION 4 (SLAVE DEVICE FAILURE)", s);
        }

        [TestMethod]
        public void Test0405()
        {
            byte[] Buf = new byte[] { 0x01, 0x80 + 5, 0x05 };
            string s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write single coil: Exception: EXCEPTION 5 (ACKNOWLEDGE FOR LONG DURATION)", s);
        }

        [TestMethod]
        public void Test0406()
        {
            byte[] Buf = new byte[] { 0x01, 0x80 + 6, 0x06 };
            string s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write single register: Exception: EXCEPTION 6 (SLAVE DEVICE BUSY)", s);
        }

        [TestMethod]
        public void Test041501()
        {
            byte[] Buf = new byte[] { 0x01, 0x80 + 15, 10 };
            string s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write multiple coils: Exception: EXCEPTION 10 (GATEWAY PATH UNAVAILABLE)", s);
        }

        [TestMethod]
        public void Test041502()
        {
            byte[] Buf = new byte[] { 0x01, 0x80 + 15, 11 };
            string s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write multiple coils: Exception: EXCEPTION 11 (GATEWAY TARGET DEVICE FAILED TO RESPOND)", s);
        }

        [TestMethod]
        public void Test0416()
        {
            byte[] Buf = new byte[] { 0x01, 0x80 + 16, 99 };
            string s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write multiple registers: Exception: EXCEPTION 99", s);
        }

        // too short

        [TestMethod]
        public void Test050001()
        {
            byte[] Buf = new byte[] { };
            string s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("DECODE: ERROR: RESPONSE TOO SHORT!", s);
        }

        [TestMethod]
        public void Test050002()
        {
            byte[] Buf = new byte[] { 0x01 };
            string s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("DECODE: ERROR: RESPONSE TOO SHORT!", s);
        }

        // fc 1

        [TestMethod]
        public void Test050101()
        {
            byte[] Buf = new byte[] { 0x01, 0x01 };
            string s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("DECODE: ERROR: RESPONSE TOO SHORT!", s);
        }

        [TestMethod]
        public void Test050102()
        {
            byte[] Buf = new byte[] { 0x01, 0x01, 0x03, 0x21, 0x22 };
            string s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Read coils: bytecount=3 ERROR: RESPONSE HAS LESS DATABYTES THEN BYTECOUNT!", s);
        }

        [TestMethod]
        public void Test050103()
        {
            byte[] Buf = new byte[] { 0x01, 0x01, 0x03, 0x21, 0x22, 0x23, 0x22 };
            string s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Read coils: bytecount=3 ERROR: RESPONSE HAS MORE DATABYTES THEN BYTECOUNT!", s);
        }

        [TestMethod]
        public void Test050104()
        {
            byte[] Buf = new byte[] { 0x01, 0x80 + 1 };
            string s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("DECODE: ERROR: RESPONSE TOO SHORT!", s);
        }

        [TestMethod]
        public void Test050105()
        {
            byte[] Buf = new byte[] { 0x01, 0x80 + 1, 0x01, 0x02 };
            string s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Read coils: Exception: EXCEPTION 1 (ILLEGAL FUNCTION) ERROR: RESPONSE TOO LONG!", s);
        }

        // fc 2

        [TestMethod]
        public void Test050201()
        {
            byte[] Buf = new byte[] { 0x01, 0x02 };
            string s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("DECODE: ERROR: RESPONSE TOO SHORT!", s);
        }

        [TestMethod]
        public void Test050202()
        {
            byte[] Buf = new byte[] { 0x01, 0x02, 0x03, 0x21, 0x22 };
            string s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Read discrete inputs: bytecount=3 ERROR: RESPONSE HAS LESS DATABYTES THEN BYTECOUNT!", s);
        }

        [TestMethod]
        public void Test050203()
        {
            byte[] Buf = new byte[] { 0x01, 0x02, 0x03, 0x21, 0x22, 0x23, 0x22 };
            string s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Read discrete inputs: bytecount=3 ERROR: RESPONSE HAS MORE DATABYTES THEN BYTECOUNT!", s);
        }

        [TestMethod]
        public void Test050204()
        {
            byte[] Buf = new byte[] { 0x01, 0x80 + 2 };
            string s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("DECODE: ERROR: RESPONSE TOO SHORT!", s);
        }

        [TestMethod]
        public void Test050205()
        {
            byte[] Buf = new byte[] { 0x01, 0x80 + 2, 0x01, 0x02 };
            string s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Read discrete inputs: Exception: EXCEPTION 1 (ILLEGAL FUNCTION) ERROR: RESPONSE TOO LONG!", s);
        }

        // fc 3

        [TestMethod]
        public void Test050301()
        {
            byte[] Buf = new byte[] { 0x01, 0x03 };
            string s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("DECODE: ERROR: RESPONSE TOO SHORT!", s);
        }

        [TestMethod]
        public void Test050302()
        {
            byte[] Buf = new byte[] { 0x01, 0x03, 0x05 };
            string s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Read holding registers: ERROR: BYTECOUNT IS NOT EVEN! ERROR: RESPONSE HAS LESS DATABYTES THEN BYTECOUNT!", s);
        }

        [TestMethod]
        public void Test050303()
        {
            byte[] Buf = new byte[] { 0x01, 0x03, 0x06, 0x21, 0x22, 0x23, 0x24, 0x25 };
            string s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Read holding registers: ERROR: RESPONSE HAS LESS DATABYTES THEN BYTECOUNT!", s);
        }

        [TestMethod]
        public void Test050304()
        {
            byte[] Buf = new byte[] { 0x01, 0x03, 0x06, 0x21, 0x22, 0x23, 0x24, 0x25, 0x26, 0x27 };
            string s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Read holding registers: values=8482,8996,9510 ERROR: RESPONSE HAS MORE DATABYTES THEN BYTECOUNT!", s);
        }

        [TestMethod]
        public void Test050306()
        {
            byte[] Buf = new byte[] { 0x01, 0x80 + 3 };
            string s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("DECODE: ERROR: RESPONSE TOO SHORT!", s);
        }

        [TestMethod]
        public void Test050307()
        {
            byte[] Buf = new byte[] { 0x01, 0x80 + 3, 0x01, 0x02 };
            string s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Read holding registers: Exception: EXCEPTION 1 (ILLEGAL FUNCTION) ERROR: RESPONSE TOO LONG!", s);
        }

        // fc 4 

        [TestMethod]
        public void Test050401()
        {
            byte[] Buf = new byte[] { 0x01, 0x04 };
            string s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("DECODE: ERROR: RESPONSE TOO SHORT!", s);
        }

        [TestMethod]
        public void Test050402()
        {
            byte[] Buf = new byte[] { 0x01, 0x04, 0x05 };
            string s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Read input registers: ERROR: BYTECOUNT IS NOT EVEN! ERROR: RESPONSE HAS LESS DATABYTES THEN BYTECOUNT!", s);
        }

        [TestMethod]
        public void Test050403()
        {
            byte[] Buf = new byte[] { 0x01, 0x04, 0x06, 0x21, 0x22, 0x23, 0x24, 0x25 };
            string s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Read input registers: ERROR: RESPONSE HAS LESS DATABYTES THEN BYTECOUNT!", s);
        }

        [TestMethod]
        public void Test050404()
        {
            byte[] Buf = new byte[] { 0x01, 0x04, 0x06, 0x21, 0x22, 0x23, 0x24, 0x25, 0x26, 0x27 };
            string s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Read input registers: values=8482,8996,9510 ERROR: RESPONSE HAS MORE DATABYTES THEN BYTECOUNT!", s);
        }

        [TestMethod]
        public void Test050405()
        {
            byte[] Buf = new byte[] { 0x01, 0x80 + 4 };
            string s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("DECODE: ERROR: RESPONSE TOO SHORT!", s);
        }

        [TestMethod]
        public void Test050406()
        {
            byte[] Buf = new byte[] { 0x01, 0x80 + 4, 0x01, 0x02 };
            string s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Read input registers: Exception: EXCEPTION 1 (ILLEGAL FUNCTION) ERROR: RESPONSE TOO LONG!", s);
        }

        // fc 5

        [TestMethod]
        public void Test050501()
        {
            byte[] Buf = new byte[] { 0x01, 0x05 };
            string s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("DECODE: ERROR: RESPONSE TOO SHORT!", s);
        }

        [TestMethod]
        public void Test050502()
        {
            byte[] Buf = new byte[] { 0x01, 0x05, 0x05 };
            string s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write single coil: ERROR: RESPONSE TOO SHORT!", s);
        }

        [TestMethod]
        public void Test050503()
        {
            byte[] Buf = new byte[] { 0x01, 0x05, 0x10, 0x11, 0x23 };
            string s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write single coil: ERROR: RESPONSE TOO SHORT!", s);
        }

        [TestMethod]
        public void Test050504()
        {
            byte[] Buf = new byte[] { 0x01, 0x05, 0x10, 0x11, 0x00, 0xFF };
            string s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write single coil: addr=4114, value=ERROR: ILLEGAL VALUE!", s);
        }

        [TestMethod]
        public void Test050505()
        {
            byte[] Buf = new byte[] { 0x01, 0x05, 0x10, 0x11, 0x00, 0x00, 0x21 };
            string s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write single coil: addr=4114, value=OFF ERROR: RESPONSE TOO LONG!", s);
        }

        [TestMethod]
        public void Test050506()
        {
            byte[] Buf = new byte[] { 0x01, 0x80 + 5 };
            string s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("DECODE: ERROR: RESPONSE TOO SHORT!", s);
        }

        [TestMethod]
        public void Test050507()
        {
            byte[] Buf = new byte[] { 0x01, 0x80 + 5, 0x01, 0x02 };
            string s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write single coil: Exception: EXCEPTION 1 (ILLEGAL FUNCTION) ERROR: RESPONSE TOO LONG!", s);
        }

        // fc 6

        [TestMethod]
        public void Test050601()
        {
            byte[] Buf = new byte[] { 0x01, 0x06 };
            string s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("DECODE: ERROR: RESPONSE TOO SHORT!", s);
        }

        [TestMethod]
        public void Test050602()
        {
            byte[] Buf = new byte[] { 0x01, 0x06, 0x05 };
            string s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write single register: ERROR: RESPONSE TOO SHORT!", s);
        }

        [TestMethod]
        public void Test050603()
        {
            byte[] Buf = new byte[] { 0x01, 0x06, 0x10, 0x11, 0x23 };
            string s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write single register: ERROR: RESPONSE TOO SHORT!", s);
        }

        [TestMethod]
        public void Test050604()
        {
            byte[] Buf = new byte[] { 0x01, 0x06, 0x10, 0x11, 0x01, 0x02, 0x21 };
            string s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write single register: addr=44114, value=258 ERROR: RESPONSE TOO LONG!", s);
        }

        [TestMethod]
        public void Test050605()
        {
            byte[] Buf = new byte[] { 0x01, 0x80 + 6 };
            string s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("DECODE: ERROR: RESPONSE TOO SHORT!", s);
        }

        [TestMethod]
        public void Test050606()
        {
            byte[] Buf = new byte[] { 0x01, 0x80 + 6, 0x01, 0x02 };
            string s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write single register: Exception: EXCEPTION 1 (ILLEGAL FUNCTION) ERROR: RESPONSE TOO LONG!", s);
        }

        // fc 15

        [TestMethod]
        public void Test051501()
        {
            byte[] Buf = new byte[] { 0x01, 15 };
            string s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("DECODE: ERROR: RESPONSE TOO SHORT!", s);
        }

        [TestMethod]
        public void Test051502()
        {
            byte[] Buf = new byte[] { 0x01, 15, 0x05 };
            string s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write multiple coils: ERROR: RESPONSE TOO SHORT!", s);
        }

        [TestMethod]
        public void Test051503()
        {
            byte[] Buf = new byte[] { 0x01, 15, 0x10, 0x11, 0x23 };
            string s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write multiple coils: ERROR: RESPONSE TOO SHORT!", s);
        }

        [TestMethod]
        public void Test051504()
        {
            byte[] Buf = new byte[] { 0x01, 15, 0x10, 0x11, 0x01, 0x02, 0x21 };
            string s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write multiple coils: addr=4114, quant=258 ERROR: RESPONSE TOO LONG!", s);
        }

        [TestMethod]
        public void Test051505()
        {
            byte[] Buf = new byte[] { 0x01, 0x80 + 15 };
            string s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("DECODE: ERROR: RESPONSE TOO SHORT!", s);
        }

        [TestMethod]
        public void Test051506()
        {
            byte[] Buf = new byte[] { 0x01, 0x80 + 15, 0x01, 0x02 };
            string s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write multiple coils: Exception: EXCEPTION 1 (ILLEGAL FUNCTION) ERROR: RESPONSE TOO LONG!", s);
        }
        
        // fc 16

        [TestMethod]
        public void Test051601()
        {
            byte[] Buf = new byte[] { 0x01, 16 };
            string s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("DECODE: ERROR: RESPONSE TOO SHORT!", s);
        }

        [TestMethod]
        public void Test051602()
        {
            byte[] Buf = new byte[] { 0x01, 16, 0x05 };
            string s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write multiple registers: ERROR: RESPONSE TOO SHORT!", s);
        }

        [TestMethod]
        public void Test051603()
        {
            byte[] Buf = new byte[] { 0x01, 16, 0x10, 0x11, 0x23 };
            string s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write multiple registers: ERROR: RESPONSE TOO SHORT!", s);
        }

        [TestMethod]
        public void Test051604()
        {
            byte[] Buf = new byte[] { 0x01, 16, 0x10, 0x11, 0x01, 0x02, 0x21 };
            string s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write multiple registers: addr=44114, quant=258 ERROR: RESPONSE TOO LONG!", s);
        }

        [TestMethod]
        public void Test051605()
        {
            byte[] Buf = new byte[] { 0x01, 0x80 + 16 };
            string s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("DECODE: ERROR: RESPONSE TOO SHORT!", s);
        }

        [TestMethod]
        public void Test051606()
        {
            byte[] Buf = new byte[] { 0x01, 0x80 + 16, 0x01, 0x02 };
            string s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write multiple registers: Exception: EXCEPTION 1 (ILLEGAL FUNCTION) ERROR: RESPONSE TOO LONG!", s);
        }

        // unsupported fc

        [TestMethod]
        public void Test0601()
        {
            byte[] Buf = new byte[] { 0x01, 0x99, 0x03 };
            string s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1,(not decoded)", s);
        }
		
    }
}

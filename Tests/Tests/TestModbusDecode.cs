using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using NUnit.Framework;

using LST.Helpers;

namespace TestProject
{
    [TestFixture]
    public class TestModbusDecode
    {
        static private byte[] Buf;
        static private string s;

        [Test]
        public void Test0101()
        {
            Buf = new byte[] { 0x01, 0x01, 0x01, 0x10, 0x00, 0x0A };
            s = ModbusDecoder.DecodeQuery(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Read coils: addr=273, quant=10",s);
        }

        [Test]
        public void Test0102()
        {
            Buf = new byte[] { 0x01, 0x02, 0x00, 0x00, 0x00, 50 };
            s = ModbusDecoder.DecodeQuery(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Read discrete inputs: addr=10001, quant=50",s);
        }


        [Test]
        public void Test0103()
        {
            Buf = new byte[] { 0x01, 0x03, 0x01, 0x10, 0x00, 0x0A };
            s = ModbusDecoder.DecodeQuery(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Read holding registers: addr=40273, quant=10",s);
        }

        [Test]
        public void Test0104()
        {
            Buf = new byte[] { 0x01, 0x04, 0x01, 0x10, 0x00, 0x0A };
            s = ModbusDecoder.DecodeQuery(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Read input registers: addr=30273, quant=10",s);
        }

        [Test]
        public void Test0105()
        {
            Buf = new byte[] { 0x01, 0x05, 0x01, 0x10, 0x00, 0x00 };
            s = ModbusDecoder.DecodeQuery(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write single coil: addr=273, value=OFF",s);
        }

        [Test]
        public void Test010502()
        {
            Buf = new byte[] { 0x01, 0x05, 0x01, 0x10, 0xFF, 0x00 };
            s = ModbusDecoder.DecodeQuery(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write single coil: addr=273, value=ON",s);
        }

        [Test]
        public void Test0106()
        {
            Buf = new byte[] { 0x01, 0x06, 0x02, 0x00, 0x12, 0x34 };
            s = ModbusDecoder.DecodeQuery(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write single register: addr=40513, value=4660", s);
        }

        [Test]
        public void Test0115()
        {
            Buf = new byte[] { 0x01, 15, 0x10, 0x20, 0x00, 50, 7, 1,2,3,4,5,6,7 };
            s = ModbusDecoder.DecodeQuery(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write multiple coils: addr=4129, quant=50", s);
        }

        [Test]
        public void Test0116()
        {
            Buf = new byte[] { 0x01, 16, 0x02, 0x00, 0x00, 8, 16, 1,2,3,4,5,6,7,8,9,10,11,12,13,14,0x90,0xA0 };
            s = ModbusDecoder.DecodeQuery(Buf, Buf.Length);
        }

        [Test]
        public void Test0200()
        {
            // too short
            Buf = new byte[] {};
            s = ModbusDecoder.DecodeQuery(Buf, Buf.Length);
            Assert.AreEqual("DECODE: ERROR: QUERY TOO SHORT!", s);
        }

        [Test]
        public void Test020100()
        {
            // too short
            Buf = new byte[] { 0x01 };
            s = ModbusDecoder.DecodeQuery(Buf, Buf.Length);
            Assert.AreEqual("DECODE: ERROR: QUERY TOO SHORT!", s);
        }

        [Test]
        public void Test020101()
        {
            // FC 01 too short
            Buf = new byte[] { 0x01, 0x01 };
            s = ModbusDecoder.DecodeQuery(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Read coils: ERROR: QUERY TOO SHORT!", s);
        }

        [Test]
        public void Test020102()
        {
            // FC 01 too short
            Buf = new byte[] { 0x01, 0x01, 0x01, 0x10, 0x00 };
            s = ModbusDecoder.DecodeQuery(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Read coils: ERROR: QUERY TOO SHORT!", s);
        }

        [Test]
        public void Test020103()
        {
            // FC 01 too long
            Buf = new byte[] { 0x01, 0x01, 0x01, 0x10, 0x00, 0x0A, 0xFF };
            s = ModbusDecoder.DecodeQuery(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Read coils: addr=273, quant=10 ERROR: QUERY TOO LONG!", s);
        }

        [Test]
        public void Test020201()
        {
            // FC 02 too short
            Buf = new byte[] { 0x01, 0x02, 0x01, 0x10, 0x00 };
            s = ModbusDecoder.DecodeQuery(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Read discrete inputs:ERROR: QUERY TOO SHORT!", s);
        }

        [Test]
        public void Test020202()
        {
            // FC 02 too long
            Buf = new byte[] { 0x01, 0x02, 0x01, 0x10, 0x00, 0x0A, 0xFF };
            s = ModbusDecoder.DecodeQuery(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Read discrete inputs: addr=10273, quant=10 ERROR: QUERY TOO LONG!", s);
        }

        [Test]
        public void Test020301()
        {
            // FC 03 too short
            Buf = new byte[] { 0x01, 0x03, 0x01, 0x10, 0x00 };
            s = ModbusDecoder.DecodeQuery(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Read holding registers: ERROR: QUERY TOO SHORT!", s);
        }

        [Test]
        public void Test020302()
        {
            // FC 03 too long
            Buf = new byte[] { 0x01, 0x03, 0x01, 0x10, 0x00, 0x0A, 0xFF };
            s = ModbusDecoder.DecodeQuery(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Read holding registers: addr=40273, quant=10 ERROR: QUERY TOO LONG!", s);
        }

        [Test]
        public void Test020401()
        {
            // FC 04 too short
            Buf = new byte[] { 0x01, 0x04, 0x01, 0x10, 0x00 };
            s = ModbusDecoder.DecodeQuery(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Read input registers: ERROR: QUERY TOO SHORT!", s);
        }

        [Test]
        public void Test020402()
        {
            // FC 04 too long
            Buf = new byte[] { 0x01, 0x04, 0x01, 0x10, 0x00, 0x0A, 0xFF };
            s = ModbusDecoder.DecodeQuery(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Read input registers: addr=30273, quant=10 ERROR: QUERY TOO LONG!", s);
        }

        [Test]
        public void Test020501()
        {
            // FC 05 too short
            Buf = new byte[] { 0x01, 0x05, 0x01, 0x10, 0x00 };
            s = ModbusDecoder.DecodeQuery(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write single coil: ERROR: QUERY TOO SHORT!", s);
        }

        [Test]
        public void Test020502()
        {
            // FC 05 invalid data
            Buf = new byte[] { 0x01, 0x05, 0x01, 0x10, 0x00, 0x01 };
            s = ModbusDecoder.DecodeQuery(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write single coil: addr=273, value=ERROR: ILLEGAL VALUE!", s);
        }

        [Test]
        public void Test020503()
        {
           // FC 05 too long
            Buf = new byte[] { 0x01, 0x05, 0x01, 0x10, 0x00, 0x00, 0xFF };
            s = ModbusDecoder.DecodeQuery(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write single coil: addr=273, value=OFF ERROR: QUERY TOO LONG!", s);
        }

        [Test]
        public void Test020601()
        {
            // FC 06 too short
            Buf = new byte[] { 0x01, 0x06, 0x01, 0x10, 0x00 };
            s = ModbusDecoder.DecodeQuery(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write single register: ERROR: QUERY TOO SHORT!", s);
        }

        [Test]
        public void Test020602()
        {
            // FC 06 too long
            Buf = new byte[] { 0x01, 0x06, 0x01, 0x10, 0x00, 0x01, 0xFF };
            s = ModbusDecoder.DecodeQuery(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write single register: addr=40273, value=1 ERROR: QUERY TOO LONG!", s);
        }

        [Test]
        public void Test021501()
        {
            // FC 15 too short
            Buf = new byte[] { 0x01, 15, 0x01, 0x10, 0x00, 0xDD };
            s = ModbusDecoder.DecodeQuery(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write multiple coils: ERROR: QUERY TOO SHORT!", s);
        }

        [Test]
        public void Test021502()
        {
            // FC 15 bytecount does not match quant
            Buf = new byte[] { 0x01, 15, 0x01, 0x10, 0x00, 0x0A, 1,  };
            s = ModbusDecoder.DecodeQuery(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write multiple coils: addr=273, quant=10 ERROR: BYTECOUNT DOES NOT MATCH QUANTITY! ERROR: QUERY TOO SHORT!", s);
        }

        [Test]
        public void Test021503()
        {
            // FC 15 bytecount does not match quant
            Buf = new byte[] { 0x01, 15, 0x01, 0x10, 0x00, 0x0A, 3, };
            s = ModbusDecoder.DecodeQuery(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write multiple coils: addr=273, quant=10 ERROR: BYTECOUNT DOES NOT MATCH QUANTITY! ERROR: QUERY TOO SHORT!", s);
        }

        [Test]
        public void Test021504()
        {
            // FC 15 too short
            Buf = new byte[] { 0x01, 15, 0x01, 0x10, 0x00, 0x0A, 2, 0x55 };
            s = ModbusDecoder.DecodeQuery(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write multiple coils: addr=273, quant=10 ERROR: QUERY TOO SHORT!", s);
        }

        [Test]
        public void Test021505()
        {
            // FC 15 too long
            Buf = new byte[] { 0x01, 15, 0x01, 0x10, 0x00, 0x0A, 2, 0x55, 0x66, 0x77 };
            s = ModbusDecoder.DecodeQuery(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write multiple coils: addr=273, quant=10 ERROR: QUERY TOO LONG!", s);
        }

        [Test]
        public void Test021601()
        {
            // FC 16 too short
            Buf = new byte[] { 0x01, 16, 0x01, 0x10, 0x00, 0xDD };
            s = ModbusDecoder.DecodeQuery(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write multiple registers: ERROR: QUERY TOO SHORT!", s);
        }

        [Test]
        public void Test021602()
        {
            // FC 16 bytecount does not match quant
            Buf = new byte[] { 0x01, 16, 0x01, 0x10, 0x00, 0x0A, 19, };
            s = ModbusDecoder.DecodeQuery(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write multiple registers: addr=40273, quant=10 ERROR: BYTECOUNT DOES NOT MATCH QUANTITY! ERROR: QUERY TOO SHORT!", s);
        }

        [Test]
        public void Test021603()
        {
            // FC 16 bytecount does not match quant
            Buf = new byte[] { 0x01, 16, 0x01, 0x10, 0x00, 0x0A, 21, };
            s = ModbusDecoder.DecodeQuery(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write multiple registers: addr=40273, quant=10 ERROR: BYTECOUNT DOES NOT MATCH QUANTITY! ERROR: QUERY TOO SHORT!", s);
        }

        [Test]
        public void Test021604()
        {
            // FC 16 too short
            Buf = new byte[] { 0x01, 16, 0x01, 0x10, 0x00, 0x0A, 20, 1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19 };
            s = ModbusDecoder.DecodeQuery(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write multiple registers: addr=40273, quant=10 ERROR: QUERY TOO SHORT!", s);
        }

        [Test]
        public void Test021605()
        {
            // FC 16 too long
            Buf = new byte[] { 0x01, 16, 0x01, 0x10, 0x00, 0x0A, 20, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19,20,21 };
            s = ModbusDecoder.DecodeQuery(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write multiple registers: addr=40273, quant=10, values=258,772,1286,1800,2314,2828,3342,3856,4370,4884 ERROR: QUERY TOO LONG!", s);
        }

        [Test]
        public void Test029901()
        {
            // unknown function code
            Buf = new byte[] { 0x01, 0x99, 0x01, 0x02, 0x03 };
            s = ModbusDecoder.DecodeQuery(Buf, Buf.Length);
            Assert.AreEqual("Slave=1,(not decoded)", s);
        }

        [Test]
        public void Test030101()
        {
            Buf = new byte[] { 0x01, 0x01, 3, 0x10, 0x11, 0x12 };
            s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Read coils: bytecount=3", s);
        }

        [Test]
        public void Test030201()
        {
            Buf = new byte[] { 0x01, 0x02, 1, 0x20 };
            s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Read discrete inputs: bytecount=1", s);
        }

        [Test]
        public void Test030301()
        {
            Buf = new byte[] { 0x01, 0x03, 8, 0x61,0xA9,0x61,0xAA,0xE6,0x71,0xE6,0x70 };
            s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Read holding registers: values=25001,25002,-6543,-6544", s);
        }

        [Test]
        public void Test030401()
        {
            Buf = new byte[] { 0x01, 0x04, 2, 0x12, 0x34 };
            s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Read input registers: values=4660", s);
        }

        [Test]
        public void Test030501()
        {
            Buf = new byte[] { 0x01, 0x05, 0x01, 0x02, 0xFF, 0x00 };
            s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write single coil: addr=259, value=ON", s);
        }

        [Test]
        public void Test030601()
        {
            Buf = new byte[] { 0x01, 0x06, 0x01, 0x02, 0x12, 0x34 };
            s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write single register: addr=40259, value=4660", s);
        }

        [Test]
        public void Test031501()
        {
            Buf = new byte[] { 0x01, 15, 0x01, 0x02, 0, 10 };
            s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write multiple coils: addr=259, quant=10", s);
        }

        [Test]
        public void Test031601()
        {
            Buf = new byte[] { 0x01, 16, 0x01, 0x02, 0, 10 };
            s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write multiple registers: addr=40259, quant=10", s);
        }

        [Test]
        public void Test0401()
        {
            Buf = new byte[] { 0x01, 0x80 + 1 };
            s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("DECODE: ERROR: RESPONSE TOO SHORT!", s);
        }

        [Test]
        public void Test040102()
        {
            Buf = new byte[] { 0x01, 0x80 + 1, 0x01 };
            s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Read coils: Exception: EXCEPTION 1 (ILLEGAL FUNCTION)", s);
        }

        [Test]
        public void Test0402()
        {
            Buf = new byte[] { 0x01, 0x80 + 2, 0x02 };
            s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Read discrete inputs: Exception: EXCEPTION 2 (ILLEGAL ADDRESS)", s);
        }

        [Test]
        public void Test0403()
        {
            Buf = new byte[] { 0x01, 0x80 + 3, 0x03 };
            s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Read holding registers: Exception: EXCEPTION 3 (ILLEGAL DATA VALUE)", s);
        }

        [Test]
        public void Test0404()
        {
            Buf = new byte[] { 0x01, 0x80 + 4, 0x04 };
            s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Read input registers: Exception: EXCEPTION 4 (SLAVE DEVICE FAILURE)", s);
        }

        [Test]
        public void Test0405()
        {
            Buf = new byte[] { 0x01, 0x80 + 5, 0x05 };
            s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write single coil: Exception: EXCEPTION 5 (ACKNOWLEDGE FOR LONG DURATION)", s);
        }

        [Test]
        public void Test0406()
        {
            Buf = new byte[] { 0x01, 0x80 + 6, 0x06 };
            s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write single register: Exception: EXCEPTION 6 (SLAVE DEVICE BUSY)", s);
        }

        [Test]
        public void Test041501()
        {
            Buf = new byte[] { 0x01, 0x80 + 15, 10 };
            s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write multiple coils: Exception: EXCEPTION 10 (GATEWAY PATH UNAVAILABLE)", s);
        }

        [Test]
        public void Test041502()
        {
            Buf = new byte[] { 0x01, 0x80 + 15, 11 };
            s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write multiple coils: Exception: EXCEPTION 11 (GATEWAY TARGET DEVICE FAILED TO RESPOND)", s);
        }

        [Test]
        public void Test0416()
        {
            Buf = new byte[] { 0x01, 0x80 + 16, 99 };
            s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write multiple registers: Exception: EXCEPTION 99", s);
        }

        // too short

        [Test]
        public void Test050001()
        {
            Buf = new byte[] { };
            s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("DECODE: ERROR: RESPONSE TOO SHORT!", s);
        }

        [Test]
        public void Test050002()
        {
            Buf = new byte[] { 0x01 };
            s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("DECODE: ERROR: RESPONSE TOO SHORT!", s);
        }

        // fc 1

        [Test]
        public void Test050101()
        {
            Buf = new byte[] { 0x01, 0x01 };
            s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("DECODE: ERROR: RESPONSE TOO SHORT!", s);
        }

        [Test]
        public void Test050102()
        {
            Buf = new byte[] { 0x01, 0x01, 0x03, 0x21, 0x22 };
            s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Read coils: bytecount=3 ERROR: RESPONSE HAS LESS DATABYTES THEN BYTECOUNT!", s);
        }

        [Test]
        public void Test050103()
        {
            Buf = new byte[] { 0x01, 0x01, 0x03, 0x21, 0x22, 0x23, 0x22 };
            s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Read coils: bytecount=3 ERROR: RESPONSE HAS MORE DATABYTES THEN BYTECOUNT!", s);
        }

        [Test]
        public void Test050104()
        {
            Buf = new byte[] { 0x01, 0x80 + 1 };
            s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("DECODE: ERROR: RESPONSE TOO SHORT!", s);
        }

        [Test]
        public void Test050105()
        {
            Buf = new byte[] { 0x01, 0x80 + 1, 0x01, 0x02 };
            s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Read coils: Exception: EXCEPTION 1 (ILLEGAL FUNCTION) ERROR: RESPONSE TOO LONG!", s);
        }

        // fc 2

        [Test]
        public void Test050201()
        {
            Buf = new byte[] { 0x01, 0x02 };
            s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("DECODE: ERROR: RESPONSE TOO SHORT!", s);
        }

        [Test]
        public void Test050202()
        {
            Buf = new byte[] { 0x01, 0x02, 0x03, 0x21, 0x22 };
            s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Read discrete inputs: bytecount=3 ERROR: RESPONSE HAS LESS DATABYTES THEN BYTECOUNT!", s);
        }

        [Test]
        public void Test050203()
        {
            Buf = new byte[] { 0x01, 0x02, 0x03, 0x21, 0x22, 0x23, 0x22 };
            s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Read discrete inputs: bytecount=3 ERROR: RESPONSE HAS MORE DATABYTES THEN BYTECOUNT!", s);
        }

        [Test]
        public void Test050204()
        {
            Buf = new byte[] { 0x01, 0x80 + 2 };
            s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("DECODE: ERROR: RESPONSE TOO SHORT!", s);
        }

        [Test]
        public void Test050205()
        {
            Buf = new byte[] { 0x01, 0x80 + 2, 0x01, 0x02 };
            s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Read discrete inputs: Exception: EXCEPTION 1 (ILLEGAL FUNCTION) ERROR: RESPONSE TOO LONG!", s);
        }

        // fc 3

        [Test]
        public void Test050301()
        {
            Buf = new byte[] { 0x01, 0x03 };
            s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("DECODE: ERROR: RESPONSE TOO SHORT!", s);
        }

        [Test]
        public void Test050302()
        {
            Buf = new byte[] { 0x01, 0x03, 0x05 };
            s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Read holding registers: ERROR: BYTECOUNT IS NOT EVEN! ERROR: RESPONSE HAS LESS DATABYTES THEN BYTECOUNT!", s);
        }

        [Test]
        public void Test050303()
        {
            Buf = new byte[] { 0x01, 0x03, 0x06, 0x21, 0x22, 0x23, 0x24, 0x25 };
            s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Read holding registers: ERROR: RESPONSE HAS LESS DATABYTES THEN BYTECOUNT!", s);
        }

        [Test]
        public void Test050304()
        {
            Buf = new byte[] { 0x01, 0x03, 0x06, 0x21, 0x22, 0x23, 0x24, 0x25, 0x26, 0x27 };
            s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Read holding registers: values=8482,8996,9510 ERROR: RESPONSE HAS MORE DATABYTES THEN BYTECOUNT!", s);
        }

        [Test]
        public void Test050306()
        {
            Buf = new byte[] { 0x01, 0x80 + 3 };
            s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("DECODE: ERROR: RESPONSE TOO SHORT!", s);
        }

        [Test]
        public void Test050307()
        {
            Buf = new byte[] { 0x01, 0x80 + 3, 0x01, 0x02 };
            s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Read holding registers: Exception: EXCEPTION 1 (ILLEGAL FUNCTION) ERROR: RESPONSE TOO LONG!", s);
        }

        // fc 4 

        [Test]
        public void Test050401()
        {
            Buf = new byte[] { 0x01, 0x04 };
            s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("DECODE: ERROR: RESPONSE TOO SHORT!", s);
        }

        [Test]
        public void Test050402()
        {
            Buf = new byte[] { 0x01, 0x04, 0x05 };
            s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Read input registers: ERROR: BYTECOUNT IS NOT EVEN! ERROR: RESPONSE HAS LESS DATABYTES THEN BYTECOUNT!", s);
        }

        [Test]
        public void Test050403()
        {
            Buf = new byte[] { 0x01, 0x04, 0x06, 0x21, 0x22, 0x23, 0x24, 0x25 };
            s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Read input registers: ERROR: RESPONSE HAS LESS DATABYTES THEN BYTECOUNT!", s);
        }

        [Test]
        public void Test050404()
        {
            Buf = new byte[] { 0x01, 0x04, 0x06, 0x21, 0x22, 0x23, 0x24, 0x25, 0x26, 0x27 };
            s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Read input registers: values=8482,8996,9510 ERROR: RESPONSE HAS MORE DATABYTES THEN BYTECOUNT!", s);
        }

        [Test]
        public void Test050405()
        {
            Buf = new byte[] { 0x01, 0x80 + 4 };
            s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("DECODE: ERROR: RESPONSE TOO SHORT!", s);
        }

        [Test]
        public void Test050406()
        {
            Buf = new byte[] { 0x01, 0x80 + 4, 0x01, 0x02 };
            s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Read input registers: Exception: EXCEPTION 1 (ILLEGAL FUNCTION) ERROR: RESPONSE TOO LONG!", s);
        }

        // fc 5

        [Test]
        public void Test050501()
        {
            Buf = new byte[] { 0x01, 0x05 };
            s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("DECODE: ERROR: RESPONSE TOO SHORT!", s);
        }

        [Test]
        public void Test050502()
        {
            Buf = new byte[] { 0x01, 0x05, 0x05 };
            s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write single coil: ERROR: RESPONSE TOO SHORT!", s);
        }

        [Test]
        public void Test050503()
        {
            Buf = new byte[] { 0x01, 0x05, 0x10, 0x11, 0x23 };
            s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write single coil: ERROR: RESPONSE TOO SHORT!", s);
        }

        [Test]
        public void Test050504()
        {
            Buf = new byte[] { 0x01, 0x05, 0x10, 0x11, 0x00, 0xFF };
            s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write single coil: addr=4114, value=ERROR: ILLEGAL VALUE!", s);
        }

        [Test]
        public void Test050505()
        {
            Buf = new byte[] { 0x01, 0x05, 0x10, 0x11, 0x00, 0x00, 0x21 };
            s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write single coil: addr=4114, value=OFF ERROR: RESPONSE TOO LONG!", s);
        }

        [Test]
        public void Test050506()
        {
            Buf = new byte[] { 0x01, 0x80 + 5 };
            s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("DECODE: ERROR: RESPONSE TOO SHORT!", s);
        }

        [Test]
        public void Test050507()
        {
            Buf = new byte[] { 0x01, 0x80 + 5, 0x01, 0x02 };
            s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write single coil: Exception: EXCEPTION 1 (ILLEGAL FUNCTION) ERROR: RESPONSE TOO LONG!", s);
        }

        // fc 6

        [Test]
        public void Test050601()
        {
            Buf = new byte[] { 0x01, 0x06 };
            s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("DECODE: ERROR: RESPONSE TOO SHORT!", s);
        }

        [Test]
        public void Test050602()
        {
            Buf = new byte[] { 0x01, 0x06, 0x05 };
            s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write single register: ERROR: RESPONSE TOO SHORT!", s);
        }

        [Test]
        public void Test050603()
        {
            Buf = new byte[] { 0x01, 0x06, 0x10, 0x11, 0x23 };
            s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write single register: ERROR: RESPONSE TOO SHORT!", s);
        }

        [Test]
        public void Test050604()
        {
            Buf = new byte[] { 0x01, 0x06, 0x10, 0x11, 0x01, 0x02, 0x21 };
            s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write single register: addr=44114, value=258 ERROR: RESPONSE TOO LONG!", s);
        }

        [Test]
        public void Test050605()
        {
            Buf = new byte[] { 0x01, 0x80 + 6 };
            s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("DECODE: ERROR: RESPONSE TOO SHORT!", s);
        }

        [Test]
        public void Test050606()
        {
            Buf = new byte[] { 0x01, 0x80 + 6, 0x01, 0x02 };
            s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write single register: Exception: EXCEPTION 1 (ILLEGAL FUNCTION) ERROR: RESPONSE TOO LONG!", s);
        }

        // fc 15

        [Test]
        public void Test051501()
        {
            Buf = new byte[] { 0x01, 15 };
            s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("DECODE: ERROR: RESPONSE TOO SHORT!", s);
        }

        [Test]
        public void Test051502()
        {
            Buf = new byte[] { 0x01, 15, 0x05 };
            s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write multiple coils: ERROR: RESPONSE TOO SHORT!", s);
        }

        [Test]
        public void Test051503()
        {
            Buf = new byte[] { 0x01, 15, 0x10, 0x11, 0x23 };
            s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write multiple coils: ERROR: RESPONSE TOO SHORT!", s);
        }

        [Test]
        public void Test051504()
        {
            Buf = new byte[] { 0x01, 15, 0x10, 0x11, 0x01, 0x02, 0x21 };
            s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write multiple coils: addr=4114, quant=258 ERROR: RESPONSE TOO LONG!", s);
        }

        [Test]
        public void Test051505()
        {
            Buf = new byte[] { 0x01, 0x80 + 15 };
            s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("DECODE: ERROR: RESPONSE TOO SHORT!", s);
        }

        [Test]
        public void Test051506()
        {
            Buf = new byte[] { 0x01, 0x80 + 15, 0x01, 0x02 };
            s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write multiple coils: Exception: EXCEPTION 1 (ILLEGAL FUNCTION) ERROR: RESPONSE TOO LONG!", s);
        }
        
        // fc 16

        [Test]
        public void Test051601()
        {
            Buf = new byte[] { 0x01, 16 };
            s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("DECODE: ERROR: RESPONSE TOO SHORT!", s);
        }

        [Test]
        public void Test051602()
        {
            Buf = new byte[] { 0x01, 16, 0x05 };
            s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write multiple registers: ERROR: RESPONSE TOO SHORT!", s);
        }

        [Test]
        public void Test051603()
        {
            Buf = new byte[] { 0x01, 16, 0x10, 0x11, 0x23 };
            s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write multiple registers: ERROR: RESPONSE TOO SHORT!", s);
        }

        [Test]
        public void Test051604()
        {
            Buf = new byte[] { 0x01, 16, 0x10, 0x11, 0x01, 0x02, 0x21 };
            s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write multiple registers: addr=44114, quant=258 ERROR: RESPONSE TOO LONG!", s);
        }

        [Test]
        public void Test051605()
        {
            Buf = new byte[] { 0x01, 0x80 + 16 };
            s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("DECODE: ERROR: RESPONSE TOO SHORT!", s);
        }

        [Test]
        public void Test051606()
        {
            Buf = new byte[] { 0x01, 0x80 + 16, 0x01, 0x02 };
            s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1, Write multiple registers: Exception: EXCEPTION 1 (ILLEGAL FUNCTION) ERROR: RESPONSE TOO LONG!", s);
        }

        // unsupported fc

        [Test]
        public void Test0601()
        {
            Buf = new byte[] { 0x01, 0x99, 0x03 };
            s = ModbusDecoder.DecodeResponse(Buf, Buf.Length);
            Assert.AreEqual("Slave=1,(not decoded)", s);
        }
    }
}

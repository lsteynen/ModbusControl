using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LST.Helpers;

namespace UnitTests
{
   
    [TestClass]
    public class ParityTests
    {
        // ---------- TryParseParity: valid names ----------

        [TestMethod]
        public void TryParseParity_None()
        {
            Assert.IsTrue(Parity.TryParseParity("none", out Parity.EParity p));
            Assert.AreEqual(Parity.EParity.None, p);
        }

        [TestMethod]
        public void TryParseParity_Odd()
        {
            Assert.IsTrue(Parity.TryParseParity("odd", out Parity.EParity p));
            Assert.AreEqual(Parity.EParity.Odd, p);
        }

        [TestMethod]
        public void TryParseParity_Even()
        {
            Assert.IsTrue(Parity.TryParseParity("even", out Parity.EParity p));
            Assert.AreEqual(Parity.EParity.Even, p);
        }

        [TestMethod]
        public void TryParseParity_Mark()
        {
            Assert.IsTrue(Parity.TryParseParity("mark", out Parity.EParity p));
            Assert.AreEqual(Parity.EParity.Mark, p);
        }

        [TestMethod]
        public void TryParseParity_Space()
        {
            Assert.IsTrue(Parity.TryParseParity("space", out Parity.EParity p));
            Assert.AreEqual(Parity.EParity.Space, p);
        }

        // ---------- TryParseParity: case insensitive ----------

        [TestMethod]
        public void TryParseParity_Case1()
        {
            Assert.IsTrue(Parity.TryParseParity("ODD", out Parity.EParity p));
            Assert.AreEqual(Parity.EParity.Odd, p);
        }

        [TestMethod]
        public void TryParseParity_Case2()
        {
            Assert.IsTrue(Parity.TryParseParity("sPaCe", out Parity.EParity p));
            Assert.AreEqual(Parity.EParity.Space, p);
        }

        // ---------- TryParseParity: invalid input ----------

        [TestMethod]
        public void TryParseParity_Inv1()
        {
            Assert.IsFalse(Parity.TryParseParity(null, out Parity.EParity p));
            Assert.AreEqual(Parity.EParity.None, p);
        }

        [TestMethod]
        public void TryParseParity_Inv2()
        {
            Assert.IsFalse(Parity.TryParseParity("", out _));
        }

        [TestMethod]
        public void TryParseParity_Inv3()
        {
            Assert.IsFalse(Parity.TryParseParity("foo", out Parity.EParity p));
            Assert.AreEqual(Parity.EParity.None, p);
        }

        [TestMethod]
        public void TryParseParity_Inv4()
        {
            Assert.IsFalse(Parity.TryParseParity("1", out _));
        }

        [TestMethod]
        public void TryParseParity_Inv5()
        {
            // Documents current behavior: only full names are supported.
            Assert.IsFalse(Parity.TryParseParity("O", out _));
        }

        [TestMethod]
        public void TryParseParity_Inv6()
        {
            // Documents current behavior: input is not trimmed.
            Assert.IsFalse(Parity.TryParseParity(" odd ", out _));
        }

        // ---------- ParityToName ----------

        [TestMethod]
        public void ParityToName_AllValues()
        {
            Assert.AreEqual("None", Parity.ParityToName(Parity.EParity.None));
            Assert.AreEqual("Odd", Parity.ParityToName(Parity.EParity.Odd));
            Assert.AreEqual("Even", Parity.ParityToName(Parity.EParity.Even));
            Assert.AreEqual("Mark", Parity.ParityToName(Parity.EParity.Mark));
            Assert.AreEqual("Space", Parity.ParityToName(Parity.EParity.Space));
        }

        [TestMethod]
        public void ParityToName_Inv1()
        {
            Assert.IsNull(Parity.ParityToName((Parity.EParity)99));
        }

    }
}
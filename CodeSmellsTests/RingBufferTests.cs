using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodeSmells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace CodeSmells.Tests
{
    [TestClass()]
    public class RingBufferTests
    {
        [TestMethod()]
        public void PutTest_AddedMoreThanCapacity_Sussess()
        {
            string[] data = new string[] { "string1", "string2", "string3", "string4", "string5" };
            RingBuffer<string> testSubject = new RingBuffer<string>(data.Length-1);

            foreach (string str in data)
                testSubject.Put(str);

            var bufferContent = testSubject.GetData();
            foreach (string str in bufferContent)
                Debug.Write($"{str} ");

            Assert.IsTrue(bufferContent.First() == data.Last());
        }
    }
}
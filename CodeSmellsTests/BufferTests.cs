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
    public class BufferTests
    {
        [TestMethod()]
        public void PutTest_OneValue_Success()
        {
            //string[] data = new string[] { "string1", "string2", "string3", "string4", "string5" };
            bool isCorrect;
            string data = "test string";
            int bufferCapacity = 5;
            Buffer<string> testSubject = new Buffer<string>(bufferCapacity);

            testSubject.Put(data);
            var bufferContent = testSubject.GetData();
            isCorrect = bufferContent.First() == data;

            Assert.IsTrue(isCorrect);
        }

        [TestMethod()]
        public void PutTest_ManyValues_EnoughCapacity_Success()
        {
            string[] data = new string[] { "string1", "string2", "string3", "string4", "string5" };
            bool isCorrect = true;
            Buffer<string> testSubject = new Buffer<string>(data.Length + 1);

            foreach(string str in data)
                testSubject.Put(str);
            var bufferContent = testSubject.GetData();
            foreach(var str in data)
            {
                isCorrect &= bufferContent.Contains(str);
                if (!isCorrect)
                    break;
            }

            Assert.IsTrue(isCorrect);
        }

        [TestMethod()]
        public void PutTest_ManyValues_NotEnoughCapacity_Fail()
        {
            string[] data = new string[] { "string1", "string2", "string3", "string4", "string5" };
            bool isCorrect;
            Buffer<string> testSubject = new Buffer<string>(data.Length - 1);

            try
            {
                foreach (string str in data)
                    testSubject.Put(str);

                isCorrect = false;
            }
            catch(IndexOutOfRangeException)
            {
                isCorrect = true;
            }
            catch(Exception e)
            {
                isCorrect = false;
                Debug.WriteLine($"{e} has been thrown, IndexOutOfRangeException expexted.");
            }

            Assert.IsTrue(isCorrect);
        }

        [TestMethod()]
        public void IsFullTest_Filled_Success()
        {
            string[] data = new string[] { "string1", "string2", "string3", "string4", "string5" };
            Buffer<string> testSubject = new Buffer<string>(data.Length);

            foreach (string str in data)
                testSubject.Put(str);

            Assert.IsTrue(testSubject.IsFull());
        }
    }
}
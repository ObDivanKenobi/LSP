using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodeSmells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace CodeSmells.Tests
{
    [TestClass()]
    public class BufferisedFileWriterTests
    {
        static string testPath = "test.txt";

        [TestMethod()]
        public void WriteTest_DataCountIsMultipleOfBufferCapacity_Success()
        {
            ClearFile();

            string[] data = new string[] { "string1", "string2", "string3", "string4", "string5", "string6" };
            BufferedFileWriter<string> testSubject = new BufferedFileWriter<string>(new Buffer<string>(3), testPath);

            foreach (var str in data)
                testSubject.Write(str);

            StreamReader reader = new StreamReader(new FileStream(testPath, FileMode.Open));
            List<string> readen = new List<string>();
            while (!reader.EndOfStream)
                readen.Add(reader.ReadLine());
            reader.Close();

            bool isOk = true;
            foreach(var str in readen)
            {
                isOk &= data.Contains(str);
                Debug.WriteLine(str);
            }

            Assert.IsTrue(isOk);
        }

        [TestMethod()]
        public void WriteTest_DataCountIsNotMultipleOfBufferCapacity_Fail()
        {
            ClearFile();

            string[] data = new string[] { "string1", "string2", "string3", "string4", "string5", "string6" };
            BufferedFileWriter<string> testSubject = new BufferedFileWriter<string>(new Buffer<string>(4), testPath);

            foreach (var str in data)
                testSubject.Write(str);

            StreamReader reader = new StreamReader(new FileStream(testPath, FileMode.Open));
            List<string> readen = new List<string>();
            while (!reader.EndOfStream)
                readen.Add(reader.ReadLine());
            reader.Close();

            bool isOk = readen.Count == data.Length;
            if (isOk)
                foreach (var str in readen)
                {
                    isOk &= data.Contains(str);
                    Debug.WriteLine(str);
                }

            Assert.IsFalse(isOk);
        }

        [TestMethod()]
        public void WriteTest_DataCountIsNotMultipleOfBufferCapacity_WriterDisposed_Success()
        {
            ClearFile();

            string[] data = new string[] { "string1", "string2", "string3", "string4", "string5", "string6" };
            BufferedFileWriter<string> testSubject = new BufferedFileWriter<string>(new Buffer<string>(4), testPath);

            foreach (var str in data)
                testSubject.Write(str);

            testSubject.Dispose();

            StreamReader reader = new StreamReader(new FileStream(testPath, FileMode.Open));
            List<string> readen = new List<string>();
            while (!reader.EndOfStream)
                readen.Add(reader.ReadLine());
            reader.Close();

            bool isOk = readen.Count == data.Length;
            if (isOk)
                foreach (var str in readen)
                {
                    isOk &= data.Contains(str);
                    Debug.WriteLine(str);
                }

            Assert.IsTrue(isOk);
        }

        [TestMethod()]
        public void WriteTest_UsingRingBuffer_Fail()
        {
            ClearFile();

            string[] data = new string[] { "string1", "string2", "string3", "string4", "string5", "string6" };
            BufferedFileWriter<string> testSubject = new BufferedFileWriter<string>(new RingBuffer<string>(4), testPath);

            foreach (var str in data)
                testSubject.Write(str);

            testSubject.Dispose();

            StreamReader reader = new StreamReader(new FileStream(testPath, FileMode.Open));
            List<string> readen = new List<string>();
            while (!reader.EndOfStream)
                readen.Add(reader.ReadLine());
            reader.Close();

            bool isOk = readen.Count == data.Length;
            foreach (var str in readen)
            {
                isOk &= data.Contains(str);
                Debug.WriteLine(str);
            }

            Assert.IsFalse(isOk);
        }

        void ClearFile()
        {
            StreamReader reader = new StreamReader(new FileStream(testPath, FileMode.Create));
            reader.Close();
        }
    }
}
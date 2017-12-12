using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSmells
{
    public class BufferedFileWriter<T> : IDisposable
    {
        public string Path { get; set; }
        Buffer<T> buffer;

        public BufferedFileWriter(Buffer<T> buf, string path)
        {
            buffer = buf;
            Path = path;

            if (!File.Exists(Path))
                File.Create(Path);
        }

        // To be sure that no data in a buffer will be lost.
        ~BufferedFileWriter()
        {
            Dispose();
        }

        public void Write(T data)
        {
            if (buffer.IsFull())
                WriteToFile();

            buffer.Put(data);
        }

        void WriteToFile()
        {
            StreamWriter writer = new StreamWriter(new FileStream(Path, FileMode.Append));
            foreach (var value in buffer.GetData())
            {
                if (value == null || value.Equals(default(T)))
                    break;
                writer.WriteLine(value);
            }

            writer.Close();
            buffer.Clear();
        }

        // It's recommended to write both destructor and Dispose method
        public void Dispose()
        {
            WriteToFile();
        }
    }
}

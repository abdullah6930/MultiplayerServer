using System;

namespace MultiplayerServer.Utilities
{
    internal class NetworkOperation
    {
        private readonly byte[] buffer;
        private int position;

        public NetworkOperation(int bufferSize = 1024)
        {
            buffer = new byte[bufferSize];
            position = 0;
        }

        public void WriteInt(int value)
        {
            byte[] data = BitConverter.GetBytes(value);
            WriteBytes(data);
        }

        public int ReadInt()
        {
            byte[] data = ReadBytes(sizeof(int));
            return BitConverter.ToInt32(data, 0);
        }

        public void WriteFloat(float value)
        {
            byte[] data = BitConverter.GetBytes(value);
            WriteBytes(data);
        }

        public float ReadFloat()
        {
            byte[] data = ReadBytes(sizeof(float));
            return BitConverter.ToSingle(data, 0);
        }

        public void WriteBool(bool value)
        {
            byte[] data = BitConverter.GetBytes(value);
            WriteBytes(data);
        }

        public bool ReadBool()
        {
            byte[] data = ReadBytes(sizeof(bool));
            return BitConverter.ToBoolean(data, 0);
        }

        public void WriteString(string value)
        {
            byte[] data = System.Text.Encoding.UTF8.GetBytes(value);
            WriteInt(data.Length);
            WriteBytes(data);
        }

        public string ReadString()
        {
            int length = ReadInt();
            byte[] data = ReadBytes(length);
            return System.Text.Encoding.UTF8.GetString(data);
        }

        public void WriteBytes(byte[] data)
        {
            Array.Copy(data, 0, buffer, position, data.Length);
            position += data.Length;
        }

        public byte[] ReadBytes(int length)
        {
            byte[] data = new byte[length];
            Array.Copy(buffer, position, data, 0, length);
            position += length;
            return data;
        }

        public byte[] GetBufferData()
        {
            return buffer;
        }

        public void Reset()
        {
            position = 0;
        }
    }
}

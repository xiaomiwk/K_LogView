using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Win.Model
{
    [Serializable]
    public class M详细信息 : ICloneable
    {
        public int Id { get; set; }

        public int 跟踪标记 { get; set; }

        public E跟踪周期 跟踪周期 { get; set; }

        public TraceEventType 等级 { get; set; }

        public string 来源 { get; set; }

        public string 标题 { get; set; }

        public string 内容 { get; set; }

        public string 辅助信息 { get; set; }

        public string 线程 { get; set; }

        public string 行号 { get; set; }

        public string 方法 { get; set; }

        public string 类型 { get; set; }

        public string 文件 { get; set; }

        public DateTime 时间 { get; set; }

        public float 耗时 { get; set; }

        public M详细信息 深克隆()
        {
            return (M详细信息)Clone();
        }

        public M详细信息 浅克隆()
        {
            return (M详细信息)MemberwiseClone();
        }

        public object Clone()
        {
            System.IO.MemoryStream membuffer = new System.IO.MemoryStream();
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter binserializer =
                new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter(
                    null,
                    new System.Runtime.Serialization.StreamingContext(
                        System.Runtime.Serialization.StreamingContextStates.Clone));
            object objitemClone;

            // Serialize the object into the memory stream
            binserializer.Serialize(membuffer, this);

            // Move the stream pointer to the beginning of the memory stream
            membuffer.Seek(0, System.IO.SeekOrigin.Begin);

            // Get the serialized object from the memory stream
            objitemClone = binserializer.Deserialize(membuffer);

            // Release the memory stream
            membuffer.Close();

            // Return the deeply cloned object
            return objitemClone;
        }

        public override string ToString()
        {
            var __sb = new StringBuilder();
            __sb.Append(来源).Append(跟踪周期).Append(等级).Append(标题).Append(内容).Append(时间);
            return __sb.ToString();
        }
    }
}

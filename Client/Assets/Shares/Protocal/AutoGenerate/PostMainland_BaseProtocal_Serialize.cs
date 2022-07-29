/* this is generated by nino */
namespace PostMainland
{
    public partial class BaseProtocal
    {
        public static BaseProtocal.SerializationHelper NinoSerializationHelper = new BaseProtocal.SerializationHelper();
        public class SerializationHelper: Nino.Serialization.NinoWrapperBase<BaseProtocal>
        {
            #region NINO_CODEGEN
            public override void Serialize(BaseProtocal value, Nino.Serialization.Writer writer)
            {
                writer.CompressAndWriteEnum(typeof(System.Byte), (ulong) value.Type);
                writer.CompressAndWriteEnum(typeof(System.UInt32), (ulong) value.Id);
                writer.CompressAndWrite(value.MessageId);
                if(value.Body != null)
                {
                    writer.CompressAndWrite(value.Body.Length);
                    foreach (var entry in value.Body)
                    {
                        writer.Write(entry);
                    }
                }
                else
                {
                    writer.CompressAndWrite(0);
                }
            }

            public override BaseProtocal Deserialize(Nino.Serialization.Reader reader)
            {
                BaseProtocal value = new BaseProtocal();
                value.Type = (PostMainland.ProtocalType)reader.DecompressAndReadEnum(typeof(System.Byte));
                value.Id = (PostMainland.ProtocalId)reader.DecompressAndReadEnum(typeof(System.UInt32));
                value.MessageId =  (System.Int64)reader.DecompressAndReadNumber();
                value.Body = reader.ReadBytes(reader.ReadLength());
                return value;
            }
            #endregion
        }
    }
}
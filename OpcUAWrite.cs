using Opc.UaFx.Client;

namespace Sem3ReWrite
{
    public abstract class OpcUAWrite
    {
        private const string OpcUrl = "opc.tcp://127.0.0.1:4840";

        public static void WriteNodeIntValue(string nodeId, int value)
        {
            var client = new OpcClient(OpcUrl);
            client.Connect();
            client.WriteNode(nodeId, value);
        }

        public static void WriteNodeFloatValue(string nodeId, float value)
        {
            var client = new OpcClient(OpcUrl);
            client.Connect();
            client.WriteNode(nodeId, value);
        }
    }
}
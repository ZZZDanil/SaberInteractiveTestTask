using System;
using System.IO;

namespace SaberInteractiveTestTask
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            //SerializeListRand();
            DeserializeListRand();
        }
        static void SerializeListRand()
        {
            ListRand listRand = CreateListForTest();

            listRand.Serialize(GetFile("SerializeFile"));
        }
        static void DeserializeListRand()
        {
            ListRand listRand = new ListRand();
            listRand.Deserialize(GetFile("SerializeFile"));

        }
        static FileStream GetFile(string file)
        {
            return new FileStream(file, FileMode.OpenOrCreate);
        }
        static ListRand CreateListForTest()
        {
            ListRand listRand = new ListRand();
            ListNode[] listNodes = new ListNode[5] { new ListNode(), new ListNode(), new ListNode(), new ListNode(), new ListNode() };
            for (int i = 0; i < listNodes.Length - 1; i++)
            {
                listNodes[i].Next = listNodes[i + 1];
            }
            for (int i = 1; i < listNodes.Length; i++)
            {
                listNodes[i].Prev = listNodes[i - 1];
            }
            for (int i = 0; i < listNodes.Length; i++)
            {
                listNodes[i].Data = $"RandomData{i}";
            }
            listNodes[1].Rand = listNodes[1];
            listNodes[2].Rand = listNodes[4];
            listNodes[3].Rand = listNodes[0];
            listRand.Head = listNodes[0];
            listRand.Tail = listNodes[4];
            listRand.Count = 5;
            return listRand;
        }
    }
}

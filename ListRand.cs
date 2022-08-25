using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SaberInteractiveTestTask
{
    class ListRand
    {
        public ListNode Head;
        public ListNode Tail;
        public int Count;
        public void Serialize(FileStream s)
        {
            // Prepare dictionary
            ListNode currentListNode = Head;
            Dictionary<ListNode, int> dict = new Dictionary<ListNode, int>();
            for (int i = 0; i < Count; i++)
            {
                dict.Add(currentListNode, i);
                currentListNode = currentListNode.Next;
            }

            // Generate data for serialize
            currentListNode = Head;
            string listNodeDatas = $"{currentListNode.Data}{(currentListNode.Rand != null ? dict[currentListNode.Rand] : -1)}";
            for (int i = 0; i < Count-1; i++)
            {
                currentListNode = currentListNode.Next;
                listNodeDatas += $"{currentListNode.Data}{(currentListNode.Rand != null ? dict[currentListNode.Rand] : -1)}";
            }

            // Out data
            byte[] byteData = Encoding.UTF8.GetBytes($"{Count}{listNodeDatas}");
            s.Write(byteData , 0, byteData.Length);
            s.Close();
        }
        public void Deserialize(FileStream s)
        {
            // Prepare serialize data
            byte[] data = new byte[s.Length];
            s.Read(data, 0, data.Length);
            string[] listRandData = Encoding.UTF8.GetString(data).Split(new char[] { '' });
            string[] listNodesGist = listRandData[1].Split(new char[] { '' });

            // Prepare dictionary and nodes
            Count = int.Parse(listRandData[0]);
            ListNode[] listNodes = new ListNode[Count];
            Dictionary<int, ListNode> dict = new Dictionary<int, ListNode>();

            listNodes[0] = new ListNode();
            dict.Add(0, listNodes[0]);
            for (int i = 1; i < Count; i++)
            {
                listNodes[i] = new ListNode();
                dict.Add(i, listNodes[i]);
                listNodes[i].Prev = listNodes[i - 1];
                listNodes[i - 1].Next = listNodes[i];
            }

            // Fill nodes data
            for (int i = 0; i < Count; i++)
            {
                string[] listNodesData = listNodesGist[i].Split(new char[] { '' });
                listNodes[i].Data = listNodesData[0];
                int Ref = int.Parse(listNodesData[1]);
                if(Ref > -1)
                    listNodes[i].Rand = dict[Ref];
            }

            // Fill List data
            Head = listNodes[0];
            Tail = listNodes[Count-1];
            s.Close();
        }
    }
}

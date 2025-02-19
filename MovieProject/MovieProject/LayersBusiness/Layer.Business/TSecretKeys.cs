using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer.Business
{
	public static class TSecretKeys
	{
		public static List<Guid> GuidList = new List<Guid>();
		public static List<Guid> ConstGuidList = new List<Guid>();

		public static void CreateGuidList(int Count)
		{
			for (int i = 0; i < Count; i++)
			{
				Guid Guid1 = Guid.NewGuid();
				GuidList.Add(Guid1);
			}
		}
		public static byte[] GuidByteArray(Guid guid)
		{
			return guid.ToByteArray();
		}
		public static byte[] GuidByteArray(string guid)
		{
			return UTF8Encoding.UTF8.GetBytes(guid);
		}
		public static byte[] GetGuidByteArrayByIndex(int Index)
		{
			return GuidList[Index].ToByteArray();
		}
		public static Guid GetGuidByIndex(int Index)
		{
			return GuidList[Index];
		}
		public static string GetGuidStringByIndex(int Index)
		{
			return GuidList[Index].ToString();
		}
		public static byte[] GetGuidByRandom(out int Index)
		{
			int rand = new Random().Next(1, GuidList.Count);
			Index = rand;
			return GuidList[rand].ToByteArray();
		}
	}

}

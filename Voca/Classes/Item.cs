namespace Voca
{
	public class Item
	{
		public Item()
		{
		}

		public Item(string item1, string item2)
		{
			Item1 = item1;
			Item2 = item2;
		}

		public string Item1 { get; set; }

		public string Item2 { get; set; }
	}
}

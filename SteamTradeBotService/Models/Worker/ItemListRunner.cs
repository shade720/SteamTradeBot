using System.Collections.Generic;
using SteamTradeBotService.DatabaseModels;

namespace SteamTradeBotService.Models.Worker;

public class ItemListRunner
{
	private readonly ItemAnalyzer _analyzer;
	private readonly List<Item> _itemList;
	public int ListCount { get; }
	public int CurrentPosition { get; private set; }

	public ItemListRunner(IEnumerable<Item> itemList, Browser browser)
	{
		_itemList = new List<Item>(itemList);
		_analyzer = new ItemAnalyzer(browser);
		ListCount = _itemList.Count;
		CurrentPosition = 0;
	}

	public PurchaseConfiguration AnalyzeNext()
	{
		var purchaseConfig = _analyzer.AnalyzeItem(_itemList[CurrentPosition].ItemName);
		CurrentPosition++;
		return purchaseConfig;
	}

	public void ResetPosition()
	{
		CurrentPosition = 0;
	}

	public void SetConfiguration(Configuration incomingConfiguration)
	{
		_analyzer.SetConfiguration(incomingConfiguration);
	}

}
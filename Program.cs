using HtmlAgilityPack;
using ScrapySharp.Extensions;

while (true)
{
    Console.WriteLine("Card Name?");
    var cardName = Console.ReadLine();
    if (cardName.Length == 0) return;

    Console.WriteLine("Set?");
    var set = Console.ReadLine();
    if (set.Length == 0) return;

    //var cardName = "bayou";

    // make the api call
    var web = new HtmlWeb();
    Console.WriteLine("Fetching...");
    var doc = web.Load($"http://api.scraperapi.com?api_key=a1aef8c96041b3087159f7bb9c3519b7&url=https://starcitygames.com/search/?search_query={cardName}&filter_set={set}&render=true");
    // get the price
    // save it to card.price
    var rows = doc.DocumentNode.CssSelect(".hawk-results-item__options-table-cell--price");

    var price = rows.First().InnerHtml;
    price = price.Replace("$", "");
    Console.WriteLine(price);
}

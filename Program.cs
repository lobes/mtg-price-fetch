using HtmlAgilityPack;
using ScrapySharp.Extensions;

while (true)
{
    Console.WriteLine("Card Name?");
    var cardName = Console.ReadLine();
    if (cardName.Length == 0) return;

    Console.WriteLine("Set?");
    var setName = Console.ReadLine();
    if (setName.Length == 0) return;

    Console.WriteLine("Foil? [y] [N]");
    var foil = Console.ReadLine();
    var isFoil = false;
    if (foil.ToLower().Contains("yes") || foil.ToLower().Contains("y")) isFoil = true;
    /*
    var cardName = "bayou";
    var setName = "alpha";
    */

    // make the api call
    var web = new HtmlWeb();
    Console.WriteLine("Fetching...");
    var doc = web.Load($"http://api.scraperapi.com?api_key=a1aef8c96041b3087159f7bb9c3519b7&url=https://starcitygames.com/search/?search_query={cardName}&render=true");
    var rows = doc.DocumentNode.CssSelect(".hawk-results-item");

    foreach (var row in rows)
    {
        // get the price
        var nameNode = row.CssSelect
            (".hawk-results-item__header > .hawk-headerDiv > .hawk-results-item__title")
            .First()
            .InnerText
            .Replace(",", "")
            .ToLower();
        var setNode = row.CssSelect
            (".hawk-results-item__header > .hawk-headerDiv > .hawk-results-item__category")
            .First()
            .InnerText
            .Replace(",", "")
            .ToLower();

        if (setNode.ToLower().Contains("foil") && !isFoil) continue;
        if (!setNode.ToLower().Contains("foil") && isFoil) continue;

        if (cardName.Replace(",", "").ToLower().Equals(nameNode) &&
            setNode.Contains(setName.Replace(",", "").ToLower()))
        {
            var price = row.CssSelect(".hawk-results-item__options-table-cell--price")
                .First()
                .InnerText
                .Replace("$", "");
            var cond = row.CssSelect(".hawk-results-item__options-table-cell--name")
                .First()
                .InnerText;

            Console.WriteLine(price);
            break;
        }
    }
}

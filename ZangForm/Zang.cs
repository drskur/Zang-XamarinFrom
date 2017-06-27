using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;
using HtmlAgilityPack;

namespace ZangForm
{
	public class Zang
	{

		public static async Task<List<Tuple<String, String>>> FetchMangaListAsync()
		{
			var web = new HtmlWeb();
			var doc = await web.LoadFromWebAsync("http://zangsisi.net");

			var nodes = doc.DocumentNode.Descendants()
						   .Where(x => x.Id == "manga-list")
						   .First().Descendants("a");

			var titles = nodes.Select(x => x.InnerText).ToList();
			var links = nodes.Select(x => x.Attributes["href"].Value).ToList();

			return titles.Zip(links, (title, link) => new Tuple<String, String>(title, link))
						 .ToList();
		}

        public static async Task<List<Tuple<String, String>>> FetchMangaItemAsync(MangaList manga)
		{
			var web = new HtmlWeb();
            var doc = await web.LoadFromWebAsync(manga.Link);

            var nodes = doc.DocumentNode.Descendants()
                           .Where(x => x.Id == "post")
                           .First().Descendants("div")
                           .Where(x => x.Attributes["class"].Value == "contents")
                           .First().Descendants("a");

			var titles = nodes.Select(x => x.InnerText).ToList();
			var links = nodes.Select(x => x.Attributes["href"].Value).ToList();

			return titles.Zip(links, (title, link) => new Tuple<String, String>(title, link))
						 .ToList();
		}

        public static async Task<List<String>> FetchMangaContentsAsync(MangaItem item)
		{
			var web = new HtmlWeb();
			var doc = await web.LoadFromWebAsync(item.Link);

			var nodes = doc.DocumentNode.Descendants()
						   .Where(x => x.Id == "post")
						   .First().Descendants("div")
						   .Where(x => x.Attributes["class"].Value == "contents")
						   .First().Descendants("img");

            return nodes.Select(x => x.Attributes["src"].Value).ToList();
		}

	}
}

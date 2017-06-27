using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Realms;

namespace ZangForm
{

	public class MangaList : RealmObject
	{
		public string Title { get; set; }
		public string Link { get; set; }
	}

    public class MangaItem : RealmObject
    {
		public string Title { get; set; }
		public string Link { get; set; }
        public MangaList Manga { get; set; }
    }

    public class MangaContent : RealmObject
    {
        public MangaItem MangaItem { get; set; }
        public string ImageURL { get; set; }
    }

	public class Manga
	{
		public static IQueryable<MangaList> QueryMangaList()
		{
			var realm = Realm.GetInstance();
			return realm.All<MangaList>().OrderBy(x => x.Title);
		}

		public static async Task<int> FetchMangaList()
		{
			var realm = Realm.GetInstance();

			var mangaList = await Zang.FetchMangaListAsync();
			realm.Write(() =>
			{
				foreach (var fetchedItem in mangaList)
				{
					realm.Add(new MangaList
					{
						Title = fetchedItem.Item1,
						Link = fetchedItem.Item2
					});
				}
			});

			return 0;
		}

        public static IQueryable<MangaItem> QueryMangaItem(MangaList manga)
		{
            var realm = manga.Realm;
            return realm.All<MangaItem>().Where(x => x.Manga == manga);
		}

        public static async Task<int> FetchMangaItem(MangaList manga)
		{
            var realm = manga.Realm;

            var mangaItems = await Zang.FetchMangaItemAsync(manga);
			realm.Write(() =>
			{
				foreach (var fetchedItem in mangaItems)
				{
                    realm.Add(new MangaItem
					{
						Title = fetchedItem.Item1,
						Link = fetchedItem.Item2,
                        Manga = manga
					});
				}
			});

			return 0;
		}

        public static IQueryable<MangaContent> QueryMangaContent(MangaItem item)
		{
			var realm = item.Realm;
            return realm.All<MangaContent>().Where(x => x.MangaItem == item);
		}

        public static async Task<int> FetchMangaContents(MangaItem item)
		{
			var realm = item.Realm;

            var contents = await Zang.FetchMangaContentsAsync(item);
			realm.Write(() =>
			{
				foreach (var content in contents)
				{
                    realm.Add(new MangaContent
					{
                        MangaItem = item,
                        ImageURL = content
					});
				}
			});

			return 0;
		}
	}
}

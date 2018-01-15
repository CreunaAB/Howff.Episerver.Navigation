using System.Collections.Generic;

using EPiServer.Core;

namespace Howff.Episerver.Navigation {
	public interface IPageLoader {
		IEnumerable<PageData> GetChildren(ContentReference contentReference);
		PageData GetPage(ContentReference contentReference);
	}
}
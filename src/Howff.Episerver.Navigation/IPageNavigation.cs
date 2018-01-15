using System.Collections.Generic;

using EPiServer.Core;

using Howff.Navigation;

namespace Howff.Episerver.Navigation {
	public interface IPageNavigation {
		IList<INavigationItem> GetItems(PageData startPage, PageData currentPage, INavigationConfig config);
		IList<INavigationItemId> GetSelectedPath(PageData startPage, PageData currentPage);
	}
}
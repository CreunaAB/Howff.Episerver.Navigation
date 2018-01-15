using EPiServer.Core;

using Howff.Navigation;

namespace Howff.Episerver.Navigation {
	public interface INavigationItemFactory {
		INavigationItem MakeNavigationItem(PageData pageData);
	}
}
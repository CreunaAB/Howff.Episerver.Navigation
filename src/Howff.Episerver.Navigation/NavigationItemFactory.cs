using EPiServer.Core;
using EPiServer.Web.Routing;

using Howff.Navigation;

namespace Howff.Episerver.Navigation {
	public class NavigationItemFactory : INavigationItemFactory {
		private readonly UrlResolver urlResolver;

		public NavigationItemFactory(UrlResolver urlResolver) {
			this.urlResolver = urlResolver;
		}

		public INavigationItem MakeNavigationItem(PageData pageData) {
			if(pageData != null) {
				var link = this.urlResolver.GetVirtualPath(pageData.ContentLink).GetUrl();
				return new NavigationPage(pageData, link);
			}
			return null;
		}
	}
}
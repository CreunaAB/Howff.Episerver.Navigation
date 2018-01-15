using System;
using System.Collections.Generic;
using System.Linq;

using EPiServer.Core;

using Howff.Navigation;

namespace Howff.Episerver.Navigation {
	public class PageNavigation : Howff.Navigation.Navigation, IPageNavigation, INavigation {
		private readonly IPageLoader pageLoader;
		private readonly INavigationItemFactory navigationItemFactory;

		public PageNavigation(IPageLoader pageLoader, INavigationItemFactory navigationItemFactory) {
			this.pageLoader = pageLoader ?? throw new ArgumentNullException(nameof(pageLoader));
			this.navigationItemFactory = navigationItemFactory ?? throw new ArgumentNullException(nameof(navigationItemFactory));
		}

		protected override IList<INavigationItem> GetChildren(INavigationItem navigationItem) {
			var navigationPage = navigationItem as NavigationPage;

			return GetChildrenFromNavigationPageLink(navigationPage);
		}

		private List<INavigationItem> GetChildrenFromNavigationPageLink(NavigationPage navigationPage) {
			if(IsValidNavigationPage(navigationPage)) {
				return GetChildrenFromContentRepository(navigationPage.PageData.PageLink);
			}
			return new List<INavigationItem>();
		}

		private static bool IsValidNavigationPage(NavigationPage navigationPage) {
			return navigationPage?.PageData != null;
		}

		private List<INavigationItem> GetChildrenFromContentRepository(ContentReference contentReference) {
			var childPages = this.pageLoader.GetChildren(contentReference);
			return MakeNavigationItemsFromChildPages(childPages);
		}

		private List<INavigationItem> MakeNavigationItemsFromChildPages(IEnumerable<PageData> childPages) {
			return childPages.Select(childPage => this.navigationItemFactory.MakeNavigationItem(childPage)).ToList();
		}

		protected override IList<INavigationItemId> GetSelectedPath(INavigationItem rootItem, INavigationItem currentItem) {
			var rootNavPage = rootItem as NavigationPage;
			var currentNavPageInPath = currentItem as NavigationPage;
			var selectedPath = new List<INavigationItemId>();

			if(rootNavPage != null && currentNavPageInPath != null) {
				bool selectedPathComplete;
				do {
					selectedPath.Insert(0, currentNavPageInPath.Id);
					var parentPage = this.pageLoader.GetPage(currentNavPageInPath.PageData.ParentLink);

					selectedPathComplete = IsRootPage(rootNavPage, currentNavPageInPath) || parentPage == null;

					currentNavPageInPath = new NavigationPage(parentPage);
				} while(!selectedPathComplete);
			}

			return selectedPath;
		}

		private static bool IsRootPage(INavigationItem rootNavPage, INavigationItem currentNavPage) {
			return currentNavPage?.Id != null && currentNavPage.Id.Equals(rootNavPage.Id);
		}

		public virtual IList<INavigationItem> GetItems(PageData startPage, PageData currentPage, INavigationConfig config) {
			var startPageNavigationItem = this.navigationItemFactory.MakeNavigationItem(startPage);
			var currentPageNavigationItem = this.navigationItemFactory.MakeNavigationItem(currentPage);
			return GetItems(startPageNavigationItem, currentPageNavigationItem, config);
		}

		public virtual IList<INavigationItemId> GetSelectedPath(PageData startPage, PageData currentPage) {
			var startPageNavigationItem = this.navigationItemFactory.MakeNavigationItem(startPage);
			var currentPageNavigationItem = this.navigationItemFactory.MakeNavigationItem(currentPage);
			return GetSelectedPath(startPageNavigationItem, currentPageNavigationItem);
		}
	}
}
using EPiServer.Core;

using Howff.Navigation;

namespace Howff.Episerver.Navigation {
	public class NavigationPage : NavigationItem, INavigationItem {
		private NavigationPageId id;
		private NavigationPageId parentId;

		public NavigationPage(PageData pageData, string link = null) {
			PageData = pageData;
			Link = link;
		}

		public INavigationItemId Id {
			get {
				if(this.id == null && PageData != null) {
					this.id = new NavigationPageId(PageData.ContentLink);
				}
				return this.id;
			}
		}

		public INavigationItemId ParentId {
			get {
				if(this.parentId == null && PageData != null) {
					this.parentId = new NavigationPageId(PageData.ParentLink);
				}
				return this.parentId;
			}
		}

		public virtual string Name => PageData?.PageName;

		public virtual string Link { get; }

		public virtual bool Selected { get; set; }

		public bool Visible => PageData != null && PageData.VisibleInMenu;

		public PageData PageData { get; }
	}
}
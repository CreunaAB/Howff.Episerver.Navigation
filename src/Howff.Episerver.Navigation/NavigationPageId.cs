using EPiServer.Core;

using Howff.Navigation;

namespace Howff.Episerver.Navigation {
	public class NavigationPageId : INavigationItemId {
		private readonly ContentReference contentReference;

		public NavigationPageId(ContentReference contentReference) {
			this.contentReference = contentReference;
		}

		public ContentReference ContentReference => this.contentReference;

		public override bool Equals(object obj) {
			var navigationPageId = obj as NavigationPageId;
			if(navigationPageId == null) {
				return false;
			}
			return this.contentReference.ID.Equals(navigationPageId.ContentReference.ID);
		}

		public override int GetHashCode() {
			return this.contentReference.GetHashCode();
		}

		public override string ToString() {
			return this.contentReference.ToString();
		}
	}
}
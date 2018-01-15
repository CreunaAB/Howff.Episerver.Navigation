using System;
using System.Collections.Generic;

using EPiServer;
using EPiServer.Core;

namespace Howff.Episerver.Navigation {
	public class DefaultPageLoader : IPageLoader {
		private readonly IContentLoader contentLoader;

		public DefaultPageLoader(IContentLoader contentLoader) {
			this.contentLoader = contentLoader ?? throw new ArgumentNullException(nameof(contentLoader));
		}

		public IEnumerable<PageData> GetChildren(ContentReference contentReference) {
			return this.contentLoader.GetChildren<PageData>(contentReference);
		}

		public PageData GetPage(ContentReference contentReference) {
			return this.contentLoader.Get<PageData>(contentReference);
		}
	}
}
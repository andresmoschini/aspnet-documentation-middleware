﻿using Microsoft.AspNet.FileProviders;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MakingSense.AspNet.Documentation
{
    public class DocumentationOptions
    {
		/// <summary>
		/// The relative request path that maps to static resources.
		/// </summary>
		public PathString RequestPath { get; set; } = PathString.Empty;

		/// <summary>
		/// The file system used to locate resources
		/// </summary>
		public IFileProvider FileProvider { get; set; }

		/// <summary>
		/// Not found handling is disabled by default, set NotFoundHtmlFile to enable it.
		/// </summary>
		public bool EnableNotFoundHandling => NotFoundHtmlFile != null && NotFoundHtmlFile.Exists;

		public IFileInfo NotFoundHtmlFile { get; set; }

		/// <summary>
		/// Default files are disabled by default, set DefaultFileName to enable it.
		/// </summary>
		public bool EnableDefaultFiles => !string.IsNullOrWhiteSpace(DefaultFileName);

		public string DefaultFileName { get; set; }

		public TimeSpan CacheMaxAge { get; set; }

		public IFileInfo LayoutFile { get; set; }

		public DirectoryOptions DirectoryOptions { get; set; } = new DirectoryOptions();

		internal void ResolveFileProvider(IHostingEnvironment hostingEnv)
		{
			if (FileProvider == null)
			{
				var root = Path.GetFullPath(Path.Combine(hostingEnv.WebRootPath, RequestPath.Value.Trim(new[] { '/', '\\' })));
				FileProvider = new PhysicalFileProvider(root);
			}
		}
	}

	public class DirectoryOptions
	{
		public bool EnableDirectoryBrowsing { get; set; } = true;

		public string[] DirectoryBrowsingStripExtensions { get; set; } = new[] { ".html", ".htm", ".markdown", ".md" };

		public string DirectoryListTemplate { get; set; } = @"<h1>{path}</h1><ul>{items}</ul>";

		public string DirectoryListItemTemplate { get; set; } = @"<li><a href=""{href}"">{name}</a></li>";
	}
}

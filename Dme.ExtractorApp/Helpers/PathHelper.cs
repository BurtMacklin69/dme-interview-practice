using System.Reflection;

namespace Dme.ExtractorApp.Helpers;

public static class PathHelper
{
	public static string GetCurrentPath()
	{
		var programPath = Assembly.GetExecutingAssembly().Location;
		var uri = new UriBuilder(programPath);
		var path = Uri.UnescapeDataString(uri.Path);
		var fullPath = Path.GetDirectoryName(path);
		return fullPath;
	}
}
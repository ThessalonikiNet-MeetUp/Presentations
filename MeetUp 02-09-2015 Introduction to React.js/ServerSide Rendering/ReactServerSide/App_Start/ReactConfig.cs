using React;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(ReactServerSide.ReactConfig), "Configure")]

namespace ReactServerSide
{
	public static class ReactConfig
	{
		public static void Configure()
		{
			// ES6 features are enabled by default. Uncomment the below line to disable them.
			// See http://reactjs.net/guides/es6.html for more information.
			//ReactSiteConfiguration.Configuration.SetUseHarmony(false);

			// Uncomment the below line if you are using Flow
			// See http://reactjs.net/guides/flow.html for more information.
			//ReactSiteConfiguration.Configuration.SetStripTypes(true);

			// If you want to use server-side rendering of React components,
			// add all the necessary JavaScript files here. This includes
			// your components as well as all of their dependencies.
			// See http://reactjs.net/ for more information. Example:
			ReactSiteConfiguration.Configuration
                .SetJsonSerializerSettings(new Newtonsoft.Json.JsonSerializerSettings {
                    ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
                })
				.AddScript("~/app/avatar.jsx")
				.AddScript("~/app/comment.jsx")
				.AddScript("~/app/commentList.jsx");
		}
	}
}
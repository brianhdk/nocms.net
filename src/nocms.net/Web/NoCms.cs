using System;
using System.IO;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.UI;

namespace NoCms.Net.Web
{
	public class NoCms
	{
		public static string Render<T>(Expression<Func<T, string>> property)
		{
			if (property == null) throw new ArgumentNullException("property");

			T entity = default(T);

			string id = "new IdGenerator().Generate<T>();";

			//using (IDocumentSession session = RavenDb.DocumentStore.OpenSession())
			//{
			//	entity = session.Load<T>(id);

			//	if (entity == null)
			//		return String.Empty;
			//}

			return property.Compile()(entity);
		}

		public static IHtmlString RenderHtml<T>(Expression<Func<T, string>> property)
		{
			return new HtmlString(Render(property));
		}

		public static IHtmlString RenderEditLink<T>(string linkText = "Edit")
		{
			return new HtmlString(String.Format("<a href=\"#\" {0}\">{1}</a>", RenderEditTrigger<T>(), linkText));
		}

		public static IHtmlString RenderEditTrigger<T>()
		{
			string id = "-"; // "new IdGenerator().Generate<T>();";

			return new HtmlString(String.Format("data-nocms='{{ \"id\" : \"{0}\", \"clrType\" : \"{1}\" }}'", id, typeof(T).AssemblyQualifiedName));
		}

		public static IHtmlString RenderCss()
		{
			var sb = new StringBuilder();
			using (var writer = new HtmlTextWriter(new StringWriter(sb)))
			{
				writer.Indent = 0;

				writer.AddAttribute(HtmlTextWriterAttribute.Rel, "stylesheet");
				writer.AddAttribute(HtmlTextWriterAttribute.Type, "text/css");
				writer.AddAttribute(HtmlTextWriterAttribute.Href, "/nocms/assets/?file=app/nocms.css");
				writer.RenderBeginTag(HtmlTextWriterTag.Link);

				return new HtmlString(sb.ToString());
			}
		}

		public static IHtmlString RenderScripts()
		{
			var sb = new StringBuilder();
			using (var writer = new HtmlTextWriter(new StringWriter(sb)))
			{
				writer.Indent = 0;

				writer.AddAttribute(HtmlTextWriterAttribute.Src, "/nocms/assets/?file=app/nocms.js");
				writer.RenderBeginTag(HtmlTextWriterTag.Script);
				writer.RenderEndTag();

				return new HtmlString(sb.ToString());
			}
		}
	}
}
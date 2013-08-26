using System.Collections.Generic;

namespace Civic.Core.Configuration
{
	/// <summary>
	/// Interface that represents a named element under a configuration section
	/// </summary>
	public interface INamedElement
	{
		/// <summary>
		/// The name of the element or entry
		/// </summary>
		string Name { get; set; }
		
		/// <summary>
		/// The attributes for this element
		/// </summary>
		Dictionary<string, string> Attributes { get; }

		/// <summary>
		/// The children elements under this element
		/// </summary>
		Dictionary<string, INamedElement> Children { get; }
	}
}

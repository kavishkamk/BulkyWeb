using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyWebRazer_Temp.Models
{
	public class Category
	{
		[Key]
		public int CategoryId { get; set; }
		[Required]
		[DisplayName("Category Name")]
		[MaxLength(30, ErrorMessage = "Name should be less than 30 characters")]
		public required string Name { get; set; }
		[Required]
		[DisplayName("Display Order")]
		[Range(1, 100, ErrorMessage = "Display Order should been 1 - 100")]
		public required int DisplayOrder { get; set; }

	}
}

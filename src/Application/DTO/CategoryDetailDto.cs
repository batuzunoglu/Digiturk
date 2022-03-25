using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTO
{
	public class CategoryDetailDto : CategoryDto
	{
		public List<MovieLightDto> Movies { get; set; }
	}
}

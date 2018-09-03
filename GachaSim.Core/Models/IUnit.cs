using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GachaSim.Core.Models
{
    public interface IUnit
    {		
		string Name { get; }		
		UnitRating Rating { get; }		
		UnitRelevance Relevance { get; }
    }
}

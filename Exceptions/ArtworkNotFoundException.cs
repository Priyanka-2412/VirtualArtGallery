using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualArtGallery.Exceptions
{
    public class ArtworkNotFoundException : ApplicationException
    {
        public ArtworkNotFoundException(string message) : base(message) { }
    }
}
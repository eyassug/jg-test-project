using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Jibble.Services
{
    public interface ICSVImportService
    {
        public Task ProcessAsync(string folderName, CancellationToken cancellationToken = default);
    }
}

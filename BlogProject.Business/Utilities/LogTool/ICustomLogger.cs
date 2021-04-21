using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject.Business.Utilities.LogTool
{
    public interface ICustomLogger
    {
        void LogError(string message);
    }
}

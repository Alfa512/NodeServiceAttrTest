using System;
using System.Collections.Generic;
using System.Text;

namespace NodeServiceAttrTest.Contracts
{
    public interface IConfigurationService
    {
        string ConnectionString { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace KWKY.Model.Base
{
    public interface IDBModel
    {
        void SetId (string guid);
        string GetId ();
    }
}

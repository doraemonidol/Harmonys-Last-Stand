using System.Collections.Generic;

namespace Data
{
    public interface IDataFunc
    {
        public void Save(Dictionary<string, object> args);

        public void Load(Dictionary<string, object> args);
    }
}
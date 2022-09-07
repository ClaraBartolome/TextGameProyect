using System;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;
using System.Text;

namespace TextGameProyect.Utils
{
    public class GameResourceManager
    {
        public ResourceManager rm;
        
        private GameResourceManager()
        {
            rm = new ResourceManager("TextGameProyect.Resources.Strings", Assembly.GetExecutingAssembly());
        }

        #region singleton impl
        private static GameResourceManager _instance;
        private static readonly object _lock = new object();

        public static GameResourceManager GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {

                    if (_instance == null)
                    {
                        _instance = new GameResourceManager();
                    }
                }
            }
            return _instance;
        }
        #endregion

    }
}
